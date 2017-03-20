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
                    {
                        var hotel3 = response.Content.ReadAsAsync<Hotel>().Result;

                        Console.WriteLine($"OPGAVE 1: \n\tHotel No.: \t{hotel3.Hotel_No} \n\tHotelname: \t{ hotel3.Name} \n\tAdresse: \t{hotel3.Address} ");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Der er sket en fejl : " + e.Message);
                }


                //Opgave 2
                string urlStringGetAllHotels = "api/hotels";

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
                        //    Console.WriteLine(item);
                        //}
                        foreach (var item in RoskildeList)
                        {
                            Console.WriteLine($"\nOPGAVE 2: \n\tHotel No.: \t{ item.Hotel_No } \n\tHotelname: \t{ item.Name } \n\tAdresse: \t{ item.Address } ");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Der er sket en fejl : " + e.Message);
                }

                //Opgave 3

                string urlStringGetAllHotels2 = "api/hotels";
                string urlStringGetAllRooms = "api/rooms";
                try
                {
                    HttpResponseMessage responsGetAllHotels = client.GetAsync(urlStringGetAllHotels2).Result;
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
                                    Console.WriteLine($"\nOPGAVE 3: \n\tHotel No.: \t{ item.Hotel_No } \n\tHotelname: \t{ item.Name } \n\tAdresse: \t{ item.Address } \n\tRoom No.: \t{ item.Room_No } \n\tRoomtype: \t{ item.Price }");
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

                //Opgave 4
                Console.WriteLine("\nOPGAVE 4");
                string urlStringGetHotel3 = "api/hotels/3";

                try
                {
                    HttpResponseMessage getResponse = client.GetAsync(urlStringGetHotel3).Result;
                    {
                        var hotel3Response = getResponse.Content.ReadAsAsync<Hotel>().Result;

                        var MyUpdatedHotel = new Hotel()
                        {
                            Hotel_No = 3,
                            Address = "Inexpensive Road 7 3333 Cheaptown",
                            Name = "Discount"
                        };

                        MyUpdatedHotel.Name = "Osvald";

                        var response = client.PutAsJsonAsync<Hotel>("API/Hotels/3", MyUpdatedHotel).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Du har opdateret et hotel");
                            Console.WriteLine("Statuskode : " + response.StatusCode);
                        }
                        else
                        {
                            Console.WriteLine("Fejl, hotellet blev ikke opdateret");
                            Console.WriteLine("Statuskode : " + response.StatusCode);
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
