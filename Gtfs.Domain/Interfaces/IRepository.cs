using Gtfs.Domain.Models;

namespace Gtfs.Domain.Interfaces;

public interface IRepository<T,K>
{
	IQueryable<T> GetAll();
	T GetById(K id);
	void Remove(T obj);
	void Add(T obj);
}