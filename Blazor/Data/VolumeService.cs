using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.DataContainer;

namespace Blazor.Data
{
    public class VolumeService : IVolumeService
    {
        private HttpClient client = new HttpClient();
        private string uri = "https://localhost:5001/calculate";
        
        public async Task<VolumeResult> CalculateVolume(double height, double radius, string type)
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/{type}?height={height}&radius={radius}");
            if (response.IsSuccessStatusCode)
            {
                string asJson = await response.Content.ReadAsStringAsync();
                VolumeResult volume = JsonSerializer.Deserialize<VolumeResult>(asJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Console.WriteLine(volume.Volume);
                return volume;
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<IList<VolumeResult>> GetHistory()
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}");
            if (response.IsSuccessStatusCode)
            {
                string asJson = await response.Content.ReadAsStringAsync();
                IList<VolumeResult> results = JsonSerializer.Deserialize<IList<VolumeResult>>(asJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return results;
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}