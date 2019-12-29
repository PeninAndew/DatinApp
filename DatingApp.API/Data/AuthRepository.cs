using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        public AuthRepository(DataContext context)
        {
            this.context = context;
            
        }
        public async Task<User> Login(string username, string password)
        {
            var user  = await this.context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user != null && VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            { 
                return user;
            }
            
            return null;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = true;
             using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {                                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if(computedHash.Length == passwordHash.Length)
                {
                    for(int i = 0; i < computedHash.Length; i++)
                    {
                        if(computedHash[i] != passwordHash[i])
                        {
                            result = false;
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            var result = await this.context.Users.AnyAsync(x => x.Username == username);
            
            return result;
        }
    }
}