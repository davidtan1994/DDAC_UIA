using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UIA_Flight_Booking_System.Models;
using UIA_Flight_Booking_System.ViewModels;
using UIA_Flight_Booking_System.Classes;

namespace UIA_Flight_Booking_System.Controllers
{
    public class CustomerController : Controller
    {
        private UIAEntities db = new UIAEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchFlights(SearchFlightsViewModel model)
        {
            var today = DateTime.Today;

            var search = from f in db.Flight_Detail where f.DepartureDateTime > today select f;

            if (!String.IsNullOrEmpty(model.fromCountry))
            {
                search = search.Where(x => x.DepartureVenue == model.fromCountry);
            }

            if (!String.IsNullOrEmpty(model.toCountry))
            {
                search = search.Where(x => x.DestinationVenue == model.toCountry);
            }

            if (model.departureDate != null && model.departureDate != DateTime.MinValue)
            {
                search = search.Where(x => DbFunctions.TruncateTime(x.DepartureDateTime) == model.departureDate);
            }

            if (model.departureDate == DateTime.MinValue)
            {
                model.departureDate = null;
            }

            model.flightList = search.OrderBy(x => x.DepartureDateTime).ToList();

            return View(model);
        }

        public ActionResult FlightBooking(Guid flightID)
        {
            FlightBookingViewModel model = new FlightBookingViewModel();

            string bookedSeats = String.Join(",", (from b in db.Booking_Detail
                                                   join s in db.Seat_Detail on b.BookingID equals s.BookingID
                                                   where b.FlightID == flightID
                                                   select s.SeatID).ToArray());
            var flightDetail = (from f in db.Flight_Detail where f.FlightID == flightID select f).FirstOrDefault();
            var priceList = (from p in db.Pricings where p.FlightID == flightID select p).ToList();

            var firstClassPriceList = (from pl in priceList where pl.ClassCategory == "First" select pl).OrderBy(x => x.Price).ToList();
            var businessClassPriceList = (from pl in priceList where pl.ClassCategory == "Business" select pl).OrderBy(x => x.Price).ToList();
            var economyClassPriceList = (from pl in priceList where pl.ClassCategory == "Economy" select pl).OrderBy(x => x.Price).ToList();

            ViewBag.BookedSeats = bookedSeats;
            model.flightID = flightID;
            model.flightDetail = flightDetail;
            model.firstClassPriceList = firstClassPriceList;
            model.businessClassPriceList = businessClassPriceList;
            model.economyClassPriceList = economyClassPriceList;

            return View(model);
        }

        public ActionResult FlightBooking(FlightBookingViewModel model, string[] Name, string[] Gender, string[] Nationality, string[] DOB, string btnProceed, string btnSubmit)
        {
            string seats = Request.Form["SeatsSelected"];
            string[] seat = seats.Split(new char[] { ',' });

            if (!String.IsNullOrEmpty(btnProceed))
            {
                string bookedSeats = String.Join(",", (from b in db.Booking_Detail
                                                       join s in db.Seat_Detail on b.BookingID equals s.BookingID
                                                       where b.FlightID == model.flightID
                                                       select s.SeatID).ToArray());
                ViewBag.BookedSeats = bookedSeats;
                ViewBag.SelectedSeats = seats;

                var flightDetail = (from f in db.Flight_Detail where f.FlightID == model.flightID select f).FirstOrDefault();
                var priceList = (from p in db.Pricings where p.FlightID == model.flightID select p).ToList();

                var firstClassPriceList = (from pl in priceList where pl.ClassCategory == "First" select pl).OrderBy(x => x.Price).ToList();
                var businessClassPriceList = (from pl in priceList where pl.ClassCategory == "Business" select pl).OrderBy(x => x.Price).ToList();
                var economyClassPriceList = (from pl in priceList where pl.ClassCategory == "Economy" select pl).OrderBy(x => x.Price).ToList();

                model.numberOfSelectedSeats = seat.Length;
                model.bookedSeats = seat;
                model.flightDetail = flightDetail;
                model.firstClassPriceList = firstClassPriceList;
                model.businessClassPriceList = businessClassPriceList;
                model.economyClassPriceList = economyClassPriceList;
                ViewBag.Disable = "disable";
            }

            if (!String.IsNullOrEmpty(btnSubmit))
            {
                Guid sessionUserID = new Guid(User.Identity.Name.Split('|')[1].ToString());

                PriceCalculation priceCalculation = new PriceCalculation();
               
                Booking_Detail bookingDetail = new Booking_Detail()
                {
                    BookingID = Guid.NewGuid(),
                    FlightID = model.flightID,
                    BookedByCustomerID = sessionUserID,
                    Amount = priceCalculation.GetTotalPrice(DOB, seat, model.flightID),
                    BookingDateTime = DateTime.Now              
            };
                db.Booking_Detail.Add(bookingDetail);
                db.SaveChanges();

                for (int i = 0; i < seat.Length; i++)
                {
                    Seat_Detail seatDetail = new Seat_Detail()
                    {
                        TicketID = Guid.NewGuid(),
                        BookingID = bookingDetail.BookingID,
                        SeatID = Convert.ToInt16(seat[i])
                    };
                    db.Seat_Detail.Add(seatDetail);
                    db.SaveChanges();

                    Passenger passenger = new Passenger()
                    {
                        PassengerID = Guid.NewGuid(),
                        Name = Name[i],
                        Gender = Gender[i],
                        DOB = Convert.ToDateTime(DOB[i]),
                        Nationality = Nationality[i],
                        TicketID = seatDetail.TicketID
                    };
                    db.Passengers.Add(passenger);
                    db.SaveChanges();
                }
                return RedirectToAction("Home", "Customer");
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult ViewMyReservations(ViewReservationsViewModel model)
        {
            Guid sessionUserID = new Guid(User.Identity.Name.Split('|')[1].ToString());

            var reservations = (from v in db.BookingList_VM where v.DepartureDateTime >= DateTime.Today && v.BookedByCustomerID == sessionUserID select v);

            if (!String.IsNullOrEmpty(model.departureCountry))
            {
                reservations = reservations.Where(x => x.DepartureVenue == model.departureCountry);
            }

            if (!String.IsNullOrEmpty(model.destinationCountry))
            {
                reservations = reservations.Where(x => x.DestinationVenue == model.destinationCountry);
            }

            if(model.filterDepartureDate != null && model.filterDepartureDate != DateTime.MinValue)
            {
                reservations = reservations.Where(x => DbFunctions.TruncateTime(x.DepartureDateTime) == model.filterDepartureDate);
            }

            model.reservationList = reservations.OrderByDescending(x=>x.BookingDateTime).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult ViewReservationDetail(Guid bookingID)
        {
            var reservationDetail = (from v in db.BookingDetails_VM where v.BookingID == bookingID select v).OrderBy(x=>x.SeatID).ToList();
            return View(reservationDetail);
        }
    }
}