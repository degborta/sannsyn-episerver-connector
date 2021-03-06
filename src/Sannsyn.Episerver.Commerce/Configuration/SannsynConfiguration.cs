﻿using System;
using System.Configuration;
using System.Data.Common;
using EPiServer.ServiceLocation;

namespace Sannsyn.Episerver.Commerce.Configuration
{
    [ServiceConfiguration(typeof(SannsynConfiguration))]
    public class SannsynConfiguration
    {
        private readonly DbConnectionStringBuilder _builder;
        private Uri _serviceUrl = null;
        private bool _logSendData = false;
        private bool _moduleDisabled = false;
        private string _configuration = "episerver";
        private int _scriptTimeout = 1500;

        public SannsynConfiguration()
        {
            var moduleDisabled = ConfigurationManager.AppSettings["Sannsyn:DisableModule"];
            bool.TryParse(moduleDisabled, out _moduleDisabled);

            // if module is disabled, we won't attempt the connection string
            if(_moduleDisabled == false)
            {
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SannsynConnection"];
                if (connectionString == null)
                {
                    throw new ConfigurationErrorsException("Missing Sannsyn connection string" );
                }

                _builder = new DbConnectionStringBuilder(false);
                _builder.ConnectionString = connectionString.ConnectionString;

                string url = _builder["Service Url"].ToString();
                if(string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException("Missing service url in Sannsyn connection string");
                }

                if (url.EndsWith("/") == false)
                    url = url + "/";

                _serviceUrl = new Uri(url);

                if (_builder.ContainsKey("Configuration"))
                {
                    _configuration = _builder["Configuration"].ToString();
                }

                var scriptTimeoutString = ConfigurationManager.AppSettings["Sannsyn:ScriptTimeout"];
                if(string.IsNullOrEmpty(scriptTimeoutString) == false)
                {
                    int.TryParse(scriptTimeoutString, out _scriptTimeout);
                }

            }
            var sendDataFlag = ConfigurationManager.AppSettings["Sannsyn:LogSendData"];
            bool.TryParse(sendDataFlag, out _logSendData);

        }

        public bool LogSendData
        {
            get { return _logSendData; }
            set { _logSendData = value; }
        }

        public Uri ServiceUrl {
            get { return _serviceUrl; } 
        }
        public string Service { get { return _builder["Service"].ToString(); } }
        public string Username { get { return _builder["User Name"].ToString(); } }
        public string Password { get { return _builder["Password"].ToString(); } }
        public string Configuration { get { return _configuration; } }

        public bool ModuleEnabled
        {
            get { return !_moduleDisabled; }
            private set { _moduleDisabled = !value; }
        }

        /// <summary>
        /// The time in milliseconds to wait for the Crec script to load
        /// from the Sannsyn servers.
        /// </summary>
        public int ScriptTimeout
        {
            get { return _scriptTimeout; }
            set { _scriptTimeout = value; }
        }
    }
}
