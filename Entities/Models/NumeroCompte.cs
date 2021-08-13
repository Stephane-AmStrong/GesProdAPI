using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class NumeroCompte
    {
        public NumeroCompte()
        {
            Ventes = new HashSet<Vente>();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Banque { get; set; }

        public virtual ICollection<Vente> Ventes { get; set; }
    }
}
