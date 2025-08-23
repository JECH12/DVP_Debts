using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        public int DebtId { get; set; }
        public double Payment_Amount { get; set; }
        public DateTime Payment_Date { get; set; } = DateTime.Now;
    }
}
