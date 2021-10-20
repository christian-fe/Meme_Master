using Memes.Models;
using System.Collections.Generic;

namespace Memes.Repository.IRepository
{
    /// <summary>
    /// Methods for Meme Entity
    /// </summary>
    public interface IMemeRepository
    {
        /// <summary>
        /// Retrieve all memes
        /// </summary>
        /// <returns>Meme objects</returns>
        ICollection<Photo> GetMemes();

        /// <summary>
        /// Retrieve all memes by category
        /// </summary>
        /// <returns></returns>
        ICollection<Photo> GetMemesByCategory(int idCategory);

        /// <summary>
        /// Retrieve only one meme
        /// </summary>
        /// <param name="MemeId"></param>
        /// <returns>object category</returns>
        Photo GetMeme(int MemeId);

        /// <summary>
        /// Validate the existence of a meme by name
        /// </summary>
        /// <param name="MemeName"></param>
        /// <returns></returns>
        bool ExistMeme(string MemeName);

        /// <summary>
        /// Get Memes By Name
        /// </summary>
        /// <param name="memeName"></param>
        /// <returns></returns>
        IEnumerable<Photo> GetMemesByName(string memeName);

        /// <summary>
        /// Validate the existence of a meme by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistMeme(int id);

        /// <summary>
        /// Create a new meme
        /// </summary>
        /// <param name="meme"></param>
        /// <returns></returns>
        bool CreateMeme(Photo meme);

        /// <summary>
        /// Update meme
        /// </summary>
        /// <param name="meme"></param>
        /// <returns></returns>
        bool UpdateMeme(Photo meme);

        /// <summary>
        /// Delete a meme
        /// </summary>
        /// <param name="meme"></param>
        /// <returns></returns>
        bool DeleteMeme(Photo meme);

        /// <summary>
        /// Perform the action save
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
