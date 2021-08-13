using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class HistoriqueWriteDto
    {
        public Guid UserId { get; set; }
        public DateTime DateEnr { get; set; }
        public string Action { get; set; }
    }
}
