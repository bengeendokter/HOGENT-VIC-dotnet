using FluentValidation;
using Shared.Clients;

namespace Shared.VirtualMachines;

public static class VirtualMachineDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsHighlyAvailable { get; set; }
        public ETemplate? Template { get; set; }
        public EBackupFrequency BackupFrequency { get; set; }
        public ClientDto.Index? Client { get; set; }
    }

    public class Detail : Index
    {
        public string? HostName { get; set; }
        public string? FQDN { get; set; }
        public string? Host { get; set; }
        public string? Poorten { get; set; }
        public EDay Availability { get; set; }
        public ESoftware Software { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Mutate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsHighlyAvailable { get; set; }
        public ETemplate? Template { get; set; }
        public EBackupFrequency BackupFrequency { get; set; }
        public string? HostName { get; set; }
        public string? FQDN { get; set; }
        public string? Host { get; set; }
        public string? Poorten { get; set; }
        public EDay Availability { get; set; }
        public ESoftware Software { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClientId { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Name).MinimumLength(3).WithMessage("De naam moet langer dan 3 karakters zijn");
                RuleFor(x => x.Name).MaximumLength(30).WithMessage("De naam is te lang");
                RuleFor(x => x.HostName).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.HostName).MinimumLength(3).WithMessage("De hostname moet langer dan 3 karakters zijn");
                RuleFor(x => x.Host).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Poorten).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.StartDate).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.StartDate).GreaterThan(DateTime.Now).WithMessage("Startdatum moet in de toekomst liggen");
                RuleFor(x => x.EndDate).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("Einddatum mag niet voor startdatum liggen");
                RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("Startdatum moet in de toekomst liggen");
                RuleFor(x => x.FQDN).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Template).NotNull().WithMessage("Kies een template of maak een nieuwe aan");
                RuleFor(x => x.ClientId).Must(x => x >= 0).NotNull().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Software).IsInEnum().WithMessage("Ongeldig software");

            }
        }
    }
}
