using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.API.DTOS.AccountDTO
{
    public class LoginAccountCommandDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
