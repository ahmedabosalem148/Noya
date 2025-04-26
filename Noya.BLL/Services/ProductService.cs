using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public IEnumerable<Product> GetAll() => _productRepository.GetAll();

        public Product GetById(int id) => _productRepository.GetById(id);

        public void Create(Product product) => _productRepository.Add(product);

        public void Update(Product product) => _productRepository.Update(product);

        public void Delete(int id) => _productRepository.Delete(id);
    }
}
