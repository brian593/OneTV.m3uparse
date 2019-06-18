using System;
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


            string fileLocation = "http://srregio.xyz/IPTV/regioflix.m3u";
            
            m3U8Parser.ParseUrl(fileLocation);
           
        }
    }
}
