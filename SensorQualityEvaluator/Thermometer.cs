using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SensorQualityEvaluator
{
    public class Thermometer : Sensor, IEvaluableSensor
    {
        private HashSet<(DateTime, decimal)> readings = new HashSet<(DateTime, decimal)>();
        public string EvaluateSensorCategory(object referenceValue) 
        {
            decimal referenceTemperature = (decimal)referenceValue;
            decimal lowestAcceptableValue = referenceTemperature - Constants.AbsoluteValueOfAcceptableMistakeForTemperature;
            decimal highestAcceptableValue = referenceTemperature + Constants.AbsoluteValueOfAcceptableMistakeForTemperature;

            IEnumerable<decimal> values = readings.Select(c => c.Item2);
            decimal average = values.Average();
            double sd = Math.Sqrt(values.Average(x => Math.Pow((double)(x - average), 2)));

            if (average <= highestAcceptableValue && average >= lowestAcceptableValue)
            {
                if (sd < Constants.UltraPreciseStandardDeviation)
                    return Constants.UltraPrecise;
                if (sd < Constants.VeryPreciseStandardDeviation)
                    return Constants.VeryPrecise;
            }
            return Constants.Precise;
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
