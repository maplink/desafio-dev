using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maplink.DesafioDev.Domain.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual string ToAutocomplete()
        {
            return $"{Street}, {Number}, {City}, {State}";
        }
    }
}