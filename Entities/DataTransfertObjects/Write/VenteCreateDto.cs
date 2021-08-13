using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class VenteCreateDto
    {
        public DateTime DateVent { get; set; }
        public string LibelleFacture { get; set; }
        public DateTime? DateEcheance { get; set; }
    }
}
