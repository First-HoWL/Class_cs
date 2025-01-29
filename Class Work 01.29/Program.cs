using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckofCards;
using System.Net.Http;
using System.Text.Json;

namespace HoWL_C_.Class_Work_01._27
{
    class Program {
        public static async Task<cardes> card(int count, string deck_id)
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={count}";
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<cardes>(JsonResponse);
            }
            return null;
        }

        public static async Task Main(string[] args){
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            deck res = new deck(1);
            Console.WriteLine(res.deck_id);
            Console.WriteLine(res.remaining);
            Console.WriteLine(res.cards);
            int r = 20;
            var a = card(r, res.deck_id);
            List <card> d = new List<card>();
            for (int i = 0; i < r; i++)
                d.Add(a.retCards(i));
            if (a == null)
                Console.WriteLine("error");
            
            else foreach (var card in d)
            {
                card.print();
            }
            Console.WriteLine(res.remaining);
        }
    }

}
