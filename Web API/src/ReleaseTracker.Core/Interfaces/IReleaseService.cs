using System;
using System.Collections;
using System.Collections.Generic;
using ReleaseTracker.Core.Models;

namespace ReleaseTracker.Core.Interfaces
{
    public interface IReleaseService
    {
        IEnumerable<Release> GetReleases(int WorkItemId);
        
    }
}