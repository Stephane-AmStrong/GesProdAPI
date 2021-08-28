using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ServiceReadDto
    {
        //public ServiceReadDto()
        //{
        //    VentProds = new HashSet<VentProdReadDto>();
        //}

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public int PrixVente { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string TauxImposition { get; set; }
        public int? MntTaxeSpecifique { get; set; }
        public string LibelleTaxeSpecifique { get; set; }
        public Guid CategoriesId { get; set; }

        public virtual CategoryReadDto Category { get; set; }
        //public virtual ICollection<VentProdReadDto> VentProds { get; set; }
    }
}
