using PigmaAPI.Common.Enums;
using PigmaAPI.Entities;

namespace PigmaAPI.Services.CompanyContacts.Services.Contracts;

public interface ICompanyContactService
{
    public Task<CompanyContact> GetById(int id);
    public Task<List<CompanyContact>> GetAll();
    public Task<ActionStatus> DeleteById(int id);
    public Task<ActionStatus> DeleteAll(List<CompanyContact> companyContactsList);
    public Task<ActionStatus> Create(CompanyContact contact);
    public Task<ActionStatus> Update(CompanyContact contact);

}
