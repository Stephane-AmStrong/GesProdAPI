using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class VenteParameters : PaginationParameters
    {
        //public uint MinYearOfBirth { get; set; }
        //public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;
        //public bool ValidYearRange => MaxYearOfBirth > MinYearOfBirth;


        public Guid? ClientsId { get; set; }
        public Guid? IdUserEnr { get; set; }
    }
}
