//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UIA_Flight_Booking_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Passenger
    {
        public System.Guid PassengerID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public System.DateTime DOB { get; set; }
        public string Nationality { get; set; }
        public System.Guid TicketID { get; set; }
    
        public virtual Seat_Detail Seat_Detail { get; set; }
    }
}