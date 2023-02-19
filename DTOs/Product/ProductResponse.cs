﻿namespace JWTAuthAPI.DTOs.Product
{
    public class ProductResponse
    {
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
    }
}
