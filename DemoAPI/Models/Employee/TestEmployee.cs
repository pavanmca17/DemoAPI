using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class TestEmployee
    {
        
        public int ID { get; set; }

        [StringLength(50)]
        public String Name { get; set; }
    }
}
