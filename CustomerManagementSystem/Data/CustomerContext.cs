using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Domain;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Data
{
    public class CustomerContext:DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
            //DbContextOptions startup classında kullanıcazbu startupta kullanrken bir connection string vericez. veri tabanının bağlantısının ne olacağını belirtmek için yazıyoruz startupta yazılan optionslar braya yansıyacak.
        }

        //dependency ile işyaparken yapıcı metodu eklemek zorundayız
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<State> States { get; set; }
    }
}
