using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace yahoo
{
	[Serializable]
	public class Country
	{
		[SerializeField] public string Code;
		[SerializeField] public string Name;

	}
	[Serializable]
	public class Result
	{
		[SerializeField] public string Name;
		[SerializeField] public string Uid;
		[SerializeField] public string Category;
		[SerializeField] public string Label;
		[SerializeField] public string Where;
		[SerializeField] public string Combined;
		[SerializeField] public double Score;

	}
	[Serializable]
	public class Area
	{
		[SerializeField] public string Id;
		[SerializeField] public string Name;
		[SerializeField] public double Score;
		[SerializeField] public int Type;

	}

	[Serializable]
	public class ResultSet
	{
		[SerializeField] public List<string> Address = new List<string>();
		[SerializeField] public string Govcode;
		[SerializeField] public Country Country;
		[SerializeField] public string Roadname;
		[SerializeField] public List<Result> Result = new List<Result>();
		[SerializeField] public List<Area> Area = new List<Area>();

	}

	[Serializable]
	public class Application
	{
		[SerializeField] public ResultSet ResultSet;

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