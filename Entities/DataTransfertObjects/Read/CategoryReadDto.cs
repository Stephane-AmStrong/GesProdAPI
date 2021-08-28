using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class CategoryReadDto
    {
        public CategoryReadDto()
        {
            //Produit = new HashSet<ProduitReadDto>();
            Services = new HashSet<ServiceReadDto>();
        }

        public Guid Id { get; set; }
        public string Libelle { get; set; }

        //public virtual ICollection<ProduitReadDto> Produit { get; set; }
        public virtual ICollection<ServiceReadDto> Services { get; set; }
    }
}
