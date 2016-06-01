using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressGenerator.Extension;

namespace AddressGenerator
{
    public class Address
    {
        public string FullStreet { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string Neighborhood { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public Address(string fullStreet, string city, string state, string neighborhood, double latitude, double longitude)
        {
            FullStreet = fullStreet;
            City = city;
            State = state;
            Neighborhood = neighborhood;
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Address ExtractAddress(string url, string fullStreet)
        {
            var cleanedUrl = url.CleanUrl();

            return new Address
            (
                fullStreet, 
                cleanedUrl.ExtractCity(), 
                cleanedUrl.ExtractState(), 
                cleanedUrl.ExtractNeighborhood(), 
                cleanedUrl.ExtracttLatitude(), 
                cleanedUrl.ExtracttLongitude()
            );
        }
    }
}
