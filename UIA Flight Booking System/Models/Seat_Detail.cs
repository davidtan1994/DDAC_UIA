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
    
    public partial class Seat_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seat_Detail()
        {
            this.Passengers = new HashSet<Passenger>();
        }
    
        public System.Guid TicketID { get; set; }
        public System.Guid BookingID { get; set; }
        public int SeatID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual Booking_Detail Booking_Detail { get; set; }
    }
}
