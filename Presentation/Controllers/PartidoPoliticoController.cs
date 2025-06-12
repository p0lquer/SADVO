using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SADVO.Application.Interface.Service;
using SADVO.Application.DTOs.PartidoPolitico;
using SADVO.Domain.Entities;

namespace SADVOWeb.Controllers
{
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoService _partidoPoliticoService;

        public PartidoPoliticoController(IPartidoPoliticoService partidoPoliticoService)
        {
            _partidoPoliticoService = partidoPoliticoService ?? throw new ArgumentNullException(nameof(partidoPoliticoService));
        }

        // GET: PartidoPolitico
        public async Task<ActionResult> Index()
        {
            try
            {
                var partidosActivos = await _partidoPoliticoService.GetPartidosActivosAsync();
                var partidosDto = partidosActivos.Select(p => new PartidoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Siglas = p.Siglas,
                    EsActivo = p.EsActivo,
                    Description = p.Description,
                    Logo = p.Logo
                });
                return View(partidosDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los partidos políticos: " + ex.Message;
                return View(new List<PartidoDto>());
            }
        }

        // GET: PartidoPolitico/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var partido = await _partidoPoliticoService.GetPartidoConDetallesAsync(id);
                if (partido == null)
                {
                    TempData["Error"] = "Partido político no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var partidoDto = new PartidoDto
                {
                    Id = partido.Id,
                    Nombre = partido.Nombre,
                    EsActivo = partido.EsActivo,
                    Siglas = partido.Siglas,
                    Description = partido.Description,
                    Logo = partido.Logo
                };

                return View(partidoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PartidoPolitico/Create
        public ActionResult Create()
        {
            return View(new CreatePartidoDto
            {
                Siglas = string.Empty,
                Description = string.Empty,
                Logo = string.Empty,
                Nombre = string.Empty,
                EsActivo = true
            });
        }

        // POST: PartidoPolitico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePartidoDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createDto);
                }

                // Validar siglas únicas
                var existeSiglas = await _partidoPoliticoService.ValidarSiglasUnicasAsync(createDto.Siglas);
                if (existeSiglas)
                {
                    ModelState.AddModelError("Siglas", "Ya existe un partido político con estas siglas");
                    return View(createDto);
                }

                // Crear nueva entidad
                var nuevoPartido = new Partido_Politico
                {
                    Siglas = createDto.Siglas.ToUpper(), // Convertir a mayúsculas para consistencia
                    Description = createDto.Description,
                    Logo = createDto.Logo,
                    EsActivo = true,
                    Nombre = createDto.Nombre // Set the required 'Nombre' property
                };

                await _partidoPoliticoService.CreateAsync(nuevoPartido);
                TempData["Success"] = "Partido político creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al crear el partido político: " + ex.Message;
                return View(createDto);
            }
        }

        // GET: PartidoPolitico/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var partido = await _partidoPoliticoService.GetByIdAsync(id);
                if (partido == null)
                {
                    TempData["Error"] = "Partido político no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new UpdatePartidoDto
                {
                    Id = partido.Id,
                    Nombre = partido.Nombre,
                    EsActivo = partido.EsActivo,
                    Siglas = partido.Siglas,
                    Description = partido.Description,
                    Logo = partido.Logo
                };

                return View(updateDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el partido político: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PartidoPolitico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdatePartidoDto updateDto)
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

                // Obtener el partido actual
                var partidoActual = await _partidoPoliticoService.GetByIdAsync(id);
                if (partidoActual == null)
                {
                    TempData["Error"] = "Partido político no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                // Validar siglas únicas solo si cambió
                if (!string.Equals(partidoActual.Siglas, updateDto.Siglas, StringComparison.OrdinalIgnoreCase))
                {
                    var existeSiglas = await _partidoPoliticoService.ValidarSiglasUnicasAsync(updateDto.Siglas);
                    if (existeSiglas)
                    {
                        ModelState.AddModelError("Siglas", "Ya existe un partido político con estas siglas");
                        return View(updateDto);
                    }
                }

                // Actualizar entidad
                partidoActual.Siglas = updateDto.Siglas.ToUpper();
                partidoActual.Description = updateDto.Description;
                partidoActual.Logo = updateDto.Logo;

                await _partidoPoliticoService.UpdateAsync(partidoActual);

                TempData["Success"] = "Partido político actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el partido político: " + ex.Message;
                return View(updateDto);
            }
        }

        // GET: PartidoPolitico/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                var partido = await _partidoPoliticoService.GetByIdAsync(id);
                if (partido == null)
                {
                    TempData["Error"] = "Partido político no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                var partidoDto = new PartidoDto
                {
                    Id = partido.Id,
                    Nombre = partido.Nombre,
                    EsActivo = partido.EsActivo,
                    Siglas = partido.Siglas,
                    Description = partido.Description,
                    Logo = partido.Logo
                };

                return View(partidoDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el partido político: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PartidoPolitico/Delete/5
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

                var partido = await _partidoPoliticoService.GetByIdAsync(id);
                if (partido == null)
                {
                    TempData["Error"] = "Partido político no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                // En lugar de eliminar físicamente, desactivar el partido
                await _partidoPoliticoService.ActivarDesactivarPartidoAsync(id, false);

                TempData["Success"] = "Partido político desactivado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al desactivar el partido político: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PartidoPolitico/Activate/5
        public async Task<ActionResult> Activate(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                await _partidoPoliticoService.ActivarDesactivarPartidoAsync(id, true);
                TempData["Success"] = "Partido político activado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al activar el partido político: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PartidoPolitico/Deactivate/5
        public async Task<ActionResult> Deactivate(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                await _partidoPoliticoService.ActivarDesactivarPartidoAsync(id, false);
                TempData["Success"] = "Partido político desactivado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al desactivar el partido político: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: PartidoPolitico/UpdateLogo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateLogo(int id, string nuevoLogo)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "ID inválido";
                    return RedirectToAction(nameof(Index));
                }

                if (string.IsNullOrWhiteSpace(nuevoLogo))
                {
                    TempData["Error"] = "El logo no puede estar vacío";
                    return RedirectToAction(nameof(Details), new { id });
                }

                await _partidoPoliticoService.ActualizarLogoPartidoAsync(id, nuevoLogo);
                TempData["Success"] = "Logo del partido actualizado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el logo: " + ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: PartidoPolitico/SearchBySiglas
        public async Task<ActionResult> SearchBySiglas(string siglas)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(siglas))
                {
                    TempData["Error"] = "Las siglas no pueden estar vacías";
                    return RedirectToAction(nameof(Index));
                }

                var partido = await _partidoPoliticoService.GetBySiglasAsync(siglas);
                if (partido == null)
                {
                    TempData["Info"] = $"No se encontró un partido con las siglas '{siglas}'";
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Details), new { id = partido.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error en la búsqueda: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}