using System.ComponentModel.DataAnnotations;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models
{
    public class OrderConfirmation
    {
        public OrderConfirmation()
        {
            Comments = string.Empty;
        }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "La fecha de confirmación es requerida")]
        public DateTime ConfirmationDate { get; set; }

        [Required(ErrorMessage = "Debe indicar si el pedido está confirmado")]
        public bool IsConfirmed { get; set; }

        public string Comments { get; set; }
    }
}