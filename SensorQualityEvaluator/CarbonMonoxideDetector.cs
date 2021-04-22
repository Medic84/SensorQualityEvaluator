using System;
using System.Collections.Generic;
using System.Linq;

namespace SensorQualityEvaluator
{
    public class CarbonMonoxideDetector : Sensor, IEvaluableSensor
    {
        private HashSet<(DateTime, int)> readings = new HashSet<(DateTime, int)>();

        public string EvaluateSensorCategory(object referenceValue)
        {
            int referenceCarbonMonoxide = (int)referenceValue;
            int lowestAcceptableValue = referenceCarbonMonoxide - Constants.AbsoluteValueOfAcceptableMistakeForCarbon;
            int highestAcceptableValue = referenceCarbonMonoxide + Constants.AbsoluteValueOfAcceptableMistakeForCarbon;
            
            IEnumerable<int> valueOfReadings = readings.Select(c => c.Item2);
            bool sensorIsWorkingCorrectly = valueOfReadings.All(c => c <= highestAcceptableValue && c >= lowestAcceptableValue);
            return sensorIsWorkingCorrectly ? Constants.Keep : Constants.Discard;
        }

        public void AddReading(DateTime time, string value) 
        {
            try
            {
                readings.Add((time, int.Parse(value)));
            }
            catch (FormatException)
            {
                throw new Exception($"Log contains unexpected value. For sensor: {this.Name} it contains reading: {value}. Integer is expected.");
            }
        }
    }
}
