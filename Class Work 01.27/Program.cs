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
    public static async Task<deck> GetResponse(int count)
    {
        var client = new HttpClient();
        string url = "https://deckofcardsapi.com/api/deck/new/shuffle/";
        string parameters = $"?deck_count={count}";
        var response = await client.GetAsync(url + parameters);
        if (response.IsSuccessStatusCode)
        {
            var JsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<deck>(JsonResponse);
        }
        return null;
    }
    
    public static async Task Main(string[] args){
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            var res = await GetResponse(1);
            //deck res = new deck();
            Console.WriteLine(res.deck_id);
            Console.WriteLine(res.remaining);
            Console.WriteLine(res.cards);
            res = await deck.card(20, res.deck_id);
            if (res == null)
                Console.WriteLine("error");
            else foreach (var card in res.cards)
            {
                card.print();
            }
            Console.WriteLine(res.remaining);
        }
    }

}
