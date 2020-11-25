using APICommercialOptimiser.Helpers;

namespace APICommercialOptimiser.Entities
{
    public class CommercialItems
    {
        public CommercialsEnum CommercialName { get; set; }
        public CommercialTypeEnum CommercialType { get; set; }
        public string Demographic { get; set; }

        public bool isAllocated { get; set; }
    }
}
