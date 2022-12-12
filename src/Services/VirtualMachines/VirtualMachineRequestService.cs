using Domain.VirtualMachines;
using Shared.VirtualMachines;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.VirtualMachines;

public class VirtualMachineRequestService : IVirtualMachineRequestService
{
    private readonly VicDbContext dbContext;

    public VirtualMachineRequestService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(VirtualMachineRequestDto.Create model)
    {
        var request = new Domain.VirtualMachines.VirtualMachineRequest(
            model.StartDate!,
            model.EndDate!,
            model.Reason!,
            model.ProjectName!,
            Domain.VirtualMachines.ERequestStatus.Requested!,
            model.ClientInfo!
        );

        dbContext.VirtualMachineRequests.Add(request);
        await dbContext.SaveChangesAsync();

        return request.Id;
    }

    public async Task<VirtualMachineRequestDto.Detail> Get(int id)
    {
        var request = await dbContext.VirtualMachineRequests.FirstOrDefaultAsync(v => v.Id == id);

        if (request is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), id);

        return new VirtualMachineRequestDto.Detail
        {
            Id = request.Id,
            Date = request.CreatedAt,
            ProjectName = request.ProjectName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason,
            Status = (Shared.VirtualMachines.ERequestStatus)request.Status,
        };
    }

    public async Task<List<VirtualMachineRequestDto.Detail>> GetAll()
    {
        return await dbContext.VirtualMachineRequests
            .Select(
                r =>
                    new VirtualMachineRequestDto.Detail
                    {
                        Id = r.Id,
                        Date = r.CreatedAt,
                        ProjectName = r.ProjectName,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        Reason = r.Reason,
                        Status = (Shared.VirtualMachines.ERequestStatus)r.Status,
                    }
            )
            .ToListAsync();
    }

    public async Task EditAsync(int id, VirtualMachineRequestDto.Detail request)
    {
        var r = await dbContext.VirtualMachineRequests.SingleOrDefaultAsync(v => v.Id == id);

        if (r is null)
            throw new EntityNotFoundException(nameof(Domain.VirtualMachines.VirtualMachineRequest), id);

        r.Status = (Domain.VirtualMachines.ERequestStatus)request.Status;

        await dbContext.SaveChangesAsync();
    }

}
