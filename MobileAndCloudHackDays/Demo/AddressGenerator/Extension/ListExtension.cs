using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressGenerator.Extension
{
    public static class ListExtension
    {
        public static string TakeRandomItem(this List<string> items)
        {
            return items.OrderBy(x => Guid.NewGuid()).Take(1).Single();
        }

    }
}
