﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class VentProd
    {
        public Guid Id { get; set; }
        public string Localisation { get; set; }
        public int QteVendu { get; set; }
        public int? PrixVente { get; set; }
        public int? MntRemise { get; set; }
        public Guid VenteId { get; set; }
        public Guid? DisponibiliteId { get; set; }
        public Guid? ServiceId { get; set; }
        public string TauxImposition { get; set; }

        public virtual Disponibilite Disponibilite { get; set; }
        public virtual Service Service { get; set; }
        public virtual Vente Vente { get; set; }
    }
}
