using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ClientWriteDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string NomEntreprise { get; set; }
        public string Ifu { get; set; }
        public string Addresse { get; set; }
    }
}
