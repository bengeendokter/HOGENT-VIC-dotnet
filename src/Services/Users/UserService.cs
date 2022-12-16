using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Clients;
using Domain.Users;
using EClientType = Domain.Users.EClientType;
using Shared.Users;

namespace Services.Clients;

public class UserService : IUserService
{
    private readonly VicDbContext dbContext;
    private readonly List<string> roles = new() { "User", "Moderator", "Admin" };

    public UserService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public ERole? GiveRoleFromString(string role)
    {
        return (role) switch
        {
            "User" => ERole.User,
            "Moderator" => ERole.Moderator,
            "Admin" => ERole.Admin,
            _ => null
        };
    }
    public async Task<UserResult.Index> GetIndexAsync(UserRequest.Index request)
    {
        var query = dbContext.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase)
            || x.Surname.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) || x.Email.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        if (request.Role is not null)
        {
            ERole? givenRole = GiveRoleFromString(request.Role);
            query = query.Where(x => x.Role.Equals(givenRole));
            

        }

        var items = await query
           .OrderByDescending(x => x.CreatedAt)
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .Select(x => new UserDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               Surname = x.Surname,
               Email = x.Email,
               Role = (Shared.ERole) x.Role,
               IsActive = x.IsActive,
               
           })
           .ToListAsync();

        var result = new UserResult.Index
        {
            Users = items,
            TotalAmount = items.Count
        };

        return result;
    }

    public async Task<UserDto.Detail> GetDetailAsync(int userId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user is null)
            throw new EntityNotFoundException(nameof(User), userId);

        return new UserDto.Detail
        {
            Id = userId,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Role = (Shared.ERole) user.Role,
            IsActive = user.IsActive
        };
    }

    public async Task<int> CreateAsync(UserDto.Mutate model)
    {
        if (await dbContext.Users.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(User), nameof(User.Name), model.Name);

        User user = new User(
            model.Name!,
            model.Surname!,
            model.Email!,
            (Domain.Users.ERole) model.Role,
            true
        );

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        return user.Id;
    }

    public async Task EditAsync(int userId, UserDto.Mutate model)
    {
        User? user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

        if (user is null)
            throw new EntityNotFoundException(nameof(User), userId);

        user.Name = model.Name!;
        user.Surname = model.Surname!;
        user.Email = model.Email!;
        user.Role = (Domain.Users.ERole) model.Role;
        user.IsActive = model.IsActive;

        await dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(int userId)
    {
        User? user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

        if (user is null)
            throw new EntityNotFoundException(nameof(User), userId);

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync();
    }
}
