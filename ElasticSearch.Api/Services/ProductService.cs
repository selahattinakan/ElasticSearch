using ElasticSearch.Api.DTOs;
using ElasticSearch.Api.Models;
using ElasticSearch.Api.Repositories;
using Nest;
using System.Collections.Immutable;

namespace ElasticSearch.Api.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;

        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var responseProduct = await _productRepository.SaveAsync(request.CreateProduct());

            if (responseProduct == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "Kayıt esnasında bir hata meydana geldi." }, System.Net.HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(responseProduct.CreateDto(), System.Net.HttpStatusCode.Created);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productListDto = new List<ProductDto>();
            foreach (var item in products)
            {
                if (item.Feature is null)
                {
                    productListDto.Add(new ProductDto(item.Id, item.Name, item.Price, item.Stock, null));
                }
                else
                {
                    productListDto.Add(new ProductDto(item.Id, item.Name, item.Price, item.Stock, new ProductFeatureDto(item.Feature.Width, item.Feature.Height, item.Feature.Color.ToString())));
                }
            }

            return ResponseDto<List<ProductDto>>.Success(productListDto, System.Net.HttpStatusCode.OK);

        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return ResponseDto<ProductDto>.Fail("Kayıt bulunamadı", System.Net.HttpStatusCode.NotFound);
            }

            return ResponseDto<ProductDto>.Success(product.CreateDto(), System.Net.HttpStatusCode.Created);
        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct)
        {
            var isSuccess = await _productRepository.UpdateAsync(updateProduct);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail("Update edilemedi", System.Net.HttpStatusCode.NotFound);
            }

            return ResponseDto<bool>.Success(true, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var deleteResponse = await _productRepository.DeleteAsync(id);

            if (!deleteResponse.IsValid && deleteResponse.Result == Result.NotFound)
            {
                return ResponseDto<bool>.Fail("Silinecek kayıt bulunamadı", System.Net.HttpStatusCode.InternalServerError);
            }
            if (!deleteResponse.IsValid)
            {
                _logger.LogError(deleteResponse.OriginalException, deleteResponse.ServerError?.ToString());
                return ResponseDto<bool>.Fail("Silinemedi", System.Net.HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(true, System.Net.HttpStatusCode.OK);
        }
    }
}
