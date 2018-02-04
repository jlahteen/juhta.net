
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Helpers;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines an identifier class for dependency injection services. A service identifier consists of two parts,
    /// scheme and specifier, which are separated by a slash. Type binded services can be defined by the 'type' scheme
    /// and name binded by the 'name' scheme.
    /// </summary>
    public class ServiceId
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serviceId">Specifies a service identifier as a string.</param>
        public ServiceId(string serviceId)
        {
            string[] parts;

            ArgumentHelper.CheckValue(nameof(serviceId), serviceId, c_regexServiceId);

            parts = serviceId.Split(c_schemeSpecifierSeparator);

            m_scheme = parts[0];

            m_specifier = parts[1];

            m_value = m_scheme + c_schemeSpecifierSeparator + m_specifier;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="scheme">Specifies a service identifier scheme.</param>
        /// <param name="specifier">Specifies a service identifier specifier.</param>
        public ServiceId(string scheme, string specifier)
        {
            ArgumentHelper.CheckValue(nameof(scheme), scheme, c_regexScheme);

            ArgumentHelper.CheckValue(nameof(specifier), specifier, c_regexSpecifier);

            m_scheme = scheme;

            m_specifier = specifier;

            m_value = m_scheme + c_schemeSpecifierSeparator + m_specifier;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the scheme part of the service identifier.
        /// </summary>
        public string Scheme
        {
            get {return(m_scheme);}
        }

        /// <summary>
        /// Gets the specifier part of the service identifier.
        /// </summary>
        public string Specifier
        {
            get {return(m_specifier);}
        }

        /// <summary>
        /// Gets the service identifier as a string.
        /// </summary>
        public string Value
        {
            get {return(m_value);}
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Defines the regex for validating service ID schemes.
        /// </summary>
        private const string c_regexScheme = @"^([a-zA-Z0-9])+$";

        /// <summary>
        /// Defines the regex for validating service IDs.
        /// </summary>
        private const string c_regexServiceId = @"^([a-zA-Z0-9])+/([a-zA-Z0-9\.:/_-])+$";

        /// <summary>
        /// Defines the regex for validating service ID specifiers.
        /// </summary>
        private const string c_regexSpecifier = @"^([a-zA-Z0-9\.:/_-])+$";

        /// <summary>
        /// Defines the separator character for the scheme and specifier parts in a service identifier.
        /// </summary>
        private const char c_schemeSpecifierSeparator = '/';

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Scheme"/> property.
        /// </summary>
        private string m_scheme;

        /// <summary>
        /// Stores the <see cref="Specifier"/> property.
        /// </summary>
        private string m_specifier;

        /// <summary>
        /// Stores the <see cref="Value"/> property.
        /// </summary>
        private string m_value;

        #endregion
    }
}
