using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Mero_Kharcha.Models;


namespace Mero_Kharcha.Services
{
    public class DebtServices : IDebtServices
    {
        private readonly string transactionsFilePath = Path.Combine(AppContext.BaseDirectory, "Debt.json");

        public async Task<List<Debt>> RetrieveDebtAsync()
        {
            try
            {
                if (!File.Exists(transactionsFilePath))
                {
                    // If the file does not exist, create it with an empty list
                    var emptyList = new List<Debt>();
                    await SaveDebtAsync(emptyList);
                    return emptyList;
                }

                var json = await File.ReadAllTextAsync(transactionsFilePath);
                return JsonSerializer.Deserialize<List<Debt>>(json) ?? new List<Debt>();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error while retrieving transactions : {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }

        public async Task AddDebtAsync(Debt debts)
        {
            try
            {
                var allDebts = await RetrieveDebtAsync();
                debts.DebtID = allDebts.Count > 0 ? allDebts.Max(t => t.DebtID) + 1 : 1;
                allDebts.Add(debts);
                await SaveDebtAsync(allDebts);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"Error while adding transactions: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }

        private async Task SaveDebtAsync(List<Debt> debts)
        {
            try
            {
                var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(transactionsFilePath, json);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error while saving transactions: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }


        public async Task<decimal> GetTotalDebtAsync()
        {
            try
            {
                var allDebts = await RetrieveDebtAsync();
                return allDebts.Sum(d => Convert.ToDecimal(d.DebtAmount));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Total debt value: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetClearedDebtAsync()
        {
            try
            {
                var allDebts = await RetrieveDebtAsync();
                return allDebts.Where(d => d.DebtStatus == "Cleared").Sum(d => Convert.ToDecimal(d.DebtAmount));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Cleared debt values: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetPendingDebtAsync()
        {
            try
            {
                var allDebts = await RetrieveDebtAsync();
                return allDebts.Where(d => d.DebtStatus == "Pending").Sum(d => Convert.ToDecimal(d.DebtAmount));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Pending debt values: {ex.Message}");
                return 0;
            }
        }

        public async Task UpdateDebtAsync(Debt updatedDebt)
        {
            try
            {
                var allDebts = await RetrieveDebtAsync();
                var existingDebt = allDebts.FirstOrDefault(d => d.DebtID == updatedDebt.DebtID);

                if (existingDebt != null)
                {
                    // Update the properties of the existing debt with the new values
                    existingDebt.DebtStatus = updatedDebt.DebtStatus;

                    await SaveDebtAsync(allDebts);
                }
                else
                {
                    Console.WriteLine($"Debt with ID {updatedDebt.DebtID} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating debt: {ex.Message}");
                throw;
            }
        }
    }
}
