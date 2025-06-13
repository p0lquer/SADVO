using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SADVO.Application.Interface.Service;
using SADVO.Application.DTOs.Candidato;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVOWeb.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly ICandidatoService _candidatoService;

        public CandidatoController(ICandidatoService candidatoService)
        {
            _candidatoService = candidatoService ?? throw new ArgumentNullException(nameof(candidatoService));
        }

        // GET: CandidatoController
        public async Task<ActionResult> Index()
        {
            try
            {
                var candidatos = await _candidatoService.GetCandidatosActivosAsync();
                var candidatosDto = candidatos.Select(c => new CandidatoDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    EsActivo = c.EsActivo,
                    Apellido = c.Apellido,
                    Foto = c.Foto
                }).ToList();

                return View(candidatosDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["Error"] = "Error al cargar los candidatos: " + ex.Message;
                return View(new List<CandidatoDto>());
            }
        }

        // GET: CandidatoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID de candidato inválido");
                }

                var candidato = await _candidatoService.GetByIdAsync(id);
                if (candidato == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }

                var candidatoDto = new CandidatoDto
                {
                    Id = candidato.Id,
                    Nombre = candidato.Nombre,
                    EsActivo = candidato.EsActivo,
                    Apellido = candidato.Apellido,
                    Foto = candidato.Foto
                };

                return View(candidatoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles del candidato: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CandidatoController/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el formulario de creación: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CandidatoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCandidatoDto createCandidatoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createCandidatoDto);
                }

                // Validar candidato único
                var esUnico = await _candidatoService.ValidarCandidatoUnicoAsync(
                    createCandidatoDto.Apellido,
                    createCandidatoDto.PartidoId);

                if (!esUnico)
                {
                    ModelState.AddModelError("", "Ya existe un candidato con ese apellido en el partido seleccionado");
                    return View(createCandidatoDto);
                }

                // Mapear DTO a entidad
                var candidato = new Candidato
                {
                    Apellido = createCandidatoDto.Apellido,
                    PartidoId = createCandidatoDto.PartidoId,
                    PuestoElectivoId = createCandidatoDto.PuestoElectivoId,
                    Foto = createCandidatoDto.Foto,
                    TypeCandidate = createCandidatoDto.TipoCandidato,
                 
                    Nombre = createCandidatoDto.nombre,
                    EsActivo = true,
                    PuestoElectivo = new Puesto_Electivo
                    {
                        Nombre = "Default Nombre", // Required member
                        EsActivo = true,          // Required member
                        Description = "Default Description" // Required member
                    },

                    Partido = new Partido_Politico
                    {
                        Nombre = "Default Nombre", 
                        EsActivo = true,          
                        Siglas = "Default Siglas", 
                        Description = "Default Description", 
                        Logo = "Default Logo" 
                    }     
                };

                await _candidatoService.CreateAsync(candidato);
                TempData["Success"] = "Candidato creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al crear el candidato: " + ex.Message;
                return View(createCandidatoDto);
            }
        }

        // GET: CandidatoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID de candidato inválido");
                }

                var candidato = await _candidatoService.GetByIdAsync(id);
                if (candidato == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }

                var updateCandidatoDto = new UpdateCandidatoDto
                {
                    Id = candidato.Id,
                    Nombre = candidato.Nombre,
                    EsActivo = candidato.EsActivo,
                    Apellido = candidato.Apellido,
                    Foto = candidato.Foto
                };

                return View(updateCandidatoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el candidato para edición: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CandidatoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateCandidatoDto updateCandidatoDto)
        {
            try
            {
                if (id != updateCandidatoDto.Id)
                {
                    return BadRequest("ID de candidato no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return View(updateCandidatoDto);
                }

                var candidatoExistente = await _candidatoService.GetByIdAsync(id);
                if (candidatoExistente == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }

                // Actualizar propiedades
                candidatoExistente.Apellido = updateCandidatoDto.Apellido;
                candidatoExistente.Foto = updateCandidatoDto.Foto;

                await _candidatoService.UpdateAsync(candidatoExistente);
                TempData["Success"] = "Candidato actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el candidato: " + ex.Message;
                return View(updateCandidatoDto);
            }
        }

        // GET: CandidatoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID de candidato inválido");
                }

                var candidato = await _candidatoService.GetByIdAsync(id);
                if (candidato == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }

                var candidatoDto = new CandidatoDto
                {
                    Id = candidato.Id,
                    Nombre = candidato.Nombre,
                    EsActivo = candidato.EsActivo,
                    Apellido = candidato.Apellido,
                    Foto = candidato.Foto
                };

                return View(candidatoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el candidato para eliminación: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CandidatoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID de candidato inválido");
                }

                var candidato = await _candidatoService.GetByIdAsync(id);
                if (candidato == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }

                await _candidatoService.DeleteAsync(id);
                TempData["Success"] = "Candidato eliminado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el candidato: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // Métodos adicionales basados en el servicio

        // GET: CandidatoController/ByPartido/5
        public async Task<ActionResult> ByPartido(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                {
                    return BadRequest("ID de partido inválido");
                }

                var candidatos = await _candidatoService.GetCandidatosByPartidoAsync(partidoId);
                var candidatosDto = candidatos.Select(c => new CandidatoDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    EsActivo = c.EsActivo,
                    Apellido = c.Apellido,
                    Foto = c.Foto
                }).ToList();

                ViewBag.PartidoId = partidoId;
                return View("Index", candidatosDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar candidatos por partido: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CandidatoController/ActivarDesactivar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActivarDesactivar(int id, bool estado)
        {
            try
            {
                var resultado = await _candidatoService.ActivarDesactivarCandidatoAsync(id, estado);
                if (resultado)
                {
                    TempData["Success"] = $"Candidato {(estado ? "activado" : "desactivado")} exitosamente";
                }
                else
                {
                    TempData["Error"] = "No se pudo cambiar el estado del candidato";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cambiar estado del candidato: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CandidatoController/ActualizarFoto/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActualizarFoto(int id, string nuevaFoto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevaFoto))
                {
                    TempData["Error"] = "La nueva foto no puede estar vacía";
                    return RedirectToAction(nameof(Details), new { id });
                }

                var resultado = await _candidatoService.ActualizarFotoCandidatoAsync(id, nuevaFoto);
                if (resultado)
                {
                    TempData["Success"] = "Foto del candidato actualizada exitosamente";
                }
                else
                {
                    TempData["Error"] = "No se pudo actualizar la foto del candidato";
                }

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar la foto: " + ex.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}