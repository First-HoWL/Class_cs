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
        public DateTimeOffset started_at { get; set; }
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

    class Oblast
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAlert { get; set; }
        public bool Isnew { get; set; }
        public bool Isold { get; set; }

        public Oblast(int id, string name, bool isAlert = false)
        {
            Id = id;
            Name = name;
            IsAlert = isAlert;
        }

        public Oblast() : this(0, "") { }
    }

    class AirAlertMap
    {
        public static string ShortenName(string name, int length)
        {
            return name.Length > length ? name.Substring(0, length - 1) + "…" : name;
        } 
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
            string url = "https://105e-85-198-148-246.ngrok-free.app";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Alerts>(JsonResponse);
            }
            return null;
        }
        public static bool inAlert(int uid, List<int> alertsUID)
        {
            for (int i = 0; i < alertsUID.Count; i++)
            {
                if (alertsUID[i] == uid) return true;
            }
            return false;
        }
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;

            List<Oblast> regions = new List<Oblast>()
            {
                new Oblast(0, ""),
                new Oblast(1, ""),
                new Oblast(2, ""),
                new Oblast(3, "Хмельницька область"),
                new Oblast(4, "Вінницька область"),
                new Oblast(5, "Рівненська область"),
                new Oblast(6, ""),
                new Oblast(7, ""),
                new Oblast(8, "Волинська область"),
                new Oblast(9, "Дніпропетровська область"),
                new Oblast(10, "Житомирська область"),
                new Oblast(11, "Закарпатська область"),
                new Oblast(12, "Запорізька область"),
                new Oblast(13, "Івано-Франківська область"),
                new Oblast(14, "Київська область"),
                new Oblast(15, "Кіровоградська область"),
                new Oblast(16, "Луганська область"),
                new Oblast(17, "Миколаївська область"),
                new Oblast(18, "Одеська область"),
                new Oblast(19, "Полтавська область"),
                new Oblast(20, "Сумська область"),
                new Oblast(21, "Тернопільська область"),
                new Oblast(22, "Харківська область"),
                new Oblast(23, "Херсонська область"),
                new Oblast(24, "Черкаська область"),
                new Oblast(25, "Чернігівська область"),
                new Oblast(26, "Чернівецька область"),
                new Oblast(27, "Львівська область"),
                new Oblast(28, "Донецька область"),
                new Oblast(29, "Автономна Республіка Крим"),
                new Oblast(30, "м. Севастополь"),
                new Oblast(31, "м. Київ")
            };
            
            const int mapWidth = 8, mapHeight = 6, cellWidth = 13;
            int[,] map = new int[mapHeight, mapWidth]
            {
            {0, 0, 0, 0, 31, 0, 0, 0 },
            {8, 0, 5, 10, 14, 25, 20, 0 },
            {27, 21, 3, 4, 24, 19, 22, 16 },
            {11, 13, 26, 0, 15, 9, 28, 0 },
            {0, 0, 0, 0, 17, 23, 12, 0 },
            {0, 0, 0, 18, 0, 29, 0, 0 },
            };

            while (true) { 
                List<int> AlertsUids = new List<int>();
                int index = 0;
                var alerts = await GetAlerts();
                if (alerts == null)
                    Console.WriteLine("error");
                else foreach (var Alert in alerts.alerts)
                {
                    if (Convert.ToInt32(Alert.location_uid) < 32) {
                        AlertsUids.Add(Convert.ToInt32(Alert.location_uid));
                            if (Convert.ToInt32(Alert.started_at.ToUnixTimeSeconds() - DateTimeOffset.Now.ToUnixTimeSeconds()) >= -60)
                            {
                                regions[Convert.ToInt32(Alert.location_uid)].Isnew = true;
                            }
                            else if (Convert.ToInt32(Alert.started_at.ToUnixTimeSeconds() - DateTimeOffset.Now.ToUnixTimeSeconds()) <= -3600)
                            {
                                regions[Convert.ToInt32(Alert.location_uid)].Isold = true;
                            }
                            else
                                regions[Convert.ToInt32(Alert.location_uid)].Isnew = false;
                        //Console.WriteLine(Alert.location_uid + ", " + Alert.location_title + " - " + Alert.alert_type + "\t\t " + Alert.notes);
                        //Console.WriteLine();
                    }
                }
                for (int i = 0; i < 10; i++) { 
                    for (int height = 0; height < mapHeight; height++) 
                    { 
                        for (int column = 0; column < mapWidth; column++)
                        {
                            if (map[height, column] == 0) { Console.BackgroundColor = ConsoleColor.Black; }
                            else if (inAlert(map[height, column], AlertsUids))
                            {
                                if (regions[map[height, column]].Isnew && DateTimeOffset.Now.ToUnixTimeSeconds() % 2 == 1)
                                    Console.BackgroundColor = ConsoleColor.White;
                                else if (regions[map[height, column]].Isold)
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                else
                                    Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else { Console.BackgroundColor = ConsoleColor.White; }
                            Console.Write($" {"",cellWidth} ");
                        }
                        Console.WriteLine();
                        for (int column = 0; column < mapWidth; column++)
                        {
                            if (map[height, column] == 0) { Console.BackgroundColor = ConsoleColor.Black; }
                            else if (inAlert(map[height, column], AlertsUids))
                            {
                                if (regions[map[height, column]].Isnew && DateTimeOffset.Now.ToUnixTimeSeconds() % 2 == 1)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else if (regions[map[height, column]].Isold) { 
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else { 
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                        
                            else { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
                            Console.Write($" {AirAlertMap.ShortenName(regions[map[height, column]].Name, cellWidth),cellWidth} ");

                        }
                        Console.WriteLine();
                        for (int column = 0; column < mapWidth; column++)
                        {
                            if (map[height, column] == 0) { Console.BackgroundColor = ConsoleColor.Black; }
                            else if (inAlert(map[height, column], AlertsUids))
                            {
                                if (regions[map[height, column]].Isnew && DateTimeOffset.Now.ToUnixTimeSeconds() % 2 == 1)
                                    Console.BackgroundColor = ConsoleColor.White;
                                else if (regions[map[height, column]].Isold)
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                else
                                    Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else { Console.BackgroundColor = ConsoleColor.White; }
                            Console.Write($" {"",cellWidth} ");
                        }

                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(0, 0);
                }
                
                Console.SetCursorPosition(0, 0);
            }
        }



    }
}

