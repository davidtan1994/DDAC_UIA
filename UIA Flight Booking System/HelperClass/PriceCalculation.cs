using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIA_Flight_Booking_System.Models;

namespace UIA_Flight_Booking_System.Classes
{
    public class PriceCalculation
    {
        private string GetAgeCategory(string dob)
        {
            DateTime birthdate = Convert.ToDateTime(dob);
            double age = ((DateTime.Today - birthdate).TotalDays)/365;
            string ageCategory = null;

            if (age <= 3 && age >= 0)
            {
                ageCategory = "Infant";
            }

            else if (age >= 4 && age <= 17)
            {
                ageCategory = "Children";
            }
            else if (age >= 18)
            {
                ageCategory = "Adult";
            }

            return ageCategory;
        }

        public decimal GetTotalPrice(string[] DOBList, string[] seats, Guid flightID)
        {
            List<decimal> ticketsPrices = new List<decimal>();
            string flightClass = null;

            using (UIAEntities db = new UIAEntities())
            {
                var flightDetail = (from f in db.Pricings where f.FlightID == flightID select f).ToList();

                for (int i = 0; i < DOBList.Length; i++)
                {
                    
                    if (Convert.ToInt16(seats[i]) <= 18 && Convert.ToInt16(seats[i]) >= 1)
                    {
                        flightClass = "First";

                    }
                    else if (Convert.ToInt16(seats[i]) <= 60 && Convert.ToInt16(seats[i]) >= 19)
                    {
                        flightClass = "Business";
                    }

                    else if (Convert.ToInt16(seats[i]) <= 180 && Convert.ToInt16(seats[i]) >= 61)
                    {
                        flightClass = "Economy";
                    }

                    string ageCategory = GetAgeCategory(DOBList[i]);
                    var price = (from f in flightDetail where f.AgeCategory == ageCategory && f.ClassCategory == flightClass select f.Price).First();

                    ticketsPrices.Add(price);
                }

                decimal totalPrice = ticketsPrices.Sum();

                return totalPrice;
            }
        }
    }
}