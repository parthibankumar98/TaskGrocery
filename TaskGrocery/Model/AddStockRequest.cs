using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskGrocery.Model
{
    public class AddStockRequest
    {

        public int GroceryId { get; set; }
        public string GroceryName { get; set; }
        public string Category { get; set; }
        public int DiscountId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int StockId { get; set; }
        public int StockQuantity { get; set; }
    }
}
