using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        public int DeliveryMethod { get; set; }
        public AddressDto ShoppingAddress { get; set; }


    }
}


