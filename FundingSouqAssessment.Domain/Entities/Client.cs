using FundingSouqAssessment.Domain.Entities;
using FundingSouqAssessment.Domain.Enums;

public class Client : BaseEntity<long>
{ 
    public string Email { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string PersonalId { get; set; }  
    public string ProfilePhotoUrl { get; set; } 
    public string MobileNumber { get; set; } 
    public Gender Gender { get; set; }   
    public ICollection<Address> Addresses { get; set; } = new List<Address>(); 
    public ICollection<Account> Accounts { get; set; } = new List<Account>(); 
}