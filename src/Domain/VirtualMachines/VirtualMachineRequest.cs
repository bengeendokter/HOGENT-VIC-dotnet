using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.VirtualMachines;

public class VirtualMachineRequest : Entity
{
    private DateTime startDate = default!;
    public DateTime StartDate
    {
        get => startDate;
        set => startDate = Guard.Against.AgainstExpression(d => d < DateTime.Now, value, "StartDate must be later than now");
    }

    private DateTime endDate = default!;
    public DateTime EndDate
    {
        get => endDate;
        set => endDate = Guard.Against.AgainstExpression(d => d < startDate, value, "EndDate must be greater than StartDate");
    }

    private string reason = default!;
    public string Reason
    {
        get => Reason;
        set => Reason = Guard.Against.NullOrEmpty(value, nameof(Reason));
    }


    private string projectName = default!;
    public string ProjectName
    {
        get => ProjectName;
        set => ProjectName = Guard.Against.NullOrEmpty(value, nameof(ProjectName));
    }

    private object client = default!;
    public object Client
    {
        get => client;
        set => client = Guard.Against.Null(value, nameof(Client));
    }


    private ERequestStatus status = default!;
    public ERequestStatus Status
    {
        get => Status;
        set => Status = Guard.Against.EnumOutOfRange(value, nameof(Status));
    }

    private VirtualMachineRequest() { }
}
