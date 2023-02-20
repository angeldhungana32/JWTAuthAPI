﻿using FluentValidation;
using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.DTOs.Product;
using JWTAuthAPI.Entities.DTOs.UserAccount;
using JWTAuthAPI.Helpers;
using JWTAuthAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.Controllers.v1
{
    public class ProductsController : V1BaseController
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductUpdateRequest> _updateValidator;
        private readonly IValidator<ProductCreateRequest> _createValidator;
        public ProductsController(IProductService productService, 
            IValidator<ProductUpdateRequest> updateValidator,
            IValidator<ProductCreateRequest> createValidator)
        {
            _productService = productService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        // POST api/v1/Products
        [HttpPost(RoutesConstant.AddProduct)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductCreateRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid) { return BadRequest(validationResult.ToString()); }

            var product = await _productService.AddProductAsync(request.ToEntity());
            if (product == null) { return BadRequest(); }

            return Created(string.Format("/Products/{0}", product.Id), product.ToResponseDTO());
        }

        // GET api/v1/Products/id
        [HttpGet(RoutesConstant.GetProduct)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var product = await _productService.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        // PUT api/v1/Products/id
        [HttpPut(RoutesConstant.UpdateProduct)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductAsync(string id, [FromBody] ProductUpdateRequest request)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid) { return BadRequest(validationResult.ToString()); }

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null) { return NotFound(); }
            if(!await _productService.AuthorizeProductOwnerAsync(User, product)) { return NotFound(); }

            var succeeded = await _productService.UpdateProductAsync(product.UpdateEntity(request));

            if (!succeeded) { throw new Exception($"Failed Product Update : {product.Id}"); }

            return NoContent();
        }

        // DELETE api/v1/Products/id
        [HttpDelete(RoutesConstant.DeleteProduct)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null) { return NotFound(); }
            if (!await _productService.AuthorizeProductOwnerAsync(User, product)) { return NotFound(); }

            var succeeded = await _productService.DeleteProductAsync(product);

            if (!succeeded) { throw new Exception($"Failed Product Delete : {product.Id}"); }

            return NoContent();
        }

        [HttpGet(RoutesConstant.GetAllProductsByUserId)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByUserIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var products = await _productService.ListAllProductsByUserIdAsync(id);
            return products == null ? NotFound() : Ok(products);
        }
    }
}
