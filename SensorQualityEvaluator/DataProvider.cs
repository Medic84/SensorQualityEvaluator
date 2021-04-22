using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SensorQualityEvaluator
{
    public static class DataProvider
    {
        public static HashSet<IEvaluableSensor> LoadSensorsInformationFromString(string input, ref decimal refTemperature, ref decimal refHumidity, ref int refCarbonMonoxide)
        {
            HashSet<IEvaluableSensor> sensors = new HashSet<IEvaluableSensor>();
            DateTime timeOfMeasurement;

            string[] lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            if (lines.Length == 0)
                return sensors;

            string[] referenceValuesRow = lines[0].Split();

            if (!decimal.TryParse(referenceValuesRow[1], NumberStyles.Any, CultureInfo.GetCultureInfo("cz-CZ"), out refTemperature))
                throw new Exception($"Unexpected reference value for Temperature: {referenceValuesRow[1]}, decimal is expected.");
            if (!decimal.TryParse(referenceValuesRow[2], NumberStyles.Any, CultureInfo.GetCultureInfo("cz-CZ"), out refHumidity))
                throw new Exception($"Unexpected reference value for Humidity: {referenceValuesRow[2]}, decimal is expected.");
            if (!int.TryParse(referenceValuesRow[3], out refCarbonMonoxide))
                throw new Exception($"Unexpected reference value for Carbon Monoxide: {referenceValuesRow[3]}, integet is expected.");

            string[] sensorsData = lines.Skip(1).ToArray();

            IEvaluableSensor currentSensor = null;
            foreach (string line in sensorsData) 
            {
                string[] splittedLine = line.Split();
                if (Constants.SensorTypes.Contains(splittedLine[0]))
                {
                    if (currentSensor != null)
                        sensors.Add(currentSensor);

                    switch (splittedLine[0])
                    {
                        case Constants.ThermometerTypeNameInLog:
                            currentSensor = new Thermometer();
                            break;
                        case Constants.HumiditySensorTypeNameInLog:
                            currentSensor = new HumiditySensor();
                            break;
                        case Constants.CarbonMonoxideSensorTypeNameInLog:
                            currentSensor = new CarbonMonoxideDetector();
                            break;
                        default:
                            break;
                    }

                    currentSensor.Name = splittedLine[1];
                }
                else
                {
                    if (!DateTime.TryParse(splittedLine[0], out timeOfMeasurement))
                        throw new Exception($"Sensor: {currentSensor.Name} has reading with unexpected DateTime format: {splittedLine[0]}");
                    currentSensor.AddReading(timeOfMeasurement, splittedLine[1]);
                }
            }

            sensors.Add(currentSensor);
            return sensors;
        }
    }
}
