using System.Text;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json;



namespace Game
{
    class MeowFactsData
    {
        public List<string> data { get; set; }
    }

    class Alert
    {
        public int? id { get; set; }
        public string? location_title { get; set; }
        public string? location_type { get; set; }
        public string? started_at { get; set; }
        public string? finished_at { get; set; }
        public string? updated_at { get; set; }
        public string? alert_type { get; set; }
        public string? location_uid { get; set; }
        public string? location_oblast { get; set; }
        public string? location_raion { get; set; }
        public string? notes { get; set; }
        public string? country { get; set; }
        public bool? calculated { get; set; }
        public int location_oblast_uid { get; set; }
    }

    class Alerts { 
        public List<Alert> alerts { get; set; }
    }
    class Program
    {
        
        public static async Task<MeowFactsData> GetMeowFacts(int count = 1, string lang = "eng")
        {
            var client = new HttpClient();
            string url = "https://meowfacts.herokuapp.com/";
            string parameters = $"?count={count}&lang={lang}";
            var response = await client.GetAsync(url + parameters);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MeowFactsData>(JsonResponse);
            }
            return null;
        }

        public static async Task<Alerts> GetAlerts()
        {
            var client = new HttpClient();
            string url = "https://43e2-85-198-148-246.ngrok-free.app/";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Alerts>(JsonResponse);
            }
            return null;
        }
        public static async Task Main(string[] args)
        {
            
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            
            
            var alerts = await GetAlerts();
            if (alerts == null)
                Console.WriteLine("error");
            else foreach (var Alert in alerts.alerts)
            {
                if (Convert.ToInt32(Alert.location_uid) < 32) { 
                    Console.WriteLine(Alert.location_uid + ", " + Alert.location_title + " - " + Alert.alert_type + "\t\t " + Alert.notes);
                    Console.WriteLine();
                }
            }
        }
    }
}
/*


********************
* Дніпропетровська *
*                  *
********************

*/
