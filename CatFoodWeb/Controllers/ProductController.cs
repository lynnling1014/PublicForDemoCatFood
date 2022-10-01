using CatFoodWeb.Models;
using CatFoodWeb.Models.service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatFoodWeb.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            tProductController products = new tProductController();
            
            return View(products.getProducts());
        }

        public ActionResult Create(int? id)
        {
            //DropDownList_Category
            tCategoryController tCategory = new tCategoryController();
            var categories = tCategory.getCategories();
            //DropDownList_Supplier
            tSupplierController tSupplier = new tSupplierController();
            var supplier = tSupplier.getSupplier();

            
            if (id == null)
            {
                SelectList selectLists = new SelectList(categories, "fCategoryID", "fCategoryName");
                ViewBag.CategorySelectList = selectLists;
                SelectList supplier_selectLists = new SelectList(supplier, "fSupplierID", "fSupplierName");
                ViewBag.SupplierSelectList = supplier_selectLists;
            }
            else
            {
                tProductController products = new tProductController();
                var oneProduct = products.getProductByID((int)id);
                SelectList selectLists = new SelectList(categories, "fCategoryID", "fCategoryName", oneProduct.fCategoryID);
                ViewBag.CategorySelectList = selectLists;
                SelectList supplier_selectLists = new SelectList(supplier, "fSupplierID", "fSupplierName" ,oneProduct.fSupplierID);
                ViewBag.SupplierSelectList = supplier_selectLists;
                return View(oneProduct);
            }

            var model = new classProduct();
            model.fProductID = 0;
            model.fActive = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(classProduct product, HttpPostedFileBase Photofile)
        {
            tProductController productFactory = new tProductController();
            //store photo
            if (Photofile != null)
            {
                string phtotName = Path.GetFileName(Photofile.FileName); //取得檔名
                //string phtotName = (Guid.NewGuid().ToString()) + Path.GetExtension(Photofile.FileName);//不重複的檔名
                var photoPath = Path.Combine(Server.MapPath("~/Content/images/"), phtotName);
                Photofile.SaveAs(photoPath); //存圖
                product.fPhoto = phtotName;
            }
            if (product.fProductID == 0)
            {
                productFactory.insert(product);
            }
            else
            {
                productFactory.update(product);
            }


            return RedirectToAction("List");
        }
    }
}