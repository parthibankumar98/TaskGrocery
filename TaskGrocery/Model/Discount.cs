using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskGrocery.Model;

namespace TaskGrocery.Model
{
    public class Discount
    {

        public int DiscountId { get; set; }
        public int GroceryId { get; set; }
        public decimal DiscountPercentage { get; set; }


    }
}
