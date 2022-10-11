using GenesisMedicalStaffingAgency.Models;
using System.Collections.Generic;

namespace GenesisMedicalStaffingAgency.IRepositories
{
    public interface ICustomerFeedBackRepository
    {
        List<ClientFeedBack> GetClientFeedBacks();
    }
}
