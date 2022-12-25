using FluentValidation;

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
        public class General
        {
            public string Id { get; set; }
            public bool Blocked { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ScreenName { get; set; }
        }
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
            public bool Blocked { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ScreenName { get; set; }

            public class Secret
            {
                public string Password { get; set; }
            }

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
            public bool IsUser { get; set; }
            public bool IsCustomer { get; set; }
            public bool IsModerator { get; set; }
            public bool IsAdministrator { get; set; }

        }
    }
}
