
////
//// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
////
//// This source code may be used, modified and distributed under the terms of
//// the MIT license. Please refer to the LICENSE.txt file for details.
////

//namespace Juhta.Net.Console
//{
//    /// <summary>
//    /// Defines a class that represents a command line option. At the command line level, command line options consist
//    /// of a prefix, name and an optional value, which is separated by a name-value separator from the name part.
//    /// </summary>
//    public class CommandLineOption : CommandLineArg
//    {
//        #region Public Properties

//        /// <summary>
//        /// Gets the name of the command line option specified by this CommandLineOption object.
//        /// </summary>
//        public string Name
//        {
//            get {return(m_name);}
//        }

//        #endregion

//        #region Internal Constructors

//        /// <summary>
//        /// Initializes a new instance.
//        /// </summary>
//        /// <param name="rawArg">Species a value for the <see cref="CommandLineArg.RawArg"/> property.</param>
//        /// <param name="name">Species a value for the <see cref="Name"/> property.</param>
//        /// <param name="value">Species a value for the <see cref="CommandLineArg.Value"/> property.</param>
//        internal CommandLineOption(string rawArg, string name, string value) : base(rawArg, value)
//        {
//            m_name = name;
//        }

//        #endregion

//        #region Private Fields

//        /// <summary>
//        /// Stores the <see cref="Name"/> property.
//        /// </summary>
//        private string m_name;

//        #endregion
//    }
//}
