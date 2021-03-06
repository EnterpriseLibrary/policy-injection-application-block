﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.PolicyInjection.Configuration;
using EnterpriseLibrary.Validation;
using EnterpriseLibrary.Validation.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.PolicyInjection.MatchingRules;
using Unity.Interception.PolicyInjection.Policies;
using Unity.Lifetime;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    [TestClass]
    public class ParameterTypeMatchingRuleDataFixture : MatchingRuleDataFixtureBase
    {
        [TestMethod]
        public void ShouldSerializeAndDeserializeCorrectly()
        {
            ParameterTypeMatchingRuleData original = GetParameterTypeMatchingRuleData();
            Assert.AreEqual(3, original.Matches.Count);

            ParameterTypeMatchingRuleData rehydrated =
                (ParameterTypeMatchingRuleData)SerializeAndDeserializeMatchingRule(original);

            Assert.AreEqual(original.Name, rehydrated.Name);
            Assert.AreEqual(original.Matches.Count, rehydrated.Matches.Count);
            for (int i = 0; i < original.Matches.Count; ++i)
            {
                AssertMatchDataEqual(original.Matches[i], rehydrated.Matches[i], "Match data different at index {0}", i);
            }
        }

        [TestMethod]
        public void ShouldCreateCorrectMatchingRule()
        {
            ValidationFactory.SetDefaultConfigurationValidatorFactory(new ConfigurationValidatorFactory(new DictionaryConfigurationSource()));

            PolicyData policyData = new PolicyData("Validate Parameters");
            policyData.Handlers.Add(new ValidationCallHandlerData());
            ParameterTypeMatchingRuleData matchingRuleData = GetParameterTypeMatchingRuleData();
            policyData.MatchingRules.Add(matchingRuleData);

            PolicyInjectionSettings settings = new PolicyInjectionSettings();
            settings.Policies.Add(policyData);

            DictionaryConfigurationSource configSource = new DictionaryConfigurationSource();
            configSource.Add(PolicyInjectionSettings.SectionName, settings);

            IUnityContainer container = new UnityContainer().AddNewExtension<Interception>();
            settings.ConfigureContainer(container);

            RuleDrivenPolicy policy = container.Resolve<InjectionPolicy>("Validate Parameters") as RuleDrivenPolicy;
            List<IMatchingRule> rules = RuleCreationFixture.GetRules(policy);

            Assert.IsNotNull(policy);
            Assert.IsTrue(rules[0] is ParameterTypeMatchingRule);
            ParameterTypeMatchingRule rule = (ParameterTypeMatchingRule)(rules[0]);
            Assert.AreEqual(3, rule.ParameterMatches.Count());
            for (int i = 0; i < matchingRuleData.Matches.Count; ++i)
            {
                AssertMatchDataEqual(
                    matchingRuleData.Matches[i],
                    rule.ParameterMatches.ElementAt(i),
                    "Mismatch at element {0}",
                    i);
            }
        }

        [TestMethod]
        public void MatchingRuleHasTransientLifetime()
        {
            ParameterTypeMatchingRuleData ruleData = GetParameterTypeMatchingRuleData();

            using (var container = new UnityContainer())
            {
                ruleData.ConfigureContainer(container, "-test");
                var registration = container.Registrations.Single(r => r.Name == "Parameter Matching Rule-test");
                Assert.AreSame(typeof(IMatchingRule), registration.RegisteredType);
                Assert.AreSame(typeof(ParameterTypeMatchingRule), registration.MappedToType);
                Assert.AreSame(typeof(TransientLifetimeManager), registration.LifetimeManager.GetType());
            }
        }

        ParameterTypeMatchingRuleData GetParameterTypeMatchingRuleData()
        {
            return new ParameterTypeMatchingRuleData(
                "Parameter Matching Rule",
                new ParameterTypeMatchData[]
                    {
                        new ParameterTypeMatchData("System.String", ParameterKind.Input),
                        new ParameterTypeMatchData("int32", ParameterKind.InputOrOutput, true),
                        new ParameterTypeMatchData("DateTime", ParameterKind.ReturnValue, false)
                    }
                );
        }

        void AssertMatchDataEqual(ParameterTypeMatchData original,
                                  ParameterTypeMatchData rehydrated,
                                  string message,
                                  params object[] args)
        {
            Assert.AreEqual(original.Match, rehydrated.Match, message, args);
            Assert.AreEqual(original.ParameterKind, rehydrated.ParameterKind, message, args);
            Assert.AreEqual(original.IgnoreCase, rehydrated.IgnoreCase, message, args);
        }

        void AssertMatchDataEqual(ParameterTypeMatchData original,
                                  ParameterTypeMatchingInfo info,
                                  string message,
                                  params object[] args)
        {
            Assert.AreEqual(original.Match, info.Match, message, args);
            Assert.AreEqual(original.ParameterKind, info.Kind, message, args);
            Assert.AreEqual(original.IgnoreCase, info.IgnoreCase, message, args);
        }
    }
}
