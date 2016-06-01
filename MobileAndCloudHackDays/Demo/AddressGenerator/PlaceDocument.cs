using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressGenerator
{
    [SerializePropertyNamesAsCamelCase]
    public class PlaceDocument
    {
        public string Id { get; set; }

        public string StreetName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Neighborhood { get; set; }

        public GeographyPoint Geolocation { get; set; }

        public string PlaceType { get; set; }

        public int NumberOfBedrooms { get; set; }

        public int NumberOfSuites { get; set; }

        public int FloorArea { get; set; }

        public decimal LocationValue { get; set; }
    }
}
