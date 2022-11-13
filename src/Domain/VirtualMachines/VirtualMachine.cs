using Domain.Common;

namespace Domain.VirtualMachines;

public class VirtualMachine : Entity
{
    public object? Client { get; set; } // TODO: relation with client domain class
    public string? Name { get; set; }
    public string? HostName { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public bool IsHighlyAvailable { get; set; }
    public string? FQDN { get; set; }
    public string? Poorten { get; set; }
    public EBackupFrequency BackupFrequency { get; set; }
    public EDay Availability { get; set; }
    public object? Template { get; set; } // TODO: relation with template domain class
    public string? Host { get; set; }
    public bool IsActive { get; set; }
    public int CPU { get; set; }
    public int RAM { get; set; }
    public int Storage { get; set; }
    public EMode Mode { get; set; }
}
