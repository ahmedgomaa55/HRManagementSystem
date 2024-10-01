using AutoMapper;
using HRMangmentSystem.API.DTOS.AccountDTO;
using HRMangmentSystem.API.ResponseBase;
using HRMangmentSystem.BusinessLayer.Helpers;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly ResponseHandler _responseHandler;
        #endregion
        #region Ctor
        public AccountController(
            IAccountRepository accountRepository,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManger,
            ResponseHandler responseHandler
        )
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManger = userManger;
            _responseHandler = responseHandler;
        }

        #endregion
        [HttpPost("CreateAdmin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdminAsync(AccountCommandDTO user)
        {
            Response<string> response;
            if (await _userManger.FindByEmailAsync(user.Email) != null)
            {
                response = _responseHandler.BadRequest<string>("Email Already Exists");
                return BadRequest(response);
            }
            if (await _userManger.FindByNameAsync(user.Username) is not null)
            {
                response = _responseHandler.BadRequest<string>("Username Already Exists");
                return BadRequest(response);
            }
            var mappedUser = _mapper.Map<AccountCommandDTO, ApplicationUser>(user);
            await _accountRepository.CreateAdminAsync(mappedUser, user.Password);
            response = _responseHandler.Success<string>("Admin Created Successfully");
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAccountCommandDTO loginData)
        {
            ApplicationUser user;
            if (ModelState.IsValid)
            {
                user = await _userManger.FindByEmailAsync(loginData.UsernameOrEmail) ??
                        await _userManger.FindByNameAsync(loginData.UsernameOrEmail);
                if (user is null)
                {
                    Response<string> response = _responseHandler.NotFound<string>("User Not Found");
                    return NotFound(response);
                }
                else
                {
                    bool rightPw = await _userManger.CheckPasswordAsync(user, loginData.Password);
                    if (!rightPw)
                    {
                        Response<string> response = _responseHandler.BadRequest<string>("Wrong Password");
                        return BadRequest(response);
                    }
                    else
                    {
                        var token = await _accountRepository.CreateLoginTokenAsync(user);
                        Response<string> response = _responseHandler.Success<string>(token);
                        return Ok(response);
                    }

                }

            }
            return BadRequest();
        }
    }
}
