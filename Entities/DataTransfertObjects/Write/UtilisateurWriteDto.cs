using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class UtilisateurWriteDto
    {
        public string CodeUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public bool NewConnexion { get; set; }
        public Guid ProfilsId { get; set; }
        public Guid SitesId { get; set; }
    }
}
