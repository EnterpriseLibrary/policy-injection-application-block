﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EnterpriseLibrary.Common.Configuration.Design;
using EnterpriseLibrary.Common.Configuration.Design.Validation;
using Unity;
using Unity.Injection;
using FakeRules = EnterpriseLibrary.PolicyInjection.MatchingRules;
using Unity.Interception.PolicyInjection.MatchingRules;

namespace EnterpriseLibrary.PolicyInjection.Configuration
{
    /// <summary>
    /// Configuration element that stores the configuration information for an instance
    /// of <see cref="NamespaceMatchingRule"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "NamespaceMatchingRuleDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "NamespaceMatchingRuleDataDisplayName")]
    public class NamespaceMatchingRuleData : MatchingRuleData
    {
        private const string MatchesPropertyName = "matches";

        /// <summary>
        /// Constructs a new <see cref="NamespaceMatchingRuleData"/> instance.
        /// </summary>
        public NamespaceMatchingRuleData()
        {
            Type = typeof(FakeRules.NamespaceMatchingRule);
        }

        /// <summary>
        /// Constructs a new <see cref="NamespaceMatchingRuleData"/> instance.
        /// </summary>
        /// <param name="matchingRuleName">Matching rule name in configuration file.</param>
        public NamespaceMatchingRuleData(string matchingRuleName)
            : this(matchingRuleName, new List<MatchData>())
        {
        }

        /// <summary>
        /// Constructs a new <see cref="NamespaceMatchingRuleData"/> instance.
        /// </summary>
        /// <param name="matchingRuleName">Matching rule name in configuration file.</param>
        /// <param name="namespaceName">Namespace pattern to match.</param>
        public NamespaceMatchingRuleData(string matchingRuleName, string namespaceName)
            : this(matchingRuleName, new MatchData[] { new MatchData(namespaceName) })
        {
        }

        /// <summary>
        /// Constructs a new <see cref="NamespaceMatchingRuleData"/> instance.
        /// </summary>
        /// <param name="matchingRuleName">Matching rule name in configuration file.</param>
        /// <param name="matches">Collection of namespace patterns to match. If any
        /// of the patterns match then the rule matches.</param>
        public NamespaceMatchingRuleData(string matchingRuleName, IEnumerable<MatchData> matches)
            : base(matchingRuleName, typeof(FakeRules.NamespaceMatchingRule))
        {
            foreach (MatchData match in matches)
            {
                Matches.Add(match);
            }
        }

        /// <summary>
        /// The collection of match data containing patterns to match.
        /// </summary>
        /// <value>The "matches" child element.</value>
        [ConfigurationProperty(MatchesPropertyName)]
        [ConfigurationCollection(typeof(MatchData))]
        [ResourceDescription(typeof(DesignResources), "NamespaceMatchingRuleDataMatchesDescription")]
        [ResourceDisplayName(typeof(DesignResources), "NamespaceMatchingRuleDataMatchesDisplayName")]
        [System.ComponentModel.Editor(CommonDesignTime.EditorTypes.CollectionEditor, CommonDesignTime.EditorTypes.FrameworkElement)]
        [EnvironmentalOverrides(false)]
        [Validation(PolicyInjectionDesignTime.Validators.MatchCollectionPopulatedValidationType)]
        public MatchDataCollection<MatchData> Matches
        {
            get { return (MatchDataCollection<MatchData>)base[MatchesPropertyName]; }
            set { base[MatchesPropertyName] = value; }
        }

        /// <summary>
        /// Configures an <see cref="IUnityContainer"/> to resolve the represented matching rule by using the specified name.
        /// </summary>
        /// <param name="container">The container to configure.</param>
        /// <param name="registrationName">The name of the registration.</param>
        protected override void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<IMatchingRule, NamespaceMatchingRule>(
                registrationName,
                new InjectionConstructor(new InjectionParameter(this.Matches.Select(match => new MatchingInfo(match.Match, match.IgnoreCase)).ToArray())));
        }
    }
}
