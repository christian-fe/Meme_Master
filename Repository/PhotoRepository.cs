using Memes.Models;
using Memes.Repository.IRepository;
using System;
using System.Collections.Generic;
using Memes.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Memes.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _db;

        public PhotoRepository(ApplicationDbContext db)
        {
            _db = db;    
        }

        /// <summary>
        /// Create a new photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public bool CreatePhoto(Photo photo)
        {
            _db.Photo.Add(photo);
            return SaveChanges();
        }

        /// <summary>
        /// Delete a Photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public bool DeletePhoto(Photo photo)
        {
            _db.Photo.Remove(photo);
            return SaveChanges();
        }

        /// <summary>
        /// Validate if photo exist by name
        /// </summary>
        /// <param name="photoName"></param>
        /// <returns></returns>
        public bool ExistPhoto(string photoName)
        {
            bool value = _db.Photo.Any(p => p.PhotoName.ToLower().Trim() == photoName.ToLower().Trim());
            return value;
        }

        /// <summary>
        /// Validate if photo exist by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistPhoto(int id)
        {
            return _db.Photo.Any(p => p.PhotoId == id);
        }

        /// <summary>
        /// Get all the photos
        /// </summary>
        /// <returns></returns>
        public ICollection<Photo> GetPhoto()
        {
            return _db.Photo.OrderBy(p => p.PhotoName).ToList();
        }

        /// <summary>
        /// Get photo by id
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public Photo GetPhoto(int photoId)
        {
            return _db.Photo.FirstOrDefault(p => p.PhotoId == photoId);
        }

        /// <summary>
        /// Get photo by categoryId
        /// </summary>
        /// <param name="idCategory"></param>
        /// <returns></returns>
        public ICollection<Photo> GetPhotoByCategory(int idCategory)
        {
            return _db.Photo.Include(cat => cat.Category).Where(cat => cat.IdCategory == idCategory).ToList();
        }

        /// <summary>
        /// Get Photo ByName
        /// </summary>
        /// <param name="photoName"></param>
        /// <returns></returns>
        public IEnumerable<Photo> GetPhotoByName(string photoName)
        {
            IQueryable<Photo> query = _db.Photo;

            if(!string.IsNullOrEmpty(photoName))
            {
                query = query.Where(p => p.PhotoName.Contains(photoName));
            }

            return query.ToList();

        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
          return _db.SaveChanges() >= 0 ? true : false;
        }

        /// <summary>
        /// Update Photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public bool UpdatePhoto(Photo photo)
        {
            _db.Photo.Update(photo);
            return SaveChanges();
        }
    }
}
