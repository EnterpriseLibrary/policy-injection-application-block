﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Configuration;
using EnterpriseLibrary.Common.Configuration.Design;

namespace EnterpriseLibrary.PolicyInjection.Configuration
{
    /// <summary>
    /// Base class for matching rule configuration data for those rules that take
    /// a single match instance consisting of a string to match and an
    /// ignore case flag.
    /// </summary>
    public abstract class StringBasedMatchingRuleData : MatchingRuleData
    {
        private const string MatchPropertyName = "match";
        private const string IgnoreCasePropertyName = "ignoreCase";

        /// <summary>
        /// Constructs a new <see cref="StringBasedMatchingRuleData"/> instance.
        /// </summary>
        protected StringBasedMatchingRuleData()
            : base()
        {
        }

        /// <summary>
        /// Constructs a new <see cref="StringBasedMatchingRuleData"/> instance.
        /// </summary>
        /// <param name="matchingRuleName">Name of the matching rule</param>
        /// <param name="matches">String to match.</param>
        /// <param name="matchingRuleType">Type of the underlying matching rule.</param>
        protected StringBasedMatchingRuleData(string matchingRuleName, string matches, Type matchingRuleType)
            : base(matchingRuleName, matchingRuleType)
        {
            Match = matches;
        }

        /// <summary>
        /// The string to match.
        /// </summary>
        /// <value>The "match" configuration attribute.</value>
        [ConfigurationProperty(MatchPropertyName, IsRequired = true)]
        [ResourceDescription(typeof(DesignResources), "StringBasedMatchingRuleDataMatchDescription")]
        [ResourceDisplayName(typeof(DesignResources), "StringBasedMatchingRuleDataMatchDisplayName")]
        public virtual string Match
        {
            get { return (string)base[MatchPropertyName]; }
            set { base[MatchPropertyName] = value; }
        }

        /// <summary>
        /// Should comparisons be case sensitive?
        /// </summary>
        /// <value>The "ignoreCase" configuration attribute. If false, comparison is
        /// case sensitive. If true, comparison is case insensitive.</value>
        [ConfigurationProperty(IgnoreCasePropertyName, DefaultValue = false, IsRequired = false)]
        [ResourceDescription(typeof(DesignResources), "StringBasedMatchingRuleDataIgnoreCaseDescription")]
        [ResourceDisplayName(typeof(DesignResources), "StringBasedMatchingRuleDataIgnoreCaseDisplayName")]
        public bool IgnoreCase
        {
            get { return (bool)base[IgnoreCasePropertyName]; }
            set { base[IgnoreCasePropertyName] = value; }
        }
    }
}
