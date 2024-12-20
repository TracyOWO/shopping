using System;
using System.Collections.Generic;

namespace shopping.Models  //訂單明細欄位設定
{
    [ModelMetadataType(typeof(z_metaOrderDetails))]
    public partial class OrderDetails
    {

        [NotMapped]
        [Display(Name = "商品分類")]
        public string? CategoryName { get; set; }
    }
}

public class z_metaOrderDetails
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "訂單編號")]
    public string? ParentNo { get; set; }
    [Display(Name = "廠商編號")]
    public string? VendorNo { get; set; }
    [Display(Name = "分類編號")]
    public string? CategoryNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "商品名稱")]
    public string? ProdName { get; set; }
    [Display(Name = "商品規格")]
    public string? ProdSpec { get; set; }
    [Display(Name = "訂購單價")]
    public int OrderPrice { get; set; }
    [Display(Name = "訂購數量")]
    public int OrderQty { get; set; }
    [Display(Name = "訂購金額")]
    public int OrderAmount { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}