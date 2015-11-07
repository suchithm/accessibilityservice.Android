using System;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Views.Accessibility;
using Android.AccessibilityServices;
using Android;

namespace UssdcodeRead
{
	[Activity ( Label = "UssdcodeRead" , MainLauncher = true , Icon = "@drawable/icon" )]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate ( Bundle bundle )
		{
			base.OnCreate ( bundle );

			SetContentView ( Resource.Layout.Main );

			var intentToAccessibility = new Intent ( this , typeof ( UssdCodeFetch ) );
			StartService ( intentToAccessibility );

			Button button = FindViewById<Button> ( Resource.Id.myButton );
			
			button.Click += delegate
			{
				button.Text = string.Format ( "{0} Accessibility service implementation" , count++ );
			};
		}
	}
	[Service(Label = "myApp", Permission = Manifest.Permission.BindAccessibilityService)]
	[IntentFilter(new []{ "android.accessibilityservice.AccessibilityService" })]
	[MetaData("android.accessibilityservice.AccessibilityService", Resource = "@xml/accessibility_service_config")]
	public class UssdCodeFetch : AccessibilityService
	{ 
		public override void OnAccessibilityEvent (AccessibilityEvent e)
		{ 
			try
			{
				
//				if ( e.Text!=null && e.EventType==EventTypes.WindowStateChanged  )
//				{ 
//					if(e.PackageName.Equals("com.android.phone"))
//					{
					Console.WriteLine( "event type : "+e.EventType); 
					Console.WriteLine( "content decription : "+e.ContentDescription);
					Console.WriteLine( "package name : "+e.PackageName);
					Console.WriteLine( "source : "+e.Source);
					Console.WriteLine( "window id : "+e.WindowId);
					Console.WriteLine( "event time : "+e.EventTime);

					var strBuilderTxt = new StringBuilder (); 
					foreach ( var txt in e.Text )
					{
						strBuilderTxt.Append ( txt );
					} 

					Console.WriteLine ( "actual text : " + strBuilderTxt );
//					}
//				}
			}
			catch ( Exception e2 )
			{
				Console.WriteLine ( e2.Message );
			}
		} 
		public override void OnInterrupt ()
		{
			throw new NotImplementedException ();
		}
		protected override void OnServiceConnected ()
		{
			var accessibilityServiceInfo = new AccessibilityServiceInfo ();
			accessibilityServiceInfo.Flags = AccessibilityServiceFlags.Default;//enum 1 
			accessibilityServiceInfo.EventTypes =EventTypes.AllMask;
			accessibilityServiceInfo.FeedbackType = Android.AccessibilityServices.FeedbackFlags.Generic; 
			SetServiceInfo(accessibilityServiceInfo);
		}
	}



}


