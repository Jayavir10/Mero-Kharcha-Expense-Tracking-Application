using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mero_Kharcha.Models;


namespace Mero_Kharcha.Services
{
    public interface IDebtServices
    {
        Task<List<Debt>> RetrieveDebtAsync();

        Task AddDebtAsync(Debt debts);

        Task<decimal> GetTotalDebtAsync();

        Task<decimal> GetClearedDebtAsync();

        Task<decimal> GetPendingDebtAsync();

        Task UpdateDebtAsync(Debt updatedDebt);
    }
}
