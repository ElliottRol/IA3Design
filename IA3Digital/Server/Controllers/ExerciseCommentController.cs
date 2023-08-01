using IA3Digital.Server.Data;
using IA3Digital.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IA3Digital.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ExerciseCommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExerciseCommentController> _logger;

        public ExerciseCommentController(
            ApplicationDbContext context,
            ILogger<ExerciseCommentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("{exerciseName}")]
        public async Task<List<ExerciseComment>> GetCommentsByExercise(string exerciseName)
        {
            _logger.LogInformation($"Searching for comments for exercise: {exerciseName}");

            return await _context.ExerciseComment
                .Where(e => e.ExerciseName == exerciseName)
                .OrderByDescending(e => e.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost]
        public async Task AddCommentsByExercise([FromBody] ExerciseComment comment)
        {
            var currentUser = User?.Identity?.Name ?? "unknown user";
            _logger.LogInformation($"Adding comment for user {currentUser} to exercise: {comment.ExerciseName}");

            ExerciseComment newComment = new ExerciseComment()
            {
                Id = 0,
                UserName = currentUser,
                ExerciseName = comment.ExerciseName,
                Comment = comment.Comment
            };

            _context.ExerciseComment.Add(newComment);
            await _context.SaveChangesAsync();
        }
    }
}
