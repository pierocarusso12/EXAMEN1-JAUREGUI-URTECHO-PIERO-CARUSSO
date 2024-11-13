using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string? ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        // Propiedad de navegación
        public virtual ICollection<OrderDetail> ?OrderDetails { get; set; }
    }
}