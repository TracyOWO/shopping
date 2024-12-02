namespace shopping.Controllers
{

    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string id) //商品明細
        {
            using var product = new z_sqlProducts();
            var model = product.GetData(id);
            return View(model);
        }
    }
}