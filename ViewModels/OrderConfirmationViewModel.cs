using System.ComponentModel.DataAnnotations;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public int OrderID { get; set; }

        [Required(ErrorMessage = "La fecha de confirmación es requerida")]
        [Display(Name = "Fecha de Confirmación")]
        public DateTime ConfirmationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe indicar si el pedido está confirmado o no")]
        [Display(Name = "¿Pedido Confirmado?")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "Comentarios")]
        [StringLength(1000, ErrorMessage = "Los comentarios no pueden exceder los 1000 caracteres")]
        public string? Comments { get; set; }

        // Propiedades adicionales para mostrar información del pedido
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}