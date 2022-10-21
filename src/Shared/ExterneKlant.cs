public class ExterneKlant : IKlant
{

    public string EterneNaam { get; set; }
    public string Type { get; set; }
    public string Naam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int GsmNummer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string BackupContact { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public ExterneKlant(string eterneNaam, string type)
    {
        EterneNaam = eterneNaam;
        Type = type;
    }


}
