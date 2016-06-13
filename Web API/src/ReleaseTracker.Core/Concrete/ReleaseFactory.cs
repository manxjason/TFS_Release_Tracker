using System;
using Newtonsoft.Json.Linq;
using ReleaseTracker.Core.Interfaces;
using ReleaseTracker.Core.Models;
using System.Configuration;

namespace ReleaseTracker.Core.Concrete
{
    public class ReleaseFactory : IReleaseFactory
    {
        public Release BuildRelease(string ReleaseDetails)
        {
            if (IsValidJson(ReleaseDetails))
            {
                var JObj = JObject.Parse(ReleaseDetails);

                return new Release()
                {
                    Id = JObj["id"].ToObject<int>(),
                    DefinitionName = JObj["releaseDefinition"]["name"].ToObject<string>(),
                    Name = JObj["name"].ToObject<string>(),
                    Created = JObj["createdOn"].ToObject<DateTime>(),
                    CreatedBy = JObj["createdBy"]["displayName"].ToObject<string>(),
                    Environments = JObj["environments"],
                    Url = new Uri(String.Format("{0}{1}{2}&_a=release-summary{3}&releaseId=",
                        ConfigurationManager.AppSettings["TfsUrlAndCollectionName"],
                        ConfigurationManager.AppSettings["ReleaseMgrUrl"],
                        JObj["releaseDefinition"]["id"].ToObject<string>(),
                        JObj["id"].ToObject<int>()))
                };
            }
            return null;
        }

        private bool IsValidJson(string JsonIn)
        {
            try
            {
                var JObj = JObject.Parse(JsonIn);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}