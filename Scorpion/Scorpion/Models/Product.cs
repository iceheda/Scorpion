using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpion.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameOfProduct { get; set; }
        public int Quantity { get; set; }
        public string DateOfIssue { get; set; }
        public string ExpirationDate { get; set; }
        public int NotificationDay { get; set; }
        public string Comment { get; set; }
    }
}
