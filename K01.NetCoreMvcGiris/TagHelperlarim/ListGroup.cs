using K01.NetCoreMvcGiris.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.TagHelperlarim
{
    [HtmlTargetElement("listgroup")]
    public class ListGroup : TagHelper
    {
        public List<Kategori> Kategoriler { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //string 

            // tagbuilder

            /* 
             <ul class="list-group">
  <li class="list-group-item">Cras justo odio</li>
  <li class="list-group-item">Dapibus ac facilisis in</li>
  <li class="list-group-item">Morbi leo risus</li>
  <li class="list-group-item">Porta ac consectetur ac</li>
  <li class="list-group-item">Vestibulum at eros</li>
</ul>
             
             */

            //var ul = new TagBuilder("ul");
            //ul.Attributes["class"] = "list-group";
            //foreach (var kategori in Kategoriler)
            //{
            //    ul.InnerHtml.AppendHtml($"<li class='list-group-item'>{kategori.Ad}</li>");
            //}

            string ul = string.Empty;
            ul = "<ul class='list-group'> ";
            foreach (var kategori in Kategoriler)
            {
                ul += $"<li class='list-group-item'>{kategori.Ad}</li>";
            }
            ul += "</ul>";

            output.Content.SetHtmlContent(ul);

        }
    }
}
