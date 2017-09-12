#if !NETDESKTOP
using System.Runtime.Loader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

#endif

namespace Dazinate.Dnn.Manifest.Ioc
{

#if !NETDESKTOP
    /// <summary>
    /// Responsible for returning information about all libraries and their depencies, for the application.
    /// </summary>
    public class GluonApplicationLibraryManager //: IGluonApplicationLibraryManager
    {
       // private readonly CandidateResolver _candidateResolver;
        public Assembly ApplicationEntryAssembly { get; }

        // public GluonEnvironment GluonEnvironment { get; }

        // protected DependencyContext OriginalApplicationDependencyContext { get; private set; }
        public string[] ProbingPaths { get; set; }
        public string BinDirectory { get; set; }



        ///// <summary>
        ///// The Application Dependency Context includes information about runtime libraries referenced by the application, as well as modules loaded by the application, and their dependencies.
        ///// </summary>
        //protected DependencyContext ApplicationDependencyContext { get; private set; }

        public GluonApplicationLibraryManager(string applicationName, string[] additionalProbingPaths)
        {
            //GluonEnvironment = env;
            ApplicationEntryAssembly = Assembly.Load(new AssemblyName(applicationName));
           // OriginalApplicationDependencyContext = DependencyContext.Load(ApplicationEntryAssembly);
            //ApplicationDependencyContext = OriginalApplicationDependencyContext;
            //if (ApplicationDependencyContext == null)
            //{
            //    throw new InvalidOperationException("Unable to load DependencyContext for application entry point assembly. Please ensure you have set buildOptions:preserveCompilationContext to true in your application's project.json.");
            //}
            BinDirectory = System.IO.Path.GetDirectoryName(ApplicationEntryAssembly.Location);

            ProbingPaths = additionalProbingPaths;



            //  ModuleDependencyContexts = new List<DependencyContext>();
            //  LoadDependencyContextsFromAssemblyFiles(shadowCopy.CopiedAssemblies);
            //var runtimeLibs = ApplicationDependencyContext.RuntimeLibraries;
            //var refAssemblies = ReferenceAssemblies;
            //_candidateResolver = new CandidateResolver(runtimeLibs, refAssemblies);

        }

        //private void LoadDependencyContextsFromAssemblyFiles(IList<FileInfo> copiedAssemblyFiles)
        //{
        //    var applicationLibraryDefaultAssemblyNames = ApplicationDependencyContext.RuntimeLibraries.SelectMany(a => a.GetDefaultAssemblyNames(ApplicationDependencyContext)).Distinct().ToList();
        //    var simpleDefaultAssemblyNames = applicationLibraryDefaultAssemblyNames.Select(a => a.Name).ToList();

        //    //List<Assembly> candidateAssemblies = new List<Assembly>();
        //    foreach (var copiedAssemblyFile in copiedAssemblyFiles)
        //    {
        //        var assemblyFileName = copiedAssemblyFile.Name;
        //        var simpleAssemblyFileName = System.IO.Path.GetFileNameWithoutExtension(assemblyFileName);
        //        // only load assemblied that aren't 
        //        if (!simpleDefaultAssemblyNames.Contains(simpleAssemblyFileName))
        //        {
        //            var assy = Assembly.ReflectionOnlyLoadFrom(copiedAssemblyFile.FullName);
        //            var depContext = DependencyContext.Load(assy);
        //            if (depContext != null)
        //            {
        //                //  ModuleDependencyContexts.Add(depContext);
        //                ApplicationDependencyContext = ApplicationDependencyContext.Merge(depContext);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Dependencies referencing this set of assemblies are considered candidates for Gluon modules / plugins.
        /// </summary>
        internal static HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Dazinate.Dnn.Manifest"
        };

        ///// <summary>
        ///// Returns all of the libraries that the application references / depends on.
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<Library> GetAllLibraries()
        //{
        //    return ConvertToLibraries(_candidateResolver.GetAll());
        //}

        //private IEnumerable<Library> ConvertToLibraries(IEnumerable<RuntimeLibrary> libs)
        //{
        //    var libraries = new List<Library>();
        //    foreach (var lib in libs)
        //    {
        //        var library = new Library(lib.Type, lib.Name, lib.Version, lib.Hash, lib.Dependencies, lib.Serviceable);
        //        libraries.Add(library);
        //    }

        //    return libraries;
        //}

        //public IEnumerable<Library> GetModuleLibraries()
        //{
        //    return ConvertToLibraries(_candidateResolver.GetCandidates());
        //}

        //private class CandidateResolver
        //{
        //    private readonly IDictionary<string, Dependency> _dependencies;

        //    public CandidateResolver(IReadOnlyList<RuntimeLibrary> dependencies, ISet<string> referenceAssemblies)
        //    {
        //        _dependencies = dependencies
        //            .ToDictionary(d => d.Name, d => CreateDependency(d, referenceAssemblies), StringComparer.OrdinalIgnoreCase);
        //    }

        //    private Dependency CreateDependency(RuntimeLibrary library, ISet<string> referenceAssemblies)
        //    {
        //        var classification = DependencyClassification.Unknown;
        //        if (referenceAssemblies.Contains(library.Name))
        //        {
        //            classification = DependencyClassification.GluonReference;
        //        }

        //        return new Dependency(library, classification);
        //    }

        //    private DependencyClassification ComputeClassification(string dependency)
        //    {
        //        Debug.Assert(_dependencies.ContainsKey(dependency));

        //        var candidateEntry = _dependencies[dependency];
        //        if (candidateEntry.Classification != DependencyClassification.Unknown)
        //        {
        //            return candidateEntry.Classification;
        //        }
        //        else
        //        {
        //            var classification = DependencyClassification.NotCandidate;
        //            //  candidateEntry.Library.
        //            foreach (var candidateDependency in candidateEntry.Library.Dependencies)
        //            {
        //                var dependencyClassification = ComputeClassification(candidateDependency.Name);
        //                if (dependencyClassification == DependencyClassification.Candidate ||
        //                    dependencyClassification == DependencyClassification.GluonReference)
        //                {
        //                    classification = DependencyClassification.Candidate;
        //                    break;
        //                }
        //            }

        //            candidateEntry.Classification = classification;

        //            return classification;
        //        }
        //    }

        //    public IEnumerable<RuntimeLibrary> GetCandidates()
        //    {
        //        foreach (var dependency in _dependencies)
        //        {
        //            if (ComputeClassification(dependency.Key) == DependencyClassification.Candidate)
        //            {
        //                yield return dependency.Value.Library;
        //            }
        //        }
        //    }

        //    public IEnumerable<RuntimeLibrary> GetAll()
        //    {
        //        foreach (var dependency in _dependencies)
        //        {
        //            ComputeClassification(dependency.Key);
        //            yield return dependency.Value.Library;
        //        }
        //    }

        //    private class Dependency
        //    {
        //        public Dependency(RuntimeLibrary library, DependencyClassification classification)
        //        {
        //            Library = library;
        //            Classification = classification;
        //        }

        //        public RuntimeLibrary Library { get; }

        //        public DependencyClassification Classification { get; set; }

        //        public override string ToString()
        //        {
        //            return $"Library: {Library.Name}, Classification: {Classification}";
        //        }
        //    }

        //    private enum DependencyClassification
        //    {
        //        Unknown = 0,
        //        Candidate = 1,
        //        NotCandidate = 2,
        //        GluonReference = 3
        //    }
        //}

        public Assembly LoadAssembly(AssemblyName name)
        {

            string assyName;
            string symbolsName;

            if (name.Name == ApplicationEntryAssembly.GetName().Name)
            {
                // main assembly is an exe
                assyName = name.Name + ".exe";
                return Assembly.Load(name);
            }

            // Look for the assembly in the probing paths.
            //var probingDirectories = GluonEnvironment.ComponentConfig.AssemblyProbingFolders;

            assyName = name.Name + ".dll";
            symbolsName = name.Name + ".pdb";

            // var fileProvider = GluonEnvironment.HostingEnvironment.ContentRootFileProvider;
            if (File.Exists(Path.Combine(BinDirectory, assyName)))
            {
                return Assembly.Load(name);
            }

            //var assyFileInfo = contentRootFileProvider.GetFileInfo(assyName);
            //if (assyFileInfo.Exists)
            //{
            //    return Assembly.Load(name);
            //}

            foreach (var probingDir in ProbingPaths)
            {
                var folderSubPath = probingDir;
                if (string.IsNullOrWhiteSpace(probingDir))
                {
                    // load direct from content path.

                }
                else
                {
                    if (!probingDir.EndsWith("\\"))
                    {
                        folderSubPath = probingDir + "\\";
                    }
                }

                string assyPath = Path.Combine(BinDirectory,folderSubPath, assyName); //$"{folderSubPath}{assyName}";
                var symbolsPath = Path.Combine(BinDirectory,folderSubPath, symbolsName); // $"{folderSubPath}{symbolsName}";

                if (!File.Exists(assyPath))
                {
                    continue;
                   
                }

                return Assembly.LoadFile(assyPath);

                //var fileInfo = contentRootFileProvider.GetFileInfo(assyPath);
                //if (!fileInfo.Exists)
                //{
                //    continue;
                //}


                //if (!string.IsNullOrWhiteSpace(fileInfo.PhysicalPath))
                //{
                //    // load from phsyical path
                //    var assy = Assembly.LoadFile(fileInfo.PhysicalPath);
                //    return assy;
                //}

               

            }

            // string probingDirectoryList = string.Join(",", probingDirectories.ToArray());

            // var contentRootDir = GluonEnvironment.HostingEnvironment.ContentRootPath;

            string probingDirectoryList = string.Join(",", ProbingPaths.ToArray());

            var contentRootDir = BinDirectory;

            throw new ArgumentException(
                $"Could not locate assembly: {name.Name}. Probed directories: {probingDirectoryList}. Content Root Directory is: {contentRootDir}");





        }

        //public bool IsReferencedAssembly(AssemblyName name)
        //{
        //    var applicationLibraryDefaultAssemblyNames = OriginalApplicationDependencyContext.RuntimeLibraries.SelectMany(a => a.GetDefaultAssemblyNames(ApplicationDependencyContext)).Distinct().ToList();
        //    var isApplicationDependency = applicationLibraryDefaultAssemblyNames.Any(a => a.Name == name.Name);
        //    return isApplicationDependency;
        //}
    }
#endif

}
