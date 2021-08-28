using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ApproSiteWriteDto
    {
        public DateTime DateApp { get; set; }
        public int QteApp { get; set; }
        public Guid DisponibilitesId { get; set; }
        public Guid IdUserEnr { get; set; }
        public DateTime DateEnr { get; set; }
    }
}
