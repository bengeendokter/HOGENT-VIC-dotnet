namespace Shared;

public class ExterneKlant : IKlant
{
    public string? ExterneNaam { get; set; }
    public string? Type { get; set; }
    public string Naam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int GsmNummer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string BackupContact { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
