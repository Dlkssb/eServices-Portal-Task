using Application.eServicesPortal.Interfaces;
using Domain.eServicesPortal.Entities;
using Infrastructure.eServicesPortal.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.eServicesPortal.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
          await _context.Users.AddAsync(user);
          await  _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
             _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            _context.Dispose();
            
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            

            var user= await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
           
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Name == normalizedUserName);

            
            return user;

        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(Guid Id)
        {
            return await _context.Users.FirstOrDefaultAsync(u=> u.Id==Id.ToString());
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public void Save()
        {
           _context.SaveChanges();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName=userName;

            return Task.CompletedTask;
        }

    

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return IdentityResult.Success;
        }
    }
}
