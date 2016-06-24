using System.Collections.Generic;
using System.Linq;

namespace Maplink.DesafioDev.Domain.Entities
{
    public class Address
    {
        private static readonly IEnumerable<string> AcceptedUfs = new[]
        {
            "AC", "AL", "AP", "AM", "BA",
            "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB",
            "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP",
            "SE", "TO"
        };

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual bool IsValidState()
        {
            return AcceptedUfs.Contains(State?.ToUpper());
        }

        public virtual string ToAutocomplete()
        {
            return $"{Street}, {Number}, {City}, {State}";
        }
    }
}