using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

namespace XamarinCustomBadge
{
	public class CustomBadge:UIView
	{
		public CustomBadge ()
		{
		
			Frame = new System.Drawing.RectangleF (0, 0, 25, 25);
			ContentScaleFactor = UIScreen.MainScreen.Scale;
			BackgroundColor = UIColor.Clear;
			BadgeTextColor = UIColor.White;
			BadgeFont = UIFont.BoldSystemFontOfSize (12);
			BadgeFrame = true;
			BadgeFrameColor = UIColor.White;
			BadgeInsetColor = UIColor.Red;
			BadgeCornerRadius = 0.4f;
			BadgeScaleFactor = 1.0f;
			BadgeShining = false;
	
		}

		public CustomBadge (string badgeText) : this ()
		{
			BadgeText = badgeText;
			AutoBadgeSizeWithString (BadgeText);
		}

		public string BadgeText {
			get;
			set;
		}

		public UIFont BadgeFont {
			get;
			set;
		}

		public UIColor BadgeTextColor {
			get;
			set;
		}

		public UIColor BadgeInsetColor {
			get;
			set;
		}

		public UIColor BadgeFrameColor {
			get;
			set;
		}

		public bool BadgeFrame {
			get;
			set;
		}

		public bool BadgeShining {
			get;
			set;
		}

		public float BadgeCornerRadius {
			get;
			set;
		}

		public float BadgeScaleFactor {
			get;
			set;
		}

		void AutoBadgeSizeWithString (string badgeString)
		{
			SizeF retValue;
			float rectWidth, rectHeight;
			SizeF stringSize = new NSString (badgeString).GetSizeUsingAttributes (new UIStringAttributes{ Font = BadgeFont });
			float flexSpace;
			if (badgeString.Length >= 2) {
				flexSpace = badgeString.Length;
				rectWidth = 25 + (stringSize.Width + flexSpace);
				rectHeight = 25;
				retValue = new SizeF (rectWidth * BadgeScaleFactor, rectHeight * BadgeScaleFactor);
			} else {
				retValue = new SizeF (25 * BadgeScaleFactor, 25 * BadgeScaleFactor);
			}
			Frame = new RectangleF (Frame.X, Frame.Y, retValue.Width, retValue.Height);
			BadgeText = badgeString;
			SetNeedsDisplay ();
		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);


			using (var context = UIGraphics.GetCurrentContext ()) {

				DrawRoundedRect (rect, context);
				if (BadgeShining) {
					DrawShine (rect, context);
				}
				if (BadgeFrame) {
					DrawFrame (rect, context);
				}
				if (!string.IsNullOrWhiteSpace (BadgeText)) {
					float sizeOfFont = 13.5f * BadgeScaleFactor;
					if (BadgeText.Length < 2) {
						sizeOfFont += sizeOfFont * 0.20f;
					}

					var nsText = new NSString (BadgeText);
					var textFont = BadgeFont.WithSize (sizeOfFont);

					var textSize = nsText.GetSizeUsingAttributes (new UIStringAttributes{ Font = textFont });
					nsText.DrawString (new PointF (rect.Width / 2 - textSize.Width / 2, 
						rect.Height / 2 - textSize.Height / 2),
						new UIStringAttributes{ Font = textFont, ForegroundColor = BadgeTextColor });
				}
			}
		}

		void DrawRoundedRect (RectangleF rect, CGContext context)
		{
			context.SaveState ();
			float radius = rect.GetMaxY () * BadgeCornerRadius;
			float puffer = rect.GetMaxY () * 0.10f;
			float maxX = rect.GetMaxX () - puffer;
			float maxY = rect.GetMaxY () - puffer;
			float minX = rect.GetMinX () + puffer;
			float minY = rect.GetMinY () + puffer;

			context.BeginPath ();
			context.SetFillColor (BadgeInsetColor.CGColor);
			context.AddArc (maxX - radius, minY + radius, radius, (float)Math.PI + ((float)Math.PI / 2f), 0f, false);
			context.AddArc (maxX - radius, maxY - radius, radius, 0, (float)Math.PI / 2f, false);
			context.AddArc (minX + radius, maxY - radius, radius, (float)Math.PI / 2f, (float)Math.PI, false);
			context.AddArc (minX + radius, minY + radius, radius, (float)Math.PI, (float)Math.PI + ((float)Math.PI / 2f), false);
			context.SetShadowWithColor (new SizeF (1, 1), 3, UIColor.Black.CGColor);
			context.FillPath ();
			context.RestoreState ();


		}

		void DrawShine (RectangleF rect, CGContext context)
		{
			//TODO
		}

		void DrawFrame (RectangleF rect, CGContext context)
		{
			//TODO
		}
	}
}

