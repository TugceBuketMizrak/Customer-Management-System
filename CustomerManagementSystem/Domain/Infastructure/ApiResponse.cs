using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomerManagementSystem.Domain.Infastructure
{
    public class ApiResponse
    {
        public  bool Status { get; set; }
        public Customer Customer { get; set; }
        public ModelStateDictionary ModalState { get; set; }
    }
}
