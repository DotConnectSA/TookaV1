using RealEstateV1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateV1.Areas.admin.Controllers
{
    public class ProfileController : Controller
    {
        // GET: admin/Profile

        public ActionResult Index()
        {
            RealEstateContext DB = new RealEstateContext();
            SelectList a = Busniss.BusnissLayer.GetCitesList();
            ViewBag.Cites = a;
            int tmp = Convert.ToInt32(a.First().Value);
            SelectList b = Busniss.BusnissLayer.GetTownList(tmp);
            ViewBag.town = b;

            SelectList c = Busniss.BusnissLayer.GetRealStateKinds();
            ViewBag.realstatekinds = c;
            T_Owner on = Busniss.BusnissLayer.GetCurrentOwner(DB);
            ViewBag.REFeature = Busniss.BusnissLayer.GetREFeatuer();
            return View(on);
        }

        public ActionResult Upload()
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customerId = Busniss.BusnissLayer.getCurrentCustomer(DB);
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                string Pth = Server.MapPath("~/customer_logos/" + customerId.ID);

                if (!Directory.Exists(Pth))
                {
                    Directory.CreateDirectory(Pth);
                }
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Pth, fileName);
                file.SaveAs(path);
                customerId.ImgeUrl = "../customer_logos//" + customerId.ID + "//" + fileName;
                DB.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

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

        public ActionResult AddFullRealState(string cities, string townes, string street, string building, string floor, string door,string realstatekind , string currency,string realstateDesc, string isrent, string realstatesituation,
            string extras,string x,string y,string Adjectives,string  availability,string cus_name,string cus_email,string cus_phone,string cus_mobile)
        { 
            
           
            return Json(true, JsonRequestBehavior.AllowGet);
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult edit_customer(string fle_upload, string ID, string Name, string cities, string townes, string street, string building, string floor, string tel, string mob, string door,string email)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Owner owmer = Busniss.BusnissLayer.GetOwnerById(Convert.ToInt32(ID));
            T_Customer customer = new T_Customer();
            customer.ID = owmer.customer.ID;
            customer.Name = Name;
            customer.Phone = tel;
            customer.Mobile = mob;
            customer.Email = email;
            customer.ImgeUrl = fle_upload;
            Busniss.BusnissLayer.updateCustomer(customer);
            T_Address address = new T_Address();
            address.ID = Busniss.BusnissLayer.GetAdressOfCustomer(customer.ID);
            address.door = door;
            address.floor = floor;
            address.Street = street;
            address.building = building;
            address.Town = Busniss.BusnissLayer.GetTownID(Convert.ToInt32(townes));
            //address.Town.ID =
            Busniss.BusnissLayer.updateAddress(address);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}