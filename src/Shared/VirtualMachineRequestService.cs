using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

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
            Date = DatumCreator(1, 0).AddMonths(-1),
            StartDate = DatumCreator(1, 0),
            EndDate = DatumCreator(2, 0),
            Reason = "Virtual machine voor een bachlerproef onderzoek ivm AI.",
            projectNaam = "bachlerproef AI",
            Status = ERequestStatus.Accepted,
            EmailAanvrager = "test@help.be"
        };
        var request2 = new VirtualMachineRequest
        {
            Id = 2,
            Date = DatumCreator(1, 1).AddMonths(-1),
            StartDate = DatumCreator(1, 1),
            EndDate = DatumCreator(2, 1),
            Reason = "Virtual machine voor een DevOps opdracht.",
            projectNaam = "Opdracht",
            Status = ERequestStatus.Handled,
            EmailAanvrager = "test@help.be"
        };
        var request3 = new VirtualMachineRequest
        {
            Id = 3,
            Date = DatumCreator(4, 4).AddMonths(-1),
            StartDate = DatumCreator(4, 4),
            EndDate = DatumCreator(4, 19),
            Reason = "Virtual machine voor iets online te zetten",
            projectNaam = "Online",
            Status = ERequestStatus.Denied,
            EmailAanvrager = "test@help.be"
        };

        _requests.Add(request1);
        _requests.Add(request2);
        _requests.Add(request3);
    }
}