using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uwp.ProjFinal.Repositories.Base
{
    public interface IRepository<T>
    {
        Task LoadAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
