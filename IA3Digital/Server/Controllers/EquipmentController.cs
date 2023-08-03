using IA3Digital.Server.Models;
using IA3Digital.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IA3Digital.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentQuery _equipmentQuery;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(
            EquipmentQuery equipmentQuery, 
            ILogger<EquipmentController> logger)
        {
            _equipmentQuery = equipmentQuery;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Equipment>?> GetByType(string type)
        {
            _logger.LogInformation($"Requesting Equipment by type {type}");

            return await _equipmentQuery.FindEquipmentByType(type);
        }


    }
}
