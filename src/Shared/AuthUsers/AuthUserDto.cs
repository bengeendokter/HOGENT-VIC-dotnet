using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthUsers;

public class AuthUserDto
{
    public class Index
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Blocked { get; set; }
    }

    public class Detail
    {
        public class General : Index { }
        public class UserRole
        {
            public string Id { get; set; }
            public string Role { get; set; }
        }
    }

    public class Mutate
    {
        public class General
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool Blocked { get; set; }

            public class Validator : AbstractValidator<General>
            {
                public Validator()
                {
                    RuleFor(x => x.Email)
                        .NotEmpty().WithMessage("Email mag niet leeg zijn.")
                        .EmailAddress().WithMessage("Geen geldig email.");
                    RuleFor(x => x.FirstName)
                        .NotEmpty().WithMessage("Voornaam mag niet leeg zijn")
                        .MaximumLength(50).WithMessage("Voornaam moet kleiner zijn dan 50 karakters.");
                    RuleFor(x => x.LastName)
                        .NotEmpty().WithMessage("Achternaam mag niet leeg zijn")
                        .MaximumLength(50).WithMessage("Achternaam moet kleiner zijn dan 50 karakters.");
                    RuleFor(x => x.Blocked)
                        .NotNull().WithMessage("Actief-status moet ingevuld zijn.");

                }
            }
        }

        public class Role
        {
            public string UserRoleId { get; set; }

            public class Validator : AbstractValidator<Role>
            {
                public Validator()
                {
                    RuleFor(x => x.UserRoleId)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("Rol moet gekozen zijn");
                }
            }

        }
    }
}
