namespace Shared.VirtualMachines;

[Flags]
public enum ESoftware
{
    Windows = 1,
    Linux = 2,
    MySQL = 4,
    MongoDB = 8,
    Docker = 16,
}
