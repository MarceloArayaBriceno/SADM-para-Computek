﻿using APS.Data.Models;
using APS.Web.Filters;
using APS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APS.Web.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class FacturaController : Controller
    {
        private readonly ApdatadbContext _context;

        public FacturaController(ApdatadbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Facturas.Include(f => f.DetallesFactura).ToListAsync();
            return View(facturas);
        }


        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            factura.DetallesFactura.ForEach(detalleFactura => _context.DetalleFacturas.Remove(detalleFactura));
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            var model = new FacturaViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacturaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var factura = new Factura
                {
                    NombreCliente = model.NombreCliente,
                    DireccionCliente = model.DireccionCliente,
                    Fecha = model.Fecha,
                    Total = model.DetallesFactura.Sum(d => d.Cantidad * d.PrecioUnitario),
                    DetallesFactura = model.DetallesFactura.Select(d => new DetalleFactura
                    {
                        Descripcion = d.Descripcion,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
                };

                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.DetallesFactura)
                .FirstOrDefaultAsync(f => f.FacturaID == id);

            if (factura == null)
            {
                return NotFound();
            }

            var model = new FacturaViewModel
            {
                FacturaID = factura.FacturaID,
                NombreCliente = factura.NombreCliente,
                DireccionCliente = factura.DireccionCliente,
                Fecha = factura.Fecha,
                Total = factura.Total,
                DetallesFactura = factura.DetallesFactura.Select(d => new DetalleFacturaViewModel
                {
                    DetalleID = d.DetalleID,
                    Descripcion = d.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FacturaViewModel model)
        {
            if (id != model.FacturaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var factura = await _context.Facturas
                    .Include(f => f.DetallesFactura)
                    .FirstOrDefaultAsync(f => f.FacturaID == id);

                if (factura == null)
                {
                    return NotFound();
                }

                factura.NombreCliente = model.NombreCliente;
                factura.DireccionCliente = model.DireccionCliente;
                factura.Fecha = model.Fecha;
                factura.Total = model.DetallesFactura.Sum(d => d.Cantidad * d.PrecioUnitario);
                factura.DetallesFactura.Clear();
                factura.DetallesFactura = model.DetallesFactura.Select(d => new DetalleFactura
                {
                    DetalleID = d.DetalleID,
                    Descripcion = d.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList();

                _context.Update(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDetalle(int facturaId, int detalleId)
        {
            var detalle = await _context.DetalleFacturas.FindAsync(detalleId);
            if (detalle == null)
            {
                return NotFound();
            }

            _context.DetalleFacturas.Remove(detalle);

            var factura = await _context.Facturas.Include(f => f.DetallesFactura)
                                                  .FirstOrDefaultAsync(f => f.FacturaID == facturaId);
            if (factura != null)
            {
                factura.Total = factura.DetallesFactura.Sum(d => d.Subtotal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}