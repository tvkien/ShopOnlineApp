using AutoMapper;
using ShopOnline.BackendApi.Models;

namespace ShopOnline.BackendApi
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            InitialMapping();
        }

        private void InitialMapping()
        {
            CreateMap<ProductCreateRequest, Domains.ProductCreateRequest>();
            CreateMap<ProductUpdateRequest, Domains.ProductUpdateRequest>();
            CreateMap<LoginRequest, Domains.LoginRequest>();
            CreateMap<RegisterRequest, Domains.RegisterRequest>();
        }
    }
}