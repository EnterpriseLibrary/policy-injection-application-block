// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Builder;
using Unity.Interception.ContainerIntegration.ObjectBuilder;
using Unity.Interception.Interceptors.InstanceInterceptors.TransparentProxyInterception;
using Unity.Policy;
using Unity.Registration;

namespace EnterpriseLibrary.PolicyInjection.Tests
{
    [TestClass]
    public class GivenInstanceInterceptionPolicySettingInjectionMemberRegisteredForType
    {
        private TransparentProxyInterceptor interceptor;
        private PolicyExposingInjectionMember assertingInjectionMember;
        private NamedTypeBuildKey fooKey = new NamedTypeBuildKey<Foo>();
        [TestInitialize]
        public void Given()
        {
            var container = new UnityContainer();

            interceptor = new TransparentProxyInterceptor();
            var injectionMember = new InstanceInterceptionPolicySettingInjectionMember(interceptor);
            assertingInjectionMember = new PolicyExposingInjectionMember();
            container.RegisterType<Foo>(injectionMember, assertingInjectionMember);
        }

        [TestMethod]
        public void ThenInjectorPolicyAdded()
        {
            var policy = assertingInjectionMember.Policies
                .Get<IInstanceInterceptionPolicy>(fooKey);
            Assert.IsNotNull(policy);
        }

        [TestMethod]
        public void ThenProvidedInterceptorMatchesProvidedInterceptor()
        {
            var policy = assertingInjectionMember.Policies
                .Get<IInstanceInterceptionPolicy>(fooKey);
            Assert.AreSame(interceptor, policy.GetInterceptor(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenInterceptorIsNull_ThenInterceptionMemberThrows()
        {
            new InstanceInterceptionPolicySettingInjectionMember(null);
        }

        private class PolicyExposingInjectionMember : InjectionMember
        {
            public IPolicyList Policies;
            public override void AddPolicies(Type serviceType, Type typeToCreate, string name, IPolicyList policies)
            {
                this.Policies = policies;
            }
        }

        class Foo
        {

        }
    }


}
