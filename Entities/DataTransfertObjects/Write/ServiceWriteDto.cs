using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ServiceWriteDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Libelle { get; set; }
        public int PrixVente { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string TauxImposition { get; set; }
        public int MntTaxeSpecifique { get; set; }
        public string LibelleTaxeSpecifique { get; set; }
        public Guid CategoryId { get; set; }
    }
}
