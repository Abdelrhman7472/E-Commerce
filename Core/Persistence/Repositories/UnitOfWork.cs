using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;

        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositories = new ();
        }

        public async Task<int> SaveChangesAsync()=> await _storeContext.SaveChangesAsync();
      
        // bey3ml check lw el type beta3o be inhert baseEntity ma3naha en 3ando repository hattkhzn fe Dictionary
        // lw howa fel dictionary hayb3tlo instance mn el repo de(ely hayb3t el unit of work) 
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
      
            return (IGenericRepository<TEntity, TKey>)
                _repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, TKey>(_storeContext));


            //var typeName =typeof(TEntity).Name;

            //if(_repositories.ContainsKey(typeName)) // lw el dictionay feh esm el type de
            //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];


            //var repo = new GenericRepository<TEntity, TKey>(_storeContext); // lw fady ba3ml new dictionary

            //_repositories.Add(typeName, repo);
            //return repo;

        }

    
    }
}
