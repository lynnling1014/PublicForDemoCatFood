using CatFoodWeb.Models;
using CatFoodWeb.Models.service;
using CatFoodWeb.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CatFoodWeb.Models.CommonEnum;

namespace CatFoodWeb.Controllers
{
    public class PurchaseController : Controller
    {
        #region 所有訂單列表
        // GET: Perchase
        public ActionResult List()
        {
            //取得所有進貨訂單
            int type = (int)PurchaseType.In;
            tPurchaseController orders= new tPurchaseController();
            return View(orders.getOrders(type));
        }

        public ActionResult MakeList()
        {
            //取得所有進貨訂單
            int type = (int)PurchaseType.Out;
            tPurchaseController orders = new tPurchaseController();
            return View(orders.getOrders(type));
        }
        #endregion


        #region 訂單頁面 read  -- Insert / update order
        public ActionResult PurchaseProduct(int? id)
        {
            var model = new PurchaseViewModel();
            model.fOrderID = 0;
            //取得類別需求量資料
            tCategoryController tCategory = new tCategoryController();
            var category = tCategory.getCategories();
            ViewBag.category = category;
            if (id!=null)
            {
                //取得訂單資料
                tPurchaseController purchaseController = new tPurchaseController();
                model = purchaseController.getOneOrder((int)id);
            }
            
            //取得所有商品資料 存入model中
            tProductController tProduct = new tProductController();
            var products = tProduct.getProducts();
            List<classProduct> l = new List<classProduct>();
            foreach (var item in products)
            {
                l.Add(item);
            }
            model.product = l;
            return View(model);
        }
        #endregion

        #region 計算重量金額ajax   

        public ActionResult Counting(string data, string type, string strOrderData)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result["resultcode"] = "0";
            result["resultmessage"] = "";
            result["TotalPrice"] = "";
            result["noBoneWeight"] = "";
            result["noBone_CategoryID"] = "";
            try
            {
                tProductController productController = new tProductController();
                tPurchaseController tPurchase = new tPurchaseController();
                PurchaseViewModel order = new PurchaseViewModel();
                List<OrderDetails> ListOrderDetails = new List<OrderDetails>();
                int NoBoneWeight = 0;
                int BoneWeight = 0;
                int HeartWeight = 0;
                int LiverWeight = 0;
                int TotalPrice = 0;
                var FrontData = JsonConvert.DeserializeObject<List<ViewDataModel>>(data);
                
                //取得商品id及數量
                //DB取得商品每包重量及價格
                //計算重量與價格
                foreach (var item in FrontData)
                {
                    classProduct productModel = new classProduct();
                    productModel = productController.getProductByID(item.pID);//商品資料
                    if (item.pCategoryID == (int)Category.MeatNoBone) 
                    {
                        NoBoneWeight += productModel.fQtyPerUnit * item.pQty;
                    }else if (item.pCategoryID == (int)Category.MeatWithBone)
                    {
                        BoneWeight += productModel.fQtyPerUnit * item.pQty;
                    }
                    else if (item.pCategoryID == (int)Category.Heart)
                    {
                        HeartWeight += productModel.fQtyPerUnit * item.pQty;
                    }
                    else if (item.pCategoryID == (int)Category.Liver)
                    {
                        LiverWeight += productModel.fQtyPerUnit * item.pQty;
                    }
                    TotalPrice += productModel.fNewPrice * item.pQty;


                    //取得商品資料
                    if (type == "1")
                    {
                        //@fProductID,@fPrice,@fQty
                        OrderDetails od = new OrderDetails();
                        od.productID = item.pID;
                        od.buyPrice = productModel.fNewPrice;
                        od.pQty = item.pQty;
                        od.pWeight = productModel.fQtyPerUnit * item.pQty;
                        ListOrderDetails.Add(od);
                    }
                }
                result["TotalPrice"] = TotalPrice.ToString();
                result["NoBoneWeight"] = NoBoneWeight.ToString();
                result["BoneWeight"] = BoneWeight.ToString();
                result["HeartWeight"] = HeartWeight.ToString();
                result["LiverWeight"] = LiverWeight.ToString();
                
                if (type == "1")
                {
                    //fArriveDate fTransferFee fNote fInOrOut
                    var OrderData = JsonConvert.DeserializeObject<ViewDataModel2>(strOrderData);
                    order.fArriveDate = OrderData.ArriveDate;
                    order.fTransferFee = OrderData.TransferFee;
                    order.fNote = OrderData.Note;
                    order.fInOrOut = "In";
                    order.fArrive = "N";
                    //取得其他訂單資料@fCount,@fTotalPrice 
                    order.details = ListOrderDetails;//各項商品資料
                    order.fCount = TotalPrice;//商品總金額(不含運費)
                    order.fTotalPrice = order.fCount + order.fTransferFee;//訂單總金額(含運)
                    tPurchase.insert(order);
                }
            }
            catch (Exception ex)
            {
                result["resultcode"] = "999";
                result["resultmessage"] = ex.Message;
            }
            finally
            {
            }
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        #endregion

        

        #region 訂單詳細內容
        public ActionResult Details(int? id)
        {
            tPurchaseController orders = new tPurchaseController();
            int orderID = (int)id;
            return View(orders.getOneOrder(orderID));
        }
        #endregion
    }
}