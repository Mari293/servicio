using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Entities
{
    public class Invoice_Detail
    {
        public int ID { set; get; }
        public int Id_Invoice { set; get; }
        public string Description { set; get; }
        public float Value { set; get; }
    }
}
