using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class VentProdUpdateDto
    {
        public Guid? Id { get; set; }
        public string Localisation { get; set; }
        public int QteVendu { get; set; }
        public int? PrixVente { get; set; }
        public int? MntRemise { get; set; }
        public Guid? VentesId { get; set; }
        public Guid? ServicesId { get; set; }
        public string TauxImposition { get; set; }
    }
}
