using Practical_Test_Interview.DAL;
using Practical_Test_Interview.Models.Category;
using Practical_Test_Interview.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical_Test_Interview.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category()
        {
            Session["PageFilterModel"] = new PageFilterModel();
            return View();
        }
        public ActionResult CategoryDetail(PageFilterModel paging)
        {
            ProductDataContext db = new ProductDataContext();
            List<CategoryModel> lloCategoryModel = new List<CategoryModel>();
            try
            {
                var categoryobj = db.SP_SELECTCategory(paging.PageNo, paging.PageSize);
                foreach (var item in categoryobj)
                {
                    lloCategoryModel.Add(new CategoryModel { CategoryID = item.CategoryId, CategoryName = item.CategoryName });
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return View(lloCategoryModel);
        }

        //
        // GET: /Category/Create

        public ActionResult AddCategory()
        {
            string Categoryid = Request.QueryString["q"];

            Session["PageFilterModel"] = new PageFilterModel();

            ProductDataContext db = new ProductDataContext();
            CategoryModel loCategoryModel = new CategoryModel();
            loCategoryModel.CategoryID = 0;
            loCategoryModel.CategoryName = "";
            try
            {
                if (!string.IsNullOrEmpty(Categoryid))
                {
                    var locategory = db.tblCategories.Where(x => x.CategoryId == Convert.ToInt32(Categoryid)).FirstOrDefault();

                    loCategoryModel.CategoryID = locategory.CategoryId;
                    loCategoryModel.CategoryName = locategory.CategoryName;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return View(loCategoryModel);
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult SaveCatagory(CategoryModel loCategoryModel)
        {
            try
            {
                // TODO: Add insert logic here
                ProductDataContext db = new ProductDataContext();
                try
                {
                    if (loCategoryModel.CategoryID > 0)
                    {
                        db.SP_UpdateCategory(loCategoryModel.CategoryID,loCategoryModel.CategoryName);
                    }
                    else
                    {
                        db.SP_InsertCategory(loCategoryModel.CategoryName);
                    }
                    
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                
                return RedirectToAction("Category");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteCategory()
        {
            string categoryid = Request.QueryString["q"];
            ProductDataContext db = new ProductDataContext();
            try
            {
                if (!string.IsNullOrEmpty(categoryid))
                {
                    db.SP_DeleteCategory(Convert.ToInt32(categoryid));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("Category");
        }

    }
}
