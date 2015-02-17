using Microsoft.AspNet.Identity;
using RealEstateV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Security.Claims;
using Postal;
using System.Net.Mail;
using RealEstateV1.Busniss;
namespace RealEstateV1.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        public static int indexNumber = 2;

        public void initialization()
        {
            var tw = new Dictionary<string, int> { { "الحي", 0 } };
            var bathno = new Dictionary<string, int> { { "عدد الحمامات", 0 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 } };
            var roomno = new Dictionary<string, int> { { " عدد الغرف", 0 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 } };
            var lower = new Dictionary<string, int> { { "السعر الادنى", 0 }, { "100", 100 }, { "200", 200 }, { "300", 300 }, { "400", 400 }, { "500", 500 } };
            var uper = new Dictionary<string, int> { { "السعر الاعلى ", 0 }, { "10000", 10000 }, { "20000", 20000 }, { "30000", 30000 }, { "40000", 40000 }, { "50000", 50000 } };
            var area = new Dictionary<string, int> { { "المساحة", 0 }, { "100", 100 }, { "200", 200 }, { "300", 300 }, { "400", 400 }, { "500", 500 } };
            var date = new Dictionary<string, int> { { "تاريخ الاضافة", 0 }, { "اخر يوم", 1 }, { "اخر اسبوع", 2 }, { "اخر شهر", 3 } };
            ViewBag.Cites = Busniss.BusnissLayer.GetCitesList();
            ViewBag.VBbathno = new SelectList(bathno, "Value", "Key");
            ViewBag.VBroomno = new SelectList(roomno, "Value", "Key");
            ViewBag.RSkind = Busniss.BusnissLayer.GetREKindList();
            ViewBag.VBlowerprice = new SelectList(lower, "Value", "Key");
            ViewBag.VBuperprice = new SelectList(uper, "Value", "Key");
            ViewBag.VBarea = new SelectList(area, "Value", "Key"); 
            ViewBag.REFeature = Busniss.BusnissLayer.GetREFeatuer();
            ViewBag.VBtown = new SelectList(tw, "Value", "Key");
            ViewBag.VBdate = new SelectList(date, "Value", "Key");

            string realstate = " عقار جديد ";
            ViewBag.jeddahImage = "house.png";
            ViewBag.jeddahcountToday = "1000"+realstate;
            ViewBag.RyadImage = "house.png";
            ViewBag.RyadcountToday = "10000"+realstate;
            ViewBag.EstImage = "house.png"; 
            ViewBag.estcountToday = "1000"+realstate;
        
            ViewBag.allcost = "1000";


            var room = new Dictionary<string, int> { { "غرفه ", 0 } };
            var asia = new Dictionary<string, int> { { "الاسيوي ", 0 } };
            ViewBag.room = new SelectList(room, "Value", "Key");
            ViewBag.asia = new SelectList(asia, "Value", "Key");
            ViewBag.ImageAds = "left-ads.jpg";
            ViewBag.imageads2 = "right-ads.jpg";
            ViewBag.index = 1;
            var resultInPage = new Dictionary<string, int> { { "2 نتائج في الصفحة", 0 }, { "5 نتائج في الصفحة", 1 }, { "10 نتائج في الصفحة", 2 } };
            ViewBag.resultInPage = new SelectList(resultInPage, "Value", "Key");
        }
        #region actions

        [HttpGet]
        public ActionResult Index()
        {
            initialization();
            Busniss.SearchItem item = new SearchItem();
            Busniss.SessionManager.searchKey = item;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Busniss.SearchItem item, int[] feature)
        {
            if (feature != null)
                item.feature = feature.ToList();
            else item.feature = new List<int>();
            Busniss.SessionManager.searchKey = item;
            if (item.SearchKind == Busniss.MyEnumType.SearchKind.Rent)
                return RedirectToAction("Rent", new { flag = false });
            if (item.SearchKind == Busniss.MyEnumType.SearchKind.Sale)
                return RedirectToAction("Sale", new { flag = false });
            if (item.SearchKind == Busniss.MyEnumType.SearchKind.Sold)
                return RedirectToAction("Sold", new { flag = false });

            return Index();
        }

        public JsonResult changeIndexCount(int number)
        {
            indexNumber = number;
            return Json(true, JsonRequestBehavior.AllowGet);  
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTown(string CityID)
        {
            ViewBag.ReturnUrl = "towninformation";
            if (String.IsNullOrEmpty(CityID))
            {
                throw new ArgumentNullException("CityID");
            }
            int id = 0;
            bool isValid = Int32.TryParse(CityID, out id);
            var result = Busniss.BusnissLayer.GetTownByCityIDDropDown(id);
            return Json(result, JsonRequestBehavior.AllowGet);        
        }

        public ActionResult Email(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }

            //add email to subscrib
            return new EmptyResult();

        }
        [HttpPost]
        public ActionResult SendEmail(string Username, string Phone, string Email, string Pargraph , int id)
        {
            if (ModelState.IsValid)
            {

                MailMessage mail = new MailMessage("bilalshammaa@gmail.com", "bilalshammaa@gmail.com");
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential();
                client.Host = "smtp.google.com";
                client.EnableSsl = true;
                mail.Subject = "this is a test email.";
                mail.Body = "this is my test email body";
                client.Send(mail);
            }

            return RedirectToAction("RealEstate",id);
        }
        /////////////////////////////////////////////////////////////////////*********

        public ActionResult GetOwner(int OwnerId)
        {
            ViewBag.ReturnUrl = "RealEstatesOwners";
            var result = Busniss.BusnissLayer.GetOwnerById(OwnerId);
            return PartialView("_OwnerPartial", result);
        }

        [Authorize]
        public ActionResult AddDiscuss(string Topic)
        {
            Busniss.BusnissLayer.AddDiscuss(Topic);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult AddTownComment(float Rate, string Title, string Comment, int Town_ID)
        {
            Busniss.BusnissLayer.AddTownComment(Rate, Title, Comment, Town_ID);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.ReturnUrl = "about";
            return View();
        }

        public ActionResult GetDescByfeature(int TownFeatureId)
        {
            var result = Busniss.BusnissLayer.GetDescByfeature(TownFeatureId);
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFeature(int FeatureDescId)
        {
            var result = Busniss.BusnissLayer.GetFeature(FeatureDescId);
            return PartialView("_TownFeaturePartial", result);
        }

        public ActionResult RealEstatesOwners()
        {
            initialization();
            ViewBag.ReturnUrl = "RealEstatesOwners";
            RealEstateContext db = new RealEstateContext();
            List<T_Owner> owner = db.TOwner.ToList();
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = owner.Count();
            List<T_Owner> res = new List<T_Owner>();
            for (int i = 0; i < owner.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(owner[i]);
                count--;
            }
            return View(res);
        }

        public ActionResult OwnersByIndex(int start, int CityID)
        {
            ViewBag.ReturnUrl = "towninformation";

            List<T_Owner> ownertList = Busniss.BusnissLayer.GetOwnerByCity(CityID);
            int count = indexNumber;
            List<int> res = new List<int>();
            for (int i = start; i < ownertList.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(ownertList[i].ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OwnersByIndexCount(int start, int CityID)
        {
            ViewBag.ReturnUrl = "towninformation";

            List<T_Owner> ownertList = Busniss.BusnissLayer.GetOwnerByCity(CityID);
            return Json(ownertList.Count(), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult RealEstate(int ID)
        {
            Busniss.BusnissLayer.AddSearchHistory(ID);
            ViewBag.ReturnUrl = "RealEstate/"+ID;
            T_RealEstate real = Busniss.BusnissLayer.GetRealEstateByID(ID);
            T_Rent rent = Busniss.BusnissLayer.GetRentByRSID(ID);
            T_Sale sale = Busniss.BusnissLayer.GetSaleByRSID(ID);
            ViewBag.featuer = Busniss.BusnissLayer.getTownFeatureKindList();
            RealEstateFull realEstateFull = new RealEstateFull();
            realEstateFull.realEstate = real;
            realEstateFull.rent = rent;
            realEstateFull.sale = sale;
            realEstateFull.featureRate = Busniss.BusnissLayer.getFeatureRate(real.address.Town.ID);
            return View(realEstateFull);
        }

        public PartialViewResult LoadImage(int ID, string kind)
        {
            T_RealEstate res = Busniss.BusnissLayer.GetRealEstateByID(ID);
            List<T_REImage> images = new List<T_REImage>();
            for (int i = 0; i < res.REImageB.Count();i++ )
            {
                Busniss.MyEnumType.imageKind k=res.REImageB.ElementAt(i).kind;
                if(k.ToString()==kind)
                {
                    images.Add(res.REImageB.ElementAt(i));
                }
            }
            return PartialView("_ImagePartial", images);
        }

        public PartialViewResult LoadVideo(int ID)
        {
            T_RealEstate res = Busniss.BusnissLayer.GetRealEstateByID(ID);
            return PartialView("_VideoPartial", res.REVideo);
        }

        public ActionResult Contact()
        {
            ViewBag.ReturnUrl = "contact";
            return View();
        }

        public ActionResult Rent(bool flag = true)
        {
            initialization();
            ViewBag.ReturnUrl = "Rent";
            Busniss.SearchItem item = null;
            if (flag == true)
            {
                item = new SearchItem();
                Busniss.SessionManager.searchKey = item;
            }
            else
            {
                if (Busniss.SessionManager.searchKey != null)
                {
                    item = Busniss.SessionManager.searchKey;
                }
            }
            var result = Busniss.BusnissLayer.getRent(item);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<T_Rent> res = new List<T_Rent>();
            for (int i = 0; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i]);
                count--;
            }
            Busniss.SessionManager.Result = res;
            return View(res);
        }

        public ActionResult RentByIndex(int start)
        {
            ViewBag.ReturnUrl = "Rent";

            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            List<T_Rent> result = Busniss.BusnissLayer.getRent(item);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<int> res = new List<int>();
            for (int i = start; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i].ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RentByIndexCount(int start)
        {
            ViewBag.ReturnUrl = "Rent";

            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            List<T_Rent> result = Busniss.BusnissLayer.getRent(item);

            return Json(result.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRentByID(int RentID)
        {
            ViewBag.ReturnUrl = "Rent";
            var result = Busniss.BusnissLayer.GetRentByID(RentID);
            return PartialView("_RentPartial", result);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeSearch(Busniss.SearchItem search)
        {
            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            else
            {
                item = new Busniss.SearchItem();
            }
            
            item.Bathno = search.Bathno;
            item.City = search.City;
            item.LowerPrice = search.LowerPrice;
            item.RoomNo = search.RoomNo;
            item.Town = search.Town;
            item.UperPrice = search.UperPrice;
            item.RealEstateKind = search.RealEstateKind;
            item.date = search.date;
            
            Busniss.SessionManager.searchKey=item;

            if(search.SearchKind==Busniss.MyEnumType.SearchKind.Rent)
            {
                return RedirectToAction("Rent", new { flag = false });
            }
            
            if (search.SearchKind == Busniss.MyEnumType.SearchKind.Sold)
            {
                return RedirectToAction("Sold", new { flag = false });
            }
            
            if (search.SearchKind == Busniss.MyEnumType.SearchKind.Sale)
            {
                return RedirectToAction("Sale", new { flag = false });
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Sale(bool flag=true)
        {
            initialization();
            ViewBag.ReturnUrl = "Sale";
            Busniss.SearchItem item = null;
            if(flag==true)
            {
                item = new SearchItem();
                Busniss.SessionManager.searchKey = item;
            }
            else
            {
                if (Busniss.SessionManager.searchKey != null)
                {
                    item = Busniss.SessionManager.searchKey;
                }
            }

            List<T_Sale> result = Busniss.BusnissLayer.getSale(item);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            ViewBag.VBtown = Busniss.BusnissLayer.GetTownByCityIDSL(item.City);
            List<T_Sale> res = new List<T_Sale>();
            for (int i = 0; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i]);
                count--;
            }
            Busniss.SessionManager.Result = res;
            return View(res);
        }

        public ActionResult SaleByIndex(int start)
        {
            ViewBag.ReturnUrl = "Sale";

            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            List<T_Sale> result = Busniss.BusnissLayer.getSale(item);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<int> res = new List<int>();
            for (int i = start; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i].ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleByIndexCount(int start)
        {
            ViewBag.ReturnUrl = "Sale";

            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            List<T_Sale> result = Busniss.BusnissLayer.getSale(item);

            return Json(result.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSaleByID(int SaleID)
        {
            ViewBag.ReturnUrl = "Sale";
            var result = Busniss.BusnissLayer.GetSaleByID(SaleID);
            return PartialView("_SalePartial", result);
        }

        public ActionResult SearchHistory()
        {
            ViewBag.ReturnUrl = "SearchHistory";
            List<RealEstateFull> result = Busniss.BusnissLayer.GetSearchHistory();
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<RealEstateFull> res = new List<RealEstateFull>();
            for (int i = 0; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i]);
                count--;
            }
            return View(res);
        }

        public ActionResult HistoryByIndex(int start)
        {
            ViewBag.ReturnUrl = "SearchHistory";

            List<RealEstateFull> result = Busniss.BusnissLayer.GetSearchHistory();
            indexNumber = 2;
            int count = indexNumber;
            List<int> res = new List<int>();
            for (int i = start; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i].realEstate.ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHistoryByID(int id)
        {
            ViewBag.ReturnUrl = "SearchHistory";
            var result = Busniss.BusnissLayer.GetHistoryByREID(id);
            return PartialView("_SearchHistoryPartial", result);
        }

        public ActionResult FavoritEstates()
        {
            ViewBag.ReturnUrl = "FavoritEstates";
            List<RealEstateFull> result = Busniss.BusnissLayer.GetFavorit();
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<RealEstateFull> res = new List<RealEstateFull>();
            for (int i = 0; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i]);
                count--;
            }
            return View(res.ToList());
        }

        public ActionResult FavoritByIndex(int start)
        {
            ViewBag.ReturnUrl = "FavoritEstates";

            List<RealEstateFull> result = Busniss.BusnissLayer.GetFavorit();
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<int> res = new List<int>();
            for (int i = start; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i].realEstate.ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFavoritByID(int id)
        {
            ViewBag.ReturnUrl = "FavoritEstates";
            var result = Busniss.BusnissLayer.GetFavoritByREID(id);
            return PartialView("_FavoritPartial", result);
        }

        [HttpGet]
        public ActionResult Sold(bool flag = true)
        {
            initialization();
            ViewBag.ReturnUrl = "Sold";
            Busniss.SearchItem item = null;
            if (flag == true)
            {
                item = new SearchItem();
                Busniss.SessionManager.searchKey = item;
            }
            else
            {
                if (Busniss.SessionManager.searchKey != null)
                {
                    item = Busniss.SessionManager.searchKey;
                }
            }
            List<T_Sale> result = Busniss.BusnissLayer.getSold(item);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<T_Sale> res = new List<T_Sale>();
            for (int i = 0; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i]);
                count--;
            }
            Busniss.SessionManager.Result = res;
            return View(res);
        }

        public ActionResult SoldByIndex(int start)
        {
            ViewBag.ReturnUrl = "Sold";

            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            List<T_Sale> result = Busniss.BusnissLayer.getSold(item);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = result.Count();
            List<int> res = new List<int>();
            for (int i = start; i < result.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(result[i].ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SoldByIndexCount(int start)
        {
            ViewBag.ReturnUrl = "Sold";

            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            List<T_Sale> result = Busniss.BusnissLayer.getSold(item);

            return Json(result.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSoldByID(int SoldID)
        {
            ViewBag.ReturnUrl = "Sold";
            var result = Busniss.BusnissLayer.GetSaleByID(SoldID);
            return PartialView("_SalePartial", result);
        }


        [HttpGet]
        public ActionResult AddRate(int id)
        {
            ViewBag.townId = id;
            addRateInfo addrate = new addRateInfo();
            return PartialView("_AddRatePartial", addrate);
        }

        [HttpPost]
        public ActionResult AddRate(addRateInfo rateInfo, int townId)
        {
            AddTownComment(rateInfo.townRate, rateInfo.title, rateInfo.comment, townId);
            Busniss.BusnissLayer.AddTownRate(rateInfo, townId);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult LikeOwner(int ID)
        {
            bool result = Busniss.BusnissLayer.LikeOwner(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisLikeOwner(int ID)
        {
            bool result = Busniss.BusnissLayer.DisLikeOwner(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LikeEstate(int ID)
        {
            bool result = Busniss.BusnissLayer.LikeEstate(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisLikeEstate(int ID)
        {
            bool result = Busniss.BusnissLayer.DisLikeEstate(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LikeD(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "Disc" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("Discuss", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Discuss", key, "true", 7);
                bool result = Busniss.BusnissLayer.LikeD(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "false")
            {
                Busniss.CookieManager.SetCookie("Discuss", key, "true", 7);
                bool result = Busniss.BusnissLayer.RemoveDisLikeD(ID);
                result = Busniss.BusnissLayer.LikeD(ID);
                return Json("addRem", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HideEstate(int estateID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = estateID.ToString();
            string value = Busniss.CookieManager.CookieValue("HideCust" + customer.ID, key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("HideCust" + customer.ID, key, "true", 7);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LikeC(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "Comm" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("Comment", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Comment", key, "true", 7);
                bool result = Busniss.BusnissLayer.LikeC(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "false")
            {
                Busniss.CookieManager.SetCookie("Comment", key, "true", 7);
                bool result = Busniss.BusnissLayer.RemoveDisLikeC(ID);
                result = Busniss.BusnissLayer.LikeC(ID);
                return Json("addRem", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DisLikeD(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "Disc" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("Discuss", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Discuss", key, "false", 7);
                bool result = Busniss.BusnissLayer.DisLikeD(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "true")
            {
                Busniss.CookieManager.SetCookie("Discuss", key, "false", 7);
                bool result = Busniss.BusnissLayer.RemoveLikeD(ID);
                result = Busniss.BusnissLayer.DisLikeD(ID);
                return Json("addRem", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DisLikeC(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "Comm" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("Comment", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Comment", key, "false", 7);
                bool result = Busniss.BusnissLayer.DisLikeC(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "true")
            {
                Busniss.CookieManager.SetCookie("Comment", key, "false", 7);
                bool result = Busniss.BusnissLayer.RemoveLikeC(ID);
                result = Busniss.BusnissLayer.DisLikeC(ID);
                return Json("addRem", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult reportD(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "RepDisc" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("ReportDiscuss", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("ReportDiscuss", key, "true", 7);
                bool result = Busniss.BusnissLayer.reportD(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult reportC(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "RepComm" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("ReportComment", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("ReportComment", key, "true", 7);
                bool result = Busniss.BusnissLayer.reportC(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult reportR(int ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = Busniss.BusnissLayer.getCurrentCustomer(DB);
            string key = "RepRep" + ID + "Cust" + customer.ID;
            string value = Busniss.CookieManager.CookieValue("ReportReport", key);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("ReportReport", key, "true", 7);
                bool result = Busniss.BusnissLayer.reportR(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult AddDiscussRepaly(int ID, string test)
        {
            T_Replay rep = Busniss.BusnissLayer.AddDiscussRepaly(ID, test);
            return PartialView("_ReplayPartial", rep);
        }

        [Authorize]
        public ActionResult AddCommentRepaly(int ID, string test)
        {
            T_Replay rep = Busniss.BusnissLayer.AddCommentRepaly(ID, test);
            return PartialView("_ReplayPartial", rep);
        }

        public ActionResult Discuss()
        {
            initialization();
            ViewBag.ReturnUrl = "discuss";
            
            List<T_Discuss> dis = Busniss.BusnissLayer.GetDiscuss();
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount=dis.Count();
            List<T_Discuss> res = new List<T_Discuss>();
            for (int i = 0; i < dis.Count();i++ )
            {
                if(count==0)
                {
                    break;
                }
                res.Add(dis[i]);
                count--;
            }
            return View(res);
        }

        public ActionResult DiscussByIndex(int start)
        {
            ViewBag.ReturnUrl = "discuss";

            List<T_Discuss> dis = Busniss.BusnissLayer.GetDiscuss();
            int count = indexNumber;
            List<int> res = new List<int>();
            for (int i = start; i < dis.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(dis[i].ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDiscussByID(int DiscussID)
        {
            ViewBag.ReturnUrl = "discuss";
            var result = Busniss.BusnissLayer.GetDiscussByID(DiscussID);
            return PartialView("_DiscussPartial", result);
        }

        public ActionResult Supply()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.ReturnUrl = "supply";
            return View();
        }

        public ActionResult TownInformation(int CityID = 0, int townID = 0)
        {
            initialization();
            ViewBag.ReturnUrl = "towninformation";
            List<T_TownComment> townCommentList = Busniss.BusnissLayer.GetCommnetList(CityID, townID);
            indexNumber = 2;
            int count = indexNumber;
            ViewBag.showCount = count;
            ViewBag.totalCount = townCommentList.Count();
            List<T_TownComment> res = new List<T_TownComment>();
            for (int i = 0; i < townCommentList.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(townCommentList[i]);
                count--;
            }
            return View(res);
        }

        public ActionResult CommentByIndex(int start,int CityID, int townID)
        {
            ViewBag.ReturnUrl = "towninformation";

            List<T_TownComment> townCommentList = Busniss.BusnissLayer.GetCommnetList(CityID, townID);
            int count = indexNumber;
            List<int> res = new List<int>();
            for (int i = start; i < townCommentList.Count(); i++)
            {
                if (count == 0)
                {
                    break;
                }
                res.Add(townCommentList[i].ID);
                count--;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentByIndexCount(int start, int CityID, int townID)
        {
            ViewBag.ReturnUrl = "towninformation";

            List<T_TownComment> townCommentList = Busniss.BusnissLayer.GetCommnetList(CityID, townID);
            return Json(townCommentList.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCommentByID(int CommentID)
        {
            ViewBag.ReturnUrl = "towninformation";
            var result = Busniss.BusnissLayer.GetCommentByID(CommentID);
            return PartialView("_TownPartial", result);
        }

        [Authorize]
        public ActionResult Owner(int id)
        {
            if (Busniss.BusnissLayer.isCurrentCustomer(id))
            {
                var owner = Busniss.BusnissLayer.GetOwnerById(id);
                return View("otherOwner", owner);
            }
            else
            {
                Busniss.BusnissLayer.AddOwnerShown(id);
                var owner = Busniss.BusnissLayer.GetOwnerById(id);
                return View(owner);
            }
        }

        public ActionResult MeOwner()
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer cust = Busniss.BusnissLayer.getCurrentCustomer(DB);
            return RedirectToAction("Owner", new { id = cust.ID });
        }


        public ActionResult REPaymentAssist()
        {
          
            return View();
        }
        #endregion

        //#region security
        //private ApplicationUserManager _userManager;
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> register(Busniss.Register model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser() { UserName = model.name, Email = model.Email };
        //        IdentityResult result = await UserManager.CreateAsync(user, model.password);
        //        if (result.Succeeded)
        //        {
        //            await SignInAsync(user, isPersistent: false);
                    

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            AddErrors(result);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}
        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindAsync(model.Email, model.Password);
        //        if (user != null)
        //        {
        //            await SignInAsync(user, model.RememberMe);
        //            return RedirectToLocal(returnUrl);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid username or password.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        //#endregion
    }


}