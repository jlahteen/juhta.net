
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Juhta.Net.Common;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Implements an event stream whose destination is one or more mailboxes.
    /// </summary>
    internal class EmailStream : EventStream, IEventStream
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="emailStreamNode">Specifies an eventStream XML node based on which the instance will be
        /// initialized.</param>
        public EmailStream(XmlNode emailStreamNode) : base(emailStreamNode)
        {
            XmlSchema configSchema = DiagnosticsLibrary.Instance.GetConfigSchema();

            m_toRecipients = emailStreamNode.GetAttribute("toRecipients");

            if (emailStreamNode.HasAttribute("ccRecipients"))
                m_ccRecipients = emailStreamNode.GetAttribute("ccRecipients");

            m_smtpHost = emailStreamNode.GetAttribute("smtpHost");

            if (emailStreamNode.HasAttribute("smtpPort"))
                m_smtpPort = Convert.ToInt32(emailStreamNode.GetAttribute("smtpPort"));
            else
                m_smtpPort = Convert.ToInt32(configSchema.GetAttributeDefaultValue("emailStreamType", "smtpPort"));

            if (emailStreamNode.HasAttribute("smtpUsername"))
            {
                m_smtpUsername = emailStreamNode.GetAttribute("smtpUsername");

                m_smtpPassword = emailStreamNode.GetAttribute("smtpPassword");
            }

            if (emailStreamNode.HasAttribute("smtpUseSsl"))
                m_smtpUseSsl = emailStreamNode.GetAttribute("smtpUseSsl").ToBoolean();
            else
                m_smtpUseSsl = configSchema.GetAttributeDefaultValue("emailStreamType", "smtpUseSsl").ToBoolean();

            m_eventFormatter = new HtmlPageFormatter();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IEventStream.Close"/>.
        /// </summary>
        /// <remarks>This method does nothing for this type of stream.</remarks>
        public void Close()
        {}

        /// <summary>
        /// See <see cref="IEventStream.Open"/>.
        /// </summary>
        /// <remarks>This method does nothing for this type of stream.</remarks>
        public void Open()
        {}

        /// <summary>
        /// See <see cref="IEventStream.WriteEvent"/>.
        /// </summary>
        public void WriteEvent(Event @event)
        {
            MailMessage email = new MailMessage();
            string subject;
            SmtpClient smtpClient = new SmtpClient(m_smtpHost, m_smtpPort);

            // Set the email properties

            // From
            email.From = new MailAddress(ProductInfo.Name.Replace(" ", "") + "@" + @event.MachineName.ToLower());

            // To
            email.To.Add(m_toRecipients.Replace(';', ','));

            // CC
            if (m_ccRecipients != null)
                email.CC.Add(m_ccRecipients.Replace(';', ','));

            // Subject

            if (@event.Message.Length > c_maxMessageExcerptLength)
                subject = @event.Message.Substring(0, c_maxMessageExcerptLength) + "...";
            else
                subject = @event.Message;

            subject = subject.Replace(Environment.NewLine, " ");

            subject = String.Format("{0} Event at {1}: {2}", @event.Type, @event.MachineName, subject);

            email.Subject = subject;

            // Message

            email.Body = m_eventFormatter.FormatEvent(@event);

            if (email.Body.StartsWith("<html", StringComparison.OrdinalIgnoreCase))
                email.IsBodyHtml = true;

            // Set the SMTP client properties

            smtpClient.EnableSsl = m_smtpUseSsl;

            if (m_smtpUsername != null)
                smtpClient.Credentials = new NetworkCredential(m_smtpUsername, m_smtpPassword);

            // Send the email
            smtpClient.Send(email);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="IEventStream.Uri"/>.
        /// </summary>
        public string Uri
        {
            get
            {
                string uri;

                if (String.IsNullOrEmpty(m_ccRecipients))
                    uri = String.Format("mailto://{0}", m_toRecipients);
                else
                    uri = String.Format("mailto://{0}?cc={1}", m_toRecipients, m_ccRecipients);

                return(System.Uri.EscapeUriString(uri));
            }
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the maximum length for an excerpt to be copied from the beginning of an event message to an email
        /// subject.
        /// </summary>
        private const int c_maxMessageExcerptLength = 80;

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the comma separated list of CC (carbon copy) recipients.
        /// </summary>
        private string m_ccRecipients;

        /// <summary>
        /// Specifies an IEventFormatter object that is used to format events.
        /// </summary>
        private IEventFormatter m_eventFormatter;

        /// <summary>
        /// Specifies the name or IP address of the SMTP server host.
        /// </summary>
        private string m_smtpHost;

        /// <summary>
        /// Specifies the password used for the SMTP server authentication.
        /// </summary>
        private string m_smtpPassword;

        /// <summary>
        /// Specifies the port for the SMTP server communication.
        /// </summary>
        private int m_smtpPort;

        /// <summary>
        /// Specifies the username used for the SMTP server authentication.
        /// </summary>
        private string m_smtpUsername;

        /// <summary>
        /// Specifies whether Secure Sockets Layer (SSL) must be used to access the SMTP server.
        /// </summary>
        private bool m_smtpUseSsl;

        /// <summary>
        /// Specifies the comma separated list of To recipients.
        /// </summary>
        private string m_toRecipients;

        #endregion
    }
}
