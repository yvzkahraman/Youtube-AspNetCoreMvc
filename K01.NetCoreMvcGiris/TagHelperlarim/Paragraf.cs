using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.TagHelperlarim
{
    [HtmlTargetElement("paragraf")]
    public class Paragraf : TagHelper
    {
        public string ParagrafIcerik { get; set; }
        public string ParagrafRenk { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /* 
             TagBuilder => html etiketi
             StringBuilder
             string
             */

            output.Attributes.SetAttribute("class", "text-" + ParagrafRenk);
            //output.Content.Append("<p> Yavuz </p>");
            //output.Content.AppendHtml("<b> Yavuz </b>");

            //output.Content.SetContent("");
            //output.Content.SetHtmlContent("");

            output.Content.SetContent(ParagrafIcerik);

            // <paragraf> Icerik </paragraf>
        }
    }
}
