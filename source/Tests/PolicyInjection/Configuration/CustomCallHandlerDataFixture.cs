// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.PolicyInjection.TestSupport.ObjectsUnderTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;

namespace EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    [TestClass]
    public class GivenACustomCallHandlerData
    {
        private CallHandlerData callHandlerData;

        [TestInitialize]
        public void Setup()
        {
            callHandlerData =
                new CustomCallHandlerData("custom", typeof(GlobalCountCallHandler))
                {
                    Order = 100,
                    Attributes = { { "callhandler", "bar" }, { "bar", "baz" } }
                };
        }

        [TestMethod]
        public void WhenConfiguredContainer_ThenCanResolveCallHandler()
        {
            using (var container = new UnityContainer())
            {
                this.callHandlerData.ConfigureContainer(container, "-suffix");

                var handler = (GlobalCountCallHandler)container.Resolve<ICallHandler>("custom-suffix");

                Assert.AreEqual("bar", handler.callHandlerName);
                Assert.AreEqual(100, handler.Order);

                Assert.AreSame(handler, container.Resolve<ICallHandler>("custom-suffix"));
            }
        }
    }
}
