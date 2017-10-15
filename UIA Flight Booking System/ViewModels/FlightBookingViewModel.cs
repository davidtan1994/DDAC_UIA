using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIA_Flight_Booking_System.Models;

namespace UIA_Flight_Booking_System.ViewModels
{
    public class FlightBookingViewModel
    {
        public Guid flightID { get; set; }
        public bool runStartUpScript { get; set; }
        public string[] bookedSeats { get; set; }
        public int numberOfSelectedSeats { get; set; }
        public Flight_Detail flightDetail { get; set; }
        public List<Pricing> firstClassPriceList { get; set; }
        public List<Pricing> businessClassPriceList { get; set; }
        public List<Pricing> economyClassPriceList { get; set; }

        public FlightBookingViewModel()
        {
            runStartUpScript = true;
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
}