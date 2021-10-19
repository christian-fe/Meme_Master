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
        ICollection<Memes.Models.Meme> GetMemes();

        /// <summary>
        /// Retrieve only one meme
        /// </summary>
        /// <param name="MemeId"></param>
        /// <returns>object category</returns>
        Memes.Models.Meme GetMeme(int MemeId);

        /// <summary>
        /// Validate the existence of a meme by name
        /// </summary>
        /// <param name="MemeName"></param>
        /// <returns></returns>
        bool ExistMeme(string MemeName);

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
        bool CreateMeme(Memes.Models.Meme meme);

        /// <summary>
        /// Update meme
        /// </summary>
        /// <param name="meme"></param>
        /// <returns></returns>
        bool UpdateMeme(Memes.Models.Meme meme);

        /// <summary>
        /// Delete a meme
        /// </summary>
        /// <param name="meme"></param>
        /// <returns></returns>
        bool DeleteMeme(Memes.Models.Meme meme);

        /// <summary>
        /// Perform the action save
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
