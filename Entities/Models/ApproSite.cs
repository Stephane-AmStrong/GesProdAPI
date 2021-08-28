using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ApproSite
    {
        public Guid Id { get; set; }
        public DateTime DateApp { get; set; }
        public int QteApp { get; set; }
        public Guid DisponibilitesId { get; set; }
        public Guid IdUserEnr { get; set; }
        public DateTime DateEnr { get; set; }

        public virtual Disponibilite Disponibilites { get; set; }
    }
}
