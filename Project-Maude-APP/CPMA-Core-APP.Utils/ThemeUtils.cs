using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.Utils
{
    public static class ThemeUtils
    {

        public static async Task SetThemeInt(int themeInt = 1)
        {

            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                await SecureStorage.SetAsync(PersistedItemKeys.Theme, themeInt.ToString());
            }
        }


        /// <summary>
        /// Method for getting the theme
        /// </summary>
        /// <returns>Returns Theme Enum value as int</returns>
        public static async Task<int> GetThemeInt()
        {
            string setting = "0";
            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                setting = await SecureStorage.GetAsync(PersistedItemKeys.Theme);
            }

            if (string.IsNullOrEmpty(setting) || setting == "True")
            {
                await SetThemeInt();
            }
            int ret = string.IsNullOrEmpty(setting) ? 0 : int.Parse(setting);
            return ret;
        }
    }
}
