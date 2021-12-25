using AutoMapper;
using HepsiBurada.Model.DbEntity;
using HepsiBurada.Model.Model;

namespace HepsiBurada.Model.Mapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Campaign, CampaignModel>().ReverseMap();
        }
    }
}
