using FluentValidation;
using PathCase.Models.Authentication;

namespace PathCase.MVC.Validations
{
    public class LoginModelValidation : AbstractValidator<LoginRequestModel>
    {
        public LoginModelValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().Length(3, 15);
        }
    }
}