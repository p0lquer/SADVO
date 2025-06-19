using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SADVO.Application.Interface.Service;
using SADVO.Application.DTOs.Candidato;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Application.ViewModels.CandidatoVM;

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
        public async Task<IActionResult> Index()
        {
            try
            {
                var candidatos = await _candidatoService.GetCandidatosActivosAsync();
                var candidatosVm = candidatos.Select(c => new CandidatoViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    EsActivo = c.EsActivo,
                    Apellido = c.Apellido,
                    Foto = c.Foto
                }).ToList();

                return View(candidatosVm);
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["Error"] = "Error al cargar los candidatos: " + ex.Message;
                return View(new List<CandidatoViewModel>());
            }
        }

        // GET: CandidatoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var candidato = await _candidatoService.GetByIdAsync(id);
                if (candidato == null)
                {
                    TempData["Error"] = $"Candidato con ID {id} no encontrado";
                    return RedirectToAction(nameof(Index));
                }

              

                // Convert DTO to ViewModel
                var candidatoViewModel = new CandidatoViewModel
                {
                    Id = candidato.Id,
                    Nombre = candidato.Nombre,
                    EsActivo = candidato.EsActivo,
                    Apellido = candidato.Apellido,
                    Foto = candidato.Foto
                    
                };

                return View(candidatoViewModel);
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
        public async Task<IActionResult> Create(CreateCandidatoViewModel candidatoVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(candidatoVm);
                }

                // Validar candidato único
                var esUnico = await _candidatoService.ValidarCandidatoUnicoAsync(
                    candidatoVm.Apellido);

                if (!esUnico)
                {
                   ModelState.AddModelError("", "Ya existe un candidato con ese apellido en el partido seleccionado");
                    return View(candidatoVm);
                }

               

                await _candidatoService.CreateAsync(new Candidato
                {
                
                    Nombre = candidatoVm.Nombre,
                    Apellido = candidatoVm.Apellido,
                    Foto = candidatoVm.Foto,
                    EsActivo = candidatoVm.EsActivo,
                    PuestoElectivoId = candidatoVm.PuestoElectivoId,
                    PartidoId = candidatoVm.PartidoId,
                    PuestoElectivo = candidatoVm.PuestoElectivo ?? null,
                    Partido = candidatoVm.Partido ?? null,
                });

                // Si la creación es exitosa, redirigir a la lista de candidatos




                TempData["Success"] = "Candidato creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al crear el candidato: " + ex.Message;
                return View(candidatoVm);
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

                var candidatoVm = await _candidatoService.GetByIdAsync(id);
                if (candidatoVm == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }



                return View(candidatoVm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el candidato para edición: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

     //   POST: CandidatoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CandidatoDto candidatoDto)
        {
            if (id != candidatoDto.Id)
                return BadRequest("ID de candidato no coincide");

            if (!ModelState.IsValid)
                return View(await _candidatoService.GetByIdAsync(id));
            if (candidatoDto == null)
            {
                throw new ArgumentNullException(nameof(candidatoDto), "El DTO de actualización no puede ser nulo");
            }
            var candidatoEntity = new Candidato
            {
                Id = candidatoDto.Id,
                Nombre = candidatoDto.Nombre,
                EsActivo = candidatoDto.EsActivo,
                Apellido = candidatoDto.Apellido,
                Foto = candidatoDto.Foto,
                PuestoElectivoId = candidatoDto.PuestoElectivoId,
                PartidoId = candidatoDto.PartidoId,
                PuestoElectivo = candidatoDto.PuestoElectivo,
                Partido = candidatoDto.Partido,
                TypeCandidate = candidatoDto.TypeCandidate
            };

            var result = await _candidatoService.UpdateAsync(candidatoEntity);
            if (result == null)
            {
                TempData["Error"] = "No se pudo actualizar el candidato";
                return View(await _candidatoService.GetByIdAsync(id));
            }

            TempData["Success"] = "Candidato actualizado exitosamente";
            return RedirectToAction(nameof(Index));
        }


        // GET: CandidatoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
               
                var candidatoViewModel = await _candidatoService.GetByIdAsync(id);
                if (candidatoViewModel == null)
                {
                    return NotFound($"Candidato con ID {id} no encontrado");
                }

            
                return View(candidatoViewModel);
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
                    Foto = c.Foto,
                     PuestoElectivoId = c.PuestoElectivoId, // Ensure this required property is set
                    PartidoId = c.PartidoId
                }).ToList();

                ViewBag.PartidoId = partidoId;

                var candidatoViewModel = candidatosDto.Select(c => new CandidatoViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    EsActivo = c.EsActivo,
                    Apellido = c.Apellido,
                    Foto = c.Foto
                }).ToList();
                return View("Index", candidatoViewModel);
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
        public async Task<ActionResult> ActualizarFoto(CandidatoDto candidatoDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(candidatoDto.Foto))
                {
                    TempData["Error"] = "La nueva foto no puede estar vacía";
                    return RedirectToAction(nameof(Details), new { candidatoDto.Id });
                }

                var resultado = await _candidatoService.ActualizarFotoCandidatoAsync(candidatoDto.Id, candidatoDto.Foto);
                if (resultado)
                {
                    TempData["Success"] = "Foto del candidato actualizada exitosamente";
                }
                else
                {
                    TempData["Error"] = "No se pudo actualizar la foto del candidato";
                }

                return RedirectToAction(nameof(Details), new { candidatoDto.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar la foto: " + ex.Message;
                return RedirectToAction(nameof(Details), new { candidatoDto.Id });
            }
        }
    }
}