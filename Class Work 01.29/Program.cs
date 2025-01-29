using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckofCards;
using System.Net.Http;
using System.Text.Json;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HoWL_C
{

    class API
    {
        public static readonly string host = "https://date.nager.at/";
        public static readonly HttpClient httpClient = new HttpClient();

        public static string MakeRequest(string url)
        {
            var responce = httpClient.GetAsync(url).Result;
            if (responce != null && responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadAsStringAsync().Result;
            }
            else throw new HttpRequestException();
        }
    }
    enum HolidayTypes
    {
        Public, Bank, School, Authorities, Optional, Observance
    }
    class Holiday
    {
        public string date { get; set; }
        public string? localName { get; set; }
        public string? name { get; set; }
        public string? countryCode { get; set; }

        public bool global { get; set; }
        public int? launchYear { get; set; }
        public List<string> types { get; set; }

        public override string ToString()
        {
            
            return $"{localName} - {date}";
        }
    }

    

    class Program
    {
        public static async Task<cardes> card(int count, string deck_id)
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={count}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<cardes>(JsonResponse);
            }
            return null;
        }


        public static async Task Main(string[] args)
        {

            /*Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            deck res = new deck(1);
            Console.WriteLine(res.deck_id);
            Console.WriteLine(res.remaining);
            Console.WriteLine(res.cards);
            int r = 20;
            cardes a = new cardes();
            a = await card(r, res.deck_id);

            if (a == null)
                Console.WriteLine("error");

            else foreach (var card in a.cards)
                {
                    card.print();
                }
            Console.WriteLine(res.remaining);*/
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;

            string url = "https://date.nager.at/api/v3/PublicHolidays/2025/UA";
            string n = API.MakeRequest(url);
            List<Holiday> g = new List<Holiday>();
            g = JsonSerializer.Deserialize<List<Holiday>>(n);
            
            if (g == null)
                Console.WriteLine("erorr");
            bool next = true;
            foreach(var Hollyday in g)
            {
                
                if (DateTime.ParseExact(Hollyday.date, "yyyy-MM-dd", null) < DateTime.Now)
                    Console.ForegroundColor = ConsoleColor.Red;
                
                else if (DateTime.ParseExact(Hollyday.date, "yyyy-MM-dd", null) >= DateTime.Now && next) {
                    Console.WriteLine($"Today - {DateTime.Now}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    next = false;
                }
                
                else
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Hollyday);
                Console.ResetColor();
            }
             

            

        }
    }

}
