using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class FournisseurWriteDto
    {
        public string NomPrenom { get; set; }
        public string Adresse { get; set; }
        public string Tel { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }
    }
}
