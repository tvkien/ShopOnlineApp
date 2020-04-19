using AutoMapper;
using ShopOnline.WebAdmin.Models;

namespace ShopOnline.WebAdmin
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            InitialMapping();
        }

        private void InitialMapping()
        {
            CreateMap<LoginRequest, Domains.LoginRequest>();
        }
    }
}