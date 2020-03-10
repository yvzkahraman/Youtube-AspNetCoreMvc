using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.TagHelperlarim
{
    //,ParentTag ="div"
    [HtmlTargetElement("sayfala")]
    public class Sayfa : TagHelper
    {
        public int AktifSayfa { get; set; }
        public int ToplamSayfa { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //string element = "<div class='pagination'> ";
            //for (int i = 1; i <= ToplamSayfa; i++)
            //{
            //    string aktif = AktifSayfa == i ? "active" : "";
            //    element += $"<div class='page-item {aktif}'> <a href='/Tag/Index?aktifSayfa={i}' class='page-link'>{i}</a> </div>";
            //}

            //element += "</div>";

            StringBuilder builder = new StringBuilder();
            builder.Append("<div class='pagination'>");
            for (int i = 1; i <= ToplamSayfa; i++)
            {
                builder.AppendFormat("<div class='page-item {0}'> <a href='/Tag/Index?aktifSayfa={1}' class='page-link'>{1}</a> </div>",AktifSayfa==i?"active":"",i);
            }
            builder.Append("</div>");


            output.Content.SetHtmlContent(builder.ToString());
        }
    }
}
