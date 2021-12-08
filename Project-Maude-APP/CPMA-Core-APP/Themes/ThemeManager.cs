using CPMA_Core_APP.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CPMA_Core_APP.Themes
{
    public class ThemeManager
    {
        /// <summary>
        /// Defines the supported themes for the sample app
        /// </summary>
        public enum Themes
        {
            Light = 0,
            Dark = 1
        }

        /// <summary>
        /// Changes the theme of the app.
        /// Add additional switch cases for more themes you add to the app.
        /// This also updates the local key storage value for the selected theme.
        /// </summary>
        /// <param name="theme"></param>
        public static async void ChangeThemeAsync(Themes theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                //Update local key value with the new theme you select.
                await ThemeUtils.SetThemeInt((int)theme);

                switch (theme)
                {
                    case Themes.Light:
                        {
                            mergedDictionaries.Add(new LightTheme());
                            break;
                        }
                    case Themes.Dark:
                        {
                            mergedDictionaries.Add(new DarkTheme());
                            break;
                        }
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }

        /// <summary>
        /// Reads current theme id from the local storage and loads it.
        /// </summary>
        public async static void LoadTheme()
        {
            Themes currentTheme = (Themes)await ThemeUtils.GetThemeInt();
            ChangeThemeAsync(currentTheme);
        }

        
    }
}