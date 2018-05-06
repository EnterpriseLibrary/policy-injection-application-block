// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace EnterpriseLibrary.PolicyInjection.Configuration
{

    internal static class PolicyInjectionDesignTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class ViewModelTypeNames
        {
            public const string PolicyInjectionSectionViewModel = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.PolicyInjection.PolicyInjectionSectionViewModel, EnterpriseLibrary.Configuration.DesignTime";

            public const string PolicyDataViewModel = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.PolicyInjection.PolicyDataViewModel, EnterpriseLibrary.Configuration.DesignTime";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Validators")]
        public static class Validators
        {
            public const string MatchCollectionPopulatedValidationType = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.PolicyInjection.MatchCollectionPopulatedValidator, EnterpriseLibrary.Configuration.DesignTime";

            public const string NameValueCollectionValidator = "EnterpriseLibrary.Configuration.Design.Validation.NameValueCollectionValidator, EnterpriseLibrary.Configuration.DesignTime";
        }
    }
}
