using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{

    public class TrainData
    {
        public string lineCode { get; set; }
        public string trainCode { get; set; }
        public string systemCode { get; set; }
        public string createTime { get; set; }
        public Dictionary<string, Car> data { get; set; }
    }

    public class Car
    {
        public HVACUnit HVAC01 { get; set; }
        public HVACUnit HVAC02 { get; set; }
        public CompressorUnit HVAC01COM01 { get; set; }
        public CompressorUnit HVAC01COM02 { get; set; }
        public CompressorUnit HVAC02COM01 { get; set; }
        public CompressorUnit HVAC02COM02 { get; set; }
        public VentilationUnit HVAC01EVP01 { get; set; }
        public VentilationUnit HVAC01EVP02 { get; set; }
        public VentilationUnit HVAC02EVP01 { get; set; }
        public VentilationUnit HVAC02EVP02 { get; set; }
        public ExhaustUnit HVAC01WEX01 { get; set; }
        public ExhaustUnit HVAC01WEX02 { get; set; }
        public ExhaustUnit HVAC02WEX01 { get; set; }
        public ExhaustUnit HVAC02WEX02 { get; set; }
    }

    public class HVACUnit
    {
        public Dictionary<string, string> designProperties { get; set; }
        public dynamicProperties dynamicProperties { get; set; }
    }

    public class CompressorUnit
    {
        public Dictionary<string, string> designProperties { get; set; }
        public CompressordynamicProperties dynamicProperties { get; set; }
    }

    public class VentilationUnit
    {
        public Dictionary<string, string> designProperties { get; set; }
        public VentilationdynamicProperties dynamicProperties { get; set; }
    }

    public class ExhaustUnit
    {
        public Dictionary<string, string> designProperties { get; set; }
        public ExhaustdynamicProperties dynamicProperties { get; set; }
    }

    public class dynamicProperties
    {
        public double? freshairTemp { get; set; }
        public double? supplyairTemp { get; set; }
        public double? returnairTemp { get; set; }
        public double? carairTemp { get; set; }
        public double? airqualitycollectionmoduleTemp { get; set; }
        public double? airqualitycollectionmoduleRH { get; set; }
        public double? airqualitycollectionmoduleCO2 { get; set; }
        public double? airqualitycollectionmoduleTVOC { get; set; }
        public double? airqualitycollectionmodulePM { get; set; }
        public double? airPressureDifference { get; set; }
        public double? targetTemp { get; set; }
        public double? voltage { get; set; }
        public double? Maincircuitbreaker { get; set; }
        public double? compressorcircuitbreaker { get; set; }
        public double? inverter { get; set; }
        public double? UVlampmalfunction { get; set; }
        public double? returnairdamper { get; set; }
        public double? freshairdamper { get; set; }
        public double? aircleaner { get; set; }
        public double? lowpressureswitch { get; set; }
        public double? highpressureswitch { get; set; }
        public double? severeFault { get; set; }
        public double? midFault { get; set; }
        public double? minorFault { get; set; }
    }


    public class CompressordynamicProperties
    {
        public double? exhaustTemp { get; set; }
        public double? suctionTemp { get; set; }
        public double? highPressure { get; set; }
        public double? lowPressure { get; set; }
        public double? compressorId { get; set; }
        public double? contactorStatus { get; set; }
    }


    public class VentilationdynamicProperties
    {
        public double? blowerId { get; set; }
        public double? ventilationcontactorStatus { get; set; }
        public double? emergencyventilationcontactorStatus { get; set; }
    }

    public class ExhaustdynamicProperties
    {
        public double? fanId { get; set; }
        public double? emergencyventilationcontactorStatus { get; set; }
        public double? exhaustId { get; set; }
    }
}

