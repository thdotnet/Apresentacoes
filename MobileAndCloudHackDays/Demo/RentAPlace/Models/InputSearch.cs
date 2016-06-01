using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAPlace.Models
{
    public class InputSearch
    {
        public string Q { get; set; }

        public string Filter { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public int CurrentPage { get; set; }


    }
}