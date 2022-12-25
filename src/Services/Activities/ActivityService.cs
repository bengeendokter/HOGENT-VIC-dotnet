using Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Activities;

namespace Services.Activities;
public class ActivityService : IActivityService
{
    private readonly VicDbContext dbContext;

    public ActivityService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<ActivityDto.Index>> GetIndexAsync(ActivityRequest.Index request)
    {
        var query = dbContext.Activities.AsQueryable();


        if (!string.IsNullOrEmpty(request.Status))
        {
            var status = Enum.Parse<Domain.Activities.EActivity>(request.Status, true);
            query = query.Where(x => x.Type.Equals(status));
        }

        query = SortActivityQuery(request.SortBy, query);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new ActivityDto.Index
            {
                Id = a.Id,
                CreatedAt = a.CreatedAt,
                Type = (Shared.Activities.EActivity)a.Type,
                VMName = a.VMName,
                ClientName = a.ClientName,
                CPU = a.CPU,
                RAM = a.RAM,
                Storage = a.Storage
            })
            .ToListAsync();

        return items;
    }

    private IQueryable<Activity> SortActivityQuery(string? sortby, IQueryable<Activity> query)
    {
        return (sortby) switch
        {
            "date" => query.OrderBy(x => x.CreatedAt),
            "name" => query.OrderBy(x => x.VMName),
            "nameDesc" => query.OrderByDescending(x => x.VMName),
            "cpu" => query.OrderByDescending(x => x.CPU),
            "cpuDesc" => query.OrderBy(x => x.CPU),
            "ram" => query.OrderByDescending(x => x.RAM),
            "ramDesc" => query.OrderBy(x => x.RAM),
            "storage" => query.OrderByDescending(x => x.Storage),
            "storageDesc" => query.OrderBy(x => x.Storage),
            _ => query.OrderByDescending(x => x.CreatedAt)
        };
    }
}
