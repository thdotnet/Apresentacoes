using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressGenerator.Extension;

namespace AddressGenerator
{
    public class PlaceTypeGenerator
    {
        public static string Get()
        {
            var types = new List<string>()
            {
                "Todos",
		        "Apartamento Padrão",
		        "Casa de Condomínio",
		        "Casa de Vila",
		        "Casa Padrão",
                "Cobertura",
		        "Flat",
		        "Kitchenette/Conjugados",
		        "Loft",
		        "Loteamento/Condomínio",
		        "Terreno Padrão"
            };

            return types.TakeRandomItem();
        }
    }
}
