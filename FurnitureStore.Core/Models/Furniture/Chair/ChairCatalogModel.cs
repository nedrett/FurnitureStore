﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FurnitureStore.Core.Models.Furniture.Chair
{
    public class ChairCatalogModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
