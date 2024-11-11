using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskGrocery.Model;

namespace TaskGrocery.Model
{
    public class Stocklist
    {

        public int StockId { get; set; }
        public int GroceryId { get; set; }
        public int StockQuantity { get; set; }

    }
}
