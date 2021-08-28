using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class BonCadeau
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string CodeSecret { get; set; }
        public int Montant { get; set; }
        public DateTime DateExp { get; set; }
        public DateTime? DateUtilisation { get; set; }
    }
}
