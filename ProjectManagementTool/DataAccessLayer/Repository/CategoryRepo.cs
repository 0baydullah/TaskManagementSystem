using DataAccessLayer.Data;
using DataAccessLayer.Enums;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly PMSDBContext _context;

        public CategoryRepo(PMSDBContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the Category.", ex);
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the Category.", ex);
            }
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                var categories = _context.Categories.ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the categories.", ex);
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the category.", ex);
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the category.", ex);
            }
        }
    }
}
