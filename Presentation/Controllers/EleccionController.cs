using Microsoft.AspNetCore.Mvc;
using SADVO.Application.Interface.Service;
using SADVO.Application.DTOs.Eleccion;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using AutoMapper;

namespace SADVOWeb.Controllers
{
    public class EleccionController : Controller
    {
        private readonly IEleccionService _eleccionService;
        private readonly IMapper _mapper;

        public EleccionController(IEleccionService eleccionService, IMapper mapper)
        {
            _eleccionService = eleccionService ?? throw new ArgumentNullException(nameof(eleccionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: Eleccion
        public async Task<ActionResult> Index()
        {
            try
            {
                var elecciones = await _eleccionService.GetAllAsync();
                var eleccionesDto = _mapper.Map<IEnumerable<EleccionDto>>(elecciones);
                return View(eleccionesDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["Error"] = "Error al cargar las elecciones: " + ex.Message;
                return View(new List<EleccionDto>());
            }
        }

        // GET: Eleccion/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido");
                }

                var eleccion = await _eleccionService.GetByIdAsync(id);
                if (eleccion == null)
                {
                    return NotFound("Elección no encontrada");
                }

                var eleccionDto = _mapper.Map<EleccionDto>(eleccion);
                return View(eleccionDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles de la elección: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Eleccion/Create
        public ActionResult Create()
        {
            return View(new CreateEleccionDto());
        }

        // POST: Eleccion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEleccionDto createEleccionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createEleccionDto);
                }

                var eleccion = _mapper.Map<Eleccion>(createEleccionDto);
                
                // Validar fecha de elección
                var fechaValida = await _eleccionService.ValidarFechaEleccionAsync(eleccion.FechaOcurrida);
                if (fechaValida)
                {
                    ModelState.AddModelError("FechaOcurrida", "Ya existe una elección programada para esta fecha");
                    return View(createEleccionDto);
                }

                var resultado = await _eleccionService.ProgramarEleccionAsync(eleccion);
                if (resultado)
                {
                    TempData["Success"] = "Elección creada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear la elección");
                    return View(createEleccionDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear la elección: " + ex.Message);
                return View(createEleccionDto);
            }
        }

        // GET: Eleccion/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido");
                }

                var eleccion = await _eleccionService.GetByIdAsync(id);
                if (eleccion == null)
                {
                    return NotFound("Elección no encontrada");
                }

                var updateEleccionDto = _mapper.Map<UpdateEleccionDto>(eleccion);
                return View(updateEleccionDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la elección para edición: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Eleccion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateEleccionDto updateEleccionDto)
        {
            try
            {
                if (id != updateEleccionDto.Id)
                {
                    return BadRequest("ID no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return View(updateEleccionDto);
                }

                var eleccionExistente = await _eleccionService.GetByIdAsync(id);
                if (eleccionExistente == null)
                {
                    return NotFound("Elección no encontrada");
                }

                // Mapear los cambios
                _mapper.Map(updateEleccionDto, eleccionExistente);
                
                var resultado = await _eleccionService.UpdateAsync(eleccionExistente);
                if (resultado != null)
                {
                    TempData["Success"] = "Elección actualizada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar la elección");
                    return View(updateEleccionDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar la elección: " + ex.Message);
                return View(updateEleccionDto);
            }
        }

        // GET: Eleccion/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido");
                }

                var eleccion = await _eleccionService.GetByIdAsync(id);
                if (eleccion == null)
                {
                    return NotFound("Elección no encontrada");
                }

                var eleccionDto = _mapper.Map<EleccionDto>(eleccion);
                return View(eleccionDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la elección para eliminación: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Eleccion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EleccionDto eleccionDto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID inválido");
                }

                var resultado = await _eleccionService.DeleteAsync(id);
                if (resultado)
                {
                    TempData["Success"] = "Elección eliminada exitosamente";
                }
                else
                {
                    TempData["Error"] = "Error al eliminar la elección";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar la elección: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // Métodos adicionales específicos para elecciones
        
        // GET: Eleccion/PorFecha
        public async Task<ActionResult> PorFecha(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                if (!fechaInicio.HasValue || !fechaFin.HasValue)
                {
                    return View(new List<EleccionDto>());
                }

                var elecciones = await _eleccionService.GetEleccionesEnRangoFechaAsync(fechaInicio.Value, fechaFin.Value);
                var eleccionesDto = _mapper.Map<IEnumerable<EleccionDto>>(elecciones);
                
                ViewBag.FechaInicio = fechaInicio;
                ViewBag.FechaFin = fechaFin;
                
                return View(eleccionesDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al buscar elecciones por fecha: " + ex.Message;
                return View(new List<EleccionDto>());
            }
        }

        // GET: Eleccion/HistorialPartido/5
        public async Task<ActionResult> HistorialPartido(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                {
                    return BadRequest("ID de partido inválido");
                }

                var elecciones = await _eleccionService.GetHistorialEleccionesAsync(partidoId);
                var eleccionesDto = _mapper.Map<IEnumerable<EleccionDto>>(elecciones);
                
                ViewBag.PartidoId = partidoId;
                
                return View(eleccionesDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el historial de elecciones del partido: " + ex.Message;
                return View(new List<EleccionDto>());
            }
        }

        // GET: Eleccion/Recientes
        public async Task<ActionResult> Recientes(int cantidad = 10)
        {
            try
            {
                if (cantidad <= 0) cantidad = 10;

                // Necesitarás agregar este método al servicio
                var elecciones = await _eleccionService.GetAllAsync(); // Temporalmente usando GetAll
                var eleccionesRecientes = elecciones
                    .OrderByDescending(e => e.FechaOcurrida)
                    .Take(cantidad);
                
                var eleccionesDto = _mapper.Map<IEnumerable<EleccionDto>>(eleccionesRecientes);
                
                ViewBag.Cantidad = cantidad;
                
                return View(eleccionesDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar las elecciones recientes: " + ex.Message;
                return View(new List<EleccionDto>());
            }
        }

        // API method for AJAX calls
        [HttpGet]
        public async Task<JsonResult> ValidarFecha(DateTime fecha)
        {
            try
            {
                var fechaValida = await _eleccionService.ValidarFechaEleccionAsync(fecha);
                return Json(new { valida = !fechaValida, mensaje = fechaValida ? "Ya existe una elección en esta fecha" : "Fecha disponible" });
            }
            catch (Exception ex)
            {
                return Json(new { valida = false, mensaje = "Error al validar la fecha: " + ex.Message });
            }
        }
    }
}