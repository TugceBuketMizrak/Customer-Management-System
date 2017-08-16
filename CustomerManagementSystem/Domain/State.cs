using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomerManagementSystem.Domain
{
    public class State
    {
        public int Id { get; set; }

        [StringLength(2)]
        public string Abbreviation { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
    }

    
}
