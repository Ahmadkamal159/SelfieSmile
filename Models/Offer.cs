using System.ComponentModel.DataAnnotations;

namespace SelfieSmile.Models
{
    public class Offer
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        public string PromoCode { get; set; }
    }
}
