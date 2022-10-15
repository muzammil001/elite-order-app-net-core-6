using EliteOrderApp.Database;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service;

public class BaseService<T> where T : class
{
    public readonly AppDbContext Context;

    
    protected void Save() => Context.SaveChanges();

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
        Save();
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        Save();
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return Context.Set<T>().Where(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>();
    }


    public T GetById(int? id)
    {
        return Context.Set<T>().Find(id);
    }

    public void Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Save();
    }
    public bool Any()
    {
        return Context.Set<T>().Any();
    }
    public bool Any(Func<T, bool> predicate)
    {
        return Context.Set<T>().Any(predicate);
    }

    
}