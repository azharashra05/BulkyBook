using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>,IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var objFromDb= _context.Products.FirstOrDefault(product=>product.Id==product.Id);
            if (objFromDb!=null)
            {
                objFromDb.Title=product.Title;
                objFromDb.Description=product.Description;
                objFromDb.ISBN=product.ISBN;
                objFromDb.Author=product.Author;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price=product.Price;
                objFromDb.Price50=product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.CoverTypeId=product.CoverTypeId;
                if(objFromDb.ImageUrl!=null)
                {
                    objFromDb.ImageUrl=product.ImageUrl;
                }

            }
        }
    }
}
