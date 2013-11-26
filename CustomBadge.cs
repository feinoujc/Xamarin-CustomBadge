using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

namespace XamarinCustomBadge
{
	public class CustomBadge:UIView
	{
		const float M_PI = (float)Math.PI;

		public CustomBadge (string badgeText, 
		                    UIColor textColor, 
		                    UIFont  font,
		                    UIColor frameColor,
		                    UIColor insetColor,
		                    float scaleFactor,
		                    bool frame,
		                    bool shining)
		{
		
			Frame = new System.Drawing.RectangleF (0, 0, 25, 25);
			ContentScaleFactor = UIScreen.MainScreen.Scale;
			BackgroundColor = UIColor.Clear;
			BadgeCornerRoundness = 0.4f;

			this.BadgeTextColor = textColor;
			this.BadgeFont = font;
			this.BadgeFrame = frame;
			this.BadgeFrameColor = frameColor;
			this.BadgeInsetColor = insetColor;
			this.BadgeCornerRoundness = 0.4f;
			this.BadgeScaleFactor = scaleFactor;
			this.BadgeShining = shining;
			AutoBadgeSizeWithString (badgeText);
	
		}

		public CustomBadge (string badgeText) : this (badgeText, 
			UIColor.White, 
			UIFont.BoldSystemFontOfSize (12), 
			UIColor.White, 
			UIColor.Red, 
			1f, 
			true, 
			true)
		{
		}

	

		public string BadgeText {
			get;
			private set;
		}

		public UIFont BadgeFont {
			get;
			private set;
		}

		public UIColor BadgeTextColor {
			get;
			private set;
		}

		public UIColor BadgeInsetColor {
			get;
			private set;
		}

		public UIColor BadgeFrameColor {
			get;
			private set;
		}

		public bool BadgeFrame {
			get;
			private set;
		}

		public bool BadgeShining {
			get;
			private set;
		}

		public float BadgeCornerRoundness {
			get;
			private set;
		}

		public float BadgeScaleFactor {
			get;
			private set;
		}

		public void AutoBadgeSizeWithString (string badgeString)
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
			float radius = rect.GetMaxY () * BadgeCornerRoundness;
			float puffer = rect.GetMaxY () * 0.10f;
			float maxX = rect.GetMaxX () - puffer;
			float maxY = rect.GetMaxY () - puffer;
			float minX = rect.GetMinX () + puffer;
			float minY = rect.GetMinY () + puffer;

			context.BeginPath ();
			context.SetFillColor (BadgeInsetColor.CGColor);
			context.AddArc (maxX - radius, minY + radius, radius, M_PI + (M_PI / 2f), 0f, false);
			context.AddArc (maxX - radius, maxY - radius, radius, 0, M_PI / 2f, false);
			context.AddArc (minX + radius, maxY - radius, radius, M_PI / 2f, M_PI, false);
			context.AddArc (minX + radius, minY + radius, radius, M_PI, M_PI + (M_PI / 2f), false);
			context.SetShadowWithColor (new SizeF (1, 1), 3, UIColor.Black.CGColor);
			context.FillPath ();
			context.RestoreState ();


		}

		void DrawShine (RectangleF rect, CGContext context)
		{
			context.SaveState ();
			float radius = rect.GetMaxY () * BadgeCornerRoundness;
			float puffer = rect.GetMaxY () * 0.10f;
			float maxX = rect.GetMaxX () - puffer;
			float maxY = rect.GetMaxY () - puffer;
			float minX = rect.GetMinX () + puffer;
			float minY = rect.GetMinY () + puffer;

			context.BeginPath ();
			context.AddArc (maxX - radius, minY + radius, radius, M_PI + (M_PI / 2f), 0f, false);
			context.AddArc (maxX - radius, maxY - radius, radius, 0, M_PI / 2f, false);
			context.AddArc (minX + radius, maxY - radius, radius, M_PI / 2f, M_PI, false);
			context.AddArc (minX + radius, minY + radius, radius, M_PI, M_PI + (M_PI / 2f), false);
			context.Clip ();

			var locations = new []{ 0f, 0.4f };
			var components = new []{ 0.92f, 0.92f, 0.92f, 1.0f, 0.82f, 0.82f, 0.82f, 0.4f };
			using (var cspace = CGColorSpace.CreateDeviceRGB ())
			using (var gradient = new CGGradient (cspace, components, locations)) {

				var sPoint = new PointF (0, 0);
				var ePoint = new PointF (0, maxY);
				context.DrawLinearGradient (gradient, sPoint, ePoint, (CGGradientDrawingOptions)0);
			}
			context.RestoreState ();

		}

		void DrawFrame (RectangleF rect, CGContext context)
		{
			float radius = rect.GetMaxY () * BadgeCornerRoundness;
			float puffer = rect.GetMaxY () * 0.10f;
			float maxX = rect.GetMaxX () - puffer;
			float maxY = rect.GetMaxY () - puffer;
			float minX = rect.GetMinX () + puffer;
			float minY = rect.GetMinY () + puffer;
			context.BeginPath ();
			float lineSize = 2;
			if (BadgeScaleFactor > 1) {
				lineSize += BadgeScaleFactor * 0.25f;
			}
			context.SetLineWidth (lineSize);
			context.SetStrokeColor (BadgeFrameColor.CGColor);

			context.AddArc (maxX - radius, minY + radius, radius, M_PI + (M_PI / 2f), 0f, false);
			context.AddArc (maxX - radius, maxY - radius, radius, 0, M_PI / 2f, false);
			context.AddArc (minX + radius, maxY - radius, radius, M_PI / 2f, M_PI, false);
			context.AddArc (minX + radius, minY + radius, radius, M_PI, M_PI + (M_PI / 2f), false);
			context.ClosePath ();
			context.StrokePath ();
		}
	}
}

