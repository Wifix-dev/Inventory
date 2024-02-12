using AutoMapper;
using Inventory.DTOs;
using Inventory.DTOs.Category;
using Inventory.DTOs.InventoryMovement;
using Inventory.DTOs.InventoryStock;
using Inventory.DTOs.MovementType;
using Inventory.DTOs.Product;
using Inventory.DTOs.Supplier;
using Inventory.Entities;
using Microsoft.Data.SqlClient;

namespace Inventory.WebAPI.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //category
            CreateMap<CategoryToCreateDTOs,Category>();
            CreateMap<CategoryToEditDTO,Category>();
            CreateMap<Category,CategoryToListDTO>();

            //InventoryMovement
            CreateMap<InventoryMovementToCreateDto,InventoryMovement>();
            CreateMap<InventoryMovementToEditDto, InventoryMovement>();
            CreateMap<InventoryMovement,InventoryMovementToListDto>()
            .ForMember(dest => dest.MovementTypeName, opt=> opt.MapFrom(src => src.MovementType.Name))
            .ForMember(dest => dest.ProductName, opt=> opt.MapFrom(src=> src.Product.Name));

            //InventoryStock
            CreateMap<InventoryStockToCreateDto,InventoryStock>();
            CreateMap<InventoryStockToEditDto,InventoryStock>();
            CreateMap<InventoryStock,InventoryStockToListDto>()
            .ForMember(dest => dest.ProductName, opt=>opt.MapFrom(src => src.Product.Name));

            //MovementType
            CreateMap<MovementTypeToCreateDto,MovementType>();
            CreateMap<MovementTypeToEditDto,MovementType>();
            CreateMap<MovementType,MovementTypeToListDto>();

            //Product
            CreateMap<ProductToCreateDto,Product>();
            CreateMap<ProductToEditDto,Product>();
            CreateMap<Product,ProductToListDto>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src=> src.Supplier.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src=> src.Category.Name));

            //Supplier
            CreateMap<SupplierToCreateDto,Supplier>();
            CreateMap<SupplierToEditDto,Supplier>();
            CreateMap<Supplier,SupplierToListDto>();            
        }
        
    }
}