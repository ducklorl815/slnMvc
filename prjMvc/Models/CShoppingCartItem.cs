using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMvc.Models
{
    public class CShoppingCartItem
    {
        [DisplayName("購買品項")]
        public int productId { get; set; }
        [DisplayName("採購量")]
        public int count { get; set; }
        [DisplayName("成交價")]
        public decimal price { get; set; }
    }
}