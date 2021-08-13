using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Produit
    {
        public Produit()
        {
            ApproProduits = new HashSet<ApproProduit>();
            Disponibilites = new HashSet<Disponibilite>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public int Prix { get; set; }
        public int PrixVente { get; set; }
        public int QteStk { get; set; }
        public int SeuilAlerte { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string TauxImposition { get; set; }
        public string LibelleTaxeSpecifique { get; set; }
        public int MntTaxeSpecifique { get; set; }
        public Guid CategoriesId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ApproProduit> ApproProduits { get; set; }
        public virtual ICollection<Disponibilite> Disponibilites { get; set; }
    }
}
