using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<BrandDTO,Brand>().ReverseMap();
            CreateMap<ChipDTO,Chip>().ReverseMap();
            CreateMap<ScreenDTO,Screen>().ReverseMap();
            CreateMap<BatteryDTO,Battery>().ReverseMap();
            CreateMap<OperatingSystemDTO,OperatingSystemProduct>().ReverseMap();
            CreateMap<ChargingPortDTO,ChargingPort>().ReverseMap();
            CreateMap<MemoryStickDTO,MemoryStick>().ReverseMap();
            CreateMap<SIMTypeDTO,SIMType>().ReverseMap();
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<RamMemoryDTO,RAMMemory>().ReverseMap();
            CreateMap<ROMMemoryDTO,ROMMemory>().ReverseMap();
            CreateMap<ColorDTO,ColorMS>().ReverseMap();
            CreateMap<ProductDTO,Product>().ReverseMap();
            CreateMap<PaymentDTO,Payment>().ReverseMap();
        }
    }
}
