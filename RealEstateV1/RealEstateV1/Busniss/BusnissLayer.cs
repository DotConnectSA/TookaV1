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

        public static List<string> hideEstateList(int custID)
        {
            List<string> list = CookieManager.getKeyList("HideCust" + custID);
            return list;
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

        public static void AddTownRate(addRateInfo rateInfo, int townId)
        {
            RealEstateContext DB = new RealEstateContext();
            for (int i = 0; i < rateInfo.allFeature.Count(); i++)
            {
                T_TownRate townRate = new T_TownRate();
                townRate.Rate = rateInfo.featureRate[i];
                townRate.Customer = getCurrentCustomer(DB);
                DB.TTownRate.Add(townRate);
                DB.SaveChanges();


                T_TownLinkFeature linkFeature = new T_TownLinkFeature();
                linkFeature.Rate = townRate;
                int id = rateInfo.allFeature[i].ID;
                T_TownFeature townFeature = DB.TTFeature.Single(x => x.ID.Equals(id));
                linkFeature.townFeature = townFeature;
                DB.TTownLinkFeature.Add(linkFeature);
                DB.SaveChanges();

                DB.TTown.Single(a => a.ID.Equals(townId)).townLinkFeature.Add(linkFeature);
                DB.SaveChanges();
            }
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
            List<T_City> c = GetCites();
            List<T_City> cityList = new List<T_City>();
            T_City city = new T_City();
            city.ID = 0;
            city.City = "المدينة";
            cityList.Add(city);
            for (int i=0;i<c.Count;i++)
            {
                cityList.Add(c[i]);
            }
            SelectList a = new SelectList(cityList, "ID", "City");
            return a;
        }

        public static T_TownComment GetCommentByID(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTownComment.Single(a => a.ID == id);
        }


        public static T_Discuss GetDiscussByID(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TDiscuss.Single(a => a.ID == id);
        }

        public static List<T_TownComment> GetCommnetList(int cityid, int townid)
        {
            RealEstateContext DB = new RealEstateContext();
            List<T_TownComment> CommentList = new List<T_TownComment>();
            if (cityid==0)
            {
                List<T_Town> townList = DB.TTown.ToList();
                for (int i=0;i<townList.Count();i++)
                {
                    for (int j=0;j<townList[i].Comment.Count();j++)
                    {
                        CommentList.Add(townList[i].Comment[j]);
                    }
                }
                return CommentList;
            }
            if (townid==0)
            {
                List<T_Town> townList = DB.TTown.Where(x => x.City.ID == cityid).ToList();
                for (int i = 0; i < townList.Count(); i++)
                {
                    for (int j = 0; j < townList[i].Comment.Count(); j++)
                    {
                        CommentList.Add(townList[i].Comment[j]);
                    }
                }
                return CommentList;
            }
            else
            {
                T_Town town = DB.TTown.Single(x => x.ID == townid);
                for (int i = 0; i < town.Comment.Count(); i++)
                {
                    CommentList.Add(town.Comment[i]);
                }
                return CommentList;
            }
        }

        public static List<T_Owner> GetOwnerByCity(int CityId)
        {
            RealEstateContext DB = new RealEstateContext();
            if(CityId==0)
            {
                return DB.TOwner.ToList();
            }
            else
            {
                return DB.TOwner.Where(a => a.customer.address.Town.City.ID == CityId).ToList();
            }
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

        public static List<T_TownFeature> GetAllFeature()
        {
            RealEstateContext DB = new RealEstateContext();
            return DB.TTFeature.ToList();
        }

        public static T_Replay AddDiscussRepaly(int DiscussID, string text)
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
            return rep;
        }

        public static T_Replay AddCommentRepaly(int CommentID, string text)
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
            return rep;
        }
        public static T_Customer getCurrentCustomer(RealEstateContext DB)
        {
            var context = new ApplicationDbContext();
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            //ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);

            T_Customer cust = DB.TCustomer.SingleOrDefault(x => x.ProfileID == currentUserId);
            
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
        public static List<T_SearchHistory> GetSearchEstates(int ownerID)
        {
            try
            {
                RealEstateContext DB = new RealEstateContext();
                return DB.TSearchHistory.Where(a => a.Customer.ID == ownerID).ToList();
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

        public static List<FeatureRate> getFeatureRate(int townID)
        {
            RealEstateContext DB = new RealEstateContext();
            List<T_TownFeature> townFeature = DB.TTFeature.ToList();
            T_Town town = DB.TTown.SingleOrDefault(a => a.ID == townID);
            var townRate = DB.TTownRate.Distinct();
            List<FeatureRate> pair = new List<FeatureRate>();
            for (int i = 0; i < town.townLinkFeature.Count(); i++)
            {
                FeatureRate p=new FeatureRate();
                p.feature = town.townLinkFeature[i].townFeature.Feature;
                p.rate = town.townLinkFeature[i].Rate.Rate;
                pair.Add(p);
            }
            List<FeatureRate> res = new List<FeatureRate>();
            for (int i = 0; i < townFeature.Count(); i++)
            {
                int counter = 0, sum = 0;
                for (int j = 0; j < pair.Count(); j++)
                {
                    if (townFeature[i].Feature == pair[j].feature)
                    {
                        sum += pair[j].rate;
                        counter++;
                    }
                }
                if(counter!=0)
                {
                    FeatureRate FR = new FeatureRate();
                    FR.feature = townFeature[i].Feature;
                    FR.rate = sum / counter;
                    FR.count = counter;
                    res.Add(FR);
                }
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

        public static SelectList GetTownByCityIDSL(int cityID)
        {
            RealEstateContext DB = new RealEstateContext();
            SelectList a = new SelectList(GetTownByCityID(cityID), "ID", "TownName");
            return a;
        }

        public static List<RealEstateFull> GetSearchHistory()
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = getCurrentCustomer(DB);

            List<T_SearchHistory> searchHistory = DB.TSearchHistory.Where(x => x.Customer.ID == customer.ID).OrderBy(x => x.QuaryDate).ToList();
            List<RealEstateFull> realEstate = new List<RealEstateFull>();

            for (int i = 0; i < searchHistory.Count(); i++)
            {
                RealEstateFull RE = new RealEstateFull();
                RE.realEstate = searchHistory[i].realEstate;
                RE.rent = GetRentByRSID(RE.realEstate.ID);
                RE.sale = GetSaleByRSID(RE.realEstate.ID);
                realEstate.Add(RE);
            }

            return realEstate;
        }

        public static List<RealEstateFull> GetFavorit()
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = getCurrentCustomer(DB);

            List<T_Favorit> favorit = DB.TFavorit.Where(x => x.Customer.ID == customer.ID).ToList();
            List<RealEstateFull> realEstate = new List<RealEstateFull>();

            for (int i = 0; i < favorit.Count(); i++)
            {
                RealEstateFull RE = new RealEstateFull();
                RE.realEstate = favorit[i].RealEstate;
                RE.rent = GetRentByRSID(RE.realEstate.ID);
                RE.sale = GetSaleByRSID(RE.realEstate.ID);
                realEstate.Add(RE);
            }

            return realEstate;
        }

        public static void AddSearchHistory(int estateID)
        {
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return;
            }
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = getCurrentCustomer(DB);

            List<T_SearchHistory> searchHistoryList = DB.TSearchHistory.Where(x => x.Customer.ID == customer.ID).OrderBy(x=>x.QuaryDate).ToList();
            T_SearchHistory searchHistory = new T_SearchHistory();
            
            searchHistory = DB.TSearchHistory.SingleOrDefault(x => x.Customer.ID == customer.ID && x.realEstate.ID == estateID);

            if (searchHistory !=null)
            {
                searchHistory.QuaryDate = DateTime.Now;
                DB.Entry(searchHistory).State = EntityState.Modified;
                DB.SaveChanges();
                return;
            }

            if (searchHistoryList.Count() < 3)
            {
                searchHistory = new T_SearchHistory();
                searchHistory.QuaryDate = DateTime.Now;
                searchHistory.Customer = getCurrentCustomer(DB);
                searchHistory.realEstate = DB.TRealEstate.SingleOrDefault(a => a.ID == estateID);
                DB.TSearchHistory.Add(searchHistory);

                DB.SaveChanges();
            }
            else
            {
                DB.TSearchHistory.Remove(searchHistoryList[0]);

                searchHistory = new T_SearchHistory();
                searchHistory.QuaryDate = DateTime.Now;
                searchHistory.Customer = getCurrentCustomer(DB);
                searchHistory.realEstate = DB.TRealEstate.SingleOrDefault(a => a.ID == estateID);
                DB.TSearchHistory.Add(searchHistory);

                DB.SaveChanges();
            }

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
            List<T_Town> st = GetTownByCityID(cityID);
            T_Town town = new T_Town();
            town.ID = 0;
            town.TownName = "الحي";
            List<T_Town> states = new List<T_Town>();
            states.Add(town);
            for (int i=0;i<st.Count();i++)
            {
                states.Add(st[i]);
            }
            var result = (from s in states
                          select new DropDown
                          {
                              id = s.ID,
                              name = s.TownName
                          }).ToList();

            return result;
        }

        public static bool LikeOwner(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            T_OwnerLike ownerLike = new T_OwnerLike();
            ownerLike.Customer = getCurrentCustomer(DB);
            T_Owner Owner = DB.TOwner.SingleOrDefault(a => a.ID == id);
            ownerLike.Owner = Owner;
            DB.Entry(ownerLike).State = EntityState.Added;
            DB.SaveChanges();
            return true;
        }

        public static bool DisLikeOwner(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = new T_Customer();
            customer = getCurrentCustomer(DB);
            T_OwnerLike like = DB.TOwnerLike.SingleOrDefault(a => a.Owner.ID == id && a.Customer.ID == customer.ID);
            DB.Entry(like).State = EntityState.Deleted;
            DB.SaveChanges();
            return true;
        }

        public static bool LikeEstate(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Favorit LikeEstate = new T_Favorit();
            LikeEstate.status = RealEstateV1.Busniss.MyEnumType.FavoritStatus.active;
            LikeEstate.Customer = getCurrentCustomer(DB);
            T_RealEstate estate = DB.TRealEstate.SingleOrDefault(a => a.ID == id);
            LikeEstate.RealEstate = estate;
            DB.Entry(LikeEstate).State = EntityState.Added;
            DB.SaveChanges();
            return true;
        }

        public static bool DisLikeEstate(int id)
        {
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = new T_Customer();
            customer = getCurrentCustomer(DB);
            T_Favorit favorite = DB.TFavorit.SingleOrDefault(a => a.RealEstate.ID == id && a.Customer.ID == customer.ID);
            DB.Entry(favorite).State = EntityState.Deleted;
            DB.SaveChanges();
            return true;
        }

        public static bool isLikedOwner(int OwnerId)
        {
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return false;
            }
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = new T_Customer();
            customer = getCurrentCustomer(DB);
            T_OwnerLike like = DB.TOwnerLike.SingleOrDefault(a => a.Owner.ID == OwnerId && a.Customer.ID == customer.ID);
            if (like == null)
                return false;
            else
                return true;

        }
        public static bool isLikedEstate(int id)
        {
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return false;
            }
            RealEstateContext DB = new RealEstateContext();
            T_Customer customer = new T_Customer();
            customer = getCurrentCustomer(DB);
            T_Favorit favorite = DB.TFavorit.SingleOrDefault(a => a.RealEstate.ID == id && a.Customer.ID == customer.ID);
            if (favorite == null)
                return false;
            else
                return true;

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
            T_Customer customer = getCurrentCustomer(DB);
            if (customer != null)
            {
                List<T_Rent> rentList = new List<T_Rent>();
                List<string> list = hideEstateList(customer.ID);
                if (list == null)
                    return result.ToList();
                for (int i = 0; i < result.Count(); i++)
                {
                    bool search = false;
                    for (int j = 0; j < list.Count(); j++)
                    {
                        int str = result.ToList()[i].RealEstate.ID;
                        if (str.ToString() == list[j])
                        {
                            search = true;
                        }
                    }
                    if (search == false)
                    {
                        rentList.Add(result.ToList()[i]);
                    }
                }
                return rentList;
            }
            return result.ToList();
        }

        public static List<T_Sale> getSale(SearchItem item)
        {
            RealEstateContext DB = new RealEstateContext();
            var result = DB.TSale.Where(a => a.Status == MyEnumType.SaleRealEstateStatus.active).OrderBy(a => a.SoldPrice).AsQueryable();
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
                if (item.feature != null)
                    foreach (int feature in item.feature)
                    {
                        result = result.Where(a => a.RealEstate.RealEstateFeature.Any(b => b.ID == feature)).AsQueryable();
                    }
            }
            T_Customer customer = getCurrentCustomer(DB);
            if(customer!=null)
            {
                List<T_Sale> saleList = new List<T_Sale>();
                List<string> list = hideEstateList(customer.ID);
                if (list == null)
                    return result.ToList();
                for (int i=0;i<result.Count();i++)
                {
                    bool search = false;
                    for (int j=0;j<list.Count();j++)
                    {
                        int str = result.ToList()[i].RealEstate.ID;
                        if (str.ToString() == list[j])
                        {
                            search = true;
                        }
                    }
                    if(search==false)
                    {
                        saleList.Add(result.ToList()[i]);
                    }
                }
                return saleList;
            }
            
            return result.ToList();
        }

        public static List<T_Sale> getSold(SearchItem item)
        {
            RealEstateContext DB = new RealEstateContext();
            var result = DB.TSale.Where(a => a.Status == MyEnumType.SaleRealEstateStatus.sold).OrderBy(a => a.SoldPrice).AsQueryable();
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
                    result = result.Where(a => a.RealEstate.Rekind.ID == item.RealEstateKind).AsQueryable();
                }
                if (item.RoomNo > 0)
                {
                    result = result.Where(a => a.RealEstate.RoomNo < item.RoomNo).AsQueryable();
                }
                if (item.Town > 0)
                {
                    result = result.Where(a => a.RealEstate.address.Town.ID < item.Town).AsQueryable();
                }
                if (item.feature != null)
                    foreach (int feature in item.feature)
                    {
                        result = result.Where(a => a.RealEstate.RealEstateFeature.Any(b => b.ID == feature)).AsQueryable();
                    }
            }
            T_Customer customer = getCurrentCustomer(DB);
            if (customer != null)
            {
                List<T_Sale> saleList = new List<T_Sale>();
                List<string> list = hideEstateList(customer.ID);
                if (list == null)
                    return result.ToList();
                for (int i = 0; i < result.Count(); i++)
                {
                    bool search = false;
                    for (int j = 0; j < list.Count(); j++)
                    {
                        int str = result.ToList()[i].RealEstate.ID;
                        if (str.ToString() == list[j])
                        {
                            search = true;
                        }
                    }
                    if (search == false)
                    {
                        saleList.Add(result.ToList()[i]);
                    }
                }
                return saleList;
            }
            return result.ToList();
        }
    }
}