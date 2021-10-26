using Memes.Models;
using System.Collections.Generic;

namespace Memes.Repository.IRepository
{
    /// <summary>
    /// Methods for User Entity
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieve all User
        /// </summary>
        /// <returns>Meme objects</returns>
        ICollection<User> GetUsers();

        /// <summary>
        /// Retrieve only one User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>object user</returns>
        User GetUser(int userId);

        /// <summary>
        /// Validate the existence of a User by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool ExistUser(string userName);

        /// <summary>
        /// Sing Up
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Registry(User user, string password);

        /// <summary>
        /// Sing In
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Login(string user, string password);

        /// <summary>
        /// Perform the action save
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
