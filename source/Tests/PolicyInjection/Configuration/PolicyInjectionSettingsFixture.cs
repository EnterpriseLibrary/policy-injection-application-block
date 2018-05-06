// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Interception.PolicyInjection.Policies;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    [TestClass]
    public class PolicyInjectionSettingsFixture
    {
        [TestMethod]
        public void SkipsInjectorsElement()
        {
            using (var source = new FileConfigurationSource("OldStyle.config", false))
            {
                PolicyInjectionSettings settings
                    = (PolicyInjectionSettings)source.GetSection(PolicyInjectionSettings.SectionName);

                Assert.IsNotNull(settings);
                Assert.AreEqual(3, settings.Policies.Count);
            }
        }
    }

    [TestClass]
    public class GivenAnEmptySection
    {
        private PolicyInjectionSettings settings;

        [TestInitialize]
        public void Setup()
        {
            settings = new PolicyInjectionSettings();
        }

        [TestMethod]
        public void WhenConfiguringContainer_ThenConfiguresNoPolicies()
        {
            using (var container = new UnityContainer())
            {
                this.settings.ConfigureContainer(container);

                Assert.AreEqual(0, container.Registrations.Where(r => r.RegisteredType.Assembly != typeof(IUnityContainer).Assembly).Count());
            }
        }
    }

    [TestClass]
    public class GivenASectionWithAnEmptyPolicy
    {
        private PolicyInjectionSettings settings;

        [TestInitialize]
        public void Setup()
        {
            settings = new PolicyInjectionSettings();
            settings.Policies.Add(new PolicyData("policy 1") { });
        }

        [TestMethod]
        public void WhenConfiguringContainer_ThenConfiguresEmptyPolicy()
        {
            using (var container = new UnityContainer())
            {
                this.settings.ConfigureContainer(container);

                Assert.AreEqual(1, container.Registrations.Where(r => r.RegisteredType.Assembly != typeof(IUnityContainer).Assembly).Count());
                Assert.AreEqual("policy 1", container.Registrations.Single(r => r.RegisteredType == typeof(InjectionPolicy)).Name);
            }
        }
    }

    [TestClass]
    public class GivenAConfigurationSourceWithoutPolicySettings
    {
        private IConfigurationSource source;

        [TestInitialize]
        public void TestInitialize()
        {
            this.source = new DictionaryConfigurationSource();
        }

        [TestMethod]
        public void WhenConfiguringContainer_ThenConfiguresNoPolicies()
        {
            using (var container = new UnityContainer())
            {
                PolicyInjectionSettings.ConfigureContainer(container, this.source);

                Assert.AreEqual(0, container.Registrations.Where(r => r.RegisteredType.Assembly != typeof(IUnityContainer).Assembly).Count());
            }
        }
    }

    [TestClass]
    public class GivenAConfigurationSourceWithPolicySettingsWithAnEmptyPolicy
    {
        private IConfigurationSource source;

        [TestInitialize]
        public void TestInitialize()
        {
            this.source = new DictionaryConfigurationSource();
            var settings = new PolicyInjectionSettings();
            settings.Policies.Add(new PolicyData("policy 1") { });

            this.source.Add(PolicyInjectionSettings.SectionName, settings);
        }

        [TestMethod]
        public void WhenConfiguringContainer_ThenConfiguresEmptyPolicy()
        {
            using (var container = new UnityContainer())
            {
                PolicyInjectionSettings.ConfigureContainer(container, this.source);

                Assert.AreEqual(1, container.Registrations.Where(r => r.RegisteredType.Assembly != typeof(IUnityContainer).Assembly).Count());
                Assert.AreEqual("policy 1", container.Registrations.Single(r => r.RegisteredType == typeof(InjectionPolicy)).Name);
            }
        }
    }
}
