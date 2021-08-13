using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Vente
    {
        public Vente()
        {
            VentProds = new HashSet<VentProd>();
        }

        public Guid Id { get; set; }
        public DateTime DateVent { get; set; }
        public long NumFact { get; set; }
        public DateTime DateEnr { get; set; }
        public double TauxRemise { get; set; }
        public string TypeFacture { get; set; }
        public string ModePaiement { get; set; }
        public int? Aib { get; set; }
        public string CompteurTotalMecef { get; set; }
        public string CompteurTypeFactureMecef { get; set; }
        public int? MontantTotal { get; set; }
        public string NimMecef { get; set; }
        public string CodeMecef { get; set; }
        public Guid ClientsId { get; set; }
        public Guid IdUserEnr { get; set; }
        public string DateMecef { get; set; }
        public string QrcodeMecef { get; set; }
        public Guid? IdValidateur { get; set; }
        public DateTime? DateValidation { get; set; }
        public Guid? NumeroCompteId { get; set; }
        public DateTime? DateEcheance { get; set; }
        public string NumDispAib { get; set; }
        public string LibelleFacture { get; set; }
        public string QrcodeMecefAvoir { get; set; }
        public string CodeMecefAvoir { get; set; }
        public string CompteurTotalMecefAvoir { get; set; }
        public string CompteurTypeFactureMecefAvoir { get; set; }
        public string DateMecefAvoir { get; set; }
        public bool Api { get; set; }

        public virtual Client Client { get; set; }
        public virtual NumeroCompte NumeroCompte { get; set; }
        public virtual ICollection<VentProd> VentProds { get; set; }
    }
}
