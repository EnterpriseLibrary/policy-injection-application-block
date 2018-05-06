// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport;
using EnterpriseLibrary.PolicyInjection.Configuration;
using EnterpriseLibrary.PolicyInjection.Tests.ConfigFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    /// <summary>
    /// Tests around merging policy injection configuration sections
    /// from multiple config files.
    /// </summary>
    [TestClass]
    public class PolicyInjectionSettingsMergeFixture
    {
        [TestInitialize]
        public void Setup()
        {
            var fileCreator = new ResourceHelper<ConfigFileLocator>();
            fileCreator.DumpResourceFileToDisk("main.config");
            fileCreator.DumpResourceFileToDisk("subordinate.config");
        }

        [TestMethod]
        public void WhenRetrievingAPiabConfigElementPresentInBothSources()
        {
            var configSource = new FileConfigurationSource("main.config");

            var piab = configSource.GetSection(PolicyInjectionSettings.SectionName) as PolicyInjectionSettings;

            Assert.IsNotNull(piab);

            Assert.AreEqual(3, piab.Policies.Count);

            var policy = piab.Policies.Get("Merged Policy");

            Assert.AreEqual(1, policy.MatchingRules.Count);

            var matchingRule = (MemberNameMatchingRuleData)policy.MatchingRules.Get(0);

            Assert.AreEqual(2, matchingRule.Matches.Count);

            var member = matchingRule.Matches[1];

            Assert.IsFalse(member.IgnoreCase);
        }
    }
}
