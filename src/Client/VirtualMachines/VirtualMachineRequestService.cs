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

    private DateTime DatumCreator(int maand, int dag)
    {
        int j = DateTime.Now.Year;
        int m = DateTime.Now.Month;
        int d = DateTime.Now.Day;

        var dagen = DateTime.DaysInMonth(j, m);

        int j1 = j;
        int m1 = m;
        int d1 = d;

        if (d1 + dag > dagen)
        {
            d1 = (d1 + dag) % dagen;
            m1 += 1;

        }
        else
        {
            d1 += dag;
        }

        if (m1 + maand > 12)
        {
            m1 = (m1 + maand) % 12;
            j1 += 1;

        }
        else
        {
            m1 += maand;
        }

        return new DateTime(j1, m1, d1);
    }

    private void SetDummyRequestList()
    {
        var request1 = new VirtualMachineRequest
        {
            Id = 1,
<<<<<<< HEAD:src/Client/VirtualMachines/VirtualMachineRequestService.cs
            Date = new DateTime(2022, 10, 25, 9, 15, 0),
            StartDate = new DateTime(2022, 11, 15, 9, 15, 0),
            EndDate = DateTime.MaxValue,
=======
            Date = DatumCreator(1, 0).AddMonths(-1),
            StartDate = DatumCreator(1, 0),
            EndDate = DatumCreator(2, 0),
>>>>>>> analytics:src/Shared/VirtualMachineRequestService.cs
            Reason = "Virtual machine voor een bachlerproef onderzoek ivm AI.",
            ProjectNaam = "bachlerproef AI",
            Status = ERequestStatus.Accepted,
            EmailAanvrager = "brecht@test.be",
            NummerAanvrager = 0469569562
        };
        var request2 = new VirtualMachineRequest
        {
            Id = 2,
<<<<<<< HEAD:src/Client/VirtualMachines/VirtualMachineRequestService.cs
            Date = new DateTime(2022, 10, 23, 9, 15, 0),
            StartDate = new DateTime(2022, 11, 05, 9, 15, 0),
            EndDate = DateTime.MaxValue,
=======
            Date = DatumCreator(1, 1).AddMonths(-1),
            StartDate = DatumCreator(1, 1),
            EndDate = DatumCreator(2, 1),
>>>>>>> analytics:src/Shared/VirtualMachineRequestService.cs
            Reason = "Virtual machine voor een DevOps opdracht.",
            ProjectNaam = "Opdracht",
            Status = ERequestStatus.Handled,
            EmailAanvrager = "diemen@test.be",
            NummerAanvrager = 0469569562
        };
        var request3 = new VirtualMachineRequest
        {
<<<<<<< HEAD:src/Client/VirtualMachines/VirtualMachineRequestService.cs
            Id = 3, 
            Date = new DateTime(2022, 10, 20, 9, 15, 0),
            StartDate = new DateTime(2022, 11, 19, 9, 15, 0),
            EndDate = DateTime.MaxValue,
=======
            Id = 3,
            Date = DatumCreator(4, 4).AddMonths(-1),
            StartDate = DatumCreator(4, 4),
            EndDate = DatumCreator(4, 19),
>>>>>>> analytics:src/Shared/VirtualMachineRequestService.cs
            Reason = "Virtual machine voor iets online te zetten",
            ProjectNaam = "Online",
            Status = ERequestStatus.Denied,
            EmailAanvrager = "harld@test.be",
            NummerAanvrager = 0469569562
        };
<<<<<<< HEAD:src/Client/VirtualMachines/VirtualMachineRequestService.cs
=======

>>>>>>> analytics:src/Shared/VirtualMachineRequestService.cs
        _requests.Add(request1);
        _requests.Add(request2);
        _requests.Add(request3);
    }
}