using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.Enums;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void AddCategory(Category category)
        {
            _categoryRepo.AddCategory(category);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepo.DeleteCategory(id);
        }

        public List<Category> GetAllCategory()
        {
            var categories = _categoryRepo.GetAllCategories();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepo.UpdateCategory(category);
        }
    }
}
