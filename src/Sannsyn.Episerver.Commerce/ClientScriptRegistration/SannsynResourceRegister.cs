﻿using System;
using System.Collections.Generic;
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Security;
using Sannsyn.Episerver.Commerce.Configuration;
using Sannsyn.Episerver.Commerce.Extensions;

namespace Sannsyn.Episerver.Commerce.ClientScriptRegistration
{
    [ClientResourceRegister]
    public class SannsynResourceRegister : IClientResourceRegister
    {

        public void RegisterResources(IRequiredClientResourceList requiredResources, HttpContextBase context)
        {
            PageRouteHelper instance = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            if (instance.Content != null)
            {
                var content = instance.Content;
                if(content is EntryContentBase)
                {

                    var userId = EPiServer.Security.PrincipalInfo.CurrentPrincipal.GetContactId();
                    EntryContentBase entry = content as EntryContentBase;
                    string productCode = entry.Code;
                    List<string> parentCategories = entry.GetParentCategoryCodes(entry.Language.Name);
                   
                    string sannsynClickUrl = GenerateClickUrl(userId, productCode, parentCategories);
                    ClientResources.RequireScript(sannsynClickUrl);
                }
            }
        }

        private string GenerateClickUrl(Guid userId, string productCode, List<string> parentCategories)
        {
            SannsynConfiguration config =
                ServiceLocator.Current.GetInstance<SannsynConfiguration>();

            // http://episerver.sannsyn.com/jsrecapi/1.0/tupleupdate/epicphoto/admin/canon-5d-m3/click/photo/catclick/dslr/catclick

            string serviceUrl = config.ServiceUrl.ToString() + "jsrecapi/1.0/tupleupdate/" + config.Service;
            string clickUrl = string.Format("{0}/{1}/{2}/click", serviceUrl, userId, productCode);
            foreach (string category in parentCategories)
            {
                clickUrl = string.Format("{0}/{1}/catclick", clickUrl,category);
            }
            return clickUrl;
        }
    }
}
