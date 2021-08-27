using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCustomer = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom_Entreprise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom_Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id_user_enr = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Historique",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historique", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumeroCompte",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banque = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroCompte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num_Port = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mecef = table.Column<bool>(type: "bit", nullable: false),
                    eMecef = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profils",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profil = table.Column<bool>(type: "bit", nullable: false),
                    Produit = table.Column<bool>(type: "bit", nullable: false),
                    Categorie = table.Column<bool>(type: "bit", nullable: false),
                    Site = table.Column<bool>(type: "bit", nullable: false),
                    Utilisateur = table.Column<bool>(type: "bit", nullable: false),
                    Visibilite = table.Column<bool>(type: "bit", nullable: false),
                    Appro_produit = table.Column<bool>(type: "bit", nullable: false),
                    Appro_site = table.Column<bool>(type: "bit", nullable: false),
                    Vente = table.Column<bool>(type: "bit", nullable: false),
                    Stock_site = table.Column<bool>(type: "bit", nullable: false),
                    Autre_sortie = table.Column<bool>(type: "bit", nullable: false),
                    Consulter_stock_alerte = table.Column<bool>(type: "bit", nullable: false),
                    Valider_Facture = table.Column<bool>(type: "bit", nullable: false),
                    Normaliser_Facture = table.Column<bool>(type: "bit", nullable: false),
                    Numero_compte = table.Column<bool>(type: "bit", nullable: false),
                    Inventaire_Produit = table.Column<bool>(type: "bit", nullable: false),
                    Situation_Journaliere = table.Column<bool>(type: "bit", nullable: false),
                    Modifier_Facture = table.Column<bool>(type: "bit", nullable: false),
                    Historique = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visibilites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visibilites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<int>(type: "int", nullable: false),
                    Prix_Vente = table.Column<int>(type: "int", nullable: false),
                    Qte_Stk = table.Column<int>(type: "int", nullable: false),
                    Seuil_alerte = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taux_Imposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Libelle_Taxe_Specifique = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mnt_Taxe_Specifique = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriesProduits",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix_Vente = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taux_Imposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mnt_Taxe_Specifique = table.Column<int>(type: "int", nullable: false),
                    Libelle_Taxe_specifique = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vent_Prod_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Categories",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Approvisionnements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_appr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id_user_enr = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FournisseursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvisionnements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FournisseursApprovisionnements",
                        column: x => x.FournisseursId,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ventes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_vent = table.Column<DateTime>(type: "datetime", nullable: false),
                    Num_Fact = table.Column<long>(type: "bigint", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Taux_Remise = table.Column<double>(type: "float", nullable: false),
                    Type_Facture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mode_Paiement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AIB = table.Column<int>(type: "int", nullable: true),
                    Compteur_Total_Mecef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compteur_Type_Facture_Mecef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Montant_Total = table.Column<int>(type: "int", nullable: true),
                    NIM_Mecef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code_Mecef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_user_enr = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_Mecef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRCode_Mecef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_Validateur = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date_validation = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumeroCompteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date_Echeance = table.Column<DateTime>(type: "datetime", nullable: true),
                    Num_Disp_Aib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_Facture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRCode_Mecef_Avoir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code_Mecef_Avoir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compteur_Total_Mecef_Avoir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compteur_Type_Facture_Mecef_Avoir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Mecef_Avoir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    API = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientsVentes",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NumeroCompteVentes",
                        column: x => x.NumeroCompteId,
                        principalTable: "NumeroCompte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    New_Connexion = table.Column<bool>(type: "bit", nullable: false),
                    ProfilsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SitesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilsUtilisateurs",
                        column: x => x.ProfilsId,
                        principalTable: "Profils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SitesUtilisateurs",
                        column: x => x.SitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disponibilites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Disponible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: true),
                    Seuil_alerte = table.Column<int>(type: "int", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id_user_enr = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SitesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProduitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduitsDisponibilites",
                        column: x => x.ProduitsId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SitesDisponibilites",
                        column: x => x.SitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appro_Produit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qte_app = table.Column<int>(type: "int", nullable: false),
                    ApprovisionnementsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id_user_enr = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProduitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appro_Produit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovisionnementsAppro_Produit",
                        column: x => x.ApprovisionnementsId,
                        principalTable: "Approvisionnements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduitsAppro_Produit",
                        column: x => x.ProduitsId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appro_Site",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_app = table.Column<DateTime>(type: "datetime", nullable: false),
                    Qte_App = table.Column<int>(type: "int", nullable: false),
                    DisponibilitesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_user_enr = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_enr = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appro_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisponibilitesAppro_Site",
                        column: x => x.DisponibilitesId,
                        principalTable: "Disponibilites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vent_Prod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Localisation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qte_Vendu = table.Column<int>(type: "int", nullable: false),
                    Prix_vente = table.Column<int>(type: "int", nullable: false),
                    Mnt_Remise = table.Column<int>(type: "int", nullable: false),
                    VentesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisponibilitesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taux_Imposition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vent_Prod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisponibilitesVent_Prod",
                        column: x => x.DisponibilitesId,
                        principalTable: "Disponibilites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vent_Prod_Vent_Prod",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentesVent_Prod",
                        column: x => x.VentesId,
                        principalTable: "Ventes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FK_ApprovisionnementsAppro_Produit",
                table: "Appro_Produit",
                column: "ApprovisionnementsId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ProduitsAppro_Produit",
                table: "Appro_Produit",
                column: "ProduitsId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_DisponibilitesAppro_Site",
                table: "Appro_Site",
                column: "DisponibilitesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_FournisseursApprovisionnements",
                table: "Approvisionnements",
                column: "FournisseursId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ProduitsDisponibilites",
                table: "Disponibilites",
                column: "ProduitsId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_SitesDisponibilites",
                table: "Disponibilites",
                column: "SitesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_CategoriesProduits",
                table: "Produits",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_Services_Categories",
                table: "Services",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ProfilsUtilisateurs",
                table: "Utilisateurs",
                column: "ProfilsId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_SitesUtilisateurs",
                table: "Utilisateurs",
                column: "SitesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_DisponibilitesVent_Prod",
                table: "Vent_Prod",
                column: "DisponibilitesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_Vent_Prod_Vent_Prod",
                table: "Vent_Prod",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_VentesVent_Prod",
                table: "Vent_Prod",
                column: "VentesId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ClientsVentes",
                table: "Ventes",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_NumeroCompteVentes",
                table: "Ventes",
                column: "NumeroCompteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appro_Produit");

            migrationBuilder.DropTable(
                name: "Appro_Site");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Historique");

            migrationBuilder.DropTable(
                name: "Parametre");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Vent_Prod");

            migrationBuilder.DropTable(
                name: "Visibilites");

            migrationBuilder.DropTable(
                name: "Approvisionnements");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Profils");

            migrationBuilder.DropTable(
                name: "Disponibilites");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Ventes");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "NumeroCompte");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
