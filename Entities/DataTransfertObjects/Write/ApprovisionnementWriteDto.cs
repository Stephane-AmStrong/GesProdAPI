using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ApprovisionnementWriteDto
    {
        public string Numero { get; set; }
        public DateTime DateAppr { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid FournisseurId { get; set; }
    }
}
