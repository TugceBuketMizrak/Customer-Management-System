using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagementSystem.Data
{
    public class StatesRepository:IStatesRepository
    {
        private CustomerContext _customerContext;
        private ILogger _logger;
        public StatesRepository(CustomerContext customerContext, ILoggerFactory loggerFACTORY)
        {
            _customerContext = customerContext;
            _logger = loggerFACTORY.CreateLogger("StatesRepository");
        }

        public async Task<List<State>> GetStatesAsync()
        {
            return await _customerContext.States.OrderBy(c => c.Abbreviation).ToListAsync();
        }
    }
}
