using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class VenteParameters : PaginationParameters
    {
        //public uint MinYearOfBirth { get; set; }
        //public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;
        //public bool ValidYearRange => MaxYearOfBirth > MinYearOfBirth;

        [JsonIgnore]
        public Guid? ClientId { get; set; }

        public Guid? IdUserEnr { get; set; }
    }
}
