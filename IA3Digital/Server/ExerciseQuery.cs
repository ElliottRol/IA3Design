using IA3Digital.Shared;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IA3Digital.Server
{

    public class ExerciseQuery
    {
        // This should be read from configuration in the future
        private readonly static string ApiUrl = "https://api.api-ninjas.com/v1/exercises";
        private readonly static string ApiKey = "J1RFC55mMzkFbMDKQb0FEQ==W6XZUQbZYuTF893o";
          

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExerciseQuery> _logger;

        public ExerciseQuery(
            IHttpClientFactory httpClientFactory,
            ILogger<ExerciseQuery> logger
            )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<Exercise>?> FindExerciseByMuscle(string muscle)
        {
            var http = _httpClientFactory.CreateClient();

            string url = ApiUrl + "?muscle=" + Uri.EscapeDataString(muscle);

            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("X-Api-Key", ApiKey);
           
            _logger.LogInformation($"Querying by muscle: {muscle} {url}");
            
            using(var res = await http.SendAsync(req))
            {
                if(res.IsSuccessStatusCode)
                {
                    // attempt to extract the list of exercises from the API and return it
                    var data = await res.Content.ReadFromJsonAsync<List<Exercise>>();
                    if (data != null)
                    {
                        _logger.LogInformation($"Received {data.Count} exercises");
                    }
                    else
                    {
                        _logger.LogInformation("No exercises found");
                    }

                    return data;
                }
                else
                {
                    // failed request. Return no data
                    _logger.LogError($"Failed to query excercise. Status: {res.StatusCode} Message: {res.ReasonPhrase}");
                    return null;
                }
            }
        }


    }
}
