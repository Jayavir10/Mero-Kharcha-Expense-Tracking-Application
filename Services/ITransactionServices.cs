using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mero_Kharcha.Models;


namespace Mero_Kharcha.Services
{
    public interface ITransactionServices
    {
        Task AddTransactionAsync(Transaction transaction);
        Task<List<Transaction>> RetrieveTransactionAsync();
        Task<decimal> GetTotalInflowAsync();
        Task<decimal> GetTotalOutflowAsync();
        Task<decimal> GetCurrentBalanceAsync();
    }
}
