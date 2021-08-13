using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Site
    {
        public Site()
        {
            Disponibilites = new HashSet<Disponibilite>();
            Utilisateurs = new HashSet<Utilisateur>();
        }

        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public string Addresse { get; set; }
        public string Tel { get; set; }

        public virtual ICollection<Disponibilite> Disponibilites { get; set; }
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
