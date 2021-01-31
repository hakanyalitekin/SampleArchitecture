using FluentValidation;
using SampleArchitecture.Api.Models;

namespace SampleArchitecture.Api.Validations
{
    //FluentValidation ile beraber gelen AbstractValidator'ı implemente ediyoruz ve hangi tip ile çalışacağını belirtiyoruz.
    public class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Id).LessThan(100).WithMessage("Id 100 den küçük olmalı");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id 0 dan büyük olmalı");

            RuleFor(x => x.FullName).NotEmpty().WithMessage("İsim  boş geçilemez.");
        }
    }
}
