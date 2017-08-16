using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomerManagementSystem.Domain
{
    public class Customer
    {
        //idler olmaz ise entity değerlendirip tablo yapmakta sorun çıakr ve otomatik tabloları yapamaz
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        public State State { get; set; }
        //herbir customera bir state ekleyecek
        //customer.state...
        //bunlar navigasyon pocketsları diyoruz
        public int StateId { get; set; }
        public int Zip { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        //yukarıdaki kod enum değerinin nasıl stringe dönüştürüleceğini belirler
        public Gender Gender { get; set; }
        public ICollection<Order> Orders { get; set; }
        //burada customer ve order arasında bire çok ilişki var
        //order tablosunda her satırda bircustomerid sütunu olacak

        public int OrderCount { get; set; }
    }
    public enum Gender
    {
        Female,
        Male
    }
}
