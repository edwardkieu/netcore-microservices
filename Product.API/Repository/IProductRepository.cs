﻿using Product.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<BookDto>> GetProducts();
        Task<BookDto> GetProductById(int productId);
        Task<BookDto> CreateUpdateProduct(BookDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
