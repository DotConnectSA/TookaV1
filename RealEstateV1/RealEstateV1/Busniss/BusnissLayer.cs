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
        public static void Register(RegisterCustomerViewModel model)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = new T_Customer();
            customer = model.Customer;
            customer.Email = model.Register.Email;
            DB.TCustomer.Add(customer);
            if (model.Customer.Kind == "owner")
            {
                T_Owner owner = new T_Owner();
                owner.customer = customer;
                owner.ShowNumber = 0;
                DB.TOwner.Add(owner);
            }
            DB.SaveChanges();
            //T_Customer cust = DB.TCustomer.Single(a => a.Email == customer.Email);
            //T_Owner owner = new T_Owner();
            //owner.customer = cust;
            //DB.SaveChanges();
        }

        public static void ExternalRegister(RegisterExternalCustomerViewModel model)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = new T_Customer();
            customer = model.Customer;
            customer.Email = model.Register.Email;
            DB.TCustomer.Add(customer);

            DB.SaveChanges();

        }

        public static List<T_City> GetCites()
        {
            RealEstateContext DB = new RealEstateContext();
            List<T_City> citys=DB.TCity.ToList();
            return citys;
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
        public static T_TownComment GetCommentByID(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTownComment.Single(a => a.ID == id);
        }
        public static List<T_Owner> GetOwnerByCity(int CityId)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TOwner.Where(a => a.customer.address.Town.City.ID == CityId).ToList();
            
        }
        public static T_Owner GetOwnerById(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TOwner.Single(a => a.ID == id);
        }
        public static void AddOwnerShown(int ownerID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TOwner.Single(a => a.ID == ownerID).ShowNumber++;
            DB.SaveChanges();

        }
        public static List<T_TownLinkFeature> GetDescByfeature(int FeatureId)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTownLinkFeature.Where(a => a.townFeature.ID == FeatureId).ToList();
        }

        public static T_TownLinkFeature GetFeature(int FeatureDescId)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTownLinkFeature.Single(a => a.ID == FeatureDescId);
        }

        public static void AddDiscussRepaly(int DiscussID, string text)
        {
            //if flag==true then its replay for discuss else its for towncomment
            RealEstateContext DB = new RealEstateContext();
            T_Replay rep = new T_Replay();
            rep.CreatedDate = DateTime.Now;
            rep.Customer = getCurrentCustomer(DB);
            rep.Replaytxt = text;
            rep.ReportNo = 0;
            DB.TDiscuss.Single(a => a.ID == DiscussID).Replay.Add(rep);
            DB.SaveChanges();
        }

        public static void AddCommentRepaly(int CommentID, string text)
        {
            //if flag==true then its replay for discuss else its for towncomment
            RealEstateContext DB = new RealEstateContext();
            T_Replay rep = new T_Replay();
            rep.CreatedDate = DateTime.Now;
            rep.Customer = getCurrentCustomer(DB);
            rep.Replaytxt = text;
            rep.ReportNo = 0;
            DB.TTownComment.Single(a => a.ID == CommentID).Replay.Add(rep);
            DB.SaveChanges();
        }
        public static T_Customer getCurrentCustomer(RealEstateContext DB)
        {
            var context = new ApplicationDbContext();
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            //ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);

            T_Customer cust = DB.TCustomer.Single(x => x.ProfileID == currentUserId);
            
            return cust;
        }
        public static bool isCurrentCustomer(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer cust = getCurrentCustomer(DB);
            if (DB.TOwner.Single(a => a.ID == id).customer.ID == cust.ID)
            {
                return true;
            }
            else
            {
                return false;
            }
 
        }
        public static List<T_Favorit> GetFavoritEstates(int ownerID)
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TFavorit.Where(a => a.Customer.ID == ownerID).ToList();
            }
            catch (Exception)
            {

                return null;
            }
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

        public static void AddTownComment(float Rate, string Title, string Comment, int Town_ID)
        {
            RealEstateContext DB = new RealEstateContext();
            T_TownComment townCom = new T_TownComment();
            townCom.Rate = Rate;
            townCom.CreateDate = DateTime.Now;
            townCom.Comment = Comment;
            townCom.Title = Title;
            townCom.DisLikeNo = 0;
            townCom.LikeNo = 0;
            townCom.ReportNo = 0;
            townCom.Customer = getCurrentCustomer(DB);

            DB.TTown.Single(a => a.ID == Town_ID).Comment.Add(townCom);

            DB.SaveChanges();
        }

        public static T_Town GetTownID(int townID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTown.Single(a => a.ID == townID);

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

        public static List<KeyValuePair<string, int>> getFeatureRate(int townID)
        {
            RealEstateContext DB = new RealEstateContext();
            List<T_TownFeature> townFeature = DB.TTFeature.ToList();
            T_Town town = DB.TTown.SingleOrDefault(a => a.ID == townID);
            var townRate = DB.TTownRate.Distinct();
            List<KeyValuePair<string, int>> pair = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < town.townLinkFeature.Count(); i++)
            {
                KeyValuePair<string, int> p = new KeyValuePair<string, int>(town.townLinkFeature[i].townFeature.Feature, town.townLinkFeature[i].Rate.Rate);
                pair.Add(p);
            }
            List<KeyValuePair<string, int>> res = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < townFeature.Count(); i++)
            {
                int counter = 0, sum = 0;
                for (int j = 0; j < pair.Count(); j++)
                {
                    if (townFeature[i].Feature == pair[j].Key)
                    {
                        sum += pair[j].Value;
                        counter++;
                    }
                }
                res.Add(new KeyValuePair<string, int>(townFeature[i].Feature, sum / counter));
            }
            return res;
        }

        public static int GetOwnerActivityNO (int ownerID)
        {
            return 0;
        }
        public static List<T_OwnerLike> getOwnerIlike(int id)
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TOwnerLike.Where(a => a.Customer.ID == id).ToList();
            }
            catch (Exception)
            {

                return null;
            }
 
        }
        public static SelectList GetREKindList()
        {
            var list= GetREKind();
            list.Add(new T_REKind() { ID = 0, Kind = "نوع العقار" });
            SelectList a = new SelectList(list, "ID", "Kind");
            
            return a;
        }
        public static List<T_Town> GetTownByCityID(int cityID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTown.Where(a=>a.City.ID==cityID).ToList();
        }


        public static T_RealEstate GetRealEstateByID(int REID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TRealEstate.SingleOrDefault(a => a.ID == REID);
        }

        public static T_Rent GetRentByRSID(int REID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TRent.SingleOrDefault(a => a.RealEstate.ID == REID);
        }

        public static T_Sale GetSaleByRSID(int REID)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TSale.SingleOrDefault(a => a.RealEstate.ID == REID);
        }
        public static List<DropDown> GetTownByCityIDDropDown(int cityID)
        {
            var states = GetTownByCityID(cityID);
            var result = (from s in states
                          select new DropDown
                          {
                              id = s.ID,
                              name = s.TownName
                          }).ToList();

            return result;
        }

        public static void LikeOwner(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            T_OwnerLike ownerLike = new T_OwnerLike();
            ownerLike.Customer = getCurrentCustomer(DB);
            ownerLike.CutomerLikeID = getCurrentCustomer(DB).ID;
            T_Owner Owner = DB.TOwner.SingleOrDefault(a => a.ID == id);
            Owner.OwnerLike.Add(ownerLike);
            DB.Entry(ownerLike).State = EntityState.Added;
            DB.SaveChanges(); 
        }
        //public static bool isLikedOwner(string OwnerId)
        //{
        //    //RealEstateContext DB = new RealEstateContext();
        //    //T_Customer customer = new T_Customer();
        //    //customer = getCurrentCustomer(DB);
        //    //int count = DB.TOwner.SingleOrDefault(a => a.OwnerLike.ToString() == OwnerId).OwnerLike.Count();
        //    //if (count == 0)
        //    //    return false;
        //    //else
        //    //    return true;
 
        //}
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

        public static bool reportC(int CommentID)
        {
            RealEstateContext DB = new RealEstateContext();
            //DB.TTownComment.Single(a => a.ID == CommentID).ReportNo++;
            T_TownComment comm = DB.TTownComment.Find(CommentID);
            comm.ReportNo++;
            DB.Entry(comm).State = EntityState.Modified;
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
        public static bool RemoveLikeD(int DiscussID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TDiscuss.Single(a => a.ID == DiscussID).LikeNo--;
            DB.SaveChanges();
            return true;
        }
        public static bool LikeC(int CommentID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TTownComment.Single(a => a.ID == CommentID).LikeNo++;
            DB.SaveChanges();
            return true;
        }
        public static bool RemoveLikeC(int CommentID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TTownComment.Single(a => a.ID == CommentID).LikeNo--;
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
        public static bool RemoveDisLikeD(int DiscussID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TDiscuss.Single(a => a.ID == DiscussID).DisLikeNo--;
            DB.SaveChanges();
            return true;
        }
        public static bool DisLikeC(int CommentID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TTownComment.Single(a => a.ID == CommentID).DisLikeNo++;
            DB.SaveChanges();
            return true;
        }
        public static bool RemoveDisLikeC(int CommentID)
        {
            RealEstateContext DB = new RealEstateContext();
            DB.TTownComment.Single(a => a.ID == CommentID).DisLikeNo--;
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