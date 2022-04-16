using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Application.Dtos;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateUpdateProduct(BookDto productDto)
        {
            Book product = _mapper.Map<BookDto, Book>(productDto);
            if (product.ProductId > 0)
            {
                _db.Books.Update(product);
            }
            else
            {
                _db.Books.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Book, BookDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
               Book product = await _db.Books.FirstOrDefaultAsync(u => u.ProductId == productId);
                if (product == null)
                {
                    return false;
                }
                _db.Books.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<BookDto> GetProductById(int productId)
        {
            Book product = await _db.Books.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<BookDto>(product);
        }

        public async Task<IEnumerable<BookDto>> GetProducts()
        {
            List<Book> productList = await _db.Books.ToListAsync();
            return _mapper.Map<List<BookDto>>(productList);
        }
    }
}
