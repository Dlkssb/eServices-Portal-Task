using Domain.eServicesPortal.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.eServicesPortal.Interfaces
{
    public interface IUserRepository:IUserStore<User>,IRepositoryBase<User>
    {
 

    }
}
