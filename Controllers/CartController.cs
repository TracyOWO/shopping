using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace shopping.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// 購物車列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            using var cart = new z_sqlCarts();
            var model = cart.GetDataList();
            return View(model);
        }

        /// <summary>
        /// 加入購物車
        /// </summary>
        /// <param name="id">商品編號</param>
        /// <param name="prodSpec">商品規格</param>
        /// <param name="qty">數量</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddCart(string id, string prodSpec = "", int qty = 1)
        {
            using var cart = new z_sqlCarts();
            cart.AddCart(id, prodSpec, qty);

            string str_action = SessionService.StringValue1;
            SessionService.StringValue1 = "";

            if (str_action == "add")
                return RedirectToAction("Index", "Cart", new { area = "" });
            if (str_action == "buy")
                return RedirectToAction("Payment", "Cart", new { area = "" });

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// 更新購物車
        /// </summary>
        /// <param name="id">商品編號</param>
        /// <param name="qty">數量</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateCart(string id, int qty)
        {
            using var cart = new z_sqlCarts();
            cart.UpdateCart(id, qty);
            return RedirectToAction("Index", "Cart", new { area = "" });
        }

        /// <summary>
        /// 更新購物車數量
        /// </summary>
        /// <param name="prodNo">商品編號</param>
        /// <param name="qty">數量</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult UpdateQty(string prodNo, int qty)
        {
            using var cart = new z_sqlCarts();
            cart.UpdateCart(prodNo, qty);
            var CartTotal = cart.GetCartTotals();
            return Json(new { success = true, value = CartTotal });
        }

        /// <summary>
        /// 刪除購物車
        /// </summary>
        /// <param name="id">購物車Id</param>
        [HttpGet]
        public IActionResult DeleteCart(int id)
        {
            using var cart = new z_sqlCarts();
            cart.DeleteCart(id);
            return RedirectToAction("Index", "Cart", new { area = "" });
        }

        /// <summary>
        /// 加入購物車
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddToCart()
        {
            string str_button = "";
            object obj_add = Request.Form["add"];
            object obj_buy = Request.Form["buy"];
            string str_add = (obj_add == null) ? "" : obj_add.ToString();
            string str_buy = (obj_buy == null) ? "" : obj_buy.ToString();
            if (str_add == "加入購物車") str_button = "add";
            if (str_buy == "立即結帳") str_button = "buy";

            if (str_button != "add" && str_button != "buy")
                return RedirectToAction("Index", "Home", new { area = "" });

            if (str_button == "buy" && !SessionService.IsLogin)
                return RedirectToAction("Login", "User", new { area = "" });

            SessionService.StringValue1 = str_button;

            string str_prod_spec = "";
            object obj_text = Request.Form["qtybutton"];
            string str_qty = (obj_text == null) ? "1" : obj_text.ToString();
            int int_qty = int.Parse(str_qty);
            obj_text = Request.Form["ProdNo"];
            string str_prod_no = (obj_text == null) ? string.Empty : obj_text.ToString();

            using var prodProperty = new z_sqlProductPropertys();
            List<ProductPropertys> PropertyList = prodProperty.GetProductPropertys(str_prod_no);
            foreach (var item in PropertyList)
            {
                obj_text = Request.Form[item.PropertyNo];
                if (obj_text != null)
                {
                    string str_prop_value = obj_text.ToString();
                    if (!string.IsNullOrEmpty(str_prod_spec)) str_prod_spec += ",";
                    str_prod_spec += item.PropertyName + ":" + str_prop_value;
                }
            }

            return RedirectToAction("AddCart", "Cart", new { area = "", id = str_prod_no, prodSpec = str_prod_spec, qty = int_qty });
        }
    }
}