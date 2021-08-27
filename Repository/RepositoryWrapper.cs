using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IImageRepository _imageRepository;
        private IAuthenticationRepository _authenticationRepository;
        private IClientRepository _clientRepository;
        private ICategoryRepository _categoryRepository;
        private IProfilRepository _profilRepository;
        private IProduitRepository _produitRepository;
        private IServiceRepository _serviceRepository;
        private ISiteRepository _siteRepository;
        private IUtilisateurRepository _utilisateurRepository;
        private IVentProdRepository _ventProdRepository;
        private IVenteRepository _venteRepository;
        private IWebHostEnvironment _webHostEnvironment;



        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private RepositoryContext _repoContext;
        private UserManager<AppUser> _userManager;
        private string folderName;
        public string FolderName
        {
            set { folderName = value; }
        }

        public IImageRepository Image
        {
            get
            {
                if (_imageRepository == null)
                {
                    _imageRepository = new ImageRepository(_webHostEnvironment, folderName);
                }
                return _imageRepository;
            }
        }

        public IAuthenticationRepository Authentication
        {
            get
            {
                if (_authenticationRepository == null)
                {
                    _authenticationRepository = new AuthenticationRepository(_repoContext, _userManager, _configuration);
                }
                return _authenticationRepository;
            }
        }
        
        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_repoContext);
                }
                return _categoryRepository;
            }
        }
        
        public IClientRepository Client
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_repoContext);
                }
                return _clientRepository;
            }
        }

        public IProfilRepository Profil
        {
            get
            {
                if (_profilRepository == null)
                {
                    _profilRepository = new ProfilRepository(_repoContext);
                }
                return _profilRepository;
            }
        }
        
        public IProduitRepository Produit 
        {
            get
            {
                if (_produitRepository == null)
                {
                    _produitRepository = new ProduitRepository(_repoContext);
                }
                return _produitRepository;
            }
        }

        public ISiteRepository Site
        {
            get
            {
                if (_siteRepository == null)
                {
                    _siteRepository = new SiteRepository(_repoContext);
                }
                return _siteRepository;
            }
        }
        
        public IServiceRepository Service
        {
            get
            {
                if (_serviceRepository == null)
                {
                    _serviceRepository = new ServiceRepository(_repoContext);
                }
                return _serviceRepository;
            }
        }

        public IUtilisateurRepository Utilisateur
        {
            get
            {
                if (_utilisateurRepository == null)
                {
                    _utilisateurRepository = new UtilisateurRepository(_repoContext);
                }
                return _utilisateurRepository;
            }
        }

        public IVentProdRepository VentProd
        {
            get
            {
                if (_ventProdRepository == null)
                {
                    _ventProdRepository = new VentProdRepository(_repoContext);
                }
                return _ventProdRepository;
            }
        }

        public IVenteRepository Vente
        {
            get
            {
                if (_venteRepository == null)
                {
                    _venteRepository = new VenteRepository(_repoContext);
                }
                return _venteRepository;
            }
        }


        public RepositoryWrapper(UserManager<AppUser> userManager, RepositoryContext repositoryContext, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _repoContext = repositoryContext;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
