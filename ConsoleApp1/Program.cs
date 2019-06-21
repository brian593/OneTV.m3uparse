using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            inicio();
        }

       

        private static async void inicio()
        {
            OneTV.m3uparse.M3u8Parser m3U8Parser = new OneTV.m3uparse.M3u8Parser();
            ObservableCollection<OneTV.m3uparse.Entitie.Item> Lista = new ObservableCollection<OneTV.m3uparse.Entitie.Item>();


            string fileLocation = "https://pastebin.com/raw/bU1c2Lju";
            //https://pastebin.com/raw/bU1c2Lju
            //http://srregio.xyz/IPTV/regioflix.m3u

            m3U8Parser.ParseUrl(fileLocation);
           
        }
    }
}
