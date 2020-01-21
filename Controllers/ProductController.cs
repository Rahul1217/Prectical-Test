using Practical_Test_Interview.DAL;
using Practical_Test_Interview.Models.Common;
using Practical_Test_Interview.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical_Test_Interview.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult ProductDetail(PageFilterModel paging)
        {
            if(Session["PageFilterModel"]!=null)
            {
                paging = (PageFilterModel)Session["PageFilterModel"];
            }
            
            ProductDataContext db = new ProductDataContext();
            List<ProductModel> lloProductModel = new List<ProductModel>();
            try
            {
                int totalrecord = 0;
                var categoryobj = db.SP_SELECTProduct(paging.PageNo, paging.PageSize);
                foreach (var item in categoryobj)
                {
                    totalrecord = item.TotalRow ?? 0;
                    lloProductModel.Add(new ProductModel { ProductID = item.ProductId, ProductName = item.ProductName, CategoryID = item.CategoryId, CategoryName = item.CategoryName });
                }
                int totalPages_pre = (totalrecord / 10);
                paging.totalpage = (totalrecord % 10) == 0 ? totalPages_pre : totalPages_pre + 1;
                paging.totalRecord = totalrecord;
            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.paging = paging;
            return View(lloProductModel);
        }
        [HttpPost]
        public ActionResult sessionvalueset(string pageno)
        {
            PageFilterModel loPageFilterModel = new PageFilterModel();
            loPageFilterModel.PageNo = Convert.ToInt32(pageno);
            Session["PageFilterModel"] = loPageFilterModel;

            return Json("success");
        }

        public ActionResult AddProduct()
        {
            string productid = Request.QueryString["q"];

            Session["PageFilterModel"] = new PageFilterModel();

            ProductModel loProductModel = new ProductModel();
            loProductModel.ProductID = 0;
            loProductModel.ProductName = "";
            loProductModel.CategoryID = 0;
            loProductModel.CategoryName = "";
            ProductDataContext db = new ProductDataContext();
            List<SelectListItem> Category = new List<SelectListItem>();
            try
            {
                if (!string.IsNullOrEmpty(productid))
                {
                    var loproduct = db.tblProducts.Where(x => x.ProductId == Convert.ToInt32(productid)).FirstOrDefault();
                    loProductModel.ProductID = loproduct.ProductId;
                    loProductModel.ProductName = loproduct.ProductName;
                    loProductModel.CategoryID = loproduct.CategoryId ?? 0;
                    loProductModel.CategoryName = "";
                }
                var llcategory = db.tblCategories.ToList();
                Category.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                foreach (var item in llcategory)
                {
                    Category.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryName });
                }
            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.Category = Category;
            return View(loProductModel);
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult SaveProduct(ProductModel loProductModel)
        {

            ProductDataContext db = new ProductDataContext();
            try
            {
                if (loProductModel.ProductID > 0)
                {
                    db.SP_UPDATEProduct(loProductModel.ProductID, loProductModel.CategoryID, loProductModel.ProductName);
                }
                else
                {
                    db.SP_InsertProduct(loProductModel.CategoryID, loProductModel.ProductName);
                }

            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("Product");
        }
        public ActionResult DeleteProduct()
        {
            string productid = Request.QueryString["q"];
            ProductDataContext db = new ProductDataContext();
            try
            {
                if (!string.IsNullOrEmpty(productid))
                {
                    db.SP_DeleteProduct(Convert.ToInt32(productid));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("Product");
        }

    }
}
