using AutoMapper;
using NET171462.ProductManagement.Repo.Models;
using SE171762.ProductManagement.API.Services.Account;
using SE171762.ProductManagement.API.Services.Category;
using SE171762.ProductManagement.API.Services.Product;


namespace SE171762.ProductManagement.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<Product, ProductResponse>()
                .ForMember(des => des.CategoryName, 
                 act => act.MapFrom(src => src.Category.CategoryName));
            CreateMap<ProductRequest, Product>().ReverseMap();

            //Category
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>()
               .ForMember(des => des.Products,
                act => act.MapFrom(src => src.Products));

            //Account
            CreateMap<AccountMember, AccountResponse>().ReverseMap();
            CreateMap<AccountRequest, AccountMember>().ReverseMap();
        }
    }
}
