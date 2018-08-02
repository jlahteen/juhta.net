
using Juhta.Net.Extensions;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AppXLibrary.Cloning
{
    public static class Clone
    {
        #region Public Methods

        public static void BuildCopies(string assemblyFileNameRoot, int count, string embeddedSourceFileNamespace)
        {
            for (int copy = 1; copy <= count; copy++)
                BuildCopy($"{assemblyFileNameRoot}{copy}", copy, embeddedSourceFileNamespace);
        }

        #endregion

        #region Private Methods

        private static void BuildCopy(string assemblyFileName, int copy, string embeddedSourceFileNamespace)
        {
            CompilerParameters compilerParameters = new CompilerParameters(GetReferenceAssemblies());
            string sourceCode;
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");
            CompilerResults compilerResults;
            StringBuilder compileErrors = new StringBuilder();

            compilerParameters.TreatWarningsAsErrors = true;

            compilerParameters.IncludeDebugInformation = false;

            compilerParameters.GenerateInMemory = false;

            compilerParameters.OutputAssembly = String.Format("{0}.dll", assemblyFileName);

            sourceCode = Assembly.GetExecutingAssembly().LoadEmbeddedResourceFile("Source.txt", embeddedSourceFileNamespace);

            sourceCode = sourceCode.Replace("{{AssemblyFileName}}", assemblyFileName);

            sourceCode = sourceCode.Replace("{{Copy}}", copy.ToString());

            compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, sourceCode);

            if (compilerResults.Errors.HasErrors)
            {
                foreach (CompilerError compilerError in compilerResults.Errors)
                    compileErrors.AppendLine(compilerError.ToString());

                throw new Exception(compileErrors.ToString());
            }
        }

        private static string[] GetReferenceAssemblies()
        {
            List<string> referenceAssemblies = new List<string>();

            referenceAssemblies.Add("System.dll");

            referenceAssemblies.Add("System.Core.dll");

            referenceAssemblies.Add("System.Xml.dll");

            referenceAssemblies.Add("Juhta.Net.dll");

            referenceAssemblies.Add("Juhta.Net.LibraryManagement.dll");

            referenceAssemblies.Add("Juhta.Net.Startup.dll");

            referenceAssemblies.Add("netstandard.dll");

            referenceAssemblies.Add(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);

            return(referenceAssemblies.ToArray());
        }

        #endregion
    }
}
