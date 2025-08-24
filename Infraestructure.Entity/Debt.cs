using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity
{
    public class Debt
    {
        public int Id { get; set; }
        public int DebtorId { get; set; } 
        public int CreditorId { get; set; }
        public double Amount {  get; set; }
        public string? Description { get; set; }
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;
        public int StateId { get; set; }
    }
}
