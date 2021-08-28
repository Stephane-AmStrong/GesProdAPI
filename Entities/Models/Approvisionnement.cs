using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Approvisionnement
    {
        public Approvisionnement()
        {
            ApproProduits = new HashSet<ApproProduit>();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }
        public DateTime DateAppr { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid FournisseursId { get; set; }

        public virtual Fournisseur Fournisseur { get; set; }
        public virtual ICollection<ApproProduit> ApproProduits { get; set; }
    }
}
