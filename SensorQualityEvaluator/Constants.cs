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

        //Defines how much can reference value of carbon monoxide differ from every measurement of sensor
        public const int AbsoluteValueOfAcceptableMistakeForCarbon = 3;
        //Defines how much can reference value of humidity differ from every measurement of sensor
        public const decimal AbsoluteValueOfAcceptableMistakeForHumidity = 1;
        //Defines how much can reference value of temperature differ from average of all measurements of sensor 
        public const decimal AbsoluteValueOfAcceptableMistakeForTemperature = (decimal)0.5;


        public const double UltraPreciseStandardDeviationOfTemperatureValues = 3;
        public const double VeryPreciseStandardDeviationOfTemperatureValues = 5;
    }
}
