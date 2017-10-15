using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIA_Flight_Booking_System.Models;

namespace UIA_Flight_Booking_System.ViewModels
{
    public class ViewReservationsViewModel
    {
        public Nullable<DateTime> filterDepartureDate { get; set; }
        public string departureCountry { get; set; }
        public string destinationCountry { get; set; }
        public Guid bookingID { get; set; }
        public List<BookingList_VM> reservationList { get; set; }
    }
}