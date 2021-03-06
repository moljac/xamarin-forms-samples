﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using EmployeeDirectory;

namespace EmployeeDirectory.Android
{
	[Activity (Label = "EmployeeDirectory", MainLauncher = true)]
	public class Activity1 : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Copy static data into working folder
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder

			var path = Path.Combine(documentsPath, "XamarinDirectory.csv");
			//Console.WriteLine (path);
			if (!File.Exists (path)) {
				var s = Resources.OpenRawResource(EmployeeDirectory.Android.Resource.Raw.XamarinDirectory);  // RESOURCE NAME ###
				// create a write stream
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				// write to the stream
				ReadWriteStream(s, writeStream);
			}
			path = Path.Combine(documentsPath, "XamarinFavorites.xml");
			//Console.WriteLine (path);
			if (!File.Exists (path)) {
				var s = Resources.OpenRawResource(EmployeeDirectory.Android.Resource.Raw.XamarinFavorites);  // RESOURCE NAME ###
				// create a write stream
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				// write to the stream
				ReadWriteStream(s, writeStream);
			}
			#endregion

			App.PhoneFeatureService = new AndroidPhoneFeatureService ();

			SetPage (App.GetMainPage ());
		}

		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}


