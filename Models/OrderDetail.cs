using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models
{
    [Table("Order Details")]
    public class OrderDetail
    {
        public OrderDetail()
        {
            Product = null!;
            Order = null!;
        }

        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        public float Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}