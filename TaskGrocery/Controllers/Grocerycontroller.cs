using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskGrocery.Service;
using TaskGrocery.Model;

namespace TaskGrocery.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class GroceryController : ControllerBase
    {
        private readonly IGroceryService _groceryService;

        public GroceryController(IGroceryService groceryService)
        {
            _groceryService = groceryService;
        }
        [HttpGet("names-and-categories")]
        public async Task<ActionResult<IEnumerable<GroceryNameAndCategory>>> GetGroceryNamesAndCategories()
        {
            var groceries = await _groceryService.GetGroceryNamesAndCategoriesAsync();
            return Ok(groceries);
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<GroceryDetails>>> GetGroceryDetails()
        {
            var groceries = await _groceryService.GetGroceryDetailsAsync();
            return Ok(groceries);
        }

        [HttpGet("grocery/{id}")]
        public async Task<ActionResult<GroceryDetails>> GetGroceryDetailsById(int id)
        {
            var grocery = await _groceryService.GetGroceryDetailsByIdAsync(id);

            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
        {
            var result = await _groceryService.AddStockAsync(request);
            if (result)

                return Ok(new { message = "Stock added successfully." });
            else
                return BadRequest("Failed to add stock.");
        }

        [HttpDelete("delete-stock/{groceryId}")]
        public async Task<IActionResult> DeleteStock(int groceryId)
        {
            var result = await _groceryService.DeleteStockAsync(groceryId);
            if (result)
                return Ok(new { message = "Stock deleted successfully." });

            else
                return BadRequest("Failed to delete stock.");
        }
        [HttpGet("grocery/details/{id}")]
        public async Task<ActionResult<GroceryDetails>> GetGroceryDetails(int id)
        {
            var grocery = await _groceryService.GetGroceryDetailsByIdAsync(id);

            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }
        [HttpPut("update-stock/{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockRequest request)
        
        {
            // Set the GroceryId from the URL
            request.GroceryId = request.GroceryId;

            // Call the service to update the stock
            bool result = await _groceryService.UpdateStockAsync(request);

            if (result)
                return Ok(new { message = "Stock updated successfully." });
            else
                return BadRequest("Failed to update stock.");
        }

         







    }
}
