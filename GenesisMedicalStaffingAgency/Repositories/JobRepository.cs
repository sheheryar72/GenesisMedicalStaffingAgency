using Dapper;
using GenesisMedicalStaffingAgency.IRepositories;
using GenesisMedicalStaffingAgency.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GenesisMedicalStaffingAgency.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDbConnection conn;
        public JobRepository(IDbConnection dbConnection)
        {
            conn = dbConnection;
        }
        public List<Jobs> GetJobs()
        {
            Serilog.Log.Information("GetJobs Method Called");
            try
            {
                string query = "Select * from Jobs;";
                var _parameter = new Dictionary<string, object>();
                List<Jobs> result = conn.Query<Jobs>(query, param: _parameter, commandType: CommandType.Text).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Jobs GetSingleJob(int? Id)
        {
            Serilog.Log.Information("GetJobs Method Called");
            try
            {
                string query = "Select * from Jobs where Id = @Id;";
                var _parameter = new Dictionary<string, object>();
                _parameter.Add("@Id", Id);
                Jobs result = conn.Query<Jobs>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int EditJob(Jobs jobs)
        {
            try
            {
                string query = "Update Jobs Values(@Title, @Description, @Requirement, @Salary, @Country, @City, @Tags) where Id = @Id";
                var param = new Dictionary<string, object>();
                param.Add("@Id", jobs.Id);
                param.Add("@Title", jobs.Title);
                param.Add("@Description", jobs.Description);
                param.Add("@Requirement", jobs.Requirement);
                param.Add("@Salary", jobs.Salary);
                param.Add("@Country", jobs.Country);
                param.Add("@City", jobs.City);
                param.Add("@Tags", jobs.Tags);
                var result = conn.Query<int>(query, param: param, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public void DeleteJob(int? Id)
        {
            Serilog.Log.Information("GetJobs Method Called");
            try
            {
                string query = "Delete from Jobs where Id = @Id;";
                var _parameter = new Dictionary<string, object>();
                _parameter.Add("@Id", Id);
                conn.Query<int>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
              
            }
        }
        public int AppliedJob(ApplyJobs applyJobs)
        {
            Serilog.Log.Information("GetJobs Method Called");
            try
            {
                string query = "Insert into ApplyJobs Values(@FullName, @Email, @AppliedFor, @Message, @Experience, @CV, @JobId) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";
                var _parameter = new Dictionary<string, object>();
                _parameter.Add("@FullName", applyJobs.FullName);
                _parameter.Add("@Email", applyJobs.Email);
                _parameter.Add("@AppliedFor", applyJobs.AppliedFor);
                _parameter.Add("@Message", applyJobs.Message);
                _parameter.Add("@Experience", applyJobs.Experience);
                _parameter.Add("@CV", applyJobs.CV);
                if (applyJobs.JobId == 0)
                    _parameter.Add("@JobId", null);
                else
                    _parameter.Add("@JobId", applyJobs.JobId);
                var result = conn.Query<int>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int AddJobs(Jobs jobs)
        {
            try
            {
                string query = "Insert into Jobs Values(@Title, @Description, @Requirement, @Salary, @Country, @City, @Tags) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
                var param = new Dictionary<string, object>();
                param.Add("@Title", jobs.Title);
                param.Add("@Description", jobs.Description);
                param.Add("@Requirement", jobs.Requirement);
                param.Add("@Salary", jobs.Salary);
                param.Add("@Country", jobs.Country);
                param.Add("@City", jobs.City);
                param.Add("@Tags", jobs.Tags);
                var result = conn.Query<int>(query, param: param, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<ApplyJobs> GetAppliedJobs()
        {
            Serilog.Log.Information("GetAppliedJobs Method Called");
            try
            {
                string query = "Select * from ApplyJobs;";
                List<ApplyJobs> result = conn.Query<ApplyJobs>(query, commandType: CommandType.Text).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool FindAdmin(string Email)
        {
            Serilog.Log.Information("GetAppliedJobs Method Called");
            try
            {
                string query = "Select Email from Admin where Email = @Email;";
                var param = new Dictionary<string, object>();
                param.Add("@Email", Email);
                string result = conn.Query<string>(query, param: param, commandType: CommandType.Text).FirstOrDefault();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void DeleteJobs(int Id)
        {
            string query = "Delete from Jobs where Id = @Id";
            var param = new Dictionary<string, object>();
            param.Add("@Id", Id);
            conn.Query<int>(query, param: param, commandType: CommandType.Text).FirstOrDefault();
        }
        public List<ApplyJobs> GetAllApplyJobs()
        {
            Serilog.Log.Information("GetJobs Method Called");
            try
            {
                string query = "Select * from ApplyJobs;";
                var _parameter = new Dictionary<string, object>();
                List<ApplyJobs> result = conn.Query<ApplyJobs>(query, param: _parameter, commandType: CommandType.Text).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
