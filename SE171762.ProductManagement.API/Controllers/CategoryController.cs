using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository.Interface;
using SE171762.ProductManagement.API.Services.Category;
using SE171762.ProductManagement.API.Services.Product;
using System.Linq.Expressions;

namespace SE171762.ProductManagement.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public static Expression<Func<Category, object>> GetOrderBy(string orderBy)
            => orderBy?.ToLower() switch
            {
                "1" => e => e.CategoryId,
                "2" => e => e.CategoryName,
                _ => e => e.CategoryName
            };


        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            int? pageIndex = 1,
            int? pageSize = 5,
            bool? isAscending = true)
        {
            Expression<Func<Category, bool>> filter = searchValue == null ? null : c => c.CategoryName.Contains(searchValue);

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.CategoryRepository.Get(
                    filter: filter,
                    pageIndex: pageIndex,
                    orderBy: keySelector,
                    pageSize: pageSize,
                    isAscending: isAscending,
                    includeProperties: "Products");
            var response = mapper.Map<IEnumerable<CategoryResponse>>(result);
            if (response == null)
                return NotFound("Category list is empty");
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
                return NotFound("Category does not exist");

            var categoryResponse = mapper.Map<CategoryResponse>(category);
            return Ok(categoryResponse);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryRequest categoryRequest)
        {
            if (unitOfWork.CategoryRepository.IsExistCategoryByName(categoryRequest.CategoryName))
            {
                return BadRequest("CategoryName already exists");
            }

            var category = mapper.Map<CategoryRequest, Category>(categoryRequest);

            unitOfWork.CategoryRepository.Insert(category);
            unitOfWork.Save();

            var createdCategory = unitOfWork.CategoryRepository.GetByID(category.CategoryId);
            var response = mapper.Map<Category, CategoryResponse>(createdCategory);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryRequest categoryRequest)
        {
            if (!unitOfWork.CategoryRepository.IsExistCategory(id))
            {
                return NotFound("Category does not exist");
            }

            var request = unitOfWork.CategoryRepository.GetByID(id);
            mapper.Map(categoryRequest, request);

            unitOfWork.CategoryRepository.Update(request);
            unitOfWork.Save();

            var response = mapper.Map<Category, CategoryResponse>(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var response = unitOfWork.CategoryRepository.GetByID(id);
            if (response == null)
                return NotFound("Category have been deleted");

            unitOfWork.CategoryRepository.Delete(response);
            unitOfWork.Save();
            return Ok("Category have been deleted");
        }
    }
}
