using LangtonsAntClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LangtonsAntClient.DataService
{
    class RestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();

            _baseAddress = "http://localhost:5128";
            _url = $"{_baseAddress}/api/langtonsant";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }



        public async Task<LangtonsAnt> InitializeLangtonsAntBackend(LangtonsAnt iLangtonsAnt)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                iLangtonsAnt.ErrMessage = "Keine Internetverbindung";
                return iLangtonsAnt;
            }

            try
            {
                string jsonLangtonsAnt = JsonSerializer.Serialize<LangtonsAnt>(iLangtonsAnt, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonLangtonsAnt, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    iLangtonsAnt = JsonSerializer.Deserialize<LangtonsAnt>(responseString, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                iLangtonsAnt.ErrMessage = $"Exception: {ex.Message}";
            }

            return iLangtonsAnt;

        }
        public async Task<LangtonsAnt> GetNextStepBackend()
        {
            LangtonsAnt langtonsAnt = new LangtonsAnt();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                langtonsAnt.ErrMessage = "Keine Internetverbindung";
                return langtonsAnt;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    langtonsAnt = JsonSerializer.Deserialize<LangtonsAnt>(responseString, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                langtonsAnt.ErrMessage = $"Exception: {ex.Message}";
            }

            return langtonsAnt;
        }
    }
}
