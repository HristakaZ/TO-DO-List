using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    internal interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Get();

        void Create(T model);

        void Update(T model);

        void Delete(T model);
    }
}
