using System;
using System.Collections.Generic;
using System.Text;
using zilver.domain.Entities;
using zilver.domain.Interfaces;

namespace zilver.services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _unitOfWork.Products.GetAllAsync();

        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                _unitOfWork.Products.Remove(product);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
