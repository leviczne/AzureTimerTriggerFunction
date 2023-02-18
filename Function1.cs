using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LearnFunction
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task RunAsync([TimerTrigger("* * * * *")]TimerInfo myTimer, ILogger log)
        {
            var client = new HttpClient();
            var apiEndpoint = "https://localhost:7226/api/Security/register";
            
           
            var requestData = new { email = "leviczne.fps@gmail.com", viajanteName = "Levi Testando Function LesGO", password = "leviczne", confirmPassword = "leviczne"};
            var requestDataJson = JsonSerializer.Serialize(requestData);
            var requestDataContent = new StringContent(requestDataJson, System.Text.Encoding.UTF8, "application/json");

          
            var response = await client.PostAsync(apiEndpoint, requestDataContent);

            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
