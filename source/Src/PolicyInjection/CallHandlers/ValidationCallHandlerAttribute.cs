﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace EnterpriseLibrary.Validation.PolicyInjection
{
    /// <summary>
    /// Applies the <see cref="ValidationCallHandler"/> to its target.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ValidationCallHandlerAttribute : HandlerAttribute
    {
        private string ruleSet;
        private SpecificationSource specificationSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCallHandlerAttribute"/> that uses the
        /// default rule set.
        /// </summary>
        public ValidationCallHandlerAttribute()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCallHandlerAttribute"/> that uses the
        /// given rule set.
        /// </summary>
        /// <param name="ruleSet">The name of the rule set to use.</param>
        public ValidationCallHandlerAttribute(string ruleSet)
        {
            this.ruleSet = ruleSet;
            this.specificationSource = SpecificationSource.Both;
        }

        /// <summary>
        /// Gets the <see cref="SpecificationSource"/> that determines where to get validation rules from.
        /// </summary>
        /// <value>The specification source.</value>
        public SpecificationSource SpecificationSource
        {
            get { return specificationSource; }
            set { specificationSource = value; }
        }

        /// <summary>
        /// Derived classes implement this method. When called, it
        /// creates a new call handler as specified in the attribute
        /// configuration.
        /// </summary>
        /// <returns>A new call handler object.</returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ValidationCallHandler(ruleSet, this.specificationSource, Order);
        }
    }
}
