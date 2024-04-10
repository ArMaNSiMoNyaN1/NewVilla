using System.Data.Entity;
using System.Linq.Expressions;
using Villa.Properties.Repository.IRepository;

namespace Villa.Properties.Repository;

public class VillaRepository : IVillaRepository
{
    private readonly IVillaRepository _db;

    public VillaRepository(IVillaRepository db)
    {
        _db = db;
    }

    public async Task<List<Models.Villa>> GetAllAsync(Expression<Func<Models.Villa, bool>> filter = null)
    {
        IQueryable<Models.Villa> query = _db.Villas;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<Models.Villa> GetAsync(Expression<Func<Models.Villa, bool>> filter = null, bool tracked = true)
    {
        IQueryable<Models.Villa> query = _db;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Models.Villa entity)
    {
        await _db.AddAsync(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(Models.Villa entity)
    {
        _db.UpdateAsync(entity);
        await SaveAsync();
    }

    public async Task RemoveAsync(Models.Villa entity)
    {
        _db.RemoveAsync(entity);
        await SaveAsync();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}