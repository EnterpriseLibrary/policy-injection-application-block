// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Configuration;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.Configuration;
using EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    public class MatchingRuleDataFixtureBase
    {
        protected void AssertMatchDataEqual(MatchData expected,
                                            MatchData actual,
                                            string errorMessage,
                                            params object[] errorArgs)
        {
            Assert.AreEqual(expected.Match, actual.Match, errorMessage, errorArgs);
            Assert.AreEqual(expected.IgnoreCase, actual.IgnoreCase, errorMessage, errorArgs);
        }

        protected static MatchingRuleData SerializeAndDeserializeMatchingRule(MatchingRuleData typeMatchingRule)
        {
            PolicyData policy = new PolicyData("policy");
            policy.MatchingRules.Add(typeMatchingRule);

            PolicyInjectionSettings settings = new PolicyInjectionSettings();
            settings.Policies.Add(policy);

            Dictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>();
            sections.Add(PolicyInjectionSettings.SectionName, settings);

            IConfigurationSource configurationSource = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            PolicyInjectionSettings deserializedSection = configurationSource.GetSection(PolicyInjectionSettings.SectionName) as PolicyInjectionSettings;
            Assert.IsNotNull(deserializedSection);

            PolicyData deserializedPolicy = deserializedSection.Policies.Get(0);
            Assert.IsNotNull(deserializedPolicy);

            return deserializedPolicy.MatchingRules.Get(0);
        }
    }
}
