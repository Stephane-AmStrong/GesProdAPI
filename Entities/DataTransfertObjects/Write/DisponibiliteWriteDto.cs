using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class DisponibiliteWriteDto
    {
        public string Disponible { get; set; }
        public int? Quantite { get; set; }
        public int SeuilAlerte { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
        public Guid? SiteId { get; set; }
        public Guid ProduitId { get; set; }
    }
}
