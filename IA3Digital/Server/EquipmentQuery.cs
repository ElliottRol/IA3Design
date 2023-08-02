using IA3Digital.Server.Models;
using IA3Digital.Shared;
using System.Linq;

namespace IA3Digital.Server
{

    public class EquipmentQuery
    {
		// This should be read from configuration in the future
        private readonly static string ApiUrl = "https://data.gov.au/data/api/3/action/datastore_search?resource_id=d6cf7206-4381-4c12-a5a0-5a5d8774522b";
          
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<EquipmentQuery> _logger;

        public EquipmentQuery(
            IHttpClientFactory httpClientFactory,
            ILogger<EquipmentQuery> logger
            )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<Equipment>?> FindEquipmentByType(string type)
        {
            var http = _httpClientFactory.CreateClient();

            _logger.LogInformation($"Querying by equipment type: {type}");
            var data = await http.GetFromJsonAsync<EquipementResponse>(ApiUrl);

            if (data?.Result?.Records == null ) { return null; }

            return data.Result.Records
                .Where(r => r.FitnessEquipment.Contains(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


    }
}
