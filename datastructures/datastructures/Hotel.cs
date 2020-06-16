using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datastructuresproject
{
    public class ListOfHotels
    {
        public static List<Hotel> hotels = new List<Hotel>(); 
    }

    public class Hotel
    { 
        public int id;
        public string name;
        public int stars;
        public int numberOfRooms;
        public List<Reservation> reservations = new List<Reservation>();
    }

    public class Reservation
    {  
        public string name;
        public DateTime checkinDate;
        public int stayDurationDays;
    }
}   