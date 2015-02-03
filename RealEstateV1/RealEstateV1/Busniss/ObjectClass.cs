using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateV1.Busniss
{
    public class SearchItem
    {
        [Display(Name = "المدينة")]
        public int City { get; set; }
        [Display(Name = "عدد الغرف")]
        public int RoomNo { get; set; }
        [Display(Name = "عدد الحمامات")]
        public int Bathno { get; set; }
        [Display(Name = "المساحة")]
        public float Area { get; set; }
        [Display(Name = "الحي")]
        public int Town { get; set; }
        [Display(Name = "السعر الاعلى")]
        public int UperPrice { get; set; }
        [Display(Name = "السعر الادنى")]
        public int LowerPrice { get; set; }
        [Display(Name = "نوع العقار")]
        public int RealEstateKind { get; set; }
        [Display(Name = "يوم الاضافة")]
        public int date { get; set; }
        public MyEnumType.SearchKind SearchKind { get; set; }
        public List<int> feature { get; set; }

    }
   
    public class DropDown
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class RealEstateFull
    {
        public RealEstateV1.Models.T_RealEstate realEstate { get; set; }
        public RealEstateV1.Models.T_Rent rent { get; set; }
        public RealEstateV1.Models.T_Sale sale { get; set; }
        public List<FeatureRate> featureRate { get; set; }
    }

    public class FeatureRate
    {
        public string feature { get; set; }
        public int rate { get; set; }
        public int count { get; set; }
    }

    public class addRateInfo
    {
        [Display(Name = "تقييم العام")]
        public int townRate { get; set; }
        public List<int> featureRate { get; set; }
        public List<RealEstateV1.Models.T_TownFeature> allFeature { get; set; }
        [Display(Name = "عنوان")]
        public string title { get; set; }
        [Display(Name = "تعليقك")]
        public string comment { get; set; }

        public addRateInfo()
        {
            featureRate = new List<int>();
            allFeature = BusnissLayer.GetAllFeature();
            for (int i = 0; i < allFeature.Count(); i++)
            {
                featureRate.Add(0);
            }
        }

    }
}