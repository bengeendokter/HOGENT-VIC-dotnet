
using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Users;

namespace Domain.VirtualMachines;

public class VirtualMachineRequest : Entity
{
    private DateTime startDate = default!;
    public DateTime StartDate
    {
        get => startDate;
        set => startDate = Guard.Against.AgainstExpression(d => d > DateTime.Now, value, "StartDate must be later than now");
    }

    private DateTime endDate = default!;
    public DateTime EndDate
    {
        get => endDate;
        set => endDate = Guard.Against.AgainstExpression(d => d > startDate, value, "EndDate must be greater than StartDate");
    }

    private string reason = default!;
    public string Reason
    {
        get => reason;
        set => reason = Guard.Against.NullOrEmpty(value, nameof(Reason));
    }


    private string projectName = default!;
    public string ProjectName
    {
        get => projectName;
        set => projectName = Guard.Against.NullOrEmpty(value, nameof(ProjectName));
    }

    private Client? client = default!;
    public Client? Client
    {
        get => client;
        set => client = value;
    }

    private VirtualMachine? virtualMachine = default!;

    public VirtualMachine? VirtualMachine
    {
        get => virtualMachine;
        set => virtualMachine = VirtualMachine;
    }

    private ERequestStatus status = default!;
    public ERequestStatus Status
    {
        get => status;
        set => status = Guard.Against.EnumOutOfRange(value, nameof(Status));
    }


    private VirtualMachineRequest() { }

    public VirtualMachineRequest(
        DateTime startDate, 
        DateTime endDate,
        string reason,
        string projectName,
        Client? client,
        VirtualMachine? virtualMachine,
        ERequestStatus status
        )
    {
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason;
        ProjectName = projectName;
        Client = client;
        VirtualMachine = virtualMachine;
        Status = status;
    }
}
