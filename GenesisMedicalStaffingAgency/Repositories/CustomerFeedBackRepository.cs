using Dapper;
using GenesisMedicalStaffingAgency.IRepositories;
using GenesisMedicalStaffingAgency.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GenesisMedicalStaffingAgency.Repositories
{
    public class CustomerFeedBackRepository : ICustomerFeedBackRepository
    {
        private readonly IDbConnection conn;
        public CustomerFeedBackRepository(IDbConnection dbConnection)
        {
            conn = dbConnection;
        }
        public List<ClientFeedBack> GetClientFeedBacks()
        {
            Serilog.Log.Information("GetClientFeedBacks Method Called");
            try
            {
                string query = "Select * from ClientFeedBack;";
                var _parameter = new Dictionary<string, object>();
                List<ClientFeedBack> result = conn.Query<ClientFeedBack>(query, param: _parameter, commandType: CommandType.Text).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
