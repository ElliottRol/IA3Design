using IA3Digital.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IA3Digital.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseQuery _exerciseQuery;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(
            ExerciseQuery exerciseQuery, 
            ILogger<ExerciseController> logger)
        {
            _exerciseQuery = exerciseQuery;
            _logger = logger;
        }

        [HttpGet]
        [Route("{muscle}")]
        public async Task<List<Exercise>?> GetExcerciseByMuscle(string muscle)
        {
            _logger.LogInformation($"Requesting Excerises by muscle {muscle}");

            return await _exerciseQuery.FindExerciseByMuscle(muscle);
        }


    }
}
