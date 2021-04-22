using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SensorQualityEvaluator
{
    public class HumiditySensor : Sensor, IEvaluableSensor
    {
        private HashSet<(DateTime, decimal)> readings = new HashSet<(DateTime, decimal)>();
        public string EvaluateSensorCategory(object referenceValue)
        {
            decimal referenceHumidity = (decimal)referenceValue;
            decimal lowestAcceptableValue = referenceHumidity - Constants.AbsoluteValueOfAcceptableMistakeForHumidity;
            decimal highestAcceptableValue = referenceHumidity + Constants.AbsoluteValueOfAcceptableMistakeForHumidity;

            IEnumerable<decimal> valueOfReadings = readings.Select(c => c.Item2);
            bool sensorIsWorkingCorrectly = valueOfReadings.All(c => c <= highestAcceptableValue && c >= lowestAcceptableValue);
            return sensorIsWorkingCorrectly ? Constants.Keep : Constants.Discard;
        }
        public void AddReading(DateTime time, string value)
        {
            try
            {
                decimal readingValue = decimal.Parse(value, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                readings.Add((time, readingValue));
            }
            catch (FormatException)
            {
                throw new Exception($"Log contains unexpected value. For sensor: {this.Name} it contains reading: {value}. Decimal with dot used as separator is expected.");
            }
        }
    }
}
