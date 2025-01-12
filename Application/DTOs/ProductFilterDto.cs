using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductFilterDto
    {
        public string? Name { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string? CategoryName { get; set; } = null;
        public string? SortBy { get; set; } = "id";
        public bool SortDescending { get; set; } = true;
    }
}
