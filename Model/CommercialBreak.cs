using APICommercialOptimiser.Helpers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APICommercialOptimiser.Model
{
    public class CommercialBreak
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BreakTypesEnum BreakName { get; set; }
        public string Demographic { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CommercialTypeEnum CommercialType { get; set; }
        public int Rating { get; set; }   
        public int OptimisedRating { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CommercialsEnum CommercialName { get; set; }


        public bool BreakSameTypeCommercials(List<CommercialBreak> breaks, int breakCapacity)
        {
            bool flag = true;
            if (breaks.Count == breakCapacity)
            {
                flag = false;
            }
            if (breaks.Count > 0)
            {
                int currentIndex = breaks.Count - 1;                
                int index = breaks.FindIndex(x => x.CommercialType == CommercialType);
                if (index > -1)
                {
                    if (index == currentIndex)
                    {
                        if (index == 1)
                        {
                            breaks.Reverse();
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }

            return flag;
        }

    }  
}
