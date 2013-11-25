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

			customBadge1.Frame = new RectangleF (View.Frame.Width / 2 - customBadge1.Frame.Size.Width / 2, 110, 25, 25);

			this.Add (customBadge1);              

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
	}
}

