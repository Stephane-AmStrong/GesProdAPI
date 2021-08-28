using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Service
    {
        public Service()
        {
            VentProds = new HashSet<VentProd>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public int PrixVente { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string TauxImposition { get; set; }
        public int? MntTaxeSpecifique { get; set; }
        public string LibelleTaxeSpecifique { get; set; }
        public Guid? VentProdId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<VentProd> VentProds { get; set; }
    }
}
