using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository.Interface;
using SE171762.ProductManagement.API.CustomActionFilter;
using SE171762.ProductManagement.API.Services.Account;
using SE171762.ProductManagement.API.Services.Category;
using System.Linq.Expressions;
using CreateAccRequest = SE171762.ProductManagement.API.Services.Account.AccountRequest.CreateAcc;

namespace SE171762.ProductManagement.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public static Expression<Func<AccountMember, object>> GetOrderBy(string orderBy)
            => orderBy?.ToLower() switch
            {
                "1" => x => x.EmailAddress,
                "2" => x => x.FullName,
                _ => x => x.EmailAddress
            };

        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            int? pageIndex = 1,
            int? pageSize = 5,
            bool? isAscending = true)
        {
            Expression<Func<AccountMember, bool>> filter = searchValue == null ? null : a => a.EmailAddress.Contains(searchValue);

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.AccountRepository.Get(
                    filter: filter,
                    pageIndex: pageIndex,
                    orderBy: keySelector,
                    pageSize: pageSize,
                    isAscending: isAscending,
                    includeProperties: "");
            var response = mapper.Map<IEnumerable<AccountResponse>>(result);
            if (response == null)
                return NotFound("Account list is empty");
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(string id)
        {
            var response = unitOfWork.AccountRepository.GetByID(id);
            if (response == null)
                return NotFound("Account does not exist or has been deleted");
            return Ok(response);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult CreateAccount([FromBody] CreateAccRequest request)
        {
            var user = new AccountMember
            {
                EmailAddress = request.EmailAddress,
                MemberPassword = request.MemberPassword,
                MemberRole = request.MemberRole
            };

            unitOfWork.AccountRepository.Insert(user);
            unitOfWork.Save();

            var response = mapper.Map<AccountResponse>(user);

            return CreatedAtAction(nameof(GetAccountById), new { id = user.MemberId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(string id, AccountRequest accountRequest)
        {
            if (!unitOfWork.AccountRepository.IsExistAccount(id))
            {
                return NotFound("Account does not exist or has been deleted");
            }

            var request = unitOfWork.AccountRepository.GetByID(id);
            mapper.Map(accountRequest, request);

            unitOfWork.AccountRepository.Update(request);
            unitOfWork.Save();

            var response = mapper.Map<AccountMember, AccountResponse>(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(string id)
        {
            var response = unitOfWork.AccountRepository.GetByID(id);
            if (response == null)
                return NotFound("Account have been deleted or does not exist");

            unitOfWork.AccountRepository.Delete(response);
            unitOfWork.Save();
            return Ok("Account have been deleted");
        }
    }
}
