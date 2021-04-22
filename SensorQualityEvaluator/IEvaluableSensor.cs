using System;

namespace SensorQualityEvaluator
{
    public interface IEvaluableSensor
    {
        public string Name
        {
            get; set;
        }

        public void AddReading(DateTime timeOfMeasurement, string result);

        public string EvaluateSensorCategory(object referenceValue);
    }
}
