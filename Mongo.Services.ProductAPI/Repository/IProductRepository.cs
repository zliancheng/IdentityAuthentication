using Mongo.Services.ProductAPI.Models.Dto;

namespace Mongo.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<ProductDto> GetProductById(int productId);
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
