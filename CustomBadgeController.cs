using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XamarinCustomBadge
{
	public partial class CustomBadgeController : UIViewController
	{
		public CustomBadgeController () : base ("CustomBadgeController", null)
		{

			var customBadge1 = new CustomBadge ("2");

	
			// Create advanced Badge

			CustomBadge customBadge2 = new CustomBadge ("CustomBadge", UIColor.Black, UIFont.FromName (UIFont.FamilyNames [4], 12), 
				UIColor.Green, UIColor.Yellow, 1.5f, true, true);


			CustomBadge customBadge3 = new CustomBadge ("Now Retina Ready!", UIColor.White, UIFont.BoldSystemFontOfSize(12), 
				UIColor.Black, UIColor.Blue, 2.0f, true, true);

			CustomBadge customBadge4 = new CustomBadge ("...and scalable", UIColor.White, UIFont.BoldSystemFontOfSize(12), 
				UIColor.Black, UIColor.Purple, 2.0f, true, true);

			CustomBadge customBadge5 = new CustomBadge ("...with shining", UIColor.Black, UIFont.BoldSystemFontOfSize(12), 
				UIColor.Black, UIColor.Orange, 1.0f, true, true);

			CustomBadge customBadge6 = new CustomBadge ("...and without shining", UIColor.White, UIFont.BoldSystemFontOfSize(12), 
				UIColor.Black, UIColor.Brown, 1.0f, true, false);

			CustomBadge customBadge7 = new CustomBadge("Open & Free for Xamarin",UIColor.White, UIFont.BoldSystemFontOfSize(12), 
				UIColor.Yellow, UIColor.Black, 1.25f, true, false);

			customBadge1.Frame =  new RectangleF (View.Frame.Width / 2 - customBadge1.Frame.Size.Width / 2 + customBadge2.Frame.Width / 2, 110, customBadge1.Frame.Width, customBadge1.Frame.Height);
		
			AddCentered (customBadge2, 110);
			Add (customBadge1);
			AddCentered (customBadge3, 150);
			AddCentered (customBadge4, 200);
			AddCentered (customBadge5, 250);
			AddCentered (customBadge6, 290);
			AddCentered (customBadge7, 320);
			            

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void AddCentered (CustomBadge badge, int y)
		{
			badge.Frame = new RectangleF (View.Frame.Width / 2 - badge.Frame.Size.Width / 2, y, badge.Frame.Width, badge.Frame.Height);

			Add(badge);
		}
	}
}

