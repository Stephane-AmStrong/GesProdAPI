using AutoMapper;
using Entities.DataTransfertObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GesProdAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Source -> Target
            //CreateMap<AppUser, AppUserReadDto>()
            //    .ForMember(dest => dest.Email, src => src.MapFrom(src => src.UserName))
            //    .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name));
            //CreateMap<AppUserReadDto, AppUser>()
            //    .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name));
            //CreateMap<AppUserWriteDto, AppUser>()
            //    .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name));

            CreateMap<CustomerRegistrationWriteDto, AppUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email));
            
            CreateMap<LoginRequestDto, AppUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email));
            
            CreateMap<LoginRequestDto, LoginRequest>();
            CreateMap<AuthenticationResponse, AuthenticationResponseReadDto>();
            //

            CreateMap<ApproProduit, ApproProduitReadDto>();
            CreateMap<ApproProduitWriteDto, ApproProduit>();

            CreateMap<ApproSite, ApproSiteReadDto>();
            CreateMap<ApproSiteWriteDto, ApproSite>();

            CreateMap<Approvisionnement, ApprovisionnementReadDto>();
            CreateMap<ApprovisionnementWriteDto, Approvisionnement>();

            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryWriteDto, Category>();
            CreateMap<CategoryReadDto, Category>();

            CreateMap<Client, ClientReadDto>();
            CreateMap<AuthCustomerReadDto, Client>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomEntreprise, src => src.MapFrom(src => src.Name))
                .ForMember(dest => dest.Tel, src => src.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Ifu, src => src.MapFrom(src => src.IFU))
                .ForMember(dest => dest.Addresse, src => src.MapFrom(src => src.Address));

            CreateMap<AppUser, AuthCustomerReadDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.IFU, src => src.MapFrom(src => src.IFU))
                .ForMember(dest => dest.Address, src => src.MapFrom(src => src.Address));


            CreateMap<Client, ClientReadDto>();
            CreateMap<ClientWriteDto, Client>();
            
            CreateMap<Disponibilite, DisponibiliteReadDto>();
            CreateMap<DisponibiliteWriteDto, Disponibilite>();
            
            CreateMap<Fournisseur, FournisseurReadDto>();
            CreateMap<FournisseurWriteDto, Fournisseur>();
            
            CreateMap<Historique, HistoriqueReadDto>();
            CreateMap<HistoriqueWriteDto, Historique>();
            
            CreateMap<NumeroCompte, NumeroCompteReadDto>();
            CreateMap<NumeroCompteWriteDto, NumeroCompte>();
            
            CreateMap<Produit, ProduitReadDto>();
            CreateMap<ProduitWriteDto, Produit>();
            
            CreateMap<Profil, ProfilReadDto>();
            CreateMap<ProfilWriteDto, Profil>();
            
            CreateMap<Service, ServiceReadDto>();
            CreateMap<ServiceWriteDto, Service>();
            CreateMap<ServiceReadDto, Service>();
            
            CreateMap<Site, SiteReadDto>();
            CreateMap<SiteWriteDto, Site>();

            CreateMap<UserRegistrationWriteDto, AppUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name + " " + src.Firstname));

            CreateMap<AppUser, AuthUserReadDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name))
                /*.ForMember(dest => dest.Firstname, src => src.MapFrom(src => src.Name))*/;

            CreateMap<AuthUserReadDto, Utilisateur>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nom, src => src.MapFrom(src => src.Name));


            CreateMap<Utilisateur, UtilisateurReadDto>();

            CreateMap<UtilisateurWriteDto, Utilisateur>();
            CreateMap<AuthUserReadDto, Utilisateur>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nom, src => src.MapFrom(src => src.Name));


            CreateMap<VentProd, VentProdReadDto>();
            CreateMap<VentProdCreateDto, VentProd>();
            CreateMap<VentProdUpdateDto, VentProd>();
            
            CreateMap<Vente, VenteReadDto>();
            CreateMap<VenteUpdateDto, Vente>();
            CreateMap<VenteCreateDto, Vente>();
            
            CreateMap<Visibilite, VisibiliteReadDto>();
            CreateMap<VisibiliteWriteDto, Visibilite>();
        }
    }
}
