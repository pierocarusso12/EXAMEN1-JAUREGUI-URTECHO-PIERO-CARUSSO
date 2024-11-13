using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models
{
    [Table("Customers")]
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
            CompanyName = string.Empty;
            ContactName = string.Empty;
            ContactTitle = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
        }

        [Key]
        [StringLength(5)]
        public string CustomerID { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(30)]
        public string? ContactName { get; set; }

        [StringLength(30)]
        public string? ContactTitle { get; set; }

        [StringLength(60)]
        public string? Address { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}