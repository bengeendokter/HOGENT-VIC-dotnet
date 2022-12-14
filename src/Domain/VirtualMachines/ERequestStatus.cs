namespace Domain.VirtualMachines;

[Flags]
public enum ERequestStatus
{
    Accepted = 1,
    Denied = 2,
    Handled = 4,
    Requested = 8
}
