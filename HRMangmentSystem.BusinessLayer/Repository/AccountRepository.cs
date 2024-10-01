using HRMangmentSystem.BusinessLayer.Helpers;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMangmentSystem.BusinessLayer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IGroupRepository _groupRepository;
        public AccountRepository(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IGroupRepository groupRepository
            )
        {
            _userManger = userManager;
            _roleManager = roleManager;
            _config = config;
            _groupRepository = groupRepository;
        }


        public async Task CreateAdminAsync(ApplicationUser user, string password)
        {
            IdentityResult result = await _userManger.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                await _userManger.AddToRoleAsync(user, UserRoles.Admin);

                if (user.GroupId != null)
                {
                    List<string> roles = await AddUserRoles(user.GroupId.Value);
                    foreach (var role in roles)
                    {
                        await _userManger.AddToRoleAsync(user, role);
                    }
                }
            }
        }
        public async Task<string> CreateLoginTokenAsync(ApplicationUser user)
        {
            var userClaims = new List<Claim>();
            userClaims.Add(new Claim("userID", user.Id));
            userClaims.Add(new Claim("userEmail", user.Email));
            userClaims.Add(new Claim("userName", user.UserName));
            userClaims.Add(new Claim("userFullName", user.FullName));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var role = await _userManger.GetRolesAsync(user);
            
            foreach (var r in role)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, r));
                userClaims.Add(new Claim("UserRole", r));

            }

            SecurityKey securityKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));


            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken HRMangmentToken = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],//url web api
                audience: _config["JWT:ValidAudiance"],//url consumer angular
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            // Save the token to the AspNetUserTokens table
            await _userManger.SetAuthenticationTokenAsync(await _userManger.FindByEmailAsync(user.Email), "JWT", "AccessToken", new JwtSecurityTokenHandler().WriteToken(HRMangmentToken));
            return new JwtSecurityTokenHandler().WriteToken(HRMangmentToken);
        }

        public async Task<List<string>> AddUserRoles(int groupId)
        {
            List<string> roles = new List<string>();
            Group group = await _groupRepository.GetGroupById(groupId);
            if (group is not null)
            {
                var groupPermissions = group.Permissions;
                foreach (var permission in groupPermissions)
                {
                    var p = PermissionGenerator.GeneratePermissions(permission.Name, permission.Create??false, permission.Read ?? false, permission.Update ?? false, permission.Delete ?? false);
                    foreach (var perm in p)
                    {
                        if (!await _roleManager.RoleExistsAsync(perm))
                            await _roleManager.CreateAsync(new IdentityRole(perm));
                        roles.Add(perm);
                    }
                }
            }
            return roles;
        }
    }
}
