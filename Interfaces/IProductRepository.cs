using Product.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAll();
        Task<Products> GetOne(int id);
    }
}