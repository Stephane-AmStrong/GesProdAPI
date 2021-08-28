using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class AutreSortie
    {
        public Guid Id { get; set; }
        public string Motif { get; set; }
        public DateTime Date { get; set; }
        public string Site { get; set; }
        public int Quantite { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid ProduitId { get; set; }

        public virtual Produit Produit { get; set; }
    }
}
