using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ApproSiteReadDto
    {
        public Guid Id { get; set; }
        public DateTime DateApp { get; set; }
        public int QteApp { get; set; }
        public Guid DisponibiliteId { get; set; }
        public Guid IdUserEnr { get; set; }
        public DateTime DateEnr { get; set; }

        public virtual DisponibiliteReadDto Disponibilite { get; set; }
    }
}
