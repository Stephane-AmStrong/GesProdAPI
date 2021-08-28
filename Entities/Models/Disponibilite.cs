using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Disponibilite
    {
        public Disponibilite()
        {
            ApproSites = new HashSet<ApproSite>();
            VentProds = new HashSet<VentProd>();
        }

        public Guid Id { get; set; }
        public string Disponible { get; set; }
        public int? Quantite { get; set; }
        public int SeuilAlerte { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid? SitesId { get; set; }
        public Guid ProduitsId { get; set; }

        public virtual Produit Produits { get; set; }
        public virtual Site Sites { get; set; }
        public virtual ICollection<ApproSite> ApproSites { get; set; }
        public virtual ICollection<VentProd> VentProds { get; set; }
    }
}
