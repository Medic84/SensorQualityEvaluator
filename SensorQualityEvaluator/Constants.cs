namespace SensorQualityEvaluator
{
    public static class Constants
    {
        public const string Keep = "keep";
        public const string Discard = "discard";
        public const string UltraPrecise = "ultra precise";
        public const string VeryPrecise = "very precise";
        public const string Precise = "precise";

        public const string ThermometerTypeNameInLog = "thermometer";
        public const string HumiditySensorTypeNameInLog = "humidity";
        public const string CarbonMonoxideSensorTypeNameInLog = "monoxide";
        public static readonly string[] SensorTypes = { ThermometerTypeNameInLog, HumiditySensorTypeNameInLog, CarbonMonoxideSensorTypeNameInLog};

        public const int AbsoluteValueOfAcceptableMistakeForCarbon = 3;
        public const decimal AbsoluteValueOfAcceptableMistakeForHumidity = 1;
        public const decimal AbsoluteValueOfAcceptableMistakeForTemperature = (decimal)0.5;
        public const double UltraPreciseStandardDeviation = 3;
        public const double VeryPreciseStandardDeviation = 5;
    }
}
