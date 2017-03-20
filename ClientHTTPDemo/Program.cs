using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientHTTPDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "http://localhost:3820";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                //Opgave 1
                string urlStringHotel3 = "api/hotels/3";
                HttpResponseMessage respons = client.GetAsync(urlStringHotel3).Result;

                var hotel3 = respons.Content.ReadAsAsync<Hotel>().Result;

                Console.WriteLine($"OPGAVE 1: \n\tHotel_No: {hotel3.Hotel_No} \n\tHotelname: { hotel3.Name} \n\tAdresse: {hotel3.Address} ");

                //Opgave 2
                string urlStringGetAllHotels = "api/hotels";
                HttpResponseMessage responsGetAllHotels = client.GetAsync(urlStringGetAllHotels).Result;

                var GetAllHotels = responsGetAllHotels.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                var RoskildeList = from r in GetAllHotels
                                   where r.Address.Contains("Roskilde")
                                   select r;

                foreach (var item in RoskildeList)
                {
                    Console.WriteLine(item.Name);
                }

                //foreach (var item in RoskildeList)
                //{
                //    Console.WriteLine(item.Hotel_No);
                //}

            }
            


        }
    }
}
