using System.Runtime.Serialization;

namespace APICommercialOptimiser.Helpers
{
    public enum CommercialsEnum
    {
        [EnumMember(Value = "Commercial 1")]       
        Commercial1,
        [EnumMember(Value = "Commercial 2")]        
        Commercial2,
        [EnumMember(Value = "Commercial 3")]        
        Commercial3,
        [EnumMember(Value = "Commercial 4")]       
        Commercial4,
        [EnumMember(Value = "Commercial 5")]        
        Commercial5,
        [EnumMember(Value = "Commercial 6")]        
        Commercial6,
        [EnumMember(Value = "Commercial 7")]        
        Commercial7,
        [EnumMember(Value = "Commercial 8")]        
        Commercial8,
        [EnumMember(Value = "Commercial 9")]        
        Commercial9,
        [EnumMember(Value = "Commercial 10")]        
        Commercial10
    }


    public enum BreakTypesEnum
    {
        NoBreak,
        Break1,
        Break2,
        Break3
    }

    public enum CommercialTypeEnum
    {
        Automotive,
        Finance,
        Travel
    }
}
