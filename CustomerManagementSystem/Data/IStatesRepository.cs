using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Domain;

namespace CustomerManagementSystem.Data
{
    public interface IStatesRepository
    {
        Task<List<State>> GetStatesAsync();
    }
}
