using Microsoft.EntityFrameworkCore;
using PigmaAPI.Common.Enums;
using PigmaAPI.Entities;
using PigmaAPI.Infrastructure.ApplicationDbContext;
using PigmaAPI.Services.CompanyContacts.Services.Contracts;

namespace PigmaAPI.Services.CompanyContacts.Services;

public class CompanyContactService : ICompanyContactService
{
    private readonly ApplicationDbContext Context;

    public CompanyContactService(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<ActionStatus> Create(CompanyContact contact)
    {

        if (contact != null && Context!=null)
        {
            contact.DateCreated = DateTime.Now;
            //Context.CompanyAgencies.Add(contact.CompanyAgencyNavigation);
            Context.CompanyContacts.Add(contact);
            Context.SaveChanges();
            return ActionStatus.Success;

        }
        return ActionStatus.Failed;

    }

   
    public async Task<ActionStatus> DeleteById(int id)
    {
        if (Context != null)
        {
            var _companyContact = Context.CompanyContacts;
            var _companyContactToDelete = _companyContact.FirstOrDefault(cc=>cc.Id==id);
            if (_companyContactToDelete != null)
            {
                Context.Remove(_companyContactToDelete);
                Context.Entry(_companyContactToDelete).State = EntityState.Deleted;
                await Context.SaveChangesAsync();
                return ActionStatus.Success;

            }
            return ActionStatus.NotFound;

        }
        return ActionStatus.Failed;

    }

    public async Task<List<CompanyContact>> GetAll()
    {
        return await Context.CompanyContacts.AsNoTracking().ToListAsync();
    }

    public async Task<CompanyContact> GetById(int id)
    {
        var _companyContact = Context.CompanyContacts.AsNoTracking();
        var _companyContactFromDB =await _companyContact.FirstOrDefaultAsync(cc => cc.Id == id);
        if (_companyContactFromDB != null)
        {
            return _companyContactFromDB;
        }
        return null;
    }

    public async Task<ActionStatus> Update(CompanyContact contact)
    {
        var result = await Context.CompanyContacts.AsNoTracking().FirstOrDefaultAsync(cc => cc.Id == contact.Id);
        if (result != null)
        {
            result = contact;
            var entry = Context.Update(result);
            await Context.SaveChangesAsync();
            return ActionStatus.Success;
        }
        
        return ActionStatus.NotFound;
    }
    public async Task<ActionStatus> DeleteAll(List<CompanyContact> companyContactsList)
    {
        foreach (var companyContact in companyContactsList)
        {
            await DeleteById(companyContact.Id);
        }

        return ActionStatus.Success;
    }

}
