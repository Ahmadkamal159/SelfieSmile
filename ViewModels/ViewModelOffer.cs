using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace SelfieSmile.ViewModels
{
    public class ViewModelOffer
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; } = new(2023,1,16);
        [Required]
        public string PromoCode { get; set; }

    }
}
