using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PL.Models;
using PL.Infrastructure.Sorting;

namespace PL.Infrastructure.Helpers
{
    //public class BuilderStandartSortTagHelper : TagHelper
    //{
    //    public BuildStandartSortState Property { get; set; }//Текущиее св-во
    //    public BuildStandartSortState Current { get; set; }
    //    public string Action { get; set; }  // действие контроллера, на которое создается ссылка
    //    public bool Up { get; set; }
    //    private IUrlHelperFactory urlHelperFactory;
    //    public BuilderStandartSortTagHelper(IUrlHelperFactory helperFactory)
    //    {
    //        urlHelperFactory = helperFactory;
    //    }
    //    [ViewContext]
    //    [HtmlAttributeNotBound]
    //    public ViewContext ViewContext { get; set; }
    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
    //        output.TagName = "a";
    //        string url = urlHelper.Action(Action, new { sortState = Property });
    //        output.Attributes.SetAttribute("href", url);
    //        if(Current == Property)
    //        {
    //            TagBuilder tag = new TagBuilder("i");
    //            tag.AddCssClass("glyphicon");
    //            if (Up == true)
    //                tag.AddCssClass("glyphicon-chevron-up");
    //            else
    //                tag.AddCssClass("glyphicon-chevron-down");
    //            output.PreContent.AppendHtml(tag);
    //        }
    //    }
    //}

}
