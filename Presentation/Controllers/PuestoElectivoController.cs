using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SADVO.Application.Interface.Service;
using SADVO.Application.DTOs.PuestoElectivo;
using SADVO.Domain.Entities;

namespace SADVOWeb.Controllers
{
    public class PuestoElectivoController : Controller
    {
        private readonly IPuestoElectivoService _puestoElectivoService;

        public PuestoElectivoController(IPuestoElectivoService puestoElectivoService)
        {
            _puestoElectivoService = puestoElectivoService ?? throw new ArgumentNullException(nameof(puestoElectivoService));
        }

        // GET: PuestoElectivo
        public async Task<ActionResult> Index()
        {
            try
            {
                var puestosActivos = await _puestoElectivoService.GetPuestosActivosAsync();
                var puestosDto = puestosActivos.Select(p => new PuestoElectivoDto
                {
                    Id = p.Id,
                    Description = p.Description,
                    Nombre = p.Nombre, // Ensure 'Nombre' is set
                    EsActivo = p.EsActivo // Ensure 'EsActivo' is set
                });
                return View(puestosDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los puestos electivos: " + ex.Message;
                return View(new List<PuestoElectivoDto>());
            }
        }

        // GET: PuestoElectivo/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var puesto = await _puestoElectivoService.GetPuestoConHistorialAsync(id);
                if (puesto == null)
                {
                    TempData["Error"] = "Puesto electivo no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var puestoDto = new PuestoElectivoDto
                {
                    Id = puesto.Id,
                    Description = puesto.Description,
                    Nombre = puesto.Nombre, // Fix: Set required member 'Nombre'
                    EsActivo = puesto.EsActivo // Fix: Set required member 'EsActivo'
                };

                return View(puestoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PuestoElectivo/Create
        public ActionResult Create()
        {
            return View(new CreatePuestoElectivoDto
            {
                Id = 0, // Default value for ID
                Nombre = string.Empty, // Required member 'Nombre' initialized
                EsActivo = true, // Required member 'EsActivo' initialized
                Description = string.Empty // Required member 'Description' initialized
            });
        }

        // POST: PuestoElectivo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePuestoElectivoDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createDto);
                }

                // Validar descripción única
                var existeDescripcion = await _puestoElectivoService.ValidarDescripcionUnicaAsync(createDto.Description);
                if (existeDescripcion)
                {
                    ModelState.AddModelError("Description", "Ya existe un puesto electivo con esta descripción");
                    return View(createDto);
                }

                // Crear nueva entidad
                var nuevoPuesto = new Puesto_Electivo
                {
                    Description = createDto.Description,
                    EsActivo = true,
                    Nombre = createDto.Nombre // Required member 'Nombre' initialized
                };

                await _puestoElectivoService.CreateAsync(nuevoPuesto);
                TempData["Success"] = "Puesto electivo creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al crear el puesto electivo: " + ex.Message;
                return View(createDto);
            }
        }

        // GET: PuestoElectivo/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var puesto = await _puestoElectivoService.GetByIdAsync(id);
                if (puesto == null)
                {
                    TempData["Error"] = "Puesto electivo no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new UpdatePuestoElectivoDto
                {
                    Id = puesto.Id,
                    Description = puesto.Description,
                    Nombre = puesto.Nombre, // Fix: Set required member 'Nombre'
                    EsActivo = puesto.EsActivo // Fix: Set required member 'EsActivo'
                };

                return View(updateDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el puesto electivo: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PuestoElectivo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdatePuestoElectivoDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    TempData["Error"] = "ID inconsistente";
                    return View(updateDto);
                }

                if (!ModelState.IsValid)
                {
                    return View(updateDto);
                }

                // Obtener el puesto actual
                var puestoActual = await _puestoElectivoService.GetByIdAsync(id);
                if (puestoActual == null)
                {
                    TempData["Error"] = "Puesto electivo no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                // Validar descripción única solo si cambió
                if (!string.Equals(puestoActual.Description, updateDto.Description, StringComparison.OrdinalIgnoreCase))
                {
                    var existeDescripcion = await _puestoElectivoService.ValidarDescripcionUnicaAsync(updateDto.Description);
                    if (existeDescripcion)
                    {
                        ModelState.AddModelError("Description", "Ya existe un puesto electivo con esta descripción");
                        return View(updateDto);
                    }
                }

                // Actualizar entidad
                puestoActual.Description = updateDto.Description;
                await _puestoElectivoService.UpdateAsync(puestoActual);

                TempData["Success"] = "Puesto electivo actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el puesto electivo: " + ex.Message;
                return View(updateDto);
            }
        }

        // GET: PuestoElectivo/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var puesto = await _puestoElectivoService.GetByIdAsync(id);
                if (puesto == null)
                {
                    TempData["Error"] = "Puesto electivo no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var puestoDto = new PuestoElectivoDto
                {
                    Id = puesto.Id,
                    Description = puesto.Description,
                    Nombre = puesto.Nombre, // Fix: Set required member 'Nombre'
                    EsActivo = puesto.EsActivo // Fix: Set required
                };

                return View(puestoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el puesto electivo: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PuestoElectivo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var puesto = await _puestoElectivoService.GetByIdAsync(id);
                if (puesto == null)
                {
                    TempData["Error"] = "Puesto electivo no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                // En lugar de eliminar físicamente, desactivar el puesto
                await _puestoElectivoService.ActivarDesactivarPuestoAsync(id, false);

                TempData["Success"] = "Puesto electivo desactivado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al desactivar el puesto electivo: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PuestoElectivo/Activate/5
        public async Task<ActionResult> Activate(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                await _puestoElectivoService.ActivarDesactivarPuestoAsync(id, true);
                TempData["Success"] = "Puesto electivo activado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al activar el puesto electivo: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PuestoElectivo/Deactivate/5
        public async Task<ActionResult> Deactivate(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                await _puestoElectivoService.ActivarDesactivarPuestoAsync(id, false);
                TempData["Success"] = "Puesto electivo desactivado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al desactivar el puesto electivo: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}