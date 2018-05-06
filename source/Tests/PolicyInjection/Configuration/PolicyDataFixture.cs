// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using EnterpriseLibrary.Common.Utility;
using EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Interception.Interceptors;
using Unity.Interception.PolicyInjection.MatchingRules;
using Unity.Interception.PolicyInjection.Policies;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    [TestClass]
    public class GivenANewPolicyData
    {
        private PolicyData policyData;

        [TestInitialize]
        public void Setup()
        {
            policyData = new PolicyData("policy");
        }

        [TestMethod]
        public void WhenConfiguredContainer_ThenCanResolvePolicy()
        {
            using (var container = new UnityContainer())
            {
                this.policyData.ConfigureContainer(container);

                var policy = container.Resolve<InjectionPolicy>("policy");

                var method = new MethodImplementationInfo(StaticReflection.GetMethodInfo<object>(o => o.ToString()), StaticReflection.GetMethodInfo<object>(o => o.ToString()));

                Assert.AreEqual("policy", policy.Name);
                Assert.IsFalse(policy.Matches(method));
                Assert.AreEqual(0, policy.GetHandlersFor(method, container).Count());
            }
        }
    }

    [TestClass]
    public class GivenAPolicyDataWithAMatchingRule
    {
        private PolicyData policyData;

        [TestInitialize]
        public void Setup()
        {
            policyData = new PolicyData("policy");
            policyData.MatchingRules.Add(new TypeMatchingRuleData("type", typeof(object).FullName));
        }

        [TestMethod]
        public void WhenConfiguredContainer_ThenCanResolvePolicy()
        {
            using (var container = new UnityContainer())
            {
                this.policyData.ConfigureContainer(container);

                var policy = container.Resolve<InjectionPolicy>("policy");

                var method = new MethodImplementationInfo(StaticReflection.GetMethodInfo<object>(o => o.ToString()), StaticReflection.GetMethodInfo<object>(o => o.ToString()));

                Assert.AreEqual("policy", policy.Name);
                Assert.IsTrue(policy.Matches(method));
                Assert.AreEqual(0, policy.GetHandlersFor(method, container).Count());

                Assert.AreNotSame(policy, container.Resolve<InjectionPolicy>("policy"));

                Assert.IsTrue(container.Registrations.Any(r => r.Name == "type-policy" && r.RegisteredType == typeof(IMatchingRule) && r.MappedToType == typeof(TypeMatchingRule)));
            }
        }
    }
}
