using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository.Interface;
using SE171762.ProductManagement.API.Services;
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

        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? orderBy = "",
            bool? isAscending = true,
            int? pageIndex = 1,
            int? pageSize = 50)
        {
            Expression<Func<Product, bool>> filter = p =>
                   (searchValue == null || p.ProductName.Contains(searchValue))
                && (minPrice == null || p.UnitPrice >= minPrice)
                && (maxPrice == null || p.UnitPrice <= maxPrice);
            Expression<Func<Product, object>> order = null;

            if ((pageIndex != null && pageSize == null) || (pageSize != null && pageIndex == null))
                return BadRequest("Please enter page size and page index to paging");
            if (orderBy != null && orderBy != "")
            {
                switch (orderBy.ToLower())
                {
                    case "1":
                        order = p => p.ProductName;
                        break;
                    case "2":
                        order = p => p.ProductId;
                        break;
                    case "3":
                        order = p => p.CategoryId;
                        break;
                    case "4":
                        order = p => p.UnitsInStock;
                        break;
                    case "5":
                        order = p => p.UnitPrice;
                        break;
                    default:
                        break;
                }
            }

            var list = unitOfWork.ProductRepository.Get(
                    filter: filter,
                    orderBy: order,
                    isAscending: isAscending ?? true,
                    pageIndex: pageIndex ?? 1,
                    pageSize: pageSize ?? 50,
                    includeProperties: "Category");
            if (list.Count() > 0)
            {
                IEnumerable<ProductResponse> result = mapper.Map<IEnumerable<ProductResponse>>(list);
                PaginatedResponse response = new PaginatedResponse
                {
                    PageIndex = pageIndex ?? 1,
                    PageSize = pageSize ?? 50,
                    TotalItems = unitOfWork.ProductRepository.Count(),
                    PageItems = result.Count(),
                    TotalPages = unitOfWork.ProductRepository.Count() / pageSize ?? 50,
                    Items = result,
                };
                return Ok(response);
            }
            else
            {
                return NotFound("Product list is empty");
            }
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