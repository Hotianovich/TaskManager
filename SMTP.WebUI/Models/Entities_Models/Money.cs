using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SMTP.WebUI.Models.Entities_Models
{
    public class Money
    {
        [Key]
        public int MoneyId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DayMoney { get; set; }
        public decimal MyMoney { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}