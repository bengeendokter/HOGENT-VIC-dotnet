using FluentValidation;

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
    }

    public class Detail : Index
    {
        public string? HostName { get; set; }
        public string? FQDN { get; set; }
        public string? Host { get; set; }
        public string? Poorten { get; set; }
        public Client? Client { get; set; }
        public bool IsHighlyAvailable { get; set; }
        public Template? Template { get; set; }
        public EBackupFrequency BackupFrequency { get; set; }
        public EDay Availability { get; set; }
        public EMode Mode { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Mutate : Detail
    {
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
                RuleFor(x => x.EndDate).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Template).NotEmpty().WithMessage("Dit veld is verplicht");
            }
        }
    }
}
