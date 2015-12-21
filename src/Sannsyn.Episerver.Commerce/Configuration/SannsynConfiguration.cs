﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer;
using EPiServer.ServiceLocation;

namespace Sannsyn.Episerver.Commerce.Configuration
{
    [ServiceConfiguration(typeof(SannsynConfiguration))]
    public class SannsynConfiguration
    {
        private readonly DbConnectionStringBuilder _builder;
        private Uri _serviceUrl = null;
        private bool _logSendData;

        public SannsynConfiguration()
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

    }
}