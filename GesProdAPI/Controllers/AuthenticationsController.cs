using AutoMapper;
using Contracts;
using Entities.DataTransfertObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace GesProdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;




        public AuthenticationsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }




        //GET api/authentications/count
        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<int> GetAllUsersCount()
        {
            _logger.LogInfo($"Count all users of database.");
            return (await _repository.Authentication.CountAllUsersAsync());
        }



        //POST api/authenticationss/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var loginRequest = _mapper.Map<LoginRequest>(loginRequestDto);
                var authenticationResponse = await _repository.Authentication.LoginWithUserNameAsync(loginRequest, loginRequestDto.Password);

                if (authenticationResponse.IsSuccess)
                {
                    _logger.LogInfo($"Utilisateur Named: {authenticationResponse.UserInfo["Name"]} has logged in successfully");
                    
                    var authenticationResponseReadDto = _mapper.Map<AuthenticationResponseReadDto>(authenticationResponse);

                    return Ok(authenticationResponseReadDto);
                }

                //ModelState.AddModelError("login failure", loginResponse.Message);
                return NotFound(authenticationResponse.Message);
            }

            return ValidationProblem(ModelState);
        }




        //POST api/authenticationss/customer/registration
        [HttpPost("customer/registration")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthCustomerReadDto>> RegisterCustomer([FromBody] CustomerRegistrationWriteDto customerRegistrationDto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            if (await GetAllUsersCount() < 1)
            {
                ModelState.AddModelError("", "create a user account first");
                return ValidationProblem(ModelState);
            }

            var userRegistration = _mapper.Map<AppUser>(customerRegistrationDto);
            userRegistration.IsCustomer = true;
            var result = await _repository.Authentication.RegisterUserAsync(userRegistration, customerRegistrationDto.Password);

            if (result.IsSuccess)
            {
                var userReadDto = _mapper.Map<AuthCustomerReadDto>(userRegistration);

                var clientEntity = _mapper.Map<Client>(userReadDto);

                await _repository.Client.CreateClientAsync(clientEntity);

                await _repository.SaveAsync();

                return Ok(userReadDto);
            }
            else
            {
                foreach (var error in result.ErrorDetails)
                {
                    ModelState.AddModelError(error, error);
                }
                return ValidationProblem(ModelState);
            }
        }




        //POST api/authenticationss/user/registration
        [HttpPost("user/registration")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthUserReadDto>> RegisterUser([FromBody] UserRegistrationWriteDto userRegistrationDto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);


            string enterprisename = "AmStrong Stéphane";

            string name = enterprisename.Split(" ")[0];
            string firstname = enterprisename.Split(" ")[1];

            var userRegistration = _mapper.Map<AppUser>(userRegistrationDto);
            var result = await _repository.Authentication.RegisterUserAsync(userRegistration, userRegistrationDto.Password);

            if (result.IsSuccess)
            {
                var userReadDto = _mapper.Map<AuthUserReadDto>(userRegistration);

                var utilisateurEntity = _mapper.Map<Utilisateur>(userReadDto);

                utilisateurEntity.Nom = userRegistrationDto.Name;
                utilisateurEntity.Prenom = userRegistrationDto.Firstname;
                utilisateurEntity.Login = userRegistrationDto.Email;
                utilisateurEntity.Pwd = userRegistrationDto.Password;

                await _repository.Utilisateur.CreateUtilisateurAsync(utilisateurEntity);

                await _repository.SaveAsync();

                return Ok(userReadDto);
            }
            else
            {
                foreach (var error in result.ErrorDetails)
                {
                    ModelState.AddModelError(error, error);
                }
                return ValidationProblem(ModelState);
            }
        }
    }
}
