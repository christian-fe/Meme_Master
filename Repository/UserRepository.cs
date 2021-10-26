using Memes.Data;
using Memes.Models;//*
using Memes.Repository.IRepository;//*
using System;
using System.Collections.Generic;
using System.Linq;

namespace Memes.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Validate if the user exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistUser(string userName)
        {
            //validate
            if (_db.User.Any(u => u.UserA== userName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            return _db.User.FirstOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public ICollection<User> GetUsers()
        {
            return _db.User.OrderBy(u => u.UserA).ToList();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string user, string password)
        {
            var u = _db.User.FirstOrDefault(u => u.UserA == user);

            if(user==null) 
            {
                return null;
            }

            //HASH
            if (!CheckPasswordHash(password, u.PasswordHash, u.PaswordSalt))
            {
                return null;     
            }

            return u;

        }

        public User Registry(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PaswordSalt = passwordSalt;

            _db.User.Add(user);
            SaveChanges();
            return user;
        }

        /// <summary>
        /// Perform Save action
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                 passwordSalt = hmac.Key;
                 passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        private bool CheckPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashComputed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i=0; i < hashComputed.Length; i++)
                {
                    if(hashComputed[i]!=passwordHash[i])
                    {
                        return false;
                    }
                    
                }
                return true;
            }
        }
    }
}
