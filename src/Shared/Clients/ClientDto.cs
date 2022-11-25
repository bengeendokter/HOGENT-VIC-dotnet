using FluentValidation;

namespace Shared.Clients;

public static class ClientDto
{
    public class Index 
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public string? PhoneNumber { get; set; }
        public EClientType ClientType { get; set; }
        public string? ClientOrganisation { get; set; }
    }

    public class Detail : Index
    {
        public string? Email { get; set; }
        public string? BackupContact { get; set; }
        public string? Education { get; set; }
        public string? ExternalType { get; set; }
    }
    public class Mutate
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public EClientType ClientType { get; set; }
        public string? ClientOrganisation { get; set; }
        public string? Email { get; set; }
        public string? BackupContact { get; set; }
        public string? Education { get; set; }
        public string? ExternalType { get; set; }
    }

    public class Validator : AbstractValidator<Mutate>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Dit veld is verplicht");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Dit veld is verplicht");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Dit veld is verplicht");
            RuleFor(x => x.BackupContact).NotEmpty().WithMessage("Dit veld is verplicht");
            RuleFor(x => x.ClientType).IsInEnum().WithMessage("Dit veld is verplicht");
            RuleFor(x => x.ClientOrganisation).NotEmpty().WithMessage("Dit veld is verplicht");
            RuleFor(x => x.Education).NotEmpty()
                .When(x => x.ClientType == EClientType.Internal).WithMessage("Dit veld is verplicht");
            RuleFor(x => x.ExternalType).NotEmpty()
                .When(x => x.ClientType == EClientType.External).WithMessage("Dit veld is verplicht");
        }
    }

}

