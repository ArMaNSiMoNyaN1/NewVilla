using System.Linq.Expressions;

namespace Villa.Properties.Repository.IRepository;

public interface IVillaRepository
{
    Task<List<Models.Villa>> GetAllAsync(Expression<Func<Models.Villa, bool>> filter = null);

    Task<Models.Villa> GetAsync(Expression<Func<Models.Villa, bool>> filter = null, bool tracked = true);

    Task CreateAsync(Models.Villa entity);
    Task UpdateAsync(Models.Villa entity);

    Task RemoveAsync(Models.Villa entity);

    Task SaveAsync();
}