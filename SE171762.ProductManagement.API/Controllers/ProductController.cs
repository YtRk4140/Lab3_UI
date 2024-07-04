using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository.Interface;
using SE171762.ProductManagement.API.Services.Category;
using SE171762.ProductManagement.API.Services.Product;
using System.Linq.Expressions;

namespace SE171762.ProductManagement.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public static Expression<Func<Product, object>> GetOrderBy(string orderBy)
            => orderBy?.ToLower() switch
            {
                "1" => e => e.ProductId,
                "2" => e => e.CategoryId,
                "3" => e => e.ProductName,
                "4" => e => e.UnitPrice,
                "5" => e => e.UnitsInStock,
                _ => e => e.ProductName
            };

        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? orderBy = "",
            bool? isAscending = true,
            int? pageIndex = 1,
            int? pageSize = 5)
        {
            Expression<Func<Product, bool>> filter = p =>
                   (searchValue == null || p.ProductName.Contains(searchValue))
                && (minPrice == null || p.UnitPrice >= minPrice)
                && (maxPrice == null || p.UnitPrice <= maxPrice);

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.ProductRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    isAscending: isAscending,
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    includeProperties: "Category");
            var response = mapper.Map<IEnumerable<ProductResponse>>(result);
            if (response == null)
                return NotFound("Product list is empty");
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Expression<Func<Product, bool>> filter = p => p.ProductId == id;
            var result = unitOfWork.ProductRepository.Get(
                    filter: filter,
                    includeProperties: "Category");
            var response = mapper.Map<ProductResponse>(result.SingleOrDefault());
            if (response == null)
                return NotFound("Product does not exist or has been deleted");

            return Ok(response);
        }


        [HttpPost]
        public IActionResult CreateProduct(ProductRequest productRequest)
        {
            if (!unitOfWork.CategoryRepository.IsExistCategory(productRequest.CategoryId))
            {
                return BadRequest("This category does not exist.");
            }

            var request = mapper.Map<ProductRequest, Product>(productRequest);

            unitOfWork.ProductRepository.Insert(request);
            unitOfWork.Save();

            var createdProduct = unitOfWork.ProductRepository.GetByID(request.ProductId);
            var response = mapper.Map<ProductResponse>(createdProduct);

            return Ok(response);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductRequest productRequest) 
        {
            if (!unitOfWork.CategoryRepository.IsExistCategory(productRequest.CategoryId))
            {
                return BadRequest("This category does not exist.");
            }

            var request = unitOfWork.ProductRepository.GetByID(id);
            if (!unitOfWork.ProductRepository.IsExistProduct(id))
            {
                return NotFound("This product does not exist or has been deleted");
            }

            mapper.Map(productRequest, request);

            unitOfWork.ProductRepository.Update(request);
            unitOfWork.Save();

            var response = mapper.Map<ProductRequest, Product>(productRequest);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var response = unitOfWork.ProductRepository.GetByID(id);
            if (response == null)
                return NotFound("Product have been deleted or does not exist");

            unitOfWork.ProductRepository.Delete(response);
            unitOfWork.Save();
            return Ok("Product have been deleted");
        }
    }
}