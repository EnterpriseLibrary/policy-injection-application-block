﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Reflection;
using EnterpriseLibrary.Common;

namespace EnterpriseLibrary.Logging.PolicyInjection
{
    /// <summary>
    /// Represents a formatter object that allows for the replacement of tokens in
    /// a log handler category string.
    /// </summary>
    /// <remarks>This class supports the following replacements:
    /// <list>
    /// <item><term>{method}</term><description>The target method name.</description></item>
    /// <item><term>{type}</term><description>The target method's implementing type.</description></item>
    /// <item><term>{namespace}</term><description>The namespace that contains the target's type.</description></item>
    /// <item><term>{assembly}</term><description>The assembly that contains the target's type.</description></item>
    /// </list></remarks>
    public class CategoryFormatter : ReplacementFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryFormatter"/> class by using information from the
        /// given method.
        /// </summary>
        /// <param name="method">The method used to generate the category replacements.</param>
        public CategoryFormatter(MethodBase method)
        {
            Add(
                new ReplacementToken("{method}",
                    delegate { return method.Name; }),
                new ReplacementToken("{type}",
                    delegate { return method.DeclaringType.Name; }),
                new ReplacementToken("{namespace}",
                    delegate { return method.DeclaringType.Namespace; }),
                new ReplacementToken("{assembly}",
                    delegate
                    {
                        return method.DeclaringType.Assembly.FullName;
                    })
                );
        }

        /// <summary>
        /// Performs the formatting operation by replacing tokens in the template.
        /// </summary>
        /// <param name="template">The template string to replace the tokens in.</param>
        /// <returns>The template, with tokens replaced.</returns>
        public string FormatCategory(string template)
        {
            return Format(template);
        }
    }
}
