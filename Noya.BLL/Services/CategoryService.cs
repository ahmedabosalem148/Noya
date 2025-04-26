using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public IEnumerable<Category> GetAll() => _categoryRepository.GetAll();

        public Category GetById(int id) => _categoryRepository.GetById(id);

        public void Create(Category category) => _categoryRepository.Add(category);

        public void Update(Category category) => _categoryRepository.Update(category);

        public void Delete(int id) => _categoryRepository.Delete(id);
    }
}
