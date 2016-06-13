using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using ReleaseTracker.Core.Interfaces;
using ReleaseTracker.Core.Models;
using RestSharp;
using RestSharp.Authenticators;


namespace ReleaseTracker.DataAccess.Services
{
    public class ReleaseService : IReleaseService
    {
        private IReleaseRepository _Repository;
        private IReleaseFactory _Factory;

        public ReleaseService(IReleaseRepository RepositoryIn, IReleaseFactory FactoryIn)
        {
            _Repository = RepositoryIn;
            _Factory = FactoryIn;
        }


        public IEnumerable<Release> GetReleases(int WorkItemId)
        {
            var Releases = new List<Release>();
            var ReleaseIds = _Repository.GetReleaseIds(WorkItemId);

            foreach (var Id in ReleaseIds)
            {
                Releases.Add(_Factory.BuildRelease(GetReleaseDetailsFromApi(Id)));
            }

            return Releases;
        }

        private string GetReleaseDetailsFromApi(int WorkItemId)
        {
            try
            {
                var Url = new Uri(ConfigurationManager.AppSettings["TfsUrlAndCollectionName"]);
                var Resource = "_apis/Release/releases/" + WorkItemId;

                HttpWebRequest Request = (HttpWebRequest) WebRequest.Create(Url + Resource);
                Request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkUser"],
                    ConfigurationManager.AppSettings["NetworkPassword"],
                    ConfigurationManager.AppSettings["NetworkProxyDomain"]);
                Request.PreAuthenticate = true;

                HttpWebResponse Response = (HttpWebResponse) Request.GetResponse();
                Stream ResponseStream = Response.GetResponseStream();

                return ConvertToJsonResponse(ResponseStream);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string ConvertToJsonResponse(Stream ResponseIn)
        {
            var Reader = new StreamReader(ResponseIn);
            var Json = Reader.ReadToEnd();
            Reader.Dispose();
            return Json;
        }
    }

}