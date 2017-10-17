using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Dazinate.Dnn.Manifest.Tests
{
    public class ServiceProviderObjectFactoryLoaderTests
    {

        private readonly ITestOutputHelper _output;


        public ServiceProviderObjectFactoryLoaderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_Load_Type()
        {          
            Console.WriteLine("Resolving type");          
            Console.WriteLine(CoreClrHelpers.GetCoreClrVersion());              
            var type = Type.GetType("Dazinate.Dnn.Manifest.ObjectFactory.IPackagesDnnManifestObjectFactory, Dazinate.Dnn.Manifest");
            Assert.NotNull(type);
        }
    }

    public static class CoreClrHelpers
    {
        static string coreCLRVersion = "NOT_YET_ASSESSED";

        public static string GetCoreClrVersion()
        {
            if (coreCLRVersion == "NOT_YET_ASSESSED") //the following code might take some time to run, but we only need to do the heavy lifting once.  Not sure if this is the best way to determine CLr version, but it works
            {
                var appDomainType = typeof(object).GetTypeInfo().Assembly?.GetType("System.AppDomain");
                var currentDomain = appDomainType?.GetProperty("CurrentDomain")?.GetValue(null);
                var deps = appDomainType?.GetMethod("GetData")?.Invoke(currentDomain, new[] { "FX_DEPS_FILE" });
                if (deps == null)
                {
                    coreCLRVersion = "";
                    return coreCLRVersion;
                }
                coreCLRVersion = GetCoreClrVersionImpl(deps.ToString());
            }

            return coreCLRVersion;
        }

        internal static string GetCoreClrVersionImpl(string deps)
        {
            var result = Regex.Match(deps, "(?:(\\d+)\\.)?(?:(\\d+)\\.)?(?:(\\d+)\\.\\d+)").Value;
            return result;
        }
    }
}
