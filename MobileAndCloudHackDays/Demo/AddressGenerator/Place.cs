using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressGenerator
{
    public class Place
    {
        public Place(Address address)
        {
            Address = address;
            _numberOfBedrooms = StaticRandom.Instance.Next(1, 6);
        }

        private int _numberOfBedrooms;

        public Address Address { get; set; }

        public string Type 
        { 
            get 
            { 
                return PlaceTypeGenerator.Get(); 
            } 
        }

        public int NumberOfBedrooms 
        { 
            get 
            {
                _numberOfBedrooms = StaticRandom.Instance.Next(1, 6);
                return _numberOfBedrooms;
            }
        }

        public int NumberOfSuites
        {
            get
            {
                return this.NumberOfBedrooms;
            }
        }

        public int FloorArea
        {
            get
            {
                return StaticRandom.Instance.Next(30, 400);
            }
        }

        public decimal LocationValue
        {
            get
            {
                return StaticRandom.Instance.Next(500, 8000);
            }
        }
    }    
}
