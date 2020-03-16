using K01.NetCoreMvcGiris.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.TagHelperlarim
{
    [HtmlTargetElement("rolGetir")]
    public class RolGetir : TagHelper
    {
        private readonly UserManager<UygKullanici> _userManager;
        public RolGetir(UserManager<UygKullanici> userManager)
        {
            _userManager = userManager;
        }

        public string UserId { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user);

            string data = string.Empty;
            foreach (var rol in roles)
            {
                if (string.IsNullOrEmpty(data))
                {
                    data += rol;
                }
                else
                {
                    data += "," + rol;
                }
            }

            output.Content.SetContent(data);
        }
    }
}
