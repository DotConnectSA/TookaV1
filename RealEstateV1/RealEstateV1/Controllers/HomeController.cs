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
    [RequireHttps]
    public class HomeController : Controller
    {

        public void initialization()
        { 
            var town = new Dictionary<string, int> { { "الحي ", 0 } };
            var bathno = new Dictionary<string, int> { { "عدد الحمامات", 0 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 } };
            var roomno = new Dictionary<string, int> { { " عدد الغرف", 0 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 } };
            var lower = new Dictionary<string, int> { { "السعر الادنى", 0 }, { "100", 100 }, { "200", 200 }, { "300", 300 }, { "400", 400 }, { "500", 500 } };
            var uper = new Dictionary<string, int> { { "السعر الاعلى ", 0 }, { "10000", 10000 }, { "20000", 20000 }, { "30000", 30000 }, { "40000", 40000 }, { "50000", 50000 } };
            var area = new Dictionary<string, int> { { "المساحة ", 0 }, { "100", 100 }, { "200", 200 }, { "300", 300 }, { "400", 400 }, { "500", 500 } };
          //  ViewData["DDL"] = new SelectList(selectItems, "Key", "Value", id == "MyTest" ? "MyTest" : "Test2");
            ViewBag.Cites = Busniss.BusnissLayer.GetCitesList();
            ViewBag.bathno = new SelectList(bathno, "Value", "Key");
            ViewBag.roomno = new SelectList(roomno, "Value", "Key");
            ViewBag.RSkind = Busniss.BusnissLayer.GetREKindList();
            ViewBag.lowerprice = new SelectList(lower, "Value", "Key");
            ViewBag.uperprice = new SelectList(uper, "Value", "Key");
            ViewBag.area = new SelectList(area, "Value", "Key"); 
            ViewBag.REFeature = Busniss.BusnissLayer.GetREFeatuer();
            ViewBag.town = new SelectList(town, "Value", "Key");


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
        }
        #region actions

        [HttpGet]
        public ActionResult Index()
        {
            initialization();

            return View();
        }

        [HttpPost]
        public ActionResult Index(Busniss.SearchItem item,int []feature)
        {
           
            //if (item.Town==0)
            //{
            //    ModelState.AddModelError("الحي", "يرجى ادخال الحي");
            //}
          //  if (ModelState.IsValid)
            {
                if (feature != null)
                    item.feature = feature.ToList();
                else item.feature = new List<int>();
                Busniss.SessionManager.searchKey = item;
                if(item.SearchKind==Busniss.MyEnumType.SearchKind.Rent)
                    return RedirectToAction("Rent");
                if(item.SearchKind==Busniss.MyEnumType.SearchKind.Sale)
                    return RedirectToAction("Sale");
                if(item.SearchKind==Busniss.MyEnumType.SearchKind.Oldsale)
                    return RedirectToAction("Result");
            }
            return Index();
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTown(string CityID)
        {
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
        public ActionResult GetTowninfo(string townID)
        {
            if (String.IsNullOrEmpty(townID))
            {
                throw new ArgumentNullException("townID");
            }
            int id = 0;
            bool isValid = Int32.TryParse(townID, out id);
            var result = Busniss.BusnissLayer.GetTownID(id);
            return Json(result.Comment.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetComment(int CommentID, int index)
        {
            ViewBag.index = index;
            var result = Busniss.BusnissLayer.GetCommentByID(CommentID);
            return PartialView("_TownPartial", result);
        }

        public ActionResult GetOwnerByCity(string CityId)
        {
            if (String.IsNullOrEmpty(CityId))
            {
                throw new ArgumentNullException("CityId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(CityId, out id);
            var result = Busniss.BusnissLayer.GetOwnerByCity(id);
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOwner(int OwnerId)
        {
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
            var owner = db.TOwner.ToList();
            return View(owner);
        }
        


        public ActionResult RealEstate(int ID)
        {
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
        
        [HttpGet]
        public ActionResult Rent()
        {
            initialization();
            ViewBag.ReturnUrl = "rent";
            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
            item = Busniss.SessionManager.searchKey;
            }
            var result = Busniss.BusnissLayer.getRent(item);
             //Busniss.SessionManager.Result = result; //to be done for the rest of views
            return View(result.AsEnumerable<T_Rent>());

        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeSearch(Busniss.SearchItem search)
        {
            Busniss.SearchItem item = null;
            if (Busniss.SessionManager.searchKey != null)
            {
                item = Busniss.SessionManager.searchKey;
            }
            else item = new Busniss.SearchItem();

            if (search.Bathno != 0)
                item.Bathno = search.Bathno;
            if (search.City != 0)
                item.City = search.City;
            if (search.LowerPrice != 0)
                item.LowerPrice = search.LowerPrice;
            if (search.RoomNo != 0)
                item.RoomNo = search.RoomNo;
            if (search.Town != 0)
                item.Town = search.Town;
            if (search.UperPrice != 0)
                item.UperPrice = search.UperPrice;
            if (search.RealEstateKind != 0)
                item.RealEstateKind = search.RealEstateKind;

             Busniss.SessionManager.searchKey=item;

            if(search.SearchKind==Busniss.MyEnumType.SearchKind.Rent)
            {  
              return RedirectToAction("Rent");
            }

             if (search.SearchKind == Busniss.MyEnumType.SearchKind.Oldsale)
             { 
             return RedirectToAction("Sale");
             }

             if (search.SearchKind == Busniss.MyEnumType.SearchKind.Sale)
             {   return RedirectToAction("Sale");
             }
             return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Sale()
        {
           initialization();
           ViewBag.ReturnUrl = "sale";

           // ViewBag.Message = "Your contact page.";

            return View();
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
            Busniss.BusnissLayer.LikeOwner(ID);
            return RedirectToAction("RealEstatesOwners", "Home");
        }

        [Authorize]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LikeD(int ID)
        {
            string value = Busniss.CookieManager.CookieValue("Discuss", "Discuss" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Discuss", "Discuss" + ID, "true",7);
                bool result = Busniss.BusnissLayer.LikeD(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "false")
            {
                Busniss.CookieManager.SetCookie("Discuss", "Discuss" + ID, "true", 7);
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
        public ActionResult LikeC(int ID)
        {
            string value = Busniss.CookieManager.CookieValue("Comment", "Comment" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Comment", "Comment" + ID, "true", 7);
                bool result = Busniss.BusnissLayer.LikeC(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "false")
            {
                Busniss.CookieManager.SetCookie("Comment", "Comment" + ID, "true", 7);
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
            string value = Busniss.CookieManager.CookieValue("Discuss", "Discuss" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Discuss", "Discuss" + ID, "false", 7);
                bool result = Busniss.BusnissLayer.DisLikeD(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "true")
            {
                Busniss.CookieManager.SetCookie("Discuss", "Discuss" + ID, "false", 7);
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
            string value = Busniss.CookieManager.CookieValue("Comment", "Comment" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("Comment", "Comment" + ID, "false", 7);
                bool result = Busniss.BusnissLayer.DisLikeC(ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (value == "true")
            {
                Busniss.CookieManager.SetCookie("Comment", "Comment" + ID, "false", 7);
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
            string value = Busniss.CookieManager.CookieValue("RepotDiscuss", "RepotDiscuss" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("RepotDiscuss", "RepotDiscuss" + ID, "true", 7);
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
            string value = Busniss.CookieManager.CookieValue("RepotComment", "RepotComment" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("RepotComment", "RepotComment" + ID, "true", 7);
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
            string value = Busniss.CookieManager.CookieValue("RepotReport", "RepotReport" + ID);
            if (value == null)
            {
                Busniss.CookieManager.SetCookie("RepotReport", "RepotReport" + ID, "true", 7);
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
            Busniss.BusnissLayer.AddDiscussRepaly(ID, test);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult AddCommentRepaly(int ID, string test)
        {
            Busniss.BusnissLayer.AddCommentRepaly(ID, test);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Discuss()
        {
            initialization();
            ViewBag.ReturnUrl = "discuss";
            var dis = Busniss.BusnissLayer.GetDiscuss();
            return View(dis);
        }

        public ActionResult Supply()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.ReturnUrl = "supply";
            return View();
        }

        public ActionResult TownInformation(int CityID = 1, int townID = 1)
        {
            initialization();
            ViewBag.ReturnUrl = "towninformation";
            T_Town t = Busniss.BusnissLayer.GetTownID(townID);


            return View(t);
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