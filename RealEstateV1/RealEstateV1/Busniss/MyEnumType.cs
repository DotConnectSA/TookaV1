﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateV1.Busniss
{
    public class MyEnumType
    {
        public enum RealEstateStatus
        {
            active=1
        }
        public enum RentRealEstateStatus
        {
            active = 1,Rent=2
        }
        public enum SaleRealEstateStatus
        {
            active = 1, sold = 2
        }
        public enum RealEstateSituation
        {
            good = 1, used = 2, underIns = 3
        }
        public enum ownerKind
        {
            own = 1,  proker= 2, office = 3
        }
        public enum FavoritStatus
        {
            active = 1
        }
        public enum imageKind
        {
            Big = 1, pic = 2, Panorama = 3, blueprint=4
        }
        public enum VideoKind
        {
            Big = 1
        }
        public enum SearchKind
        {
            Sale=0,Rent=1,Sold=2
        }

    }
}