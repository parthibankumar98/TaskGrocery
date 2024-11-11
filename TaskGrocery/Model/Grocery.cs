using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskGrocery.Model;

namespace TaskGrocery.Model
{
    public class Grocery
    {

        public int GroceryId { get; set; }
        public string GroceryName { get; set; }
        public string Category { get; set; }


    }
}
