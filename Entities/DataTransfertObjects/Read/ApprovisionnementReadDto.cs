using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ApprovisionnementReadDto
    {
        public ApprovisionnementReadDto()
        {
            ApproProduits = new HashSet<ApproProduitReadDto>();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }
        public DateTime DateAppr { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid FournisseursId { get; set; }

        public virtual FournisseurReadDto Fournisseur { get; set; }
        public virtual ICollection<ApproProduitReadDto> ApproProduits { get; set; }
    }
}
