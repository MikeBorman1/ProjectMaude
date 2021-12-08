using System;
using System.Collections.Generic;
using System.Text;

namespace CPMA_Core_APP.Common
{
    public static partial class StaticVariables
    {
        /// <summary>
        /// Keys for identifying variables cached in the secure store when the app is closed. such as the users session and the theme they are using
        /// </summary>
        public static class PersistedItemKeys
        {
            public const string Session = "SessionString";
            public const string Theme = "ThemeString";
            public const string Postcode = "Postcode";
            

            
        }
    }
}
