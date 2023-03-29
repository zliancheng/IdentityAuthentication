using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.ProductAPI.Models.Dto;
using Mongo.Services.ProductAPI.Repository;

namespace Mongo.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _response = new ResponseDto();
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var productDtos = await _productRepository.GetProducts();
                _response.Result = productDtos;
            }catch(Exception ex)
            {
                _response.ErrorMessage = ex.ToString();
                _response.isSuccess = false;

            }
            return _response;
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                var productDto = await _productRepository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.ToString();
                _response.isSuccess = false;

            }
            return _response;
        }
        [Authorize]
        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.ToString();
                _response.isSuccess = false;

            }
            return _response;
        }
        [Authorize]
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.ToString();
                _response.isSuccess = false;

            }
            return _response;
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var result = await _productRepository.DeleteProduct(id);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.ToString();
                _response.isSuccess = false;

            }
            return _response;
        }
    }
}
