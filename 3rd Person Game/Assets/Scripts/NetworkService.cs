﻿using UnityEngine;
using System.Collections;
using System;

public class NetworkService
{
	private const string xmlApi = "http://api.openweathermap.org/data/2.5/forecast/weather?q=sydney,au&APPID=9e4dc2b37bd3e1c0baf4242a9a66b1fd&mode=xml";
	private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=sydney,au&APPID=9e4dc2b37bd3e1c0baf4242a9a66b1fd";
	private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
	
	public bool IsResponseValid(WWW www)
	{
		if (www.error != null) 
		{
			Debug.Log ("Bad Connection");
			return false;
		} 
		else if (string.IsNullOrEmpty (www.text)) 
		{
			Debug.Log ("Bad Data");
			return false;
		} else 
		{
			return true;
		}
	}

	private IEnumerator CallApi(string url, Action<string> callback)
	{
		WWW www = new WWW (url);
		yield return www;

		if (!IsResponseValid (www)) 
		{
			yield break;
		}

		callback (www.text);
	}

	public IEnumerator GetWeatherXML(Action<string> callback)
	{
		return CallApi (xmlApi, callback);
	}

	public IEnumerator GetWeatherJSON(Action<string> callback)
	{
		return CallApi (jsonApi, callback);
	}

	public IEnumerator DownloadImage(Action<Texture2D> callback)
	{
		WWW www = new WWW (webImage);
		yield return www;

		callback (www.texture);
	}
}
