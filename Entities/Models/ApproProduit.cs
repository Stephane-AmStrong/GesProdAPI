using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ApproProduit
    {
        public Guid Id { get; set; }
        public int QteApp { get; set; }
        public Guid ApprovisionnementsId { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid ProduitsId { get; set; }

        public virtual Approvisionnement Approvisionnement { get; set; }
        public virtual Produit Produit { get; set; }
    }
}
