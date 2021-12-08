using System;
using System.Collections.Generic;
using System.Text;

namespace CPMA_Core_APP.Utils
{
    public static class SearchUtils
    {
        /// <summary>
        /// When searching for things in a string i.e. searching a list of tags.
        /// </summary>
        /// <param name="searchVal">The text being searched</param>
        /// <param name="searchCriteria">What we are looking for in it</param>
        /// <returns></returns>
        public static bool SearchBy(string searchVal, string searchCriteria)
        {
            return searchVal.ToUpper().Contains(searchCriteria.ToUpper());
        }
    }
}
