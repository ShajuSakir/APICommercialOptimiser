using APICommercialOptimiser.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICommercialOptimiser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommercialController: ControllerBase
    {
        private IPlaceCommercials _commercialService;

        public CommercialController(IPlaceCommercials commercialService)
        {
            _commercialService = commercialService;
        }

        [HttpGet]
        [Authorize]
        [Route("GetCommercials/{isOptimised}")]
        public IActionResult GetCommercials(bool isOptimised)
        {
            var result = _commercialService.GetCommercials(isOptimised);
            return Ok(result);
            
        }
        
        [HttpGet]
        [Authorize]
        [Route("GetCommercialsUneven")]
        public IActionResult GetCommercialsUneven()
        {
            var result = _commercialService.GetCommercialsByUnevenStructure();
            return Ok(result);
            
        }

    }
}
