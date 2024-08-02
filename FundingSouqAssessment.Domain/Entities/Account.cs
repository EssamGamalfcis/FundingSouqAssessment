using FundingSouqAssessment.Domain.Entities;

public class Account : BaseEntity<long>
{ 
    public string AccountNumber { get; set; }     
    public decimal Balance { get; set; } 
    public Client Client { get; set; } 
    public long ClientId { get; set; }
}