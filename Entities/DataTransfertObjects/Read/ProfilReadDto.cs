using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.DataTransfertObjects
{
    public partial class ProfilReadDto
    {
        public ProfilReadDto()
        {
            Utilisateurs = new HashSet<UtilisateurReadDto>();
        }

        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public bool Profil1 { get; set; }
        public bool Produit { get; set; }
        public bool Categorie { get; set; }
        public bool Site { get; set; }
        public bool Utilisateur { get; set; }
        public bool Visibilite { get; set; }
        public bool ApproProduit { get; set; }
        public bool ApproSite { get; set; }
        public bool Vente { get; set; }
        public bool StockSite { get; set; }
        public bool AutreSortie { get; set; }
        public bool ConsulterStockAlerte { get; set; }
        public bool ValiderFacture { get; set; }
        public bool NormaliserFacture { get; set; }
        public bool NumeroCompte { get; set; }
        public bool InventaireProduit { get; set; }
        public bool SituationJournaliere { get; set; }
        public bool ModifierFacture { get; set; }
        public bool Historique { get; set; }

        public virtual ICollection<UtilisateurReadDto> Utilisateurs { get; set; }
    }
}
