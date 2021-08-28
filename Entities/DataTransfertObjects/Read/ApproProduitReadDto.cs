using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ApproProduitReadDto
    {
        public Guid Id { get; set; }
        public int QteApp { get; set; }
        public Guid ApprovisionnementsId { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid ProduitsId { get; set; }

        public virtual ApprovisionnementReadDto Approvisionnement { get; set; }
        public virtual ProduitReadDto Produit { get; set; }
    }
}
