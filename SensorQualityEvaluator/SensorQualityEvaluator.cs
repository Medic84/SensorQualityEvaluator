using System.Collections.Generic;
using System.Text;

namespace SensorQualityEvaluator
{
    public static class SensorQualityEvaluator
    {
        private static HashSet<IEvaluableSensor> allSensors;
        private static decimal referenceTemperature = 0;
        private static decimal referenceHumidity = 0;
        private static int referenceCarbonMonoxide = 0;

        public static string EvaluateLogFile(string logContentsStr) 
        {
            StringBuilder stringBuilder = new StringBuilder();

            allSensors = DataProvider.LoadSensorsInformationFromString(logContentsStr, ref referenceTemperature, ref referenceHumidity, ref referenceCarbonMonoxide);

            foreach (IEvaluableSensor sensor in allSensors) 
            {
                object referenceValue = null;
                if (sensor.GetType() == typeof(Thermometer))
                    referenceValue = referenceTemperature;
                if (sensor.GetType() == typeof(HumiditySensor))
                    referenceValue = referenceHumidity;
                if (sensor.GetType() == typeof(CarbonMonoxideDetector))
                    referenceValue = referenceCarbonMonoxide;

                stringBuilder.AppendLine($"\"{sensor.Name}\": \"{ sensor.EvaluateSensorCategory(referenceValue) }\",");
            };

            if(stringBuilder.Length > 0)
                stringBuilder.Length = stringBuilder.Length - 3;
            return stringBuilder.ToString();
        }
    }
}
