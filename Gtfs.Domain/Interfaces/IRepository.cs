namespace Gtfs.Domain.Interfaces;

public interface IRepository<T,K>
{
	IEnumerable<T> GetAll();
	T GetById(K id);
	void Remove(T obj);
	void Add(T obj);
}