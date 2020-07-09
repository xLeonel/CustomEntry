using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CustomRender;
using CustomRender.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRender))]
namespace CustomRender.Droid
{
    public class CustomEntryRender : EntryRenderer
    {

        private Android.Graphics.Color BorderColor = Android.Graphics.Color.Black;

        private int BorderWidth = 1;

        private Thickness TextPadding = new Thickness(0);

        private CustomEntry CustomEntry;

        public CustomEntryRender(Context context) : base(context) 
        { 
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            BorderColor = CustomEntry.BorderColor.ToAndroid();

            if (CustomEntry.BorderWidth != 0)
            {
                BorderWidth = CustomEntry.BorderWidth;
            }

            TextPadding = CustomEntry.TextPadding;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            CustomEntry = e.NewElement as CustomEntry;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (CustomEntry != null)
            {
                ChangeCustomEntryProperties(CustomEntry);

                Control.Gravity = GravityFlags.CenterVertical;

                int left = Convert.ToInt32(TextPadding.Left);
                int right = Convert.ToInt32(TextPadding.Right);
                int top = Convert.ToInt32(TextPadding.Top);
                int bottom = Convert.ToInt32(TextPadding.Bottom);

                Control.SetPadding(left, top, right, bottom);

                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");

                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor);
            }
        }

        private void ChangeCustomEntryProperties(CustomEntry entry)
        {
            //var customEntry = entry;

            var nativeEditText = (EditText)Control;
            var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            shape.Paint.Color = BorderColor;
            shape.Paint.SetStyle(Paint.Style.Stroke);
            nativeEditText.Background = shape;
            GradientDrawable gd = new GradientDrawable();
            //gd.SetColor(Android.Graphics.Color.White);
            gd.SetStroke(BorderWidth, BorderColor);
            nativeEditText.SetBackground(gd);
        }

    }
}
