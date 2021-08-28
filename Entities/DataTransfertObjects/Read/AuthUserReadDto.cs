using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransfertObjects
{
    public class AuthUserReadDto
    {
        public Guid Id { get; set; }
        public string CodeUser { get; set; }
        public string Name { get; set; }
        public Guid? ProfilsId { get; set; }
        public Guid? SitesId { get; set; }

        public DateTime? Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DisabledAt { get; set; }

        public virtual ProfilReadDto Profil { get; set; }
        public virtual SiteReadDto Site { get; set; }
    }
}
