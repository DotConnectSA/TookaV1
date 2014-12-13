using RealEstateV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace RealEstateV1.Busniss
{
    public static class BusnissLayer
    {
        
        public static List<T_City> GetCites()
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TCity.ToList();
        }
        public static List<T_TownFeature> getTownFeatureKind( )
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTFeature.ToList();
        }
        public static SelectList getTownFeatureKindList()
        {
            RealEstateContext DB = new RealEstateContext();
            SelectList a = new SelectList(getTownFeatureKind(), "ID", "Feature");
            return a;
        }
        
        public static SelectList GetCitesList()
        {
            RealEstateContext DB = new RealEstateContext();
            SelectList a = new SelectList(GetCites(), "ID", "City");
            return a;
        }
        public static void AddOwnerShown(int ownerID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TOwner.Single(a => a.ID == ownerID).ShowNumber++;
            DB.SaveChanges();

        }
        public static void AddRepaly(int DiscussID,string text)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Replay rep = new T_Replay();
            rep.CreatedDate = DateTime.Now;
            rep.Customer = getCurrentCustomer(DB);
            rep.Replaytxt = text;
            rep.ReportNo = 0;
            DB.TDiscuss.Single(a => a.ID == DiscussID).Replay.Add(rep);
            DB.SaveChanges();
        }
        public static T_Customer getCurrentCustomer(RealEstateContext DB)
        {
            var context = new ApplicationDbContext();
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);

            T_Customer cust = DB.TCustomer.Single(x => x.ProfileID == currentUser.Id);
            
            return cust;
        }
        public static void AddDiscuss(string topic)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Discuss dis = new T_Discuss();
            dis.CreatedDate = DateTime.Now;
            dis.DisLikeNo = 0;
            dis.LikeNo = 0;
            dis.ReportNo = 0;
            dis.Topic = topic;

            dis.Customer = getCurrentCustomer(DB);

            DB.TDiscuss.Add(dis);

            DB.SaveChanges();
        }

        public static void AddTownComment(float Rate,string Title, string Comment)
        {
            RealEstateContext DB = new RealEstateContext();
            T_TownComment townCom = new T_TownComment();
            townCom.Rate = Rate;
            townCom.CreateDate = DateTime.Now;
            townCom.Comment = Comment;
            townCom.Title = Title;
            townCom.Customer = getCurrentCustomer(DB);

            DB.TTownComment.Add(townCom);
            DB.SaveChanges();
        }

        public static T_Town GetTownID(int townID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTown.Single(a => a.ID == townID);

        }
        public static T_Owner AddOwnerbyID(int ownerID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TOwner.Single(a => a.ID == ownerID);
          

        }
        public static List<T_REFeature> GetREFeatuer()
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TREFeature.ToList();
        }
        public static SelectList GetREFeatureList()
        {
            RealEstateContext DB = new RealEstateContext();
            SelectList a = new SelectList(GetREFeatuer(), "ID", "Feature");
            return a;
        }
        public static List<T_REKind> GetREKind()
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TREKind.ToList();
        }

        public static List<T_RealEstate> GetREOwners(int ownerID) 
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TRealEstate.Where(a => a.Owner.ID == ownerID).ToList();
            }
            catch (Exception)
            {

                return null;
            }
           
        }
        public static List<T_Rent> GetREOwnersRent(int ownerID)
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TRent.Where(a => a.RealEstate.Owner.ID == ownerID && a.Status == MyEnumType.RentRealEstateStatus.active).ToList();
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static List<T_Sale> GetREOwnerSale(int ownerID)
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TSale.Where(a => a.RealEstate.Owner.ID == ownerID && a.Status == MyEnumType.SaleRealEstateStatus.active).ToList();
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static List<T_Sale> GetSoldRE(int ownerID)
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TSale.Where(a => a.RealEstate.Owner.ID == ownerID && a.Status ==MyEnumType.SaleRealEstateStatus.sold).ToList();
            }
            catch (Exception)
            {

                return null;
            }
           
        }
        public static int GetOwnerActivityNO (int ownerID)
        {
            return 0;
        }
        public static SelectList GetREKindList()
        {
            var list= GetREKind();
            list.Add(new T_REKind() { ID = 0, Kind = "نوع العقار" });
            SelectList a = new SelectList(list, "ID", "Kind");
            
            return a;
        }
        public static List<T_Town> GetTwonByCityID(int cityID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTown.Where(a=>a.City.ID==cityID).ToList();
        }
        public static T_RealEstate GetRealEstateByID(int REID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TRealEstate.SingleOrDefault(a => a.ID == REID);
        }
        public static List<DropDown> GetTownByCityID(int cityID)
        {
            var states = GetTwonByCityID(cityID);
            var result = (from s in states
                          select new DropDown
                          {
                              id = s.ID,
                              name = s.TownName
                          }).ToList();

            return result;
        }
        public static bool reportD(int DiscussID)
        {
            RealEstateContext DB = new RealEstateContext();
            //DB.TDiscuss.Single(a => a.ID == DiscussID).ReportNo++;
            T_Discuss diss = DB.TDiscuss.Find(DiscussID);
            diss.ReportNo++;
            DB.Entry(diss).State = EntityState.Modified;
            DB.SaveChanges();
            return true;

        }
        public static bool LikeD(int DiscussID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TDiscuss.Single(a => a.ID == DiscussID).LikeNo++;
            DB.SaveChanges();
            return true;
        }
        public static bool DisLikeD(int DiscussID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TDiscuss.Single(a => a.ID == DiscussID).DisLikeNo++;
            DB.SaveChanges();
            return true;

        }
        public static bool reportR(int ReplayID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TRepaly.Single(a => a.ID == ReplayID).ReportNo++;
            DB.SaveChanges();
            return true;

        }

        public static List<T_Discuss> GetDiscuss()
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TDiscuss.ToList();
        }
        public static List<T_Rent> getRent(SearchItem item)
        {
            RealEstateContext DB = new RealEstateContext();
             var result = DB.TRent.OrderBy(a => a.RentPrice).AsQueryable();
            if (Busniss.SessionManager.searchKey != null)
            {
                if (item.Area > 0)
                {
                    result = result.Where(a => a.RealEstate.Area == item.Area).AsQueryable();
                }
                if (item.Bathno > 0)
                {
                    result = result.Where(a => a.RealEstate.BathroomNo == item.Bathno).AsQueryable();
                }
                if (item.City > 0)
                {
                    result = result.Where(a => a.RealEstate.address.Town.City.ID == item.City).AsQueryable();
                }
                if (item.LowerPrice > 0)
                {
                    result = result.Where(a => a.Price < item.LowerPrice).AsQueryable();
                }
                if (item.UperPrice > 0)
                {
                    result = result.Where(a => a.Price > item.UperPrice).AsQueryable();
                }
                if (item.RealEstateKind > 0)
                {
                    result = result.Where(a => a.RealEstate.Rekind.ID < item.RealEstateKind).AsQueryable();
                }
                if (item.RoomNo > 0)
                {
                    result = result.Where(a => a.RealEstate.RoomNo < item.RoomNo).AsQueryable();
                }
                if (item.Town > 0)
                {
                    result = result.Where(a => a.RealEstate.address.Town.ID < item.Town).AsQueryable();
                }
                if(item.feature!=null)
                foreach (int feature in item.feature)
                {
                    result = result.Where(a => a.RealEstate.RealEstateFeature.Any(b => b.ID == feature)).AsQueryable();
                }
            }
                return result.ToList();
        }
    }
}