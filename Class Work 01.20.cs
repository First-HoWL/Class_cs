using System.Text;
using Game.Game;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;



namespace Game
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();
            string url = "https://meowfacts.herokuapp.com/";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }


        }
    }
}
