using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CatFoodWeb.Models
{
    public class classProduct
    {
        [DisplayName("編號")]
        public int fProductID { get; set; }

        [DisplayName("商品名稱")]
        public string fProductName { get; set; }

        [DisplayName("描述")]
        public string fProductDescription { get; set; }

        [DisplayName("類別")]
        public string fCategoryName { get; set; }

        [DisplayName("供應商")]
        public string fSupplierName { get; set; }

        [DisplayName("原價")]
        public int fUnitPrice { get; set; }

        [DisplayName("特價")]
        public int fNewPrice { get; set; }

        [DisplayName("每包重量(克)")]
        public int fQtyPerUnit { get; set; }

        [DisplayName("庫存量(克)")]
        public int fInStore { get; set; }

        [DisplayName("商品圖")]
        public string fPhoto { get; set; }

        [DisplayName("建立日期")]
        public string fCreateDate { get; set; }

        [DisplayName("修改日期")]
        public string fUpdateDate { get; set; }

        [DisplayName("上/下架")]
        public string fActive { get; set; }

        public int fCategoryID { get; set; }
        public int fSupplierID { get; set; }
    }
}