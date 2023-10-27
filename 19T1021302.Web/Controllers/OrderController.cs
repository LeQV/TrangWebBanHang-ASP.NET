using _19T1021302.BusinessLayer;
using _19T1021302.BusinessLayers;
using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021302.Web.Models;
namespace _19T1021302.Web.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        private const string SHOPPING_CART = "ShoppingCart";
        private const string ERROR_MESSAGE = "ErrorMessage";
        private const string ORDER_SEARCH = "OrderSearch";
        private const int PAGE_SIZE = 4;

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //TODO: Code chức năng tìm kiếm, phân trang cho đơn hàng
            OrderSearchInput condition = Session[ORDER_SEARCH] as OrderSearchInput;
            if (condition == null)
            {
                condition = new OrderSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    Status = -99
                };
            }

            return View(condition);
        }
        public ActionResult Search(OrderSearchInput condition)
        {
            int rowCount = 0;
            var data = OrderService.ListOrders(condition.Page, condition.PageSize, condition.Status, condition.SearchValue, out rowCount);
            OrderSearchOutput result = new OrderSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                Status = condition.Status,
                RowCount = rowCount,
                Data = data
            };
            Session[ORDER_SEARCH] = result;
            return View(result);
        }
        /// <summary>
        /// Xem thông tin và chi tiết của đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {      
            var Order = OrderService.GetOrder(id);
            if (Order == null)
                return RedirectToAction("Index");
            var ListDetails = OrderService.ListOrderDetails(id);
            var ViewModel = new DetailOrderView()
            {
                OrderID = Order.OrderID,
                CustomerID = Order.CustomerID,
                OrderTime = Order.OrderTime,
                EmployeeID = Order.EmployeeID,
                AcceptTime = Order.AcceptTime,
                CustomerAddress = Order.CustomerAddress,
                CustomerContactName = Order.CustomerContactName,
                CustomerEmail = Order.CustomerEmail,
                CustomerName = Order.CustomerName,
                EmployeeFullName = Order.EmployeeFullName,
                FinishedTime = Order.FinishedTime,
                ShippedTime = Order.ShippedTime,
                ShipperID = Order.ShipperID,
                ShipperName = Order.ShipperName,
                ShipperPhone = Order.ShipperPhone,
                Status = Order.Status,
                ListProducts = ListDetails
            };
            //TODO: Code chức năng lấy và hiển thị thông tin của đơn hàng và chi tiết của đơn hàng

            return View(ViewModel);
        }
        /// <summary>
        /// Giao diện Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("EditDetail/{orderID}/{productID}")]
        public ActionResult EditDetail(int orderID = 0, int productID = 0)
        {
            //TODO: Code chức năng để lấy chi tiết đơn hàng cần edit
            var orderDetail = OrderService.GetOrderDetail(orderID, productID);
            var order = OrderService.GetOrder(orderID);
            if (order == null)
            {
                return RedirectToAction("Index");
            }
            if (orderDetail == null)
            {
                var Order = OrderService.GetOrder(orderID);
                var ListDetails = OrderService.ListOrderDetails(orderID);
                var ViewModel = new DetailOrderView()
                {
                    OrderID = Order.OrderID,
                    CustomerID = Order.CustomerID,
                    OrderTime = Order.OrderTime,
                    EmployeeID = Order.EmployeeID,
                    AcceptTime = Order.AcceptTime,
                    CustomerAddress = Order.CustomerAddress,
                    CustomerContactName = Order.CustomerContactName,
                    CustomerEmail = Order.CustomerEmail,
                    CustomerName = Order.CustomerName,
                    EmployeeFullName = Order.EmployeeFullName,
                    FinishedTime = Order.FinishedTime,
                    ShippedTime = Order.ShippedTime,
                    ShipperID = Order.ShipperID,
                    ShipperName = Order.ShipperName,
                    ShipperPhone = Order.ShipperPhone,
                    Status = Order.Status,
                    ListProducts = ListDetails
                };
                return View("Details", ViewModel);

            }
            return View(orderDetail);
        }
        /// <summary>
        /// Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult UpdateDetail(OrderDetail data)
        {
            //TODO: Code chức năng để cập nhật chi tiết đơn hàng
            var order = OrderService.GetOrder(data.OrderID);
            if(order.Status==OrderStatus.CANCEL || order.Status==OrderStatus.FINISHED)
                return Json(ApiResult.CreateFailResult("Trạng thái đơn hàng hiện tại không cho phép chỉnh sửa"), JsonRequestBehavior.AllowGet);
            if (data.Quantity <= 0)
                return Json(ApiResult.CreateFailResult("Số lượng không hợp lệ"), JsonRequestBehavior.AllowGet);

            var Product = ProductDataService.GetProduct(data.ProductID);
            OrderService.SaveOrderDetail(data.OrderID, data.ProductID, data.Quantity, data.SalePrice);
            return Json(ApiResult.CreateSuccessResult(""), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xóa 1 chi tiết trong đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteDetail/{orderID}/{productID}")]
        public ActionResult DeleteDetail(int orderID = 0, int productID = 0)
        {
            //TODO: Code chức năng xóa 1 chi tiết trong đơn hàng
            var order = OrderService.GetOrder(orderID);
            if (order == null)
                return RedirectToAction("Index");
            var orderDetail = OrderService.GetOrderDetail(orderID, productID);
            if(orderDetail==null)
                return RedirectToAction($"Details/{orderID}");
            if (order.Status==1 || order.Status==2 || order.Status==3)
                OrderService.DeleteOrderDetail(orderID, productID);
            return RedirectToAction($"Details/{orderID}");
        }
        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            //TODO: Code chức năng để xóa đơn hàng (nếu được phép xóa)
            var Order = OrderService.GetOrder(id);
            if (Order == null)
            {
                return RedirectToAction("Index");
            }

            var result = OrderService.DeleteOrder(Order.OrderID);
            if (result == false)
            {
                return RedirectToAction($"Details/{id}");
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Chấp nhận đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Accept(int id = 0)
        {
            //TODO: Code chức năng chấp nhận đơn hàng (nếu được phép)
            var result = OrderService.AcceptOrder(id);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Xác nhận chuyển đơn hàng cho người giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Shipping(int id = 0, int shipperID = 0)
        {
            //TODO: Code chức năng chuyển đơn hàng sang trạng thái đang giao hàng (nếu được phép)

            if (Request.HttpMethod == "GET")
            {
                ViewBag.OrderID = id;
                return View();
            }

            //return View();
            //return Content("chuỗi thông báo");
            //return RedirectToAction()
            //return Json(object, ...)

            var data = CommonDataService.GetShipper(shipperID);
            if (data == null || shipperID == 0)
                return Json(ApiResult.CreateFailResult("Shipper không hợp lệ"), JsonRequestBehavior.AllowGet);

            bool result = OrderService.ShipOrder(id, shipperID);
            if (!result)
                return Json(ApiResult.CreateFailResult("Không chuyển được trạng thái sang giao hàng"), JsonRequestBehavior.AllowGet);

            return Json(ApiResult.CreateSuccessResult(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Ghi nhận hoàn tất thành công đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Finish(int id = 0)
        {
            //TODO: Code chức năng ghi nhận hoàn tất đơn hàng (nếu được phép)
            var resutlt = OrderService.FinishOrder(id);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Hủy bỏ đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Cancel(int id = 0)
        {
            //TODO: Code chức năng hủy đơn hàng (nếu được phép)
            var result = OrderService.CancelOrder(id);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reject(int id = 0)
        {
            //TODO: Code chức năng từ chối đơn hàng (nếu được phép)
            OrderService.RejectOrder(id);
            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Sử dụng 1 biến session để lưu tạm giỏ hàng (danh sách các chi tiết của đơn hàng) trong quá trình xử lý.
        /// Hàm này lấy giỏ hàng hiện đang có trong session (nếu chưa có thì tạo mới giỏ hàng rỗng)
        /// </summary>
        /// <returns></returns>
        private List<OrderDetail> GetShoppingCart()
        {
            List<OrderDetail> shoppingCart = Session[SHOPPING_CART] as List<OrderDetail>;
            if (shoppingCart == null)
            {
                shoppingCart = new List<OrderDetail>();
                Session[SHOPPING_CART] = shoppingCart;
            }
            return shoppingCart;
        }
        /// <summary>
        /// Giao diện lập đơn hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = TempData[ERROR_MESSAGE] ?? "";
            return View(GetShoppingCart());
        }
        /// <summary>
        /// Tìm kiếm mặt hàng để bổ sung vào giỏ hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult SearchProducts(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(page, PAGE_SIZE, searchValue, 0, 0, out rowCount);
            ViewBag.Page = page;
            return View(data);
        }
        /// <summary>
        /// Bổ sung thêm hàng vào giỏ hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(OrderDetail data)
        {
            if (data == null)
            {
                TempData[ERROR_MESSAGE] = "Dữ liệu không hợp lệ";
                return RedirectToAction("Create");
            }
            if (data.SalePrice <= 0 || data.Quantity <= 0)
            {
                TempData[ERROR_MESSAGE] = "Giá bán và số lượng không hợp lệ";
                return RedirectToAction("Create");
            }

            List<OrderDetail> shoppingCart = GetShoppingCart();
            var existsProduct = shoppingCart.FirstOrDefault(m => m.ProductID == data.ProductID);

            if (existsProduct == null) //Nếu mặt hàng cần được bổ sung chưa có trong giỏ hàng thì bổ sung vào giỏ
            {

                shoppingCart.Add(data);
            }
            else //Trường hợp mặt hàng cần bổ sung đã có thì tăng số lượng và thay đổi đơn giá
            {
                existsProduct.Quantity += data.Quantity;
                existsProduct.SalePrice = data.SalePrice;
            }
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ hàng
        /// </summary>
        /// <param name="id">Mã mặt hàng</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int id = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            int index = shoppingCart.FindIndex(m => m.ProductID == id);
            if (index >= 0)
                shoppingCart.RemoveAt(index);
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCart()
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Khởi tạo đơn hàng (với phần thông tin chi tiết của đơn hàng là giỏ hàng đang có trong session)
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Init(int customerID = 0, int employeeID = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                TempData[ERROR_MESSAGE] = "Không thể tạo đơn hàng với giỏ hàng trống";
                return RedirectToAction("Create");
            }

            if (customerID == 0 || employeeID == 0)
            {
                TempData[ERROR_MESSAGE] = "Vui lòng chọn khách hàng và nhân viên phụ trách";
                return RedirectToAction("Create");
            }

            int orderID = OrderService.InitOrder(customerID, employeeID, DateTime.Now, shoppingCart);

            Session.Remove(SHOPPING_CART); //Xóa giỏ hàng 

            return RedirectToAction($"Details/{orderID}");
        }
    }

}