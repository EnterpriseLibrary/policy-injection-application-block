// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Builder;
using Unity.Lifetime;
using Unity.Policy;
using Unity.Registration;

namespace EnterpriseLibrary.Common.Tests
{
    [TestClass]
    public class GivenAContainerConfiguredWithTheTransientPolicyBuildupExtension
    {
        private IUnityContainer container;

        [TestInitialize]
        public void Setup()
        {
            this.container = new UnityContainer().AddNewExtension<TransientPolicyBuildUpExtension>();
        }

        [TestMethod]
        [Ignore]
        public void WhenResolvingThroughTheExtension_ThenTransientPoliciesAreUsed()
        {
            var singleton = new object();

            var transientLifetimeManager = new ExternallyControlledLifetimeManager();
            transientLifetimeManager.SetValue(singleton);

            object instance =
                this.container.Configure<TransientPolicyBuildUpExtension>()
                    .BuildUp(
                        typeof(object),
                        null,
                        null,
                        new TestTransientLifetimePolicy { LifetimeManager = transientLifetimeManager });

            Assert.AreSame(singleton, instance);
        }

        [TestMethod]
        [Ignore]
        public void WhenResolvingThroughTheExtensionWithAName_ThenTransientPoliciesAreUsed()
        {
            var singleton = new object();

            var transientLifetimeManager = new ExternallyControlledLifetimeManager();
            transientLifetimeManager.SetValue(singleton);

            object instance =
                this.container.Configure<TransientPolicyBuildUpExtension>()
                    .BuildUp(
                        typeof(object),
                        null,
                        "name",
                        new TestTransientLifetimePolicy { LifetimeManager = transientLifetimeManager });

            Assert.AreSame(singleton, instance);
        }

        public class TestTransientLifetimePolicy : InjectionMember
        {
            public LifetimeManager LifetimeManager { get; set; }

            public override void AddPolicies(Type serviceType, Type typeToCreate, string name, IPolicyList policies)
            {
                policies.Set<ILifetimePolicy>(this.LifetimeManager, new NamedTypeBuildKey(typeToCreate, name));
            }
        }
    }
}
