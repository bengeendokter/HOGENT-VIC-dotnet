using FluentValidation;

namespace Shared.Clients;

public static class UserDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public string? Voornaam { get; set; }
        public ERole Role { get; set; }
        public bool IsActief { get; set; }
        public string? Email { get; set; }
    }

    public class Detail : Index { }

    public class Mutate 
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public ERole Role { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Surname).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Role).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.IsActive).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Dit veld is verplicht");
            }
        }
    }

}

