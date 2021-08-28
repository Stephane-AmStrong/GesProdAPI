using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class VentProdCreateDto
    {
        public string Localisation { get; set; }
        public int QteVendu { get; set; }
        //public int? MntRemise { get; set; }
        public Guid? VenteId { get; set; }
        [Required]
        public Guid ServiceId { get; set; }

    }
}
