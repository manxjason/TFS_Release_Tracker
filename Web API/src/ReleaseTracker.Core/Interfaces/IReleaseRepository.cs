using System.Collections;
using System.Collections.Generic;
using ReleaseTracker.Core.Models;

namespace ReleaseTracker.Core.Interfaces
{
    public interface IReleaseRepository
    {
        IEnumerable<int> GetReleaseIds(int WorkItemId);
    }
}