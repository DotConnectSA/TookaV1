using System.Collections.Generic;
using System.Data.Entity;

namespace RealEstateV1.Models
{
   
    public class RealEstateContext : DbContext 
    {
       
        static RealEstateContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RealEstateContext>());
           
        }
      
        public DbSet<T_Address> TAddress { get; set; }
        public DbSet<T_City> TCity { get; set; }
        public DbSet<T_Customer> TCustomer { get; set; }
        public DbSet<T_Discuss> TDiscuss { get; set; }
        public DbSet<T_Favorit> TFavorit { get; set; }
        public DbSet<T_Owner> TOwner { get; set; }
        public DbSet<T_OwnerLike> TOwnerLike { get; set; }
        public DbSet<T_position> TPosition { get; set; }
        public DbSet<T_RealEstate> TRealEstate { get; set; }
        public DbSet<T_REFeature> TREFeature { get; set; }
        public DbSet<T_REImage> TREImage { get; set; }
        public DbSet<T_REKind> TREKind { get; set; }
        public DbSet<T_Rent> TRent { get; set; }
        public DbSet<T_Replay> TRepaly { get; set; }
        public DbSet<T_REVideo> TRVideo { get; set; }
        public DbSet<T_Sale> TSale { get; set; }
        public DbSet<T_SearchHistory> TSearchHistory { get; set; }
        public DbSet<T_SMS_Email> TSMSEmail { get; set; }
        public DbSet<T_TownFeature> TTFeature { get; set; }
        public DbSet<T_Town> TTown { get; set; }
        public DbSet<T_TownComment> TTownComment { get; set; }
        public DbSet<T_TownLinkFeature> TTownFeature { get; set; }
        public DbSet<T_TownRate> TTownRate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_RealEstate>().
              HasMany(c => c.RealEstateFeature).
              WithMany(p => p.RrealEstate).
              Map(
               m =>
               {
                   m.MapLeftKey("RealEstateId");
                   m.MapRightKey("FeatureId");
                   m.ToTable("RE_Feature_relation");
               });
        }
        
    }
    public class RealEstateInitializer : DropCreateDatabaseIfModelChanges<RealEstateContext>
    {
       
        protected override void Seed(RealEstateContext context)
        {
            var cites = new List<T_City>
            {
                new T_City{City="الرياض"},
                new T_City{City="الخبر"},
                new T_City{City="الدمام"},
                new T_City{City="جدة"},
                new T_City{City="الطائف"},
                new T_City{City="الهفوف"},
                new T_City{City="مكه"},
                new T_City{City="المدينه المنورة"},
                new T_City{City="حائل"}
                
            };

           cites.ForEach(s=>context.TCity.Add(s));

             var kinds = new List<T_REKind>
            {
                new T_REKind{Kind="قصر"},
                new T_REKind{Kind="فيلا"},
                new T_REKind{Kind=" فيلا دوبلكس"},
                new T_REKind{Kind=" فيلا روف"},
                new T_REKind{Kind="دور"},
                new T_REKind{Kind="شقة"},
                new T_REKind{Kind="أرض"},
                new T_REKind{Kind="مزرعة"},
                new T_REKind{Kind="عمارة"},
                new T_REKind{Kind="برج"},
                new T_REKind{Kind="شالية"}
                
            };

           kinds.ForEach(s=>context.TREKind.Add(s));

           context.SaveChanges();
        }
    }
      
}