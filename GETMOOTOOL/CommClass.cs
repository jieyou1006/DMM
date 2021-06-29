using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GETMOOTOOL
{
    class CommClass
    {
        public string CheckUrl(string strUrl)
        {
            if (strUrl.Length > 10)
            {
                if (strUrl.Substring(0, 5) != "https")
                {
                    strUrl = "https:" + strUrl;
                }
            }
            return strUrl;
        }
    }
}
