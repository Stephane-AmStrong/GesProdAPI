using System;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Entities
{
    public partial class RepositoryContext : IdentityDbContext<AppUser>
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<ApproProduit> ApproProduits { get; set; }
        public virtual DbSet<ApproSite> ApproSites { get; set; }
        public virtual DbSet<Approvisionnement> Approvisionnements { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Disponibilite> Disponibilites { get; set; }
        public virtual DbSet<Fournisseur> Fournisseurs { get; set; }
        public virtual DbSet<Historique> Historiques { get; set; }
        public virtual DbSet<NumeroCompte> NumeroComptes { get; set; }
        public virtual DbSet<Parametre> Parametres { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Profil> Profils { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<VentProd> VentProds { get; set; }
        public virtual DbSet<Vente> Ventes { get; set; }
        public virtual DbSet<Visibilite> Visibilites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Database=DB_InterfacPlus_Prod;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AppUser>().HasIndex(x => new { x.Id });
            base.OnModelCreating(modelBuilder);


            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<ApproProduit>(entity =>
            {
                entity.ToTable("Appro_Produit");

                entity.HasIndex(e => e.ApprovisionnementsId, "IX_FK_ApprovisionnementsAppro_Produit");

                entity.HasIndex(e => e.ProduitsId, "IX_FK_ProduitsAppro_Produit");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.IdUserEnr).HasColumnName("Id_user_enr");

                entity.Property(e => e.QteApp).HasColumnName("Qte_app");

                entity.HasOne(d => d.Approvisionnement)
                    .WithMany(p => p.ApproProduits)
                    .HasForeignKey(d => d.ApprovisionnementsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApprovisionnementsAppro_Produit");

                entity.HasOne(d => d.Produit)
                    .WithMany(p => p.ApproProduits)
                    .HasForeignKey(d => d.ProduitsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProduitsAppro_Produit");
            });

            modelBuilder.Entity<ApproSite>(entity =>
            {
                entity.ToTable("Appro_Site");

                entity.HasIndex(e => e.DisponibilitesId, "IX_FK_DisponibilitesAppro_Site");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateApp)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_app");

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.IdUserEnr).HasColumnName("Id_user_enr");

                entity.Property(e => e.QteApp).HasColumnName("Qte_App");

                entity.HasOne(d => d.Disponibilite)
                    .WithMany(p => p.ApproSites)
                    .HasForeignKey(d => d.DisponibilitesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisponibilitesAppro_Site");
            });

            modelBuilder.Entity<Approvisionnement>(entity =>
            {
                entity.HasIndex(e => e.FournisseursId, "IX_FK_FournisseursApprovisionnements");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateAppr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_appr");

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.IdUserEnr).HasColumnName("Id_user_enr");

                entity.Property(e => e.Numero).IsRequired();

                entity.HasOne(d => d.Fournisseur)
                    .WithMany(p => p.Approvisionnements)
                    .HasForeignKey(d => d.FournisseursId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FournisseursApprovisionnements");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Libelle).IsRequired();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ifu).HasColumnName("IFU");

                entity.Property(e => e.NomEntreprise)
                    .IsRequired()
                    .HasColumnName("Nom_Entreprise");

                entity.Property(e => e.Tel).IsRequired();
            });

            modelBuilder.Entity<Disponibilite>(entity =>
            {
                entity.HasIndex(e => e.ProduitsId, "IX_FK_ProduitsDisponibilites");

                entity.HasIndex(e => e.SitesId, "IX_FK_SitesDisponibilites");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.Disponible).IsRequired();

                entity.Property(e => e.IdUserEnr).HasColumnName("Id_user_enr");

                entity.Property(e => e.SeuilAlerte).HasColumnName("Seuil_alerte");

                entity.HasOne(d => d.Produit)
                    .WithMany(p => p.Disponibilites)
                    .HasForeignKey(d => d.ProduitsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProduitsDisponibilites");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Disponibilites)
                    .HasForeignKey(d => d.SitesId)
                    .HasConstraintName("FK_SitesDisponibilites");
            });

            modelBuilder.Entity<Fournisseur>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Addresse).IsRequired();

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.IdUserEnr).HasColumnName("Id_user_enr");

                entity.Property(e => e.NomPrenom)
                    .IsRequired()
                    .HasColumnName("Nom_Prenom");

                entity.Property(e => e.Tel).IsRequired();
            });

            modelBuilder.Entity<Historique>(entity =>
            {
                entity.ToTable("Historique");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<NumeroCompte>(entity =>
            {
                entity.ToTable("NumeroCompte");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Banque).IsRequired();

                entity.Property(e => e.Numero).IsRequired();
            });

            modelBuilder.Entity<Parametre>(entity =>
            {
                entity.ToTable("Parametre");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EMecef).HasColumnName("eMecef");

                entity.Property(e => e.NumPort).HasColumnName("Num_Port");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasIndex(e => e.CategoriesId, "IX_FK_CategoriesProduits");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Libelle).IsRequired();

                entity.Property(e => e.LibelleTaxeSpecifique).HasColumnName("Libelle_Taxe_Specifique");

                entity.Property(e => e.MntTaxeSpecifique).HasColumnName("Mnt_Taxe_Specifique");

                entity.Property(e => e.PrixVente).HasColumnName("Prix_Vente");

                entity.Property(e => e.QteStk).HasColumnName("Qte_Stk");

                entity.Property(e => e.SeuilAlerte).HasColumnName("Seuil_alerte");

                entity.Property(e => e.TauxImposition)
                    .IsRequired()
                    .HasColumnName("Taux_Imposition");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Produits)
                    .HasForeignKey(d => d.CategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoriesProduits");
            });

            modelBuilder.Entity<Profil>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ApproProduit).HasColumnName("Appro_produit");

                entity.Property(e => e.ApproSite).HasColumnName("Appro_site");

                entity.Property(e => e.AutreSortie).HasColumnName("Autre_sortie");

                entity.Property(e => e.ConsulterStockAlerte).HasColumnName("Consulter_stock_alerte");

                entity.Property(e => e.InventaireProduit).HasColumnName("Inventaire_Produit");

                entity.Property(e => e.Libelle).IsRequired();

                entity.Property(e => e.ModifierFacture).HasColumnName("Modifier_Facture");

                entity.Property(e => e.NormaliserFacture).HasColumnName("Normaliser_Facture");

                entity.Property(e => e.NumeroCompte).HasColumnName("Numero_compte");

                entity.Property(e => e.Profil1).HasColumnName("Profil");

                entity.Property(e => e.SituationJournaliere).HasColumnName("Situation_Journaliere");

                entity.Property(e => e.StockSite).HasColumnName("Stock_site");

                entity.Property(e => e.ValiderFacture).HasColumnName("Valider_Facture");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.CategoriesId, "IX_FK_Services_Categories");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Libelle).IsRequired();

                entity.Property(e => e.LibelleTaxeSpecifique).HasColumnName("Libelle_Taxe_specifique");

                entity.Property(e => e.MntTaxeSpecifique).HasColumnName("Mnt_Taxe_Specifique");

                entity.Property(e => e.PrixVente).HasColumnName("Prix_Vente");

                entity.Property(e => e.TauxImposition)
                    .IsRequired()
                    .HasColumnName("Taux_Imposition");

                entity.Property(e => e.VentProdId).HasColumnName("Vent_Prod_Id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_Categories");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Addresse).IsRequired();

                entity.Property(e => e.Libelle).IsRequired();

                entity.Property(e => e.Tel).IsRequired();
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasIndex(e => e.ProfilsId, "IX_FK_ProfilsUtilisateurs");

                entity.HasIndex(e => e.SitesId, "IX_FK_SitesUtilisateurs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CodeUser).HasColumnName("Code_User");

                entity.Property(e => e.Login).IsRequired();

                entity.Property(e => e.NewConnexion).HasColumnName("New_Connexion");

                entity.Property(e => e.Nom).IsRequired();

                entity.Property(e => e.Prenom).IsRequired();

                entity.Property(e => e.Pwd).IsRequired();

                entity.HasOne(d => d.Profil)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.ProfilsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfilsUtilisateurs");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.SitesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SitesUtilisateurs");
            });

            modelBuilder.Entity<VentProd>(entity =>
            {
                entity.ToTable("Vent_Prod");

                entity.HasIndex(e => e.DisponibilitesId, "IX_FK_DisponibilitesVent_Prod");

                entity.HasIndex(e => e.ServicesId, "IX_FK_Vent_Prod_Vent_Prod");

                entity.HasIndex(e => e.VentesId, "IX_FK_VentesVent_Prod");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MntRemise).HasColumnName("Mnt_Remise");

                entity.Property(e => e.PrixVente).HasColumnName("Prix_vente");

                entity.Property(e => e.QteVendu).HasColumnName("Qte_Vendu");

                entity.Property(e => e.TauxImposition).HasColumnName("Taux_Imposition");

                entity.HasOne(d => d.Disponibilite)
                    .WithMany(p => p.VentProds)
                    .HasForeignKey(d => d.DisponibilitesId)
                    .HasConstraintName("FK_DisponibilitesVent_Prod");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.VentProds)
                    .HasForeignKey(d => d.ServicesId)
                    .HasConstraintName("FK_Vent_Prod_Vent_Prod");

                entity.HasOne(d => d.Vente)
                    .WithMany(p => p.VentProds)
                    .HasForeignKey(d => d.VentesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VentesVent_Prod");
            });

            modelBuilder.Entity<Vente>(entity =>
            {
                entity.HasIndex(e => e.ClientsId, "IX_FK_ClientsVentes");

                entity.HasIndex(e => e.NumeroCompteId, "IX_FK_NumeroCompteVentes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Aib).HasColumnName("AIB");

                entity.Property(e => e.Api).HasColumnName("API");

                entity.Property(e => e.CodeMecef).HasColumnName("Code_Mecef");

                entity.Property(e => e.CodeMecefAvoir).HasColumnName("Code_Mecef_Avoir");

                entity.Property(e => e.CompteurTotalMecef).HasColumnName("Compteur_Total_Mecef");

                entity.Property(e => e.CompteurTotalMecefAvoir).HasColumnName("Compteur_Total_Mecef_Avoir");

                entity.Property(e => e.CompteurTypeFactureMecef).HasColumnName("Compteur_Type_Facture_Mecef");

                entity.Property(e => e.CompteurTypeFactureMecefAvoir).HasColumnName("Compteur_Type_Facture_Mecef_Avoir");

                entity.Property(e => e.DateEcheance)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Echeance");

                entity.Property(e => e.DateEnr)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_enr");

                entity.Property(e => e.DateMecef).HasColumnName("Date_Mecef");

                entity.Property(e => e.DateMecefAvoir).HasColumnName("Date_Mecef_Avoir");

                entity.Property(e => e.DateValidation)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_validation");

                entity.Property(e => e.DateVent)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_vent");

                entity.Property(e => e.IdUserEnr).HasColumnName("Id_user_enr");

                entity.Property(e => e.IdValidateur).HasColumnName("Id_Validateur");

                entity.Property(e => e.LibelleFacture).HasColumnName("Libelle_Facture");

                entity.Property(e => e.ModePaiement).HasColumnName("Mode_Paiement");

                entity.Property(e => e.MontantTotal).HasColumnName("Montant_Total");

                entity.Property(e => e.NimMecef).HasColumnName("NIM_Mecef");

                entity.Property(e => e.NumDispAib).HasColumnName("Num_Disp_Aib");

                entity.Property(e => e.NumFact).HasColumnName("Num_Fact");

                entity.Property(e => e.QrcodeMecef).HasColumnName("QRCode_Mecef");

                entity.Property(e => e.QrcodeMecefAvoir).HasColumnName("QRCode_Mecef_Avoir");

                entity.Property(e => e.TauxRemise).HasColumnName("Taux_Remise");

                entity.Property(e => e.TypeFacture)
                    .IsRequired()
                    .HasColumnName("Type_Facture");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Ventes)
                    .HasForeignKey(d => d.ClientsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientsVentes");

                entity.HasOne(d => d.NumeroCompte)
                    .WithMany(p => p.Ventes)
                    .HasForeignKey(d => d.NumeroCompteId)
                    .HasConstraintName("FK_NumeroCompteVentes");
            });

            modelBuilder.Entity<Visibilite>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
