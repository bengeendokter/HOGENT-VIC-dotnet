using Shared.VirtualMachines;

namespace Client.VirtualMachines;

public class VirtualMachineRequestService : IVirtualMachineRequestService
{
    private readonly List<VirtualMachineRequest> _requests = new List<VirtualMachineRequest>();
    public VirtualMachineRequestService()
    {
        SetDummyRequestList();
    }
    
    public VirtualMachineRequest? Get(int id)
    {
        return _requests.FirstOrDefault(x => x.Id == id);
    }

    public List<VirtualMachineRequest> GetAll()
    {
        Console.WriteLine("test");
        return _requests;
    }

    public VirtualMachineRequest? Update(int id, VirtualMachineRequest request)
    {
        throw new NotImplementedException();
    }

    private void SetDummyRequestList()
    {
        var request1 = new VirtualMachineRequest
        {
            Id = 1,
            Date = new DateTime(2022, 10, 25, 9, 15, 0),
            StartDate = new DateTime(2022, 11, 15, 9, 15, 0),
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor een bachlerproef onderzoek ivm AI.",
            ProjectNaam = "bachlerproef AI",
            Status = ERequestStatus.Accepted,
            EmailAanvrager = "brecht@test.be",
            NummerAanvrager = 0469569562
        };
        var request2 = new VirtualMachineRequest
        {
            Id = 2,
            Date = new DateTime(2022, 10, 23, 9, 15, 0),
            StartDate = new DateTime(2022, 11, 05, 9, 15, 0),
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor een DevOps opdracht.",
            ProjectNaam = "Opdracht",
            Status = ERequestStatus.Handled,
            EmailAanvrager = "diemen@test.be",
            NummerAanvrager = 0469569562
        };
        var request3 = new VirtualMachineRequest
        {
            Id = 3, 
            Date = new DateTime(2022, 10, 20, 9, 15, 0),
            StartDate = new DateTime(2022, 11, 19, 9, 15, 0),
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor iets online te zetten",
            ProjectNaam = "Online",
            Status = ERequestStatus.Denied,
            EmailAanvrager = "harld@test.be",
            NummerAanvrager = 0469569562
        };
        _requests.Add(request1);
        _requests.Add(request2);
        _requests.Add(request3);
    }
}
