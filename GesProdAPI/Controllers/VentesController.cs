using AutoMapper;
using Contracts;
using Entities.DataTransfertObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Contracts.IVentProdRepository;
using static Entities.Models.VenteParameters;

namespace GesProdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VentesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly string _baseURL;

        public VentesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _baseURL = string.Concat(httpContextAccessor.HttpContext.Request.Scheme, "://", httpContextAccessor.HttpContext.Request.Host);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllVentes([FromQuery] VenteParameters venteParameters)
        {
            
            var ventes = await _repository.Vente.GetAllVentesAsync(venteParameters);

            var metadata = new
            {
                ventes.TotalCount,
                ventes.PageSize,
                ventes.CurrentPage,
                ventes.TotalPages,
                ventes.HasNext,
                ventes.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all ventesDisponible ventesDisponible from database.");

            var ventesReadDto = _mapper.Map<IEnumerable<VenteReadDto>>(ventes);

            ventesReadDto.ToList().ForEach(venteReadDto =>
            {
                venteReadDto.VentProds.ToList().ForEach( ventProd =>
                {
                    if (ventProd.Service != null && !string.IsNullOrWhiteSpace(ventProd.Service.Photo)) ventProd.Service.Photo = $"{_baseURL}{ventProd.Service.Photo.Replace("~", "")}";
                });
            });

            return Ok(ventesReadDto);
        }
        


        [HttpGet("current-client")]
        public async Task<IActionResult> GetLoggIngCustomerVentes([FromQuery] PaginationParameters paginationParameters)
        {
            var venteParameters = new VenteParameters
            {
                PageNumber = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize,
                IdUserEnr = null,
                ClientsId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            };

            var ventes = await _repository.Vente.GetAllVentesAsync(venteParameters);

            var metadata = new
            {
                ventes.TotalCount,
                ventes.PageSize,
                ventes.CurrentPage,
                ventes.TotalPages,
                ventes.HasNext,
                ventes.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all ventesDisponible ventesDisponible from database.");

            var ventesReadDto = _mapper.Map<IEnumerable<VenteReadDto>>(ventes);

            ventesReadDto.ToList().ForEach(venteReadDto =>
            {
                venteReadDto.VentProds.ToList().ForEach( ventProd =>
                {
                    if (ventProd.Service != null && !string.IsNullOrWhiteSpace(ventProd.Service.Photo)) ventProd.Service.Photo = $"{_baseURL}{ventProd.Service.Photo.Replace("~", "")}";
                });
            });

            return Ok(ventesReadDto);
        }
        


        [HttpGet("utilisateurs")]
        public async Task<IActionResult> GetUserVentes([FromQuery] PaginationParameters paginationParameters)
        {
            var venteParameters = new VenteParameters
            {
                PageNumber = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize,
                ClientsId = null,
                IdUserEnr = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            };

            var ventes = await _repository.Vente.GetAllVentesAsync(venteParameters);

            var metadata = new
            {
                ventes.TotalCount,
                ventes.PageSize,
                ventes.CurrentPage,
                ventes.TotalPages,
                ventes.HasNext,
                ventes.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all ventesDisponible ventesDisponible from database.");

            var ventesReadDto = _mapper.Map<IEnumerable<VenteReadDto>>(ventes);

            ventesReadDto.ToList().ForEach(venteReadDto =>
            {
                venteReadDto.VentProds.ToList().ForEach( ventProd =>
                {
                    if (ventProd.Service != null && !string.IsNullOrWhiteSpace(ventProd.Service.Photo)) ventProd.Service.Photo = $"{_baseURL}{ventProd.Service.Photo.Replace("~", "")}";
                });
            });

            return Ok(ventesReadDto);
        }



        [HttpGet("{id}", Name = "VenteById")]
        public async Task<IActionResult> GetVenteById(Guid id)
        {
            var vente = await _repository.Vente.GetVenteByIdAsync(id);

            if (vente == null)
            {
                _logger.LogError($"Vente with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned venteUpdateDto with id: {id}");

                var venteReadDto = _mapper.Map<VenteReadDto>(vente);

                venteReadDto.VentProds.ToList().ForEach(ventProd =>
                {
                    if (!string.IsNullOrWhiteSpace(ventProd.Service.Photo)) ventProd.Service.Photo = $"{_baseURL}{ventProd.Service.Photo.Replace("~", "")}";
                });

                return Ok(venteReadDto);
            }
        }





        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/declared")]
        public async Task<IActionResult> GetTurnoverTheDeclaredOnes(DateTime debutPeriode, DateTime finPeriode)
        {
            _logger.LogInfo($"Returned declared Turnover from {debutPeriode} to {finPeriode}.");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, Target.TheDeclaredOnes));
        }
        




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/non-declared")]
        public async Task<IActionResult> GetTurnoverTheNonDeclaredOnes(DateTime debutPeriode, DateTime finPeriode)
        {
            _logger.LogInfo($"Returned non-declared Turnover from {debutPeriode} to {finPeriode}.");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, Target.TheNonDeclaredOnes));
        }





        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/categorie/{categoryId}/declared")]
        public async Task<IActionResult> GetTurnoverCategoryTheDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? categoryId)
        {
            _logger.LogInfo($"Returning Category with id {categoryId}.");
            var category = await _repository.Category.GetCategoryByIdAsync(categoryId.Value);

            if (category == null)
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found.");
                return NotFound($"Category with id: {categoryId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned declared Turnover from {debutPeriode} to {finPeriode} having categoryId equal to '{categoryId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, category, Target.TheDeclaredOnes));
        }
        




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/categorie/{categoryId}/non-declared")]
        public async Task<IActionResult> GetTurnoverCategoryTheNonDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? categoryId)
        {
            _logger.LogInfo($"Returning Category with id {categoryId}.");
            var category = await _repository.Category.GetCategoryByIdAsync(categoryId.Value);

            if (category == null)
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found.");
                return NotFound($"Category with id: {categoryId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned non-declared Turnover from {debutPeriode} to {finPeriode} having categoryId equal to '{categoryId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, category, Target.TheNonDeclaredOnes));
        }





        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/produit/{produitId}/declared")]
        public async Task<IActionResult> GetTurnoverProduitTheDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? produitId)
        {
            _logger.LogInfo($"Returning Produit with id {produitId}.");
            var produit = await _repository.Produit.GetProduitByIdAsync(produitId.Value);

            if (produit == null)
            {
                _logger.LogError($"Produit with id: {produitId}, hasn't been found.");
                return NotFound($"Produit with id: {produitId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned declared Turnover from {debutPeriode} to {finPeriode} having produitId equal to '{produitId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, produit, Target.TheDeclaredOnes));
        }
        




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/produit/{produitId}/non-declared")]
        public async Task<IActionResult> GetTurnoverProduitTheNonDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? produitId)
        {
            _logger.LogInfo($"Returning Produit with id {produitId}.");
            var produit = await _repository.Produit.GetProduitByIdAsync(produitId.Value);

            if (produit == null)
            {
                _logger.LogError($"Produit with id: {produitId}, hasn't been found.");
                return NotFound($"Produit with id: {produitId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned non-declared Turnover from {debutPeriode} to {finPeriode} having produitId equal to '{produitId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, produit, Target.TheNonDeclaredOnes));
        }
        




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/service/{serviceId}/declared")]
        public async Task<IActionResult> GetTurnoverServiceTheDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? serviceId)
        {
            _logger.LogInfo($"Returning Service with id {serviceId}.");
            var service = await _repository.Service.GetServiceByIdAsync(serviceId.Value);

            if (service == null)
            {
                _logger.LogError($"Service with id: {serviceId}, hasn't been found.");
                return NotFound($"Service with id: {serviceId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned declared Turnover from {debutPeriode} to {finPeriode} having serviceId equal to '{serviceId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, service, Target.TheDeclaredOnes));
        }
        




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/service/{serviceId}/non-declared")]
        public async Task<IActionResult> GetTurnoverServiceTheNonDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? serviceId)
        {
            _logger.LogInfo($"Returning Service with id {serviceId}.");
            var service = await _repository.Service.GetServiceByIdAsync(serviceId.Value);

            if (service == null)
            {
                _logger.LogError($"Service with id: {serviceId}, hasn't been found.");
                return NotFound($"Service with id: {serviceId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned non-declared Turnover from {debutPeriode} to {finPeriode} having serviceId equal to '{serviceId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, service, Target.TheNonDeclaredOnes));
        }




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/utilisateur/{utilisateurId}/declared")]
        public async Task<IActionResult> GetTurnoverUtilisateurTheDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? utilisateurId)
        {
            _logger.LogInfo($"Returning Utilisateur with id {utilisateurId}.");
            var utilisateur = await _repository.Utilisateur.GetUtilisateurByIdAsync(utilisateurId.Value);

            if (utilisateur == null)
            {
                _logger.LogError($"Utilisateur with id: {utilisateurId}, hasn't been found.");
                return NotFound($"Utilisateur with id: {utilisateurId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned declared Turnover from {debutPeriode} to {finPeriode} having utilisateurId equal to '{utilisateurId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, utilisateur.Id, Target.TheDeclaredOnes));
        }
        



        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/utilisateur/{utilisateurId}/non-declared")]
        public async Task<IActionResult> GetTurnoverUtilisateurTheNonDeclaredOnes(DateTime debutPeriode, DateTime finPeriode, Guid? utilisateurId)
        {
            _logger.LogInfo($"Returning Utilisateur with id {utilisateurId}.");
            var utilisateur = await _repository.Utilisateur.GetUtilisateurByIdAsync(utilisateurId.Value);

            if (utilisateur == null)
            {
                _logger.LogError($"Utilisateur with id: {utilisateurId}, hasn't been found.");
                return NotFound($"Utilisateur with id: {utilisateurId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned non-declared Turnover from {debutPeriode} to {finPeriode} having utilisateurId equal to '{utilisateurId}'");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, utilisateur.Id, Target.TheNonDeclaredOnes));
        }





        [HttpPost]
        public async Task<IActionResult> CreateVente([FromBody] VenteCreateDto vente)
        {
            if (vente == null)
            {
                _logger.LogError("Vente object sent from vente is null.");
                return BadRequest("Vente object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid venteUpdateDto object sent from vente.");
                return BadRequest("Invalid model object");
            }

            if (vente.ClientsId == null) vente.ClientsId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var venteEntity = _mapper.Map<Vente>(vente);

            var services = await _repository.Service.GetAllServicesAsync();

            venteEntity.Id = Guid.NewGuid();
            venteEntity.NumFact = await _repository.Vente.GetNextNumberAsync();
            venteEntity.DateEnr = DateTime.Now;
            venteEntity.TypeFacture = "FV";
            venteEntity.ModePaiement = "A";
            venteEntity.IdUserEnr = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            venteEntity.VentProds.ToList().ForEach(vendProd =>
            {
                vendProd.Id = Guid.NewGuid();
                vendProd.PrixVente = services.First(x=>x.Id == vendProd.ServicesId.Value).PrixVente;
                vendProd.TauxImposition = services.First(x=>x.Id == vendProd.ServicesId.Value).TauxImposition;
            });

            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);

            await _repository.Vente.CreateVenteAsync(venteEntity);
            await _repository.SaveAsync();

            var venteReadDto = _mapper.Map<VenteReadDto>(venteEntity);
            return CreatedAtRoute("VenteById", new { id = venteReadDto.Id }, venteReadDto);
        }
        


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVente(Guid id, [FromBody] VenteUpdateDto venteUpdateDto)
        {
            if (venteUpdateDto == null)
            {
                _logger.LogError("Vente object sent from vente is null.");
                return BadRequest("Vente object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid venteUpdateDto object sent from vente.");
                return BadRequest("Invalid model object");
            }

            var venteEntity = await _repository.Vente.GetVenteByIdAsync(id);
            if (venteEntity == null)
            {
                _logger.LogError($"Vente with id: {id}, hasn't been found.");
                return NotFound($"Vente with id: {id}, hasn't been found.");
            }

            _mapper.Map(venteUpdateDto, venteEntity);
            var services = await _repository.Service.GetAllServicesAsync();


            venteEntity.DateEnr = DateTime.Now;
            //venteEntity.TypeFacture = "FV";
            //venteEntity.ModePaiement = "A";
            //venteEntity.ClientsId = venteUpdateDto.ClientsId;
            venteEntity.IdUserEnr = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            venteEntity.VentProds.ToList().ForEach(vendProd =>
            {
                if(vendProd.Id == Guid.Empty) vendProd.Id = Guid.NewGuid();
                vendProd.PrixVente = services.First(x => x.Id == vendProd.ServicesId.Value).PrixVente;
                vendProd.TauxImposition = services.First(x => x.Id == vendProd.ServicesId.Value).TauxImposition;
                vendProd.VentesId = venteEntity.Id;
            });



            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);

            //await _repository.Vente.UpdateVenteAsync(venteEntity);
            //await _repository.VentProd.UpdateVentProdAsync(venteEntity.VentProds);
            await _repository.SaveAsync();

            var venteReadDto = _mapper.Map<VenteReadDto>(venteEntity);

            return Ok(venteReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVente(Guid id)
        {
            var vente = await _repository.Vente.GetVenteByIdAsync(id);

            if (vente == null)
            {
                _logger.LogError($"Vente with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Vente.DeleteVenteAsync(vente);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
