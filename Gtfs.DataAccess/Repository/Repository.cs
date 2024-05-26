using Gtfs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.DataAccess.Repository;

public class Repository<T,K> : IRepository<T,K> where T : class
{
	private readonly DbSet<T> _dbSet;

	public Repository(GtfsContext context)
	{
		_dbSet = context.Set<T>();
	}

	public IQueryable<T> GetAll()
	{
		return _dbSet.AsQueryable();
	}

	public T GetById(K id)
	{
		var entity = _dbSet.Find(id);
		if (entity == null)
		{
			throw new KeyNotFoundException($"Entity with ID {id} not found.");
		}
		return entity;
	}
    
	public void Add(T obj)
	{
		_dbSet.Add(obj);
	}

	public void Remove(T obj)
	{
		_dbSet.Remove(obj);
	}
}