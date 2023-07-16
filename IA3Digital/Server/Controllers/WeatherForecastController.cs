using IA3Digital.Server.Data;
using IA3Digital.Server.Models;
using IA3Digital.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IA3Digital.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ApplicationDbContext context,
            ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<TableDTO>> Get()
        {
            var currentUser = User?.Identity?.Name;
            _logger.LogInformation($"Fetching weather for user {currentUser}");

            // Get Weather Forecasts  
            var data = await _context.Table
                 // Only get entries for the current logged in user
                 .Where(x => x.UserName == currentUser)
                 // Use AsNoTracking to disable EF change tracking
                 // Use ToListAsync to avoid blocking a thread
                 .AsNoTracking().ToListAsync();

            // Convert to the DTO version to return to the UI
            return data.Select(d =>
            {
                return new TableDTO()
                {
                    Id = d.Id,
                    Date = d.Date,
                    TemperatureC = d.TemperatureC,
                    TemperatureF = d.TemperatureF,
                    Summary = d.Summary,
                    UserName = d.UserName
                };
            }).ToList();
        }

        [HttpPost]
        public void Post([FromBody] TableDTO newTableEntry)
        {
            var currentUser = User?.Identity?.Name;
            _logger.LogInformation($"Creating new weather for user {currentUser}");

            // create a new table to store from the data from the user interface
            var table = new Models.Table()
            {
                Id = newTableEntry.Id,
                Date = newTableEntry.Date,
                TemperatureC = newTableEntry.TemperatureC,
                TemperatureF = newTableEntry.TemperatureF,
                Summary = newTableEntry.Summary,
                UserName = currentUser
            };


            // Save to the database
            _context.Table.Add(table);
            _context.SaveChanges();
        }

        [HttpPut]
        public void Put([FromBody] TableDTO updateTableEntry)
        {
             var currentUser = User?.Identity?.Name;
            _logger.LogInformation($"Updating weather for user {currentUser} - ID: {updateTableEntry.Id}");

            // Find the existing Weather Forecast record using the id
            var existingTable =
                _context.Table
                .Where(x => x.Id == updateTableEntry.Id)
                .Where(x => x.UserName == currentUser)
                .FirstOrDefault();

            // update the values in it to the new ones if the record exists
            if (existingTable != null)
            {
                // Update the values
                existingTable.Date = updateTableEntry.Date;
                existingTable.Summary = updateTableEntry.Summary;
                existingTable.TemperatureC = updateTableEntry.TemperatureC;
                existingTable.TemperatureF = updateTableEntry.TemperatureF;

                // Save to the database
                _context.SaveChanges();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            var currentUser = User?.Identity?.Name;
            _logger.LogInformation($"Deleting weather for user {currentUser} - ID: {id}");

            // Find the existing Weather Forecast record using the id
            var existingTable =
                _context.Table
                .Where(x => x.Id == id)
                .Where(x => x.UserName == currentUser)
                .FirstOrDefault();

            // if the record was found, delete it
            if (existingTable != null)
            {
                // Remove from the database
                _context.Table.Remove(existingTable);
                _context.SaveChanges();
            }
        }

    }
}