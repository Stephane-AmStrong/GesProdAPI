using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ApproProduit
    {
        public Guid Id { get; set; }
        public int QteApp { get; set; }
        public Guid ApprovisionnementId { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid ProduitId { get; set; }

        public virtual Approvisionnement Approvisionnement { get; set; }
        public virtual Produit Produit { get; set; }
    }
}
