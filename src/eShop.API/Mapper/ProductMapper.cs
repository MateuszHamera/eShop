using eShop.API.Dto;
using eShop.API.Entity;

namespace eShop.API.Mapper;

public static class ProductMapper
{
    public static Product MapToEntity(this ProductDto dto) =>
        new()
        {
            Name = dto.Name,
            Price = dto.Price
        };

    public static ProductDto MapToDto(this Product entity) => 
        new(entity.Name, entity.Price);
}