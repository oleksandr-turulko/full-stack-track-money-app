using AutoMapper;
using TrackMoney.Data.Models.Dtos.Transactions;
using TrackMoney.Data.Models.Entities;

namespace TrackMoney.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Transaction, TransactionViewDto>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString())) // Assuming TransactionType is an enum
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
        }
    }
}
