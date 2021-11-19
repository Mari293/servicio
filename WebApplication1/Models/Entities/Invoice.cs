using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Entities
{
    public class Invoice
    {
        public int ID { set; get; }
        public int Id_Client { set; get; }
        public int Cod { set; get; }
        public List<Invoice_Detail> details { set; get; }
    }
}
