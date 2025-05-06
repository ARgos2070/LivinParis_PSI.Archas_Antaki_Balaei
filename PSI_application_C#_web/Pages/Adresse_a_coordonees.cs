using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PSI_application_C__web
{
    public class Adresse_a_coordonees
    {
        //le site est https://opencagedata.com/dashboard#geocoding;
        public static async Task<bool> GetCoords(string street, string ville, string code_postal, string Pays)
        {
            string apiKey = "d223a8ca27d54e1fa8ab51a9571518b1";
            string address = street + ", " + ville + ", " + code_postal + ", " + Pays;
            GeocodingResult Resultat = await GetCoordinatesFromAddress(apiKey, address);
            if (Resultat.RequestsRemaining == 0)
            {
                //Console.WriteLine("plus de demande aujourd'hui");
            }
            if (Resultat.Latitude == 0.0 || Resultat.Longitude == 0.0 || Resultat.RequestsRemaining == -1)
            {
                return false;
            }
            else if (Resultat.Latitude >= 49.1 || Resultat.Latitude <= 47.9 ||
                Resultat.Longitude >= 3.1 || Resultat.Longitude <= 1.9)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<GeocodingResult> GetCoordsGeocodingResult(string street, string ville, string code_postal, string Pays)
        {
            string apiKey = "d223a8ca27d54e1fa8ab51a9571518b1";
            string address = street + ", " + ville + ", " + code_postal + ", " + Pays;
            GeocodingResult Resultat = await GetCoordinatesFromAddress(apiKey, address);
            return Resultat;
        }

        private static async Task<GeocodingResult> GetCoordinatesFromAddress(string apiKey, string address)
        {
            string baseUrl = "https://api.opencagedata.com/geocode/v1/json";
            string requestUrl = $"{baseUrl}?q={Uri.EscapeDataString(address)}&key={apiKey}";
            double latitude = 0.0;
            double longitude = 0.0;
            int requestsRemaining = -1;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(responseBody);

                    var results = jsonResponse["results"];
                    if (results != null && results.HasValues)
                    {
                        latitude = (double)results[0]["geometry"]["lat"];
                        longitude = (double)results[0]["geometry"]["lng"];
                        requestsRemaining = (int)jsonResponse["rate"]["remaining"];
                    }
                    else
                    {
                        Console.WriteLine("No results found for the given address.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return new GeocodingResult
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    RequestsRemaining = requestsRemaining
                };
            }
        }

        public struct GeocodingResult
        {
            public double Latitude;
            public double Longitude;
            public int RequestsRemaining;
        }
    }
}
