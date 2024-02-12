using AutoMapper;
using Inventory.DTOs;
using Inventory.DTOs.Category;
using Inventory.Entities;

namespace Inventory.WebAPI.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryToCreateDTOs,Category>();
            CreateMap<CategoryToEditDTO,Category>();
            CreateMap<Category,CategoryToListDTO>();
        }
        
    }
}