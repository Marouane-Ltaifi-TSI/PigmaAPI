namespace PigmaAPI.Entities
{
    public class CompanyAgency
    {
        public int Id { get; set; }
        public string? Adress { get; set; } = null;
        public string? AdditionalAddress { get; set; } = null;
        public int? PostalCode { get; set; } = null;
        public int? VilleID { get; set; } = null;
        public string? MeansOfPayment { get; set; } = null;
        public string? Iban { get; set; } = null;
        public string? Bic { get; set; } = null;
        public virtual ICollection<CompanyContact>? CompanyContact { get; set; }=null;
    }
}
