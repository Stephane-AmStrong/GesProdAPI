using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class NumeroCompteReadDto
    {
        public NumeroCompteReadDto()
        {
            Ventes = new HashSet<VenteReadDto>();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Banque { get; set; }

        public virtual ICollection<VenteReadDto> Ventes { get; set; }
    }
}
