using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mero_Kharcha.Models;
using static MudBlazor.CategoryTypes;

namespace Mero_Kharcha.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly string transactionsFilePath = Path.Combine(AppContext.BaseDirectory, "Transactions.json");

        public async Task<List<Transaction>> RetrieveTransactionAsync()
        {
            try
            {
                if (!File.Exists(transactionsFilePath))
                {
                    // If the file does not exist, create it with an empty list
                    var emptyList = new List<Transaction>();
                    await SaveTransactionAsync(emptyList);
                    return emptyList;
                }

                var json = await File.ReadAllTextAsync(transactionsFilePath);
                return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error while retrieving transactions : {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            try
            {
                var transactions = await RetrieveTransactionAsync();
                transaction.TransID = transactions.Count > 0 ? transactions.Max(t => t.TransID) + 1 : 1;
                transactions.Add(transaction);
                await SaveTransactionAsync(transactions);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"Error while adding transactions: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }

        private async Task SaveTransactionAsync(List<Transaction> transaction)
        {
            try
            {
                var json = JsonSerializer.Serialize(transaction, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(transactionsFilePath, json);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error while saving transactions: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }


        public async Task<decimal> GetTotalInflowAsync()
        {
            try
            {
                var transactions = await RetrieveTransactionAsync();
                return transactions.Where(t => t.TransType == "Inflow").Sum(t => Convert.ToDecimal(t.TransAmount));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Total inflow balance: {ex.Message}");
                return 0;
            }
        }

        // Method to calculate total outflow
        public async Task<decimal> GetTotalOutflowAsync()
        {
            try
            {
                var transactions = await RetrieveTransactionAsync();
                return transactions.Where(t => t.TransType == "Outflow").Sum(t => Convert.ToDecimal(t.TransAmount));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Total outflow balance: {ex.Message}");
                return 0;
            }
        }

        // Method to calculate current balance
        public async Task<decimal> GetCurrentBalanceAsync()
        {
            try
            {
                var inflow = await GetTotalInflowAsync();
                var outflow = await GetTotalOutflowAsync();

                return inflow - outflow;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Current balance: {ex.Message}");
                return 0;
            }

        }
    }
}
