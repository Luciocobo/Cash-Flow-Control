using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DailySummaryService.Domain
{
    public class DailyBalance
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal Balance => TotalCredits - TotalDebits;
        public DateTime UpdatedAt { get; set; }
    }
}
