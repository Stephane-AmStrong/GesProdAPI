using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class VentProdReadDto
    {
        public Guid Id { get; set; }
        public string Localisation { get; set; }
        public int QteVendu { get; set; }
        public int? PrixVente { get; set; }
        public int? MntRemise { get; set; }
        public Guid VenteId { get; set; }
        public Guid? ServiceId { get; set; }
        public string TauxImposition { get; set; }
        public virtual ServiceReadDto Service { get; set; }
        public virtual VenteReadDto Vente { get; set; }
    }
}
