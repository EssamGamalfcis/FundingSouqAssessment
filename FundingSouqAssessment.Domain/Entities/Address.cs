namespace FundingSouqAssessment.Domain.Entities
{
    public class Address : BaseEntity<long>
    { 
        public string Country { get; set; } 
        public string City { get; set; } 
        public string Street { get; set; }  
        public string ZipCode { get; set; }
        public long ClientId { get; set; }
        public Client Client { get; set; }
    }
}
