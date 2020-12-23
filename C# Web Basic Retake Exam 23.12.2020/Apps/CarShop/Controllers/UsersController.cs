using System.ComponentModel.DataAnnotations;
using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this._usersService.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Error("Invalid username or password.");
            }

            this.SignIn(userId);
            return this.Redirect("/Cars/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < 4 || input.Username.Length > 20)
            {
                return this.Error("Username should be between 4 and 20 character long.");
            }

            if (string.IsNullOrEmpty(input.Email))
            {
                return this.Error("Invalid email.");
            }

            if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 5 || input.Password.Length > 20)
            {
                return this.Error("Password is required and should be between 5 and 20 characters.");
            }

            if (input.ConfirmPassword != input.Password)
            {
                return this.Error("Passwords do not match.");
            }

            if (!this._usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken.");
            }

            if (!this._usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken.");
            }

            this._usersService.Create(input.Username, input.Email, input.Password, input.UserType);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
