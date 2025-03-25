using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using log4net;

namespace BusinessLogicLayer.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILog _log = LogManager.GetLogger(typeof(CategoryService));

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void AddCategory(Category category)
        {
            try
            {
                _categoryRepo.AddCategory(category);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                _categoryRepo.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw;
            }
        }

        public List<Category> GetAllCategory()
        {
            try
            {
                var categories = _categoryRepo.GetAllCategories();

                return categories;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                var category = _categoryRepo.GetCategoryById(id);

                return category;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                _categoryRepo.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }
    }
}
