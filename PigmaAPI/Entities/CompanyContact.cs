namespace PigmaAPI.Entities;

public class CompanyContact
{
    public int Id { get; set; }
    public int? CompanyAgencyId { get; set; }
    public string FirstName { get; set; }
    public string Address { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? LastUpdated { get; set; }
    public virtual CompanyAgency? CompanyAgencyNavigation { get; set; }=null;
}
