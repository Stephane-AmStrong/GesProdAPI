using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class CategoryReadDto
    {
        public CategoryReadDto()
        {
            Produits = new HashSet<ProduitReadDto>();
            Services = new HashSet<ServiceReadDto>();
        }

        public Guid Id { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<ProduitReadDto> Produits { get; set; }
        public virtual ICollection<ServiceReadDto> Services { get; set; }
    }
}
