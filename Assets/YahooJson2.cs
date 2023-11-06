using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace yahoo2
{

	[Serializable]
	public class ResultInfo
	{
		public int Count;
		public int Total;
		public int Start;
		public int Status;
		public string Description;
		public string Copyright;
		public double Latency;

	}
	[Serializable]
	public class Geometry
	{
		public string Type;
		public string Coordinates;

	}
	[Serializable]
	public class Country
	{
		public string Code;
		public string Name;

	}
	[Serializable]
	public class AddressElement
	{
		public string Name;
		public string Kana;
		public string Level;

	}
	[Serializable]
	public class Genre
	{
		public string Code;
		public string Name;

	}
	[Serializable]
	public class Building
	{
		public string Id;
		public string Name;
		public string Floor;
		public string Area;

	}

	[Serializable]
	public class Station
	{
		public string Id;
		public string SubId;
		public string Name;
		public string Railway;
		public string Exit;
		public string ExitId;
		public string Distance;
		public string Time;
		public Geometry Geometry;

	}
	[Serializable]
	public class Detail
	{
		public string Image1;
		public string Image2;
		public string Image3;
		public string Image4;
		public string Image5;
		public string Image6;
		public string Image7;
		public string PcUrl1;
		public string PetFlag;
		public string Smoking;
		public string ZipCode;
		public string Capacity;
		public string Copyright;
		public string LunchFlag;
		public string DatingFlag;
		public string FamilyFlag;
		public string InfoLatLon;
		public string LunchPrice;
		public string ParlorFlag;
		public string DinnerPrice;
		public string WhatsNewUrl;
		public string BirthdayFlag;
		public string YourselfFlag;
		public string TableSeatFlag;
		public string AfterPartyFlag;
		public string BrunchMenuFlag;
		public string LadysPartyFlag;
		public string LargeGroupFlag;
		public string PersistencyUrl;
		public string AnniversaryFlag;
		public string BarrierFreeFlag;
		public string CalorieDataFlag;
		public string HorigotatsuFlag;
		public string LunchBuffetFlag;
		public string AllYouCanEatFlag;
		public string BrownBaggingFlag;
		public string BreakfastMenuFlag;
		public string ChildFriendlyFlag;
		public string HealthierMenuFlag;
		public string LunchSaladBarFlag;
		public string OnlineReserveFlag;
		public string PersistencyImage1;
		public string PersistencyImage2;
		public string PersistencyImage3;
		public string PersistencyTitle1;
		public string PersistencyTitle2;
		public string PersistencyTitle3;
		public string ReligiousMenuFlag;
		public string ReservationPcUrl1;
		public string AllYouCanDrinkFlag;
		public string VegetarianMenuFlag;
		public string LunchPlentyMenuFlag;
		public string LunchAfter14MenuFlag;
		public string LunchLimitedMenuFlag;
		public string LunchServiceTimeFlag;
		public string MatchmakingPartyFlag;
		public string ReservationLabelCode;
		public string HypoallergenicMenuFlag;
		public string LunchIncludedDrinkFlag;
		public string AllYouCanEatDessertFlag;
		public string LunchIncludedDessertFlag;
		public string OnlineReserveRequestFlag;
		public string LunchRefillBreadAndRiceFlag;
		public string LunchWeekendSpetialMenuFlag;
		public string CassetteOwner;
		public string CassetteHeader;
		public string CassetteFooter;
		public string CassetteOwnerUrl;
		public string CassetteOwnerMobileUrl;
		public string CassetteOwnerLogoImage;
		public string YUrl;

	}
	[Serializable]

	public class Property
	{
		public string Uid;
		public string CassetteId;
		public string Yomi;
		public Country Country;
		public string Address;
		public List<AddressElement> AddressElement;
		public string GovernmentCode;
		public string AddressMatchingLevel;
		public string Tel1;
		public List<Genre> Genre;
		public List<Building> Building;
		public List<Station> Station;
		public DateTime CreateDate;
		public string LeadImage;
		public string ParkingFlag;
		public string SmartPhoneCouponFlag;
		//public IList<undefined> Coupon;
		public string KeepCount;
		public string OpenForBusiness;
		public Detail Detail;

	}
	[Serializable]
	public class Feature
	{
		public string Id;
		public string Gid;
		public string Name;
		public Geometry Geometry;
		public List<string> Category;
		public string Description;
		//public IList<undefined> Style;
		public Property Property;

	}
	[Serializable]
	public class Application
	{
		public ResultInfo ResultInfo;
		public List<Feature> Feature;

	}

	[Serializable]
	public class parameters
	{

		public static Application Deserialize(string json)
		{
			Application user = JsonUtility.FromJson<Application>(json);

			return user;
		}

		public static string Serialize(Application model)
		{
			string json = JsonUtility.ToJson(model);
			return json;
		}
	}
}