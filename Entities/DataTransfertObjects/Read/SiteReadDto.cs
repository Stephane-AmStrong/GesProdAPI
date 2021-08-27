using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class SiteReadDto
    {
        public SiteReadDto()
        {
            Disponibilites = new HashSet<DisponibiliteReadDto>();
            Utilisateurs = new HashSet<UtilisateurReadDto>();
        }

        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public string Adresse { get; set; }
        public string Tel { get; set; }

        public virtual ICollection<DisponibiliteReadDto> Disponibilites { get; set; }
        public virtual ICollection<UtilisateurReadDto> Utilisateurs { get; set; }
    }
}
