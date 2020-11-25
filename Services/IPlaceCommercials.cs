using APICommercialOptimiser.Model;
using System.Collections.Generic;

namespace APICommercialOptimiser.Services
{
    public interface IPlaceCommercials
    {
        IEnumerable<CommercialBreak> GetCommercials(bool isOptimised);
        IEnumerable<CommercialBreak> GetCommercialsByUnevenStructure();
    }
}
