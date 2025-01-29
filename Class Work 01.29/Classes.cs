
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeckofCards
{
    enum suits
    {
        Hearts,
        Clubs,
        Spades,
        Diamonds
    }
    class cardes
    { 
        public List<card> cards { get; set; }
    }
    class deckResponse
    {
        public string deck_id { get; set; }
        public int remaining { get; set; }
        public bool shuffled { get; set; }
    }

    class deck
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public List<card> cards { get; set; }
        public int remaining { get; set; }

        public deck(string deck_id)
        {
            string url = $"https://deckofcardsapi.com/api/deck/{this.deck_id}/";
            var deck = deckRequest(url);
            this.deck_id = deck.deck_id;
            this.remaining = deck.remaining;
            this.shuffled = deck.shuffled;
        }

        public deck(int deckCount = 1)
        {
            var client = new HttpClient();
            string url = "https://deckofcardsapi.com/api/deck/new/shuffle/";
            string parameters = $"?deck_count={deckCount}";
            var deck = deckRequest(url + parameters);
            this.deck_id = deck.deck_id;
            this.remaining = deck.remaining;
            this.shuffled = deck.shuffled;
        }
        private deckResponse deckRequest(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<deckResponse>(JsonResponse);
            }
            return null;
        }

        public async Task<deck> ReShuffle()
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{this.deck_id}/shuffle/?remaining=true";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<deck>(JsonResponse);
            }
            return null;
        }

        public async Task<deck> CardBack()
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{this.deck_id}/shuffle/";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<deck>(JsonResponse);
            }
            return null;
        }


    }

    class card
    {
        public string code { get; set; }
        public string value { get; set; }
        public string suit { get; set; }

        public override string ToString()
        {
            string a = "", b = "";
            if (this.suit == "HEARTS")
                a = "♥";
            else if (this.suit == "CLUBS")
                a = "♣";
            else if (this.suit == "DIAMONDS")
                a = "♦";
            else if (this.suit == "SPADES")
                a = "♠";
            if (value == "0" || value == "10")
                b = "1";
            return $"{b}{code}\b{a}";
        }
        public void print()
        {
            string a = "", b = "";
            if (this.suit == "HEARTS")
                a = "♥";
            else if (this.suit == "CLUBS")
                a = "♣";
            else if (this.suit == "DIAMONDS")
                a = "♦";
            else if (this.suit == "SPADES")
                a = "♠";
            if (value == "0" || value == "10")
                b = "1";
            if (this.suit == "HEARTS" || this.suit == "CLUBS")
                Console.ForegroundColor = ConsoleColor.Red;
            else { Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White; }
            Console.WriteLine($"{b}{code}\b{a}");
            Console.ResetColor();
        }

    }
}


