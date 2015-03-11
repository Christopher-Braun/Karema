using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mvc4WebRole.CustomHelpers
{
    public static class CustomHtmlHelper
    {
        /// <summary>
        /// Für das Ajax + Redirect Problem
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="viewContext"></param>
        /// <returns></returns>
        public static IHtmlString GetPageUrl(this HtmlHelper htmlHelper, ViewContext viewContext)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append("data-url='");
            urlBuilder.Append(viewContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.UriEscaped));
            urlBuilder.Append("'");
            return htmlHelper.Raw(urlBuilder.ToString());
        }
    }
}