using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models
{
    [Table("Orders")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            CustomerID = string.Empty;
            ShipName = string.Empty;
            ShipAddress = string.Empty;
            ShipCity = string.Empty;
            ShipRegion = string.Empty;
            ShipPostalCode = string.Empty;
            ShipCountry = string.Empty;
            Comments = string.Empty;
        }

        [Key]
        public int OrderID { get; set; }

        [Required]
        [StringLength(5)]
        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public string? Comments { get; set; }
        public bool IsPending { get; set; } = true;

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}