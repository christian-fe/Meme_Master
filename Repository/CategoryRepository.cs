using Meme.Data;//*
using Meme.Models;//*
using Meme.Repository.IRepository;//*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool CreateCategory(Category category)
        {
            _db.Categories.Add(category);
            return SaveChanges();
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool DeleteCategory(Category category)
        {
            _db.Categories.Update(category);
            return SaveChanges();
        }

        /// <summary>
        /// Validate the category existence by name
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public bool ExistCategory(string categoryName)
        {
            return _db.Categories.Any(cat => cat.CategoryName.ToLower().Trim() == categoryName.ToLower().Trim());            
        }

        /// <summary>
        /// Validate the the category existence by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistCategory(int id)
        {
            return _db.Categories.Any(cat => cat.IdCategory == id);            
        }

        /// <summary>
        /// Obtain all categories
        /// </summary>
        /// <returns></returns>
        public ICollection<Category> GetCategories()
        {
            return _db.Categories.OrderBy(cat => cat.CategoryName).ToList();
        }

        /// <summary>
        /// return a catagory object funded by Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategory(int categoryId)
        {
            return _db.Categories.FirstOrDefault(cat => cat.IdCategory == categoryId);
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            return SaveChanges();
        }
    }
}
