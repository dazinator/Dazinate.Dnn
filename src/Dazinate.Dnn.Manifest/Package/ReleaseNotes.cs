using System;
using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package
{
    [Serializable]
    public class ReleaseNotes : BusinessBase<ReleaseNotes>, IReleaseNotes
    {
        public static readonly PropertyInfo<string> SourceFileProperty = RegisterProperty<string>(c => c.SourceFile);
        public string SourceFile
        {
            get { return GetProperty(SourceFileProperty); }
            set { SetProperty(SourceFileProperty, value); }
        }

        public static readonly PropertyInfo<string> ContentsProperty = RegisterProperty<string>(c => c.Contents);
        public string Contents
        {
            get { return GetProperty(ContentsProperty); }
            set { SetProperty(ContentsProperty, value); }
        }

        public bool IsEmpty()
        {
            var empty = string.IsNullOrWhiteSpace(SourceFile) && string.IsNullOrWhiteSpace(Contents);
            return empty;
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Dependency(ContentsProperty, SourceFileProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Dependency(SourceFileProperty, ContentsProperty));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(ContentsProperty, (c) =>
            {
                ReleaseNotes target = (ReleaseNotes)c.Target;

                if (!string.IsNullOrWhiteSpace(target.Contents) && !string.IsNullOrWhiteSpace(target.SourceFile))
                {
                    c.AddErrorResult("Release Notes cannot have a contents, and a source file specified. You must specify one or the other.");
                }
            }));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(SourceFileProperty, (c) =>
            {
                ReleaseNotes target = (ReleaseNotes)c.Target;

                if (!string.IsNullOrWhiteSpace(target.Contents) && !string.IsNullOrWhiteSpace(target.SourceFile))
                {
                    c.AddErrorResult("Release Notes cannot have a contents, and a source file specified. You must specify one or the other.");
                }
            }));

        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}