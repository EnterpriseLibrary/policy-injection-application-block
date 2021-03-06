﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Interception.PolicyInjection.MatchingRules;
using Unity.Lifetime;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    [TestClass]
    public class ReturnTypeMatchingRuleDataFixture : MatchingRuleDataFixtureBase
    {
        [TestMethod]
        public void CanSerializeTypeMatchingRule()
        {
            ReturnTypeMatchingRuleData returnTypeMatchingRule = new ReturnTypeMatchingRuleData("RuleName", "System.Int32");
            returnTypeMatchingRule.IgnoreCase = true;

            ReturnTypeMatchingRuleData deserializedRule = SerializeAndDeserializeMatchingRule(returnTypeMatchingRule) as ReturnTypeMatchingRuleData;

            Assert.IsNotNull(deserializedRule);
            Assert.AreEqual(returnTypeMatchingRule.Name, deserializedRule.Name);
            Assert.IsTrue(deserializedRule.IgnoreCase);
            Assert.AreEqual(returnTypeMatchingRule.Match, deserializedRule.Match);
        }

        [TestMethod]
        public void MatchingRuleHasTransientLifetime()
        {
            ReturnTypeMatchingRuleData ruleData = new ReturnTypeMatchingRuleData("RuleName", "System.Int32");

            using (var container = new UnityContainer())
            {
                ruleData.ConfigureContainer(container, "-test");
                var registration = container.Registrations.Single(r => r.Name == "RuleName-test");
                Assert.AreSame(typeof(IMatchingRule), registration.RegisteredType);
                Assert.AreSame(typeof(ReturnTypeMatchingRule), registration.MappedToType);
                Assert.AreSame(typeof(TransientLifetimeManager), registration.LifetimeManager.GetType());
            }
        }
    }
}
