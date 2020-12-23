using System.Collections.Generic;
using CarShop.ViewModels.Issues;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CarShop.Services
{
    public interface IIssueService
    {
        GetIssuesViewModel GetIssues(string carId, string userId);

        void Create(string carId, string description);

        void Delete(string issueId);

        void Fix(string issueId);
    }
}
