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
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string PRODUCT_SEARCH = "ProductSearch";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ProductSearchInput condition = Session[PRODUCT_SEARCH] as ProductSearchInput;
            if (condition == null)
            {
                condition = new ProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0,
                };
            }
            return View(condition);
        }

        public ActionResult Search(ProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out rowCount);
            Models.ProductSearchOutput result = new ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data,
                CategoryID = condition.CategoryID,
                SupplierID = condition.SupplierID,
            };

            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0
            };
            ViewBag.Title = "bổ sung sản phẩm";
            return View(data);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveCreate(Product model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrEmpty(model.ProductName))
                ModelState.AddModelError(nameof(model.ProductName), "Tên không được để trống");
            if (string.IsNullOrEmpty(model.Unit))
                ModelState.AddModelError(nameof(model.Unit), "Đơn vị tính không được để trống");
            if (model.Price <= 0)
                ModelState.AddModelError(nameof(model.Price), "Giá không hợp lệ");
            if (string.IsNullOrWhiteSpace(model.CategoryID.ToString()))
                ModelState.AddModelError(nameof(model.CategoryID), "Vui lòng chọn loại hàng");
            if (string.IsNullOrWhiteSpace(model.SupplierID.ToString()))
                ModelState.AddModelError(nameof(model.SupplierID), "Vui lòng chọn nhà cung cấp");
            if (uploadPhoto != null)
            {
                //var ext = new string[] { ".jpg", ".JPG", ".jpeg", ".JPEG", ".bmp", ".BMP", ".png", ".PNG" };
                
                    string path = Server.MapPath("~/Images/Products");
                    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    model.Photo = $"Images/Products/{fileName}";                 
            }
            else
            {
                ModelState.AddModelError(nameof(model.Photo), "Vui lòng chọn file ảnh");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Thêm sản phẩm";
                return View("Create", model);
            }
            ProductDataService.AddProduct(model);
            return RedirectToAction("Index");
        }

        //public ActionResult Update( ProductViewModel model, HttpPostedFileBase uploadPhoto)
        //{
        //    if (string.IsNullOrEmpty(model.ProductName))
        //        ModelState.AddModelError(nameof(model.ProductName), "Tên không được để trống");
        //    if (string.IsNullOrEmpty(model.Unit))
        //        ModelState.AddModelError(nameof(model.Unit), "Đơn vị tính không được để trống");
        //    if (model.Price <= 0)
        //        ModelState.AddModelError(nameof(model.Price), "Giá không hợp lệ");
        //    if (uploadPhoto != null)
        //    {
        //        string path = Server.MapPath("~/Images/Products");
        //        string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
        //        string filePath = System.IO.Path.Combine(path, fileName);
        //        uploadPhoto.SaveAs(filePath);
        //        model.Photo = $"Images/Products/{fileName}";
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Title = "Chỉnh sửa sản phẩm";
        //        return View("Edit", model);
        //    }
        //    var data = new Product()
        //    {
        //        ProductID = model.ProductID,
        //        ProductName = model.ProductName,
        //        CategoryID = model.CategoryID,
        //        SupplierID = model.SupplierID,
        //        Unit = model.Unit,
        //        Price = model.Price,
        //        Photo = model.Photo,
        //    };
        //    ProductDataService.UpdateProduct(data);
        //    return RedirectToAction("Index");
        //}
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(ProductViewModel viewModel, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrEmpty(viewModel.ProductName))
                ModelState.AddModelError(nameof(viewModel.ProductName), "Tên không được để trống");
            if (string.IsNullOrEmpty(viewModel.Unit))
                ModelState.AddModelError(nameof(viewModel.Unit), "Đơn vị tính không được để trống");
            if (viewModel.Price <= 0)
                ModelState.AddModelError(nameof(viewModel.Price), "Giá không hợp lệ");
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                viewModel.Photo = $"Images/Products/{fileName}";
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Chỉnh sửa sản phẩm";
                return View("Edit", viewModel);
            }
            var model = new Product()
            {
                ProductID = viewModel.ProductID,
                ProductName = viewModel.ProductName,
                CategoryID = viewModel.CategoryID,
                SupplierID = viewModel.SupplierID,
                Unit = viewModel.Unit,
                Price = viewModel.Price,
                Photo = viewModel.Photo,
            };
            ProductDataService.UpdateProduct(model);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = ProductDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");

            var ListOfAttributes = ProductDataService.ListAttributes(data.ProductID);
            var ListOfPhotos = ProductDataService.ListPhotos(data.ProductID);
            var model = new ProductViewModel()
            {
                ProductID = data.ProductID,
                ProductName = data.ProductName,
                CategoryID = data.CategoryID,
                SupplierID = data.SupplierID,
                Unit = data.Unit,
                Price = data.Price,
                Photo = data.Photo,
                Attributes = ListOfAttributes,
                Photos = ListOfPhotos
            };
            ViewBag.Title = "Cập nhật sản phẩm";
            return View(model);
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                var ListOfPhotos = ProductDataService.ListPhotos(id);
                var ListOfAttributes = ProductDataService.ListAttributes(id);
                foreach (var item in ListOfPhotos)
                {
                    ProductDataService.DeletePhoto(item.PhotoID);
                }
                foreach (var item in ListOfAttributes)
                {
                    ProductDataService.DeleteAttribute(item.AttributeID);
                }
                ProductDataService.DeleteProduct(id);


                return RedirectToAction("Index");
            }
            var model = ProductDataService.GetProduct(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    var model = new ProductPhoto()
                    {
                        PhotoID = 0,
                    };
                    ViewBag.ProductID = productID;
                    ViewBag.Title = "Bổ sung ảnh";
                    return View(model);
                case "edit":
                    model = ProductDataService.GetPhoto(photoID);
                    ViewBag.ProductID = productID;
                    ViewBag.Title = "Thay đổi ảnh";
                    return View(model);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrEmpty(model.Description))
                ModelState.AddModelError(nameof(model.Description), "Mô tả không được để trống");
            if (model.DisplayOrder <= 0)
                ModelState.AddModelError(nameof(model.DisplayOrder), "Số không hợp lệ");
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = $"Images/Products/{fileName}";
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.PhotoID == 0 ? "Bổ sung ảnh" : "Thay đổi ảnh";
                return View("Photo", model);
            }
            if (model.PhotoID == 0)
            {
                ProductDataService.AddPhoto(model);
            }
            else
            {
                ProductDataService.UpdatePhoto(model);
            }
            var ListOfAttributes = ProductDataService.ListAttributes(model.ProductID);
            var ListOfPhotos = ProductDataService.ListPhotos(model.ProductID);
            var data = ProductDataService.GetProduct(model.ProductID);
            var productView = new ProductViewModel()
            {
                ProductID = data.ProductID,
                ProductName = data.ProductName,
                CategoryID = data.CategoryID,
                SupplierID = data.SupplierID,
                Unit = data.Unit,
                Price = data.Price,
                Photo = data.Photo,
                Attributes = ListOfAttributes,
                Photos = ListOfPhotos
            };
            return View("Edit", productView);
        }



        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, long attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    var model = new ProductAttribute()
                    {
                        AttributeID = 0,
                    };
                    ViewBag.ProductID = productID;
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View(model);
                case "edit":
                    model = ProductDataService.GetAttribute(attributeID);
                    ViewBag.ProductID = productID;
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View(model);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute model)
        {
            if (string.IsNullOrEmpty(model.AttributeName))
                ModelState.AddModelError(nameof(model.AttributeName), "Tên thuộc tính không được để trống");
            if (string.IsNullOrEmpty(model.AttributeValue))
                ModelState.AddModelError(nameof(model.AttributeValue), "Giá trị thuộc tính không được để trống");
            if (model.DisplayOrder <= 0)
                ModelState.AddModelError(nameof(model.DisplayOrder), "Thứ tự không hợp lệ");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.AttributeID == 0 ? "Bổ sung thuộc tính" : "Chỉnh sửa thuộc tính";
                return View("Attribute", model);
            }
            if (model.AttributeID == 0)
                ProductDataService.AddAttribute(model);
            else
                ProductDataService.UpdateAttribute(model);

            var ListOfAttributes = ProductDataService.ListAttributes(model.ProductID);
            var ListOfPhotos = ProductDataService.ListPhotos(model.ProductID);
            var data = ProductDataService.GetProduct(model.ProductID);
            var productView = new ProductViewModel()
            {
                ProductID = data.ProductID,
                ProductName = data.ProductName,
                CategoryID = data.CategoryID,
                SupplierID = data.SupplierID,
                Unit = data.Unit,
                Price = data.Price,
                Photo = data.Photo,
                Attributes = ListOfAttributes,
                Photos = ListOfPhotos
            };
            return View("Edit", productView);
        }
    }
}