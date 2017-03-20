using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientHTTPDemo
{
    public class Room
    {
        public int Room_No { get; set; }

        public int Hotel_No { get; set; }

        public string Types { get; set; }

        public double? Price { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }

        public virtual Hotel Hotel { get; set; }

        public override string ToString()
        {
            //return string.Format("HotelNo: {0}, Name: {1}, Address: {2}", HotelNo, Name, Address);
            //return string.Format( " " + Room_No + " " + Hotel_No + " " + Types + " " + Price );
            return " " + Room_No + " " + Hotel_No + " " + Types + " " + Price;
        }
    }
}
