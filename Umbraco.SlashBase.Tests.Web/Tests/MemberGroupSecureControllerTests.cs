﻿namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Web.Security;

    using NUnit.Framework;

    using umbraco.cms.businesslogic.member;
    using umbraco.providers.members;

    public class MemberGroupSecureControllerTests : BaseTestFixture
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
        public void Get_WhenNotLoggedIn_ShouldReturnException()
        {
            var result = this.Client.GetAsync("MemberGroupSecure").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }
    }
}