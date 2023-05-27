using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.eServicesPortal.Interfaces
{
    public interface IRepositoryBase<T>  where T : class
    {
        public Task<T> GetUser(Guid Id);

        public Task<List<T>> GetUsers();




    }
}
