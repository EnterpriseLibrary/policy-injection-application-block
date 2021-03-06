﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Common.Configuration.Design;
using Unity;
using Unity.Injection;
using FakeRules = EnterpriseLibrary.PolicyInjection.MatchingRules;
using Unity.Interception.PolicyInjection.MatchingRules;

namespace EnterpriseLibrary.PolicyInjection.Configuration
{
    /// <summary>
    /// A configuration element that stores configuration information for
    /// an instance of <see cref="TagAttributeMatchingRule"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "TagAttributeMatchingRuleDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "TagAttributeMatchingRuleDataDisplayName")]
    public class TagAttributeMatchingRuleData : StringBasedMatchingRuleData
    {
        /// <summary>
        /// Constructs a new <see cref="TagAttributeMatchingRuleData"/> instance.
        /// </summary>
        public TagAttributeMatchingRuleData()
        {
            Type = typeof(FakeRules.TagAttributeMatchingRule);
        }

        /// <summary>
        /// Constructs a new <see cref="TagAttributeMatchingRuleData"/> instance.
        /// </summary>
        /// <param name="matchingRuleName">Matching rule instance name in configuration.</param>
        /// <param name="tagToMatch">Tag string to match.</param>
        public TagAttributeMatchingRuleData(string matchingRuleName, string tagToMatch)
            : base(matchingRuleName, tagToMatch, typeof(FakeRules.TagAttributeMatchingRule))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [ResourceDescription(typeof(DesignResources), "TagAttributeMatchingRuleDataMatchDescription")]
        [ResourceDisplayName(typeof(DesignResources), "TagAttributeMatchingRuleDataMatchDisplayName")]
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
            container.RegisterType<IMatchingRule, TagAttributeMatchingRule>(registrationName, new InjectionConstructor(this.Match, this.IgnoreCase));
        }
    }
}
