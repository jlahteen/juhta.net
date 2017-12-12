
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a class that can be used to write object graphs into trace elements. In addition to referenced objects,
    /// object graphs will also contain the values of all public fields and properties for each single object in a
    /// graph. However, if an object implements the <see cref="IEnumerable"/> interface, it will be used to graph an
    /// object instead of the public fields and properties.
    /// </summary>
    internal class ObjectGraphWriter
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="traceElement">Specifies a TraceElement object where object graphs will be written.</param>
        public ObjectGraphWriter(TraceElement traceElement)
        {
            m_traceElement = traceElement;

            m_graphObjects = new List<object>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes a graph of a specified object into this TraceElement instance.
        /// </summary>
        /// <param name="objectID">Specifies an object identifier, e.g. a variable or property name.</param>
        /// <param name="object">Specifies an object.</param>
        public void WriteGraph(string objectID, object @object)
        {
            m_graphObjects.Clear();

            DoWriteGraph(objectID, @object);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Does write a graph of a specified object into this TraceElement instance.
        /// </summary>
        /// <param name="objectID">Specifies an object identifier, e.g. a variable or property name.</param>
        /// <param name="object">Specifies an object.</param>
        /// <remarks>This method is recursive.</remarks>
        private void DoWriteGraph(string objectID, object @object)
        {
            Type objectType = null;
            string s;
            IEnumerator enumerator;
            PropertyInfo[] properties;
            BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            Exception propertyValueException;
            object propertyValue = null;
            FieldInfo[] fields;

            if (objectID == null)
                objectID = "<objectID = null>";

            if (@object == null)
                objectType = typeof(object);
            else
                objectType = @object.GetType();

            if (@object == null)

                // The object is null so the graph is simple

                m_traceElement.WriteLine("{0} {1} = null", objectType.FullName, objectID);

            else if (@object is string)
            {
                // The object is a string

                s = @object.ToString();

                s = s.Replace("\n", "\\n");
                s = s.Replace("\r", "\\r");

                m_traceElement.WriteLine("{0} {1} = \"{2}\"", objectType.FullName, objectID, s);
            }
            else if (objectType.IsValueType)

                // The type of the object is a value type

                m_traceElement.WriteLine("{0} {1} = {2}", objectType.FullName, objectID, @object);

            else if (m_graphObjects.Contains(@object))

                // We ran into a circular object chain

                 m_traceElement.WriteLine("{0} {1}: <circular object chain detected: object listed somewhere in the graph>", objectType.FullName, objectID);

            else if (IsEnumerable(@object))
            {
                // The object is enumerable

                m_graphObjects.Add(@object);

                m_traceElement.WriteLine("System.Collections.IEnumerable {0}:", objectID);

                m_traceElement.IncreaseRelativeIndent();

                enumerator = ((IEnumerable)@object).GetEnumerator();

                enumerator.Reset();

                for (int i = 0; enumerator.MoveNext(); i++)
                    DoWriteGraph(objectID + "[" + i.ToString() + "]", enumerator.Current);

                m_traceElement.DecreaseRelativeIndent();

                m_traceElement.WriteLine("End of System.Collections.IEnumerable");
            }
            else
            {
                // The object is not enumerable

                m_graphObjects.Add(@object);

                m_traceElement.WriteLine("{0} {1}:", objectType.FullName, objectID);

                m_traceElement.IncreaseRelativeIndent();

                // List the properties of the object

                properties = objectType.GetProperties(bindingFlags);

                foreach (PropertyInfo property in properties)

                    if (property.GetIndexParameters().Length > 0)
                        m_traceElement.WriteLine("{0} {1}: <indexed property: graph not supported>", property.PropertyType.FullName, property.Name);

                    else
                    {
                        propertyValueException = null;

                        try
                        {
                            propertyValue = property.GetValue(@object, null);
                        }

                        catch (Exception ex)
                        {
                            propertyValueException = ex;
                        }

                        if (propertyValueException == null)
                            DoWriteGraph(property.Name, propertyValue);
                        else
                            m_traceElement.WriteLine("{0} {1}: <failed to get the property value: {2}>", property.PropertyType.FullName, property.Name, propertyValueException.GetType().FullName);
                    }

                // List the fields of the object

                fields = objectType.GetFields(bindingFlags);

                foreach (FieldInfo field in fields)
                    DoWriteGraph(field.Name, field.GetValue(@object));

                m_traceElement.DecreaseRelativeIndent();

                m_traceElement.WriteLine("End of {0}", objectType.FullName);
            }
        }

        /// <summary>
        /// Checks whether a specified object implements the IEnumerable interface.
        /// </summary>
        /// <param name="object">Specifies an object.</param>
        /// <returns>Returns true if the specified object implements the IEnumerable interface and the corresponding
        /// collection contains at least one item, otherwise false.</returns>
        private bool IsEnumerable(object @object)
        {
            IEnumerator enumerator;

            if (!(@object is IEnumerable))
                return(false);

            enumerator = ((IEnumerable)@object).GetEnumerator();

            enumerator.Reset();

            return(enumerator.MoveNext());
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the list of reference type (other than string) objects currently included in the graph. This list
        /// will be used to detect circular object chains.
        /// </summary>
        private List<object> m_graphObjects;

        /// <summary>
        /// Specifies the TraceElement instance where object graphs will be written.
        /// </summary>
        private TraceElement m_traceElement;

        #endregion
    }
}
