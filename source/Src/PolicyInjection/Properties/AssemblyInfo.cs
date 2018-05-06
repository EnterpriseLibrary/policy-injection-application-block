// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using EnterpriseLibrary.Common.Configuration.Design;
using EnterpriseLibrary.PolicyInjection.Configuration;

[assembly: ReliabilityContract(Consistency.WillNotCorruptState, Cer.None)]

[assembly: AllowPartiallyTrustedCallers]
[assembly: SecurityRules(SecurityRuleSet.Level1)]

[assembly: ComVisible(false)]

[assembly: HandlesSection(PolicyInjectionSettings.SectionName)]

[assembly: AddApplicationBlockCommand(
                PolicyInjectionSettings.SectionName,
                typeof(PolicyInjectionSettings),
                TitleResourceName = "AddPolicyInjectionSettings",
                TitleResourceType = typeof(DesignResources))]
