﻿namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;
    using System.Web.Security;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    [SlashBaseSecurity(AllowedMembers = new[] { "admin" })]
    public class MemberSecureController : SlashBaseController
    {
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello admin member!") };
        }

        [SlashBaseSecurity(AllowedMembers = new[] { "user" })]
        public HttpResponseMessage Get(string id)
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello admin/user member!") };
        }
    }
}