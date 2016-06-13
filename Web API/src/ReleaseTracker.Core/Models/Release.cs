using System;
using Newtonsoft.Json.Linq;

namespace ReleaseTracker.Core.Models
{
    public class Release
    {
        public int Id { get; set; } 
        public string DefinitionName { get; set; }
        public string Name { get; set; }
        public string Stage { get; set; }
        public Uri Url { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public JToken Environments { get; set; }
    }
}