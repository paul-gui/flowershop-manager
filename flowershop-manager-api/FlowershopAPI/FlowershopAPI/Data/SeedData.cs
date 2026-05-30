using FlowerShopAPI.Models;

namespace FlowerShopAPI.Data;

public static class SeedData
{
    public static readonly Destination[] Destinations =
    [
        new()
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Florărie"
        },
        new()
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "En Gros"
        }
    ];
}

