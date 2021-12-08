﻿using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using Android.Graphics;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Views;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.Droid.Renderers;
using Android.Widget;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CPMA_Core_APP.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        Xamarin.Forms.TabbedPage tabbedPage;
        BottomNavigationView bottomNavigationView;
        Android.Views.IMenuItem lastItemSelected;
        private bool firstTime = true;
        int lastItemId = -1;
        public CustomTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                tabbedPage = e.NewElement as CustomTabbedPage;
                bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;
                bottomNavigationView.NavigationItemSelected += BottomNavigationView_NavigationItemSelected;

                //Call to remove animation
                SetShiftMode(bottomNavigationView, false, false);

                //Call to change the font
                //ChangeFont();
            }

            if (e.OldElement != null)
            {
                bottomNavigationView.NavigationItemSelected -= BottomNavigationView_NavigationItemSelected;
            }


        }

        //Change Tab font
        //void ChangeFont()
        //{
        //    var fontFace = Typeface.CreateFromAsset(Context.Assets, "Quicksand-Bold.ttf");
        //    var bottomNavMenuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;

        //    for (int i = 0; i < bottomNavMenuView.ChildCount; i++)
        //    {
        //        var item = bottomNavMenuView.GetChildAt(i) as BottomNavigationItemView;
        //        var itemTitle = item.GetChildAt(1);

        //        var smallTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(0));
        //        var largeTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(1));

        //        lastItemId = bottomNavMenuView.SelectedItemId;

        //        smallTextView.SetTypeface(fontFace, TypefaceStyle.Bold);
        //        largeTextView.SetTypeface(fontFace, TypefaceStyle.Bold);

        //        //Set text color
        //        var textColor = (item.Id == bottomNavMenuView.SelectedItemId) ? tabbedPage.On<Xamarin.Forms.PlatformConfiguration.Android>().GetBarSelectedItemColor().ToAndroid() : tabbedPage.On<Xamarin.Forms.PlatformConfiguration.Android>().GetBarItemColor().ToAndroid();
        //        smallTextView.SetTextColor(textColor);
        //        largeTextView.SetTextColor(textColor);
        //    }
        //}

        //Remove tint color
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (bottomNavigationView != null)
            {
                bottomNavigationView.ItemIconTintList = null;
            }

            if (firstTime && bottomNavigationView != null)
            {
                for (int i = 0; i < Element.Children.Count; i++)
                {
                    var item = bottomNavigationView.Menu.GetItem(i);
                    if (bottomNavigationView.SelectedItemId == item.ItemId)
                    {
                        SetupBottomNavigationView(item);
                        break;
                    }
                }
                firstTime = false;
            }

        }

        void BottomNavigationView_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            var bottomNavMenuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;
            var normalColor = tabbedPage.UnselectedTabColor.ToAndroid();
            var selectedColor = tabbedPage.SelectedTabColor.ToAndroid();

            if (lastItemSelected != null)
            {
                if(lastItemSelected.Icon != null)
                {
                    lastItemSelected.Icon.SetColorFilter(normalColor, PorterDuff.Mode.SrcIn);
                }
            }

            if ($"{e.Item}" != "App")
            {
                if (e.Item.Icon != null)
                {
                    e.Item.Icon.SetColorFilter(selectedColor, PorterDuff.Mode.SrcIn);
                }
                lastItemSelected = e.Item;
            }

            if (lastItemId != -1)
            {
                SetTabItemTextColor(bottomNavMenuView.GetChildAt(lastItemId) as BottomNavigationItemView, normalColor);
            }

            SetTabItemTextColor(bottomNavMenuView.GetChildAt(e.Item.ItemId) as BottomNavigationItemView, selectedColor);


            SetupBottomNavigationView(e.Item);
            this.OnNavigationItemSelected(e.Item);

            lastItemId = e.Item.ItemId;

        }


        void SetTabItemTextColor(BottomNavigationItemView bottomNavigationItemView, Android.Graphics.Color textColor)
        {
            var itemTitle = bottomNavigationItemView.GetChildAt(1);
            var smallTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(0));
            var largeTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(1));

            smallTextView.SetTextColor(textColor);
            largeTextView.SetTextColor(textColor);
        }


        //Adding line view
        void SetupBottomNavigationView(IMenuItem item)
        {
            int lineBottomOffset = 8;
            int lineWidth = 4;
            int itemHeight = bottomNavigationView.Height - lineBottomOffset;
            int itemWidth = (bottomNavigationView.Width / Element.Children.Count);
            int leftOffset = item.ItemId * itemWidth;
            int rightOffset = itemWidth * (Element.Children.Count - (item.ItemId + 1));
            GradientDrawable bottomLine = new GradientDrawable();
            bottomLine.SetShape(ShapeType.Line);
            bottomLine.SetStroke(lineWidth, Xamarin.Forms.Color.DarkGray.ToAndroid());

            var layerDrawable = new LayerDrawable(new Drawable[] { bottomLine });
            layerDrawable.SetLayerInset(0, leftOffset, itemHeight, rightOffset, 0);

            bottomNavigationView.SetBackground(layerDrawable);
        }


        //Remove animation
        public void SetShiftMode(BottomNavigationView bottomNavigationView, bool enableShiftMode, bool enableItemShiftMode)
        {
            try
            {
                if (!(bottomNavigationView.GetChildAt(0) is BottomNavigationMenuView menuView))
                {
                    System.Diagnostics.Debug.WriteLine("Unable to find BottomNavigationMenuView");
                    return;
                }
                var shiftMode = menuView.Class.GetDeclaredField("mShiftingMode");
                shiftMode.Accessible = true;
                shiftMode.SetBoolean(menuView, enableShiftMode);
                shiftMode.Accessible = false;
                shiftMode.Dispose();
                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    if (!(menuView.GetChildAt(i) is BottomNavigationItemView item))
                        continue;
                    item.SetShiftingMode(enableItemShiftMode);
                    item.SetChecked(item.ItemData.IsChecked);
                }
                menuView.UpdateMenuView();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unable to set shift mode: {ex}");
            }
        }
    }
}