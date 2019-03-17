using System;
using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class Product
    {
        
        public int ID { get; set; }

        [StringLength(100)]
        public String Name { get; set; }

        [StringLength(100)]
        public String Desc { get; set; }

    }
}