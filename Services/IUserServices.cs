using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mero_Kharcha.Models;

namespace Mero_Kharcha.Services
{
    public interface IUserServices
    {
        Task<List<User>> RetrieveUsersAsync();
        Task CreateUserAsync(User user);

    }
}
