using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Utilisateur
    {
        public Guid Id { get; set; }
        public string CodeUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public bool NewConnexion { get; set; }
        public Guid ProfilsId { get; set; }
        public Guid SitesId { get; set; }

        public virtual Profil Profils { get; set; }
        public virtual Site Sites { get; set; }
    }
}
