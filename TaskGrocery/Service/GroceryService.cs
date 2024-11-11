using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskGrocery.Model;
using TaskGrocery.Data;
using Microsoft.Data.SqlClient;

namespace TaskGrocery.Service
{
    public interface IGroceryService
    {
        Task<List<GroceryDetails>> GetGroceryDetailsAsync();
        Task<GroceryDetails> GetGroceryDetailsByIdAsync(int id);
        Task<List<GroceryDetails>> GetGroceryAsync(int id);
        Task<List<GroceryNameAndCategory>> GetGroceryNamesAndCategoriesAsync();
        Task<bool> AddStockAsync(AddStockRequest request);
        Task<bool> DeleteStockAsync(int groceryId);
        Task<bool> UpdateStockAsync(UpdateStockRequest request);
    }
    public class GroceryService : IGroceryService
    {
        private readonly GroceryDbContext _context;

        public GroceryService(GroceryDbContext context)
        {
            _context = context;
        }
        public async Task<List<GroceryNameAndCategory>> GetGroceryNamesAndCategoriesAsync()
        {
            try
            {
                // Execute stored procedure and map result to GroceryNameAndCategory
                var groceryNameAndCategories = await _context.GroceryNameAndCategory
                                                             .FromSqlRaw("EXEC GetGroceryNamesAndCategories")
                                                             .ToListAsync();

                return groceryNameAndCategories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<List<GroceryDetails>> GetGroceryDetailsAsync()
        {
            try
            {
                return await _context.GroceryDetails
                                     .FromSqlRaw("EXEC GetGroceryDetails")
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<GroceryDetails> GetGroceryDetailsByIdAsync(int id)
        {
            var groceries = await GetGroceryAsync(id);
            return groceries.FirstOrDefault(g => g.GroceryId == id);
        }
        public async Task<List<GroceryDetails>> GetGroceryAsync(int id )
        {
            return await _context.GroceryDetails
                                 .FromSqlRaw("EXEC GetGroceryDetailsById @GroceryId " ,  new SqlParameter("@GroceryId", id))
                                 .ToListAsync();
        }

        public async Task<bool> AddStockAsync(AddStockRequest request)
        {
            try
            {
                // Call the AddGroceryStock stored procedure
                var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC AddGroceryStock @GroceryId, @GroceryName, @Category, @DiscountId, @DiscountPercentage,@StockId, @StockQuantity",
            new SqlParameter("@GroceryId", request.GroceryId),
            new SqlParameter("@GroceryName", request.GroceryName ?? (object)DBNull.Value),
            new SqlParameter("@Category", request.Category ?? (object)DBNull.Value),
            new SqlParameter("@DiscountId", request.DiscountId),
            new SqlParameter("@DiscountPercentage", request.DiscountPercentage),
            new SqlParameter("@StockId", request.StockId),
            new SqlParameter("@StockQuantity", request.StockQuantity));

          

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteStockAsync(int groceryId)
        {
            try
            {
                // Call the DeleteGroceryStock stored procedure
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DeleteGroceryStock @p0",
                    parameters: new object[] { groceryId });

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }



        // Keep only one version of UpdateStockAsync with a return type of Task<bool>
        public async Task<bool> UpdateStockAsync(UpdateStockRequest request)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateGroceryStock @GroceryId, @GroceryName, @Category,@DiscountId, @DiscountPercentage, @StockId,@StockQuantity",
                    new SqlParameter("@GroceryId", request.GroceryId),
                    new SqlParameter("@GroceryName", request.GroceryName ?? (object)DBNull.Value),
                    new SqlParameter("@Category", request.Category ?? (object)DBNull.Value),
                    new SqlParameter("@DiscountPercentage", request.DiscountPercentage),
                    new SqlParameter("@StockQuantity", request.StockQuantity),
                    new SqlParameter("@StockId", request.StockId),
                    new SqlParameter("@DiscountId",request.DiscountId)
            
                );
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

       
    }
}
