namespace Shared;

public class InterneKlant : IKlant
{
    public string? Departement { get; set; }
    public string? Opleiding { get; set; }
    public string Naam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int GsmNummer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string BackupContact { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
