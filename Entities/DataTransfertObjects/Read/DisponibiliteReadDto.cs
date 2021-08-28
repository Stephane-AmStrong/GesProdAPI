using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class DisponibiliteReadDto
    {
        public DisponibiliteReadDto()
        {
            ApproSites = new HashSet<ApproSiteReadDto>();
            VentProds = new HashSet<VentProdReadDto>();
        }

        public Guid Id { get; set; }
        public string Disponible { get; set; }
        public int? Quantite { get; set; }
        public int SeuilAlerte { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid? SiteId { get; set; }
        public Guid ProduitId { get; set; }

        public virtual ProduitReadDto Produit { get; set; }
        public virtual SiteReadDto Site { get; set; }
        public virtual ICollection<ApproSiteReadDto> ApproSites { get; set; }
        public virtual ICollection<VentProdReadDto> VentProds { get; set; }
    }
}
