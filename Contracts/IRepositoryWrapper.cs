using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IImageRepository Image { get; }
        IAuthenticationRepository Authentication { get; }
        ICategoryRepository Category { get; }
        IClientRepository Client { get; }
        IProduitRepository Produit { get; }
        IProfilRepository Profil { get; }
        IServiceRepository Service { get; }
        ISiteRepository Site { get; }
        IUtilisateurRepository Utilisateur { get; }
        IVentProdRepository VentProd { get; }
        IVenteRepository Vente { get; }
        string FolderName { set; }

        Task SaveAsync();
    }
}
