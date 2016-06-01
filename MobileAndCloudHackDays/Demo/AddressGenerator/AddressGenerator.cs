using AddressGenerator.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressGenerator
{
    public class AddressGenerator
    {

        public static string Get()
        {
            var streets = new List<string>()
            {
                "Av Paulista", 
                "Rua Bom Pastor", 
                "Av Nazaré", 
                "Av Dr Gentil de Moura", 
                "Rua Santa Cruz", 
                "Rua Afonso Celso", 
                "Rua Loefgreen", 
                "Av do Cursino", 
                "Av Dr Ricardo Jafet", 
                "Rua Vergueiro", 
                "Rua Domingos de Morais" 
            };
            
            return string.Concat( streets.TakeRandomItem(), ", ", StaticRandom.Instance.Next(1, 2000));
        }
    }
}
