# Dazinate.Dnn
This repository contains some .net utility libaries (work still in progress) that may be useful for stand alone applications that 
wish to "do things" with Dnn. 

These libraries do not have any depedencies on any Dnn assemblies, so that they can be used from applications outside Dnn.

## Manifests

This library, provides a domain model for working with Dnn manifest files, including validation. 

You can load a manifest like so:

```
           var factory = new PackagesDnnManifestFactory();
           string manifestXml = xmlContents; // the xml file contents you want to load.
           var dnnManifest = factory.Get(manifestXml);

```

Once you have the business object, you will see that it has methods and properties to fully manipulate the manifest, 
by adding and editing packages, dependencies, components etc.

The business object fully supports data-binding, and validation rules, thanks to CSLA, so you can directly bind it to your UI framework of choice (WPF, Windows Forms etc)

Dirty tracking, and validation rules are run automatically, and you can check the status of the business object easily:

```
            Assert.True(dnnManifest.IsValid);
            Assert.False(dnnManifest.IsDirty);
```

If the state of the manifest is invalid, you can inspect the failed rules:

```
            var brokenRules = dnnManifest.GetBrokenRules();
            var allBrokenRulesText = brokenRules.ToString();
            foreach (var brokenRule in brokenRules)
            {
              string individualBrokenRuleText =  brokenRule.ToString();
            }
            
```

This will all be familiar to anyone that has worked with CSLA in the past.

## Currently WIP

I have not yet issued the first major release, as I am still developing this library. It doesn't yet support all of the manifest - see issues for stuff left to do.
The first release will support the manifest in it's entirity though.
