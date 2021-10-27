using Memes.Models;
using System;
using System.Collections.Generic;

namespace Memes.Repository.IRepository
{
    /// <summary>
    /// Methods for Category Entity
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Retrieve all categories
        /// </summary>
        /// <returns>Categories objects</returns>
        ICollection<Category> GetCategories();

        /// <summary>
        /// Retrieve only one category
        /// </summary>
        /// <param name="CategoriaId"></param>
        /// <returns>object category</returns>
        Category GetCategory(int CategoryId);

        /// <summary>
        /// Validate the existence of a category by name
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        bool ExistCategory(string CategoryName);

        /// <summary>
        /// Validate the existence of a category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistCategory(int id);

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool CreateCategory(Category category);

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool UpdateCategory(Category category);

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool DeleteCategory(Category category);



        /// <summary>
        /// Perform the action save
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool SaveChanges();

    }
}
