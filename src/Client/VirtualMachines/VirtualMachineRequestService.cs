using System.Net.Http.Json;

namespace Client.VirtualMachines;

public class VirtualMachineRequestService : IVirtualMachineRequestService
{
    private readonly HttpClient client;
    private const string endpoint = "api/virtualmachinerequest";

    public VirtualMachineRequestService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<VirtualMachineRequestDto.Index>> GetAll()
    {
        return await client.GetFromJsonAsync<List<VirtualMachineRequestDto.Index>>(endpoint) ?? new();
    }

    public async Task<VirtualMachineRequestDto.Detail> Get(int id)
    {
        var response = await client.GetFromJsonAsync<VirtualMachineRequestDto.Detail>(
            $"{endpoint}/{id}"
        );
        return response!;
    }

    public async Task<int> CreateAsync(VirtualMachineRequestDto.Create model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task EditAsync(int id, VirtualMachineRequestDto.Detail request)
    {
        await client.PutAsJsonAsync($"{endpoint}/{id}", request);
    }




    //private DateTime DatumCreator(int maand, int dag)
    //{
    //    int j = DateTime.Now.Year;
    //    int m = DateTime.Now.Month;
    //    int d = DateTime.Now.Day;

    //    var dagen = DateTime.DaysInMonth(j, m);

    //    int j1 = j;
    //    int m1 = m;
    //    int d1 = d;

    //    if (d1 + dag > dagen)
    //    {
    //        d1 = (d1 + dag) % dagen;
    //        m1 += 1;

    //    }
    //    else
    //    {
    //        d1 += dag;
    //    }

    //    if (m1 + maand > 12)
    //    {
    //        m1 = (m1 + maand) % 12;
    //        j1 += 1;

    //    }
    //    else
    //    {
    //        m1 += maand;
    //    }

    //    //maand check
    //    var specifiek = DateTime.DaysInMonth(j1, m1);
    //    if (d1 > specifiek)
    //    {
    //        m1 += 1;
    //        d1 = d1 % specifiek;
    //    }

    //    //jaar check
    //    if (m1 > 12)
    //    {
    //        m1 = m1 % 12;
    //        j1 += 1;
    //    }

    //    return new DateTime(j1, m1, d1);
    //}

    //private void SetDummyRequestList()
    //{
    //    var request1 = new VirtualMachineRequestDto.Detail
    //    {
    //        Id = 1,
    //        Date = DatumCreator(1, 0).AddMonths(-1),
    //        ProjectName = "Bachlerproef AI",
    //        StartDate = DatumCreator(1, 0),
    //        EndDate = DatumCreator(2, 0),
    //        Reason = "Virtual machine voor een bachlerproef onderzoek ivm AI.",
    //        Status = ERequestStatus.Requested,
    //    };
    //    var request2 = new VirtualMachineRequestDto.Detail
    //    {
    //        Id = 2,
    //        Date = DatumCreator(1, 1).AddMonths(-1),
    //        ProjectName = "DotNet applicatie software development",
    //        StartDate = DatumCreator(1, 1),
    //        EndDate = DatumCreator(2, 1),
    //        Reason = "Virtual machine voor een DevOps opdracht.",
    //        Status = ERequestStatus.Denied,

    //    };
    //    var request3 = new VirtualMachineRequestDto.Detail
    //    {
    //        Id = 3,
    //        Date = DatumCreator(4, 4).AddMonths(-1),
    //        ProjectName = "Online",
    //        StartDate = DatumCreator(4, 4),
    //        EndDate = DatumCreator(4, 19),
    //        Reason = "Virtual machine voor iets online te zetten",
    //        Status = ERequestStatus.Handled,
    //    };

    //    _requests.Add(request1);
    //    _requests.Add(request2);
    //    _requests.Add(request3);
    //}
}