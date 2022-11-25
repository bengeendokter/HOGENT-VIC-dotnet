using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachines;

public static class VirtualMachineRequestDto
{
    public class Detail
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? ProjectName { get; set; }
        public Client? Client { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public ERequestStatus Status { get; set; }
    }
    public class Create
    {
        public string? ProjectName { get; set; }
        public Client? Client { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(x => x.ProjectName).MinimumLength(3).WithMessage("De projectnaam moet minstens 3 tekens bevatten");
                RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.StartDate).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.StartDate).GreaterThan(DateTime.Now).WithMessage("Startdatum moet in de toekomst liggen");
                RuleFor(x => x.EndDate).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("Startdatum moet in de toekomst liggen");
                RuleFor(x => x.Reason).NotEmpty().WithMessage("Dit veld is verplicht");
                RuleFor(x => x.Reason).MinimumLength(10).WithMessage("Geef voldoende info bij het aanvragen van de VM");
            }
        }
    }

}
