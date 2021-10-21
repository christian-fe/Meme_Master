using Memes.Models;
using System.Collections.Generic;

namespace Memes.Repository.IRepository
{
    /// <summary>
    /// Methods for Photo Entity
    /// </summary>
    public interface IPhotoRepository
    {
        /// <summary>
        /// Retrieve all photo
        /// </summary>
        /// <returns>Meme objects</returns>
        ICollection<Photo> GetPhoto();

        /// <summary>
        /// Retrieve all photos by category
        /// </summary>
        /// <returns></returns>
        ICollection<Photo> GetPhotoByCategory(int idCategory);

        /// <summary>
        /// Retrieve only one photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns>object category</returns>
        Photo GetPhoto(int photoId);

        /// <summary>
        /// Validate the existence of a photo by name
        /// </summary>
        /// <param name="photoName"></param>
        /// <returns></returns>
        bool ExistPhoto(string photoName);

        /// <summary>
        /// Get Photos By Name
        /// </summary>
        /// <param name="photoName"></param>
        /// <returns></returns>
        IEnumerable<Photo> GetPhotoByName(string photoName);

        /// <summary>
        /// Validate the existence of a photo by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistPhoto(int id);

        /// <summary>
        /// Create a new photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        bool CreatePhoto(Photo photo);

        /// <summary>
        /// Update photo
        /// </summary>
        /// <param name="meme"></param>
        /// <returns></returns>
        bool UpdatePhoto(Photo photo);

        /// <summary>
        /// Delete a photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        bool DeletePhoto(Photo photo);

        /// <summary>
        /// Perform the action save
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
