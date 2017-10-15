using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIA_Flight_Booking_System.Models;

namespace UIA_Flight_Booking_System.ViewModels
{
    public class SearchFlightsViewModel
    {
        public string fromCountry { get; set; }
        public string toCountry { get; set; }
        public Nullable<DateTime> departureDate { get; set; }
        public List<Flight_Detail> flightList { get; set; }
    }

    public enum Country
    {
        Brunei,
        England,
        Indonesia,
        Malaysia,
        Singapore,
        Thailand,
        Ukraine
    }
}