using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ProduitWriteDto
    {
        public string Code { get; set; }
        public string Libelle { get; set; }
        public int Prix { get; set; }
        public int PrixVente { get; set; }
        public int QteStk { get; set; }
        public int SeuilAlerte { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string TauxImposition { get; set; }
        public string LibelleTaxeSpecifique { get; set; }
        public int MntTaxeSpecifique { get; set; }
        public Guid CategoryId { get; set; }
    }
}
