using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class FournisseurReadDto
    {
        public FournisseurReadDto()
        {
            Approvisionnements = new HashSet<ApprovisionnementReadDto>();
        }

        public Guid Id { get; set; }
        public string NomPrenom { get; set; }
        public string Adresse { get; set; }
        public string Tel { get; set; }
        public DateTime DateEnr { get; set; }
        public Guid IdUserEnr { get; set; }

        public virtual ICollection<ApprovisionnementReadDto> Approvisionnements { get; set; }
    }
}
