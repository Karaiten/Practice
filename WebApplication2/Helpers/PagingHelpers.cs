using SoftwareStore.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace SoftwareStore.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
    PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            int current = pageInfo.PageNumber;
            int total = pageInfo.TotalPages;

            void AppendLink(int page, string text = null, bool isActive = false)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(page));
                tag.SetInnerText(text ?? page.ToString());

                tag.AddCssClass("btn");

                if (isActive)
                {
                    tag.AddCssClass("btn-primary");
                    tag.AddCssClass("selected");
                }
                else
                {
                    tag.AddCssClass("btn-default");
                }

                result.Append(tag.ToString());
            }

            // Попередня
            if (current > 1)
                AppendLink(current - 1, "«");

            // Стиснене відображення сторінок
            int range = 2;
            int start = Math.Max(1, current - range);
            int end = Math.Min(total, current + range);

            if (start > 1)
            {
                AppendLink(1);
                if (start > 2) result.Append("<span class='dots'>...</span>");
            }

            for (int i = start; i <= end; i++)
            {
                AppendLink(i, null, i == current);
            }

            if (end < total)
            {
                if (end < total - 1) result.Append("<span class='dots'>...</span>");
                AppendLink(total);
            }

            // Наступна
            if (current < total)
                AppendLink(current + 1, "»");

            return MvcHtmlString.Create(result.ToString());
        }

    }
}
