using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Core;
using Project.Core.DTOs;
using Project.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // Get all products from the service
            var products = await _service.GetAllAsync();
            // Map products to product DTOs
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            // Return the mapped product DTOs as a success response
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Get product by ID from the service
            var product = await _service.GetByIdAsync(id);
            // Map product to product DTO
            var productDto = _mapper.Map<ProductDto>(product);
            // Return the mapped product DTO as a success response
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            // Map product DTO to product and add it to the service
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            // Map added product to product DTO
            var addedProductDto = _mapper.Map<ProductDto>(product);
            // Return the mapped added product DTO as a success response
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, addedProductDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            // Map product update DTO to product and update it in the service
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            // Return a success response with no content
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            // Get product by ID from the service
            var product = await _service.GetByIdAsync(id);
            // Remove the product from the service
            await _service.RemoveAsync(product);
            // Return a success response with no content
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}