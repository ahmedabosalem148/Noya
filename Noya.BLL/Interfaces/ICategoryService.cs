﻿using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllWithProducts();
        Category GetById(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
