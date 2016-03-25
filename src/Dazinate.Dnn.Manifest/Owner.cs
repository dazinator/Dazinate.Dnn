using System;
using Csla;
using Dazinate.Dnn.Manifest.ObjectFactory;

namespace Dazinate.Dnn.Manifest
{
    [Serializable]
    public class Owner : BusinessBase<Owner>, IOwner
    {
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> OrganisationProperty = RegisterProperty<string>(c => c.Organisation);
        public string Organisation
        {
            get { return GetProperty(OrganisationProperty); }
            set { SetProperty(OrganisationProperty, value); }
        }

        public static readonly PropertyInfo<string> UrlProperty = RegisterProperty<string>(c => c.Url);
        public string Url
        {
            get { return GetProperty(UrlProperty); }
            set { SetProperty(UrlProperty, value); }
        }

        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);
        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(EmailProperty, (c) =>
            {
                Owner target = (Owner)c.Target;
                var regexUtils = new RegexUtilities();
                if (!string.IsNullOrWhiteSpace(target.Email))
                {
                    if (!regexUtils.IsValidEmail(target.Email))
                    {
                        c.AddWarningResult("Are you sure this is a valid email address?");
                    }
                }
            }));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(UrlProperty, (c) =>
            {
                Owner target = (Owner)c.Target;

                if (!string.IsNullOrWhiteSpace(target.Url))
                {
                    Uri uriResult;
                    bool result = Uri.TryCreate(target.Url, UriKind.Absolute, out uriResult)
                                  && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                    if (!result)
                    {
                        c.AddWarningResult("Url should be a http or https address.");
                    }
                }
            }));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(NameProperty, (c) =>
            {
                Owner target = (Owner)c.Target;

                if (string.IsNullOrWhiteSpace(target.Name))
                {
                    c.AddWarningResult("You should specify the owner's name of the package.");
                }
            }));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(OrganisationProperty, (c) =>
            {
                Owner target = (Owner)c.Target;
                if (string.IsNullOrWhiteSpace(target.Name))
                {
                    c.AddWarningResult("You should specify an organization name for this package.");
                }
            }));




        }
    }
}