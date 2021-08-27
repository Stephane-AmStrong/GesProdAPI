using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ClientReadDto
    {
        public ClientReadDto()
        {
            Ventes = new HashSet<VenteReadDto>();
        }

        public Guid Id { get; set; }
        //public string Nom { get; set; }
        //public string Prenom { get; set; }
        public string Tel { get; set; }
        public string NomEntreprise { get; set; }
        public string Ifu { get; set; }
        public string Adresse { get; set; }

        public virtual ICollection<VenteReadDto> Ventes { get; set; }
    }
}
