using ReleaseTracker.Core.Models;

namespace ReleaseTracker.Core.Interfaces
{
    public interface IReleaseFactory
    {
        Release BuildRelease(string ReleaseDetails);
    }
}