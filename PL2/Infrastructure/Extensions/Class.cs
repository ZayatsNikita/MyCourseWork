using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Infrastructure.Extensions
{
        public static class HtmlHelperExtensions
        {
            public static IEnumerable<SelectListItem> GetEnumSelectListWithDefaultValue<TEnum>(this IHtmlHelper htmlHelper, TEnum defaultValue)
                where TEnum : struct
            {
                var selectList = htmlHelper.GetEnumSelectList<TEnum>().ToList();
                selectList.Single(x => x.Value == $"{(int)(object)defaultValue}").Selected = true;
                return selectList;
            }
        }
}
