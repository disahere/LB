﻿namespace ASPMVC1.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string StyledDescription { get; set; }
        public int Quantity { get; set; }
    }
}