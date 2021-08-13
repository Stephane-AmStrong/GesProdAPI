using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Category
    {
        public Category()
        {
            Produits = new HashSet<Produit>();
            Services = new HashSet<Service>();
        }

        public Guid Id { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Produit> Produits { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
