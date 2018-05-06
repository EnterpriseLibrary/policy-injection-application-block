// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using EnterpriseLibrary.PolicyInjection.Installers;

namespace EnterpriseLibrary.PolicyInjection.CallHandlers.Tests
{
    [RunInstaller(true)]
    public class PerfCounterForCallHandlerTestsInstaller : System.Configuration.Install.Installer
    {
        private readonly PerformanceCountersInstaller countersInstaller;

        public PerfCounterForCallHandlerTestsInstaller()
        {
            countersInstaller = new PerformanceCountersInstaller(PerformanceCounterCallHandlerFixture.TestCategoryName);
            Installers.Add(countersInstaller);
        }
    }
}
