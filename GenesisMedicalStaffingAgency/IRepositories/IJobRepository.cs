using GenesisMedicalStaffingAgency.Models;
using System.Collections.Generic;

namespace GenesisMedicalStaffingAgency.IRepositories
{
    public interface IJobRepository
    {
        List<Jobs> GetJobs();
        int AppliedJob(ApplyJobs applyJobs);
        int AddJobs(Jobs jobs);
        List<ApplyJobs> GetAppliedJobs();
        bool FindAdmin(string Email);
        void DeleteJobs(int Id);
        int EditJob(Jobs jobs);
        void DeleteJob(int? Id);
        Jobs GetSingleJob(int? Id);
        List<ApplyJobs> GetAllApplyJobs();
    }
}
