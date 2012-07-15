﻿namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Security;

    using NUnit.Framework;

    using umbraco.cms.businesslogic.member;
    using umbraco.providers.members;
    using Umbraco.SlashBase.Tests.Web.Helpers;

    public class MemberSecureControllerTests : BaseTestFixture
    {
        /// <summary>
        /// The membership provider
        /// </summary>
        private UmbracoMembershipProvider provider;

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.provider = new UmbracoMembershipProvider();
           
            var config = new NameValueCollection
                {
                    { "name", "UmbracoMembershipProvider" },
                    { "enablePasswordRetrieval", "false" },
                    { "enablePasswordReset", "false" },
                    { "requiresQuestionAndAnswer", "false" },
                    { "defaultMemberTypeAlias", "Another Type" },
                    { "passwordFormat", "Hashed" }
                };
            this.provider.Initialize(config["name"], config);
        }

        [Test]
        public void Get_WhenLoggedIn_ShouldReturnOK()
        {
            var member = Membership.GetUser("admin");

            var loggedIn = LoginHelper.DoLogin(member, this.Client);

            var result = this.Client.GetAsync("uBase/MemberSecure").Result;

            Assert.That(result.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void Get_WhenNotLoggedIn_ShouldReturnForbidden()
        {
            var result = this.Client.GetAsync("uBase/MemberSecure").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }
    }
}
