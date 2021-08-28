using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ApproProduitWriteDto
    {
        public int QteApp { get; set; }
        public Guid ApprovisionnementId { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid ProduitId { get; set; }
    }
}
