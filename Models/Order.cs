using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tuto4.Models;

namespace MovieStore.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }
        [Display(Name = "User ID")]
        public string UserID { get; set; }
        [Display(Name = "Delivery Name")]
        public string DeliveryName { get; set; }
        [Display(Name = "Delivery Address")]
        public Address DeliveryAddress { get; set; }
        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public List<OrderLine> OrderLines { get; set; }

    }
}