using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Data;
using EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Controllers
{
    public class OrdersController : Controller
    {
        private readonly NorthwindContext _context;

        public OrdersController(NorthwindContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Where(o => o.IsPending) 
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => new Order
                    {
                        OrderID = o.OrderID,
                        CustomerID = o.CustomerID,
                        OrderDate = o.OrderDate,
                        IsConfirmed = o.IsConfirmed,
                        IsPending = o.IsPending,
                        Customer = o.Customer != null ? new Customer
                        {
                            CustomerID = o.Customer.CustomerID,
                            CompanyName = o.Customer.CompanyName ?? "Sin empresa",
                            ContactName = o.Customer.ContactName ?? "Sin contacto",
                            Phone = o.Customer.Phone ?? "Sin teléfono"
                        } : null
                    })
                    .ToListAsync();

                if (!orders.Any())
                {
                    TempData["InfoMessage"] = "No hay pedidos pendientes.";
                }

                return View(orders);
            }
            catch (Exception ex)
            {
#if DEBUG
                TempData["ErrorMessage"] = $"Error al cargar los pedidos: {ex.Message}";
#else
                TempData["ErrorMessage"] = "Error al cargar los pedidos.";
#endif
                return View(new List<Order>());
            }
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "ID de pedido no válido.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                    .Select(o => new Order
                    {
                        OrderID = o.OrderID,
                        CustomerID = o.CustomerID,
                        OrderDate = o.OrderDate,
                        IsConfirmed = o.IsConfirmed,
                        ConfirmationDate = o.ConfirmationDate,
                        Comments = o.Comments ?? string.Empty,
                        IsPending = o.IsPending,
                        Customer = o.Customer != null ? new Customer
                        {
                            CompanyName = o.Customer.CompanyName ?? "Sin empresa",
                            ContactName = o.Customer.ContactName ?? "Sin contacto",
                            Phone = o.Customer.Phone ?? "Sin teléfono",
                            Address = o.Customer.Address ?? string.Empty
                        } : null,
                        OrderDetails = o.OrderDetails.Select(od => new OrderDetail
                        {
                            OrderID = od.OrderID,
                            ProductID = od.ProductID,
                            UnitPrice = od.UnitPrice,
                            Quantity = od.Quantity,
                            Discount = od.Discount,
                            Product = new Product  
                            {
                                ProductID = od.Product != null ? od.Product.ProductID : 0,
                                ProductName = od.Product != null ? od.Product.ProductName ?? "Producto sin nombre" : "Producto sin nombre"
                            }
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(m => m.OrderID == id);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Pedido no encontrado.";
                    return RedirectToAction(nameof(Index));
                }

               
                order.OrderDetails ??= new List<OrderDetail>();

                return View(order);
            }
            catch (Exception ex)
            {
#if DEBUG
                TempData["ErrorMessage"] = $"Error al cargar el pedido: {ex.Message}";
#else
        TempData["ErrorMessage"] = "Error al cargar el pedido. Por favor, intente nuevamente.";
#endif
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id, DateTime confirmationDate, string isConfirmed, string? comments)
        {
            if (string.IsNullOrEmpty(isConfirmed))
            {
                TempData["ErrorMessage"] = "Debe seleccionar un estado de confirmación.";
                return RedirectToAction(nameof(Details), new { id });
            }

            try
            {
                var order = await _context.Orders.FindAsync(id);

                if (order is null)
                {
                    TempData["ErrorMessage"] = "Pedido no encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                order.ConfirmationDate = confirmationDate;
                order.IsConfirmed = isConfirmed.ToLower() == "true";
                order.Comments = comments ?? string.Empty;
                order.IsPending = false;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Pedido confirmado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al confirmar el pedido: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}