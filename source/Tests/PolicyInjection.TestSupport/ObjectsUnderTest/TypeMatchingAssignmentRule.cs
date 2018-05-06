// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Reflection;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.PolicyInjection.Configuration;
using Unity.Interception.PolicyInjection.MatchingRules;

namespace EnterpriseLibrary.PolicyInjection.TestSupport.ObjectsUnderTest
{
    [ConfigurationElementType(typeof(CustomMatchingRuleData))]
    public class TypeMatchingAssignmentRule : IMatchingRule
    {
        private Type matchType;

        public TypeMatchingAssignmentRule(Type matchType)
        {
            this.matchType = matchType;
        }

        public bool Matches(MethodBase member)
        {
            //if(member is Type)
            //{
            //    return member == matchType;
            //}

            return (member.DeclaringType == matchType);
        }
    }
}
