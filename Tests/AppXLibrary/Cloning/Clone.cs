
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

        public static void BuildCopies(int count, string embeddedSourceFileNamespace)
        {
            for (int i = 1; i <= count; i++)
                BuildCopy(i, embeddedSourceFileNamespace);
        }

        #endregion

        #region Private Methods

        private static void BuildCopy(int copy, string embeddedSourceFileNamespace)
        {
            CompilerParameters compilerParameters = new CompilerParameters(GetReferenceAssemblies());
            string sourceCode;
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");
            CompilerResults compilerResults;
            StringBuilder compileErrors = new StringBuilder();

            compilerParameters.TreatWarningsAsErrors = true;

            compilerParameters.IncludeDebugInformation = false;

            compilerParameters.GenerateInMemory = false;

            compilerParameters.OutputAssembly = String.Format("AppXLibrary{0}.dll", copy);

            sourceCode = Assembly.GetExecutingAssembly().LoadEmbeddedResourceFile("Source.txt", embeddedSourceFileNamespace);

            sourceCode = sourceCode.Replace("XXX", copy.ToString());

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

            referenceAssemblies.Add("netstandard.dll");

            referenceAssemblies.Add(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);

            return(referenceAssemblies.ToArray());
        }

        #endregion
    }
}
