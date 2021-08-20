using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class VenteCreateDto
    {
        public VenteCreateDto()
        {
            VentProds = new HashSet<VentProdCreateDto>();
        }


        public DateTime DateVent { get; set; }
        public string LibelleFacture { get; set; }
        public DateTime? DateEcheance { get; set; }
        public Guid? ClientsId { get; set; }

        public virtual ICollection<VentProdCreateDto> VentProds { get; set; }
    }
}
