using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class UtilisateurReadDto
    {
        public Guid Id { get; set; }
        public string CodeUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public bool NewConnexion { get; set; }
        public Guid ProfilId { get; set; }
        public Guid SiteId { get; set; }

        public virtual ProfilReadDto Profil { get; set; }
        public virtual SiteReadDto Site { get; set; }
    }
}
