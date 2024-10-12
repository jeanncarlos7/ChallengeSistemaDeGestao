using Microsoft.AspNetCore.Mvc;
using SistemaDeGestao.Services;

namespace SistemaDeGestao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly RecommendationService _recommendationService;

        public RecommendationController(RecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("recommend")]
        public IActionResult GetRecommendation(string category)
        {
            var recommendation = _recommendationService.GetRecommendation(category);
            return Ok(recommendation);
        }
    }
}
