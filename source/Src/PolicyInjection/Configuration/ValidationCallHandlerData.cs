﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Configuration;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.Configuration.Design;
using EnterpriseLibrary.PolicyInjection.Configuration;
using EnterpriseLibrary.Validation.PolicyInjection;
using Unity;
using Unity.Injection;
using Unity.Interception.PolicyInjection.Pipeline;

namespace EnterpriseLibrary.Validation.Configuration
{
    /// <summary>
    /// A configuration element class that stores the configuration data for
    /// the <see cref="ValidationCallHandler"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "ValidationCallHandlerDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "ValidationCallHandlerDataDisplayName")]
    [AddSateliteProviderCommand(ValidationSettings.SectionName)]
    public class ValidationCallHandlerData : CallHandlerData
    {
        private const string RuleSetPropertyName = "ruleSet";
        private const string SpecificationSourcePropertyName = "specificationSource";

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCallHandlerData"/> class.
        /// </summary>
        public ValidationCallHandlerData()
        {
            Type = typeof(ValidationCallHandler);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCallHandlerData"/> class by using the specified handler name.
        /// </summary>
        /// <param name="handlerName">The name of the handler in the configuration.</param>
        public ValidationCallHandlerData(string handlerName)
            : base(handlerName, typeof(ValidationCallHandler))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCallHandlerData"/> class by using the specified handler name and order.
        /// </summary>
        /// <param name="handlerName">The name of the handler in the configuration.</param>
        /// <param name="handlerOrder">The order of the handler in the configuration.</param>
        public ValidationCallHandlerData(string handlerName, int handlerOrder)
            : base(handlerName, typeof(ValidationCallHandler))
        {
            this.Order = handlerOrder;
        }

        /// <summary>
        /// Gets The rule set name to use for all types.
        /// </summary>
        /// <remarks>
        /// An empty string specifies the default rule set. 
        /// </remarks>
        /// <value>The "ruleSet" configuration property.</value>
        [ConfigurationProperty(RuleSetPropertyName)]
        [Reference(typeof(ValidationSettings), typeof(ValidationRulesetData))]
        public string RuleSet
        {
            get { return (string)base[RuleSetPropertyName]; }
            set { base[RuleSetPropertyName] = value; }
        }

        /// <summary>
        /// SpecificationSource (Both | Attributes | Configuration) : Where to look for validation rules. Default is Both.
        /// </summary>
        /// <value>The "specificationSource" configuration attribute.</value>
        [ConfigurationProperty(SpecificationSourcePropertyName, IsRequired = true, DefaultValue = SpecificationSource.Both)]
        [ResourceDescription(typeof(DesignResources), "ValidationCallHandlerDataSpecificationSourceDescription")]
        [ResourceDisplayName(typeof(DesignResources), "ValidationCallHandlerDataSpecificationSourceDisplayName")]
        public SpecificationSource SpecificationSource
        {
            get { return (SpecificationSource)base[SpecificationSourcePropertyName]; }
            set { base[SpecificationSourcePropertyName] = value; }
        }

        /// <summary>
        /// Configures an <see cref="IUnityContainer"/> to resolve the represented call handler by using the specified name.
        /// </summary>
        /// <param name="container">The container to configure.</param>
        /// <param name="registrationName">The name of the registration.</param>
        protected override void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<ICallHandler, ValidationCallHandler>(
                registrationName,
                new InjectionConstructor(this.RuleSet, this.SpecificationSource, this.Order));
        }
    }
}
