using eShop.API.Dto;
using eShop.API.Entity;

namespace eShop.API.Mapper;

public static class ProductMapper
{
    public static Product MapToEntity(this CreateUpdateProductDto dto) =>
        new()
        {
            Name = dto.Name,
            Price = dto.Price
        };
}