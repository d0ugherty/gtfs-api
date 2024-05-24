using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.DataAccess.Repository;

public class Repository<T,K> : IRepository<T,K> where T : class
{
	private readonly DbSet<T> _dbSet;

	public Repository(GtfsContext context)
	{
		_dbSet = context.Set<T>();
	}

	public IEnumerable<T> GetAll()
	{
		return _dbSet.ToList();
	}

	public T GetById(K id)
	{
		return _dbSet.Find(id) ?? throw new InvalidOperationException();
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