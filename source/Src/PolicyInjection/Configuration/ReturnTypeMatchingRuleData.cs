﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using EnterpriseLibrary.Common.Configuration.Design;
using Unity;
using Unity.Injection;
using FakeRules = EnterpriseLibrary.PolicyInjection.MatchingRules;
using Unity.Interception.PolicyInjection.MatchingRules;

namespace EnterpriseLibrary.PolicyInjection.Configuration
{
    /// <summary>
    /// A configuration element that stores configuration information about an
    /// instance of <see cref="ReturnTypeMatchingRule"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "ReturnTypeMatchingRuleDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "ReturnTypeMatchingRuleDataDisplayName")]
    public class ReturnTypeMatchingRuleData : StringBasedMatchingRuleData
    {
        /// <summary>
        /// Constructs a new <see cref="ReturnTypeMatchingRuleData"/> instance.
        /// </summary>
        public ReturnTypeMatchingRuleData()
            : base()
        {
            Type = typeof(FakeRules.ReturnTypeMatchingRule);
        }

        /// <summary>
        /// Constructs a new <see cref="ReturnTypeMatchingRuleData"/> instance.
        /// </summary>
        /// <param name="matchingRuleName">Matching rule instance name in configuration.</param>
        /// <param name="returnTypeName">Return type to match.</param>
        public ReturnTypeMatchingRuleData(string matchingRuleName, string returnTypeName)
            : base(matchingRuleName, returnTypeName, typeof(FakeRules.ReturnTypeMatchingRule))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [ResourceDescription(typeof(DesignResources), "ReturnTypeMatchingRuleDataMatchDescription")]
        [ResourceDisplayName(typeof(DesignResources), "ReturnTypeMatchingRuleDataMatchDisplayName")]
        [Editor(CommonDesignTime.EditorTypes.TypeSelector, CommonDesignTime.EditorTypes.UITypeEditor)]
        public override string Match
        {
            get { return base.Match; }
            set { base.Match = value; }
        }

        /// <summary>
        /// Configures an <see cref="IUnityContainer"/> to resolve the represented matching rule by using the specified name.
        /// </summary>
        /// <param name="container">The container to configure.</param>
        /// <param name="registrationName">The name of the registration.</param>
        protected override void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<IMatchingRule, ReturnTypeMatchingRule>(registrationName, new InjectionConstructor(this.Match, this.IgnoreCase));
        }
    }
}
