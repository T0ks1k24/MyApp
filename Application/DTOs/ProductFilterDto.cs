using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductFilterDto
    {
        public string? nameFilter { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string? Sort { get; set; } = null;
        public bool? InStock { get; set; } = true;
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
