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

                try
                {
                    HttpResponseMessage response = client.GetAsync(urlStringHotel3).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var hotel3 = response.Content.ReadAsAsync<Hotel>().Result;

                        Console.WriteLine($"OPGAVE 1: \n\tHotel_No: {hotel3.Hotel_No} \n\tHotelname: { hotel3.Name} \n\tAdresse: {hotel3.Address} ");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Der er sket en fejl : " + e.Message);
                }


                //Opgave 2
                //string urlStringGetAllHotels = "api/hotels";

                //try
                //{
                //    HttpResponseMessage responsGetAllHotels = client.GetAsync(urlStringGetAllHotels).Result;
                //    if (responsGetAllHotels.IsSuccessStatusCode)
                //    {
                //        var GetAllHotels = responsGetAllHotels.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                //        var RoskildeList = from h in GetAllHotels
                //                           where h.Address.Contains("Roskilde")
                //                           select h;

                //        foreach (var item in RoskildeList)
                //        {
                //            Console.WriteLine($"\nOPGAVE 2: \n\tHotel_No: {item.Hotel_No} \n\tHotelname: { item.Name} \n\tAdresse: {item.Address} ");
                //        }
                //    }
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Der er sket en fejl : " + e.Message);
                //}

                //Opgave 3

                string urlStringGetAllHotels = "api/hotels";
                string urlStringGetAllRooms = "api/rooms";
                try
                {
                    HttpResponseMessage responsGetAllHotels = client.GetAsync(urlStringGetAllHotels).Result;
                    if (responsGetAllHotels.IsSuccessStatusCode)
                    {
                        var GetAllHotels = responsGetAllHotels.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                        var RoskildeList = from h in GetAllHotels
                                           where h.Address.Contains("Roskilde")
                                           select h;

                        //foreach (var item in RoskildeList)
                        //{
                        //    Console.WriteLine($"\nOPGAVE 2: \n\tHotel_No: {item.Hotel_No} \n\tHotelname: { item.Name} \n\tAdresse: {item.Address} ");
                        //}



                        try
                        {
                            HttpResponseMessage responsGetAllRooms = client.GetAsync(urlStringGetAllRooms).Result;
                            if (responsGetAllRooms.IsSuccessStatusCode)
                            {
                                var GetAllRooms = responsGetAllRooms.Content.ReadAsAsync<IEnumerable<Room>>().Result;

                                var RoomList = from r in GetAllRooms
                                               select r;

                                //foreach (var item in RoomList)
                                //{
                                //    Console.WriteLine(item);
                                //}

                                var RoskildeHotelsAndRooms = from ros in RoskildeList
                                                             join room in RoomList
                                                             on ros.Hotel_No equals room.Hotel_No
                                                             select new { ros.Hotel_No, ros.Name, ros.Address, room.Room_No, room.Types, room.Price };

                                foreach (var item in RoskildeHotelsAndRooms)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Der er sket en fejl : " + e.Message);
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Der er sket en fejl : " + e.Message);
                }



            }
        }
    }
}
