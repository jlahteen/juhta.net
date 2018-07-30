
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// A static class that contains extension methods for the <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the first attribute of a specified type from the current assembly.
        /// </summary>
        /// <typeparam name="TAttribute">Specifies an attribute type.</typeparam>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the first attribute of the specified type, or null, if no attributes of the specified type
        /// were found.</returns>
        private static TAttribute GetAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(TAttribute), false);

            if (attributes == null)
                return(null);

            else if (attributes.Length == 0)
                return(null);

            else
                return((TAttribute)attributes[0]);
        }

        /// <summary>
        /// Gets the attributes of a specified type from the current assembly.
        /// </summary>
        /// <typeparam name="TAttribute">Specifies an attribute type.</typeparam>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the attributes of the specified type, or null, if no attributes of the specified type were
        /// found.</returns>
        private static TAttribute[] GetAttributes<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(TAttribute), false);

            if (attributes == null)
                return(null);

            else if (attributes.Length == 0)
                return(null);

            else
                return(Array.ConvertAll<object, TAttribute>(attributes, x => (TAttribute)x));
        }

        /// <summary>
        /// Gets the value of the current assembly's Company attribute.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the value of the Company attribute, or null, if the attibute was not found.</returns>
        public static string GetCompany(this Assembly assembly)
        {
            AssemblyCompanyAttribute companyAttribute = GetAttribute<AssemblyCompanyAttribute>(assembly);

            if (companyAttribute != null)
                return(companyAttribute.Company);
            else
                return(null);
        }

        /// <summary>
        /// Gets the value of the current assembly's Copyright attribute.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the value of the Copyright attribute, or null, if the attibute was not found.</returns>
        public static string GetCopyright(this Assembly assembly)
        {
            AssemblyCopyrightAttribute copyrightAttribute = GetAttribute<AssemblyCopyrightAttribute>(assembly);

            if (copyrightAttribute != null)
                return(copyrightAttribute.Copyright);
            else
                return(null);
        }

        /// <summary>
        /// Gets the value of the current assembly's Description attribute.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the value of the Description attribute, or null, if the attibute was not found.</returns>
        public static string GetDescription(this Assembly assembly)
        {
            AssemblyDescriptionAttribute descriptionAttribute = GetAttribute<AssemblyDescriptionAttribute>(assembly);

            if (descriptionAttribute != null)
                return(descriptionAttribute.Description);
            else
                return(null);
        }

        /// <summary>
        /// Gets the directory of the current assembly.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the directory of the current assembly without an ending directory separator.</returns>
        public static string GetDirectory(this Assembly assembly)
        {
            string directory;

            directory = Path.GetDirectoryName(assembly.ManifestModule.FullyQualifiedName);

            return(directory.TrimEnd(Path.DirectorySeparatorChar));
        }

        /// <summary>
        /// Gets the file name of the current assembly.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the file name of the current assembly.</returns>
        public static string GetFileName(this Assembly assembly)
        {
            return(Path.GetFileName(assembly.ManifestModule.FullyQualifiedName));
        }

        /// <summary>
        /// Gets the file name of the current assembly without the extension.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the file name of the current assembly without the extension.</returns>
        public static string GetFileNameWithoutExtension(this Assembly assembly)
        {
            return(Path.GetFileNameWithoutExtension(assembly.ManifestModule.FullyQualifiedName));
        }

        /// <summary>
        /// Gets the value of the current assembly's Product attribute.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the value of the Product attribute, or null, if the attibute was not found.</returns>
        public static string GetProduct(this Assembly assembly)
        {
            AssemblyProductAttribute productAttribute = GetAttribute<AssemblyProductAttribute>(assembly);

            if (productAttribute != null)
                return(productAttribute.Product);
            else
                return(null);
        }

        /// <summary>
        /// Gets the product version associated with the current assembly.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the product version associated with the current assembly.</returns>
        public static string GetProductVersion(this Assembly assembly)
        {
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return(fileVersionInfo.ProductVersion);
        }

        /// <summary>
        /// Gets the value of the current assembly's Title attribute.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the value of the Title attribute, or null, if the attibute was not found.</returns>
        public static string GetTitle(this Assembly assembly)
        {
            AssemblyTitleAttribute titleAttribute = GetAttribute<AssemblyTitleAttribute>(assembly);

            if (titleAttribute != null)
                return(titleAttribute.Title);
            else
                return(null);
        }

        /// <summary>
        /// Gets the value of the current assembly's Version attribute.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <returns>Returns the value of the Version attribute, or null, if the attibute was not found.</returns>
        public static string GetVersion(this Assembly assembly)
        {
            Version version;

            version = assembly.GetName().Version;

            if (version != null)
                return(version.ToString());
            else
                return(null);
        }

        /// <summary>
        /// Loads an embedded resource file from the current assembly.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <param name="fileName">Specifies the name of a resource file to be loaded.</param>
        /// <returns>Returns the contents of the requested resource file.</returns>
        /// <remarks>This method loads the first resource file whose name matches the specified file name regardless of
        /// the file namespace.</remarks>
        public static string LoadEmbeddedResourceFile(this Assembly assembly, string fileName)
        {
            return(assembly.LoadEmbeddedResourceFile(fileName, null));
        }

        /// <summary>
        /// Loads an embedded resource file from the current assembly.
        /// </summary>
        /// <param name="assembly">Specifies the current assembly.</param>
        /// <param name="fileName">Specifies the name of a resource file to be loaded.</param>
        /// <param name="fileNamespace">Specifies the namespace of a resource file to be loaded. Can be null in which
        /// case the method loads the first resource file whose name matches the specified file name.</param>
        /// <returns>Returns the contents of the requested resource file.</returns>
        public static string LoadEmbeddedResourceFile(this Assembly assembly, string fileName, string fileNamespace)
        {
            string[] resourceNames;
            string fullFileName = null;
            StreamReader reader;
            String fileContents;

            // Initialize the full file name

            if (fileNamespace == null)
            {
                resourceNames = assembly.GetManifestResourceNames();

                foreach (string s in resourceNames)
                    if (s.EndsWith("." + fileName))
                    {
                        fullFileName = s;

                        break;
                    }
            }
            else
                fullFileName = fileNamespace + "." + fileName;

            // Read the contents of the file

            reader = new StreamReader(assembly.GetManifestResourceStream(fullFileName));

            fileContents = reader.ReadToEnd();

            reader.Close();

            return(fileContents);
        }

        #endregion
    }
}
