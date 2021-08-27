using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Fournisseur
    {
        public Fournisseur()
        {
            Approvisionnements = new HashSet<Approvisionnement>();
        }

        public Guid Id { get; set; }
        public string NomPrenom { get; set; }
        public string Adresse { get; set; }
        public string Tel { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }

        public virtual ICollection<Approvisionnement> Approvisionnements { get; set; }
    }
}
