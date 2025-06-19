//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using SADVO.Application.DTOs.AsignacionDirigente;
//using SADVO.Application.DTOs.DIrigentePolitico;
//using SADVO.Application.Interface.Service;
//using SADVO.Application.Service;
//using SADVO.Domain.Entities;

//namespace SADVOWeb.Controllers
//{
//    public class DirigenteController : Controller
//    {
//        private readonly IDirigentePoliticoService _dirigenteService;
//        private readonly IUsuariosService _usuarioService; // Asumiendo que tienes este servicio
//        private readonly IPartidoPoliticoService _partidoService; // Asumiendo que tienes este servicio
//        private readonly IMapper _mapper;

//        public DirigenteController(
//            IDirigentePoliticoService dirigenteService,
//            IUsuariosService usuarioService,
//            IPartidoPoliticoService partidoService,
//            IMapper mapper)
//        {
//            _dirigenteService = dirigenteService ?? throw new ArgumentNullException(nameof(dirigenteService));
//            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
//            _partidoService = partidoService ?? throw new ArgumentNullException(nameof(partidoService));
//            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//        }

//        // GET: Dirigente
//        public async Task<ActionResult> Index()
//        {
//            try
//            {
//                var dirigentes = await _dirigenteService.GetAllAsync();
//                var dirigentesDto = _mapper.Map<IEnumerable<DirigentePoliticoDto>>(dirigentes);
//                return View(dirigentesDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar los dirigentes: " + ex.Message;
//                return View(new List<DirigentePoliticoDto>());
//            }
//        }

//        // GET: Dirigente/Details/5
//        public async Task<ActionResult> Details(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    return BadRequest("ID inválido");
//                }

//                var dirigente = await _dirigenteService.GetByIdAsync(id);
//                if (dirigente == null)
//                {
//                    return NotFound("Dirigente no encontrado");
//                }

//                var dirigenteDto = _mapper.Map<DirigentePoliticoDetailsDto>(dirigente);
//                return View(dirigenteDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar los detalles del dirigente: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // GET: Dirigente/Create
//        public async Task<ActionResult> Create()
//        {
//            try
//            {
//                await CargarViewBagsAsync();
//                return View(new CreateDirigenteDto());
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar el formulario: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Dirigente/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(CreateDirigenteDto createDirigenteDto)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    await CargarViewBagsAsync();
//                    return View(createDirigenteDto);
//                }

//                // Validar si ya existe el dirigente
//                var existeDirigente = await _dirigenteService.ValidarDirigenteExistenteAsync(
//                    createDirigenteDto.UsuarioId,
//                    createDirigenteDto.PartidoPoliticoId);

//                if (existeDirigente)
//                {
//                    ModelState.AddModelError("", "Este usuario ya es dirigente del partido seleccionado");
//                    await CargarViewBagsAsync();
//                    return View(createDirigenteDto);
//                }

//                // Asignar dirigente al partido
//                var resultado = await _dirigenteService.AsignarDirigenteAPartidoAsync(
//                    createDirigenteDto.UsuarioId,
//                    createDirigenteDto.PartidoPoliticoId);

//                if (resultado)
//                {
//                    TempData["Success"] = "Dirigente asignado exitosamente";
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Error al asignar el dirigente");
//                    await CargarViewBagsAsync();
//                    return View(createDirigenteDto);
//                }
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", "Error al crear el dirigente: " + ex.Message);
//                await CargarViewBagsAsync();
//                return View(createDirigenteDto);
//            }
//        }

//        // GET: Dirigente/Edit/5
//        public async Task<ActionResult> Edit(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    return BadRequest("ID inválido");
//                }

//                var dirigente = await _dirigenteService.GetByIdAsync(id);
//                if (dirigente == null)
//                {
//                    return NotFound("Dirigente no encontrado");
//                }

//                var updateDirigenteDto = _mapper.Map<UpdateDirigenteDto>(dirigente);
//                await CargarViewBagsAsync();

//                return View(updateDirigenteDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar el dirigente para edición: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Dirigente/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit(int id, UpdateDirigenteDto updateDirigenteDto)
//        {
//            try
//            {
//                if (id != updateDirigenteDto.Id)
//                {
//                    return BadRequest("ID no coincide");
//                }

//                if (!ModelState.IsValid)
//                {
//                    await CargarViewBagsAsync();
//                    return View(updateDirigenteDto);
//                }

//                var dirigenteExistente = await _dirigenteService.GetByIdAsync(id);
//                if (dirigenteExistente == null)
//                {
//                    return NotFound("Dirigente no encontrado");
//                }

//                // Validar si ya existe otro dirigente con la misma combinación (excluyendo el actual)
//                var dirigentes = await _dirigenteService.GetDirigentesByPartidoAsync(updateDirigenteDto.PartidoPoliticoId);
//                var existeOtro = dirigentes.Any(d => d.UsuarioId == updateDirigenteDto.UsuarioId && d.Id != id);

//                if (existeOtro)
//                {
//                    ModelState.AddModelError("", "Este usuario ya es dirigente del partido seleccionado");
//                    await CargarViewBagsAsync();
//                    return View(updateDirigenteDto);
//                }

//                // Mapear cambios
//                _mapper.Map(updateDirigenteDto, dirigenteExistente);

//                var resultado = await _dirigenteService.UpdateAsync(dirigenteExistente);
//                if (resultado != null)
//                {
//                    TempData["Success"] = "Dirigente actualizado exitosamente";
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Error al actualizar el dirigente");
//                    await CargarViewBagsAsync();
//                    return View(updateDirigenteDto);
//                }
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", "Error al actualizar el dirigente: " + ex.Message);
//                await CargarViewBagsAsync();
//                return View(updateDirigenteDto);
//            }
//        }

//        // GET: Dirigente/Delete/5
//        public async Task<ActionResult> Delete(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    return BadRequest("ID inválido");
//                }

//                var dirigente = await _dirigenteService.GetByIdAsync(id);
//                if (dirigente == null)
//                {
//                    return NotFound("Dirigente no encontrado");
//                }

//                var dirigenteDto = _mapper.Map<DirigentePoliticoDetailsDto>(dirigente);
//                return View(dirigenteDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar el dirigente para eliminación: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Dirigente/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    return BadRequest("ID inválido");
//                }

//                var dirigente = await _dirigenteService.GetByIdAsync(id);
//                if (dirigente == null)
//                {
//                    TempData["Error"] = "Dirigente no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Remover dirigente usando el método específico del servicio
//                var resultado = await _dirigenteService.RemoverDirigenteDePartidoAsync(
//                    dirigente.UsuarioId,
//                    dirigente.PartidoPoliticoId);

//                if (resultado)
//                {
//                    TempData["Success"] = "Dirigente removido exitosamente";
//                }
//                else
//                {
//                    TempData["Error"] = "Error al remover el dirigente";
//                }

//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al eliminar el dirigente: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // Métodos específicos adicionales

//        // GET: Dirigente/PorPartido/5
//        public async Task<ActionResult> PorPartido(int partidoId)
//        {
//            try
//            {
//                if (partidoId <= 0)
//                {
//                    return BadRequest("ID de partido inválido");
//                }

//                var dirigentes = await _dirigenteService.GetDirigentesByPartidoAsync(partidoId);
//                var dirigentesDto = _mapper.Map<IEnumerable<DirigentePoliticoDto>>(dirigentes);

//                // Obtener nombre del partido para mostrar en la vista
//                var partido = await _partidoService.GetByIdAsync(partidoId);
//                ViewBag.PartidoNombre = partido?.Nombre ?? "Partido Desconocido";
//                ViewBag.PartidoId = partidoId;

//                return View(dirigentesDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar dirigentes del partido: " + ex.Message;
//                return View(new List<DirigentePoliticoDto>());
//            }
//        }

//        // GET: Dirigente/PorUsuario/5
//        public async Task<ActionResult> PorUsuario(int usuarioId)
//        {
//            try
//            {
//                if (usuarioId <= 0)
//                {
//                    return BadRequest("ID de usuario inválido");
//                }

//                // Usar el repositorio directamente ya que el servicio no tiene este método
//                var dirigentes = await _dirigenteService.GetAllAsync();
//                var dirigentesPorUsuario = dirigentes.Where(d => d.UsuarioId == usuarioId);

//                var dirigentesDto = _mapper.Map<IEnumerable<DirigentePoliticoDto>>(dirigentesPorUsuario);

//                // Obtener información del usuario para mostrar en la vista
//                var usuario = await _usuarioService.GetByIdAsync(usuarioId);
//                ViewBag.UsuarioNombre = usuario?.Nombre ?? "Usuario Desconocido";
//                ViewBag.UsuarioId = usuarioId;

//                return View(dirigentesDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar dirigencias del usuario: " + ex.Message;
//                return View(new List<DirigentePoliticoDto>());
//            }
//        }

//        // API method for AJAX validation
//        [HttpGet]
//        public async Task<JsonResult> ValidarDirigente(int usuarioId, int partidoPoliticoId, int? dirigenteId = null)
//        {
//            try
//            {
//                if (usuarioId <= 0 || partidoPoliticoId <= 0)
//                {
//                    return Json(new { valido = false, mensaje = "IDs inválidos" });
//                }

//                var existe = await _dirigenteService.ValidarDirigenteExistenteAsync(usuarioId, partidoPoliticoId);

//                // Si estamos editando, verificar que no sea el mismo registro
//                if (existe && dirigenteId.HasValue)
//                {
//                    var dirigentes = await _dirigenteService.GetDirigentesByPartidoAsync(partidoPoliticoId);
//                    var dirigenteActual = dirigentes.FirstOrDefault(d => d.UsuarioId == usuarioId);
//                    existe = dirigenteActual != null && dirigenteActual.Id != dirigenteId.Value;
//                }

//                return Json(new
//                {
//                    valido = !existe,
//                    mensaje = existe ? "Este usuario ya es dirigente del partido seleccionado" : "Combinación válida"
//                });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { valido = false, mensaje = "Error al validar: " + ex.Message });
//            }
//        }

//        // Método privado para cargar ViewBags
//        private async Task CargarViewBagsAsync()
//        {
//            try
//            {
//                var usuarios = await _usuarioService.GetAllAsync();
//                var partidos = await _partidoService.GetAllAsync();

//                ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
//                ViewBag.Partidos = new SelectList(partidos, "Id", "Nombre");
//            }
//            catch (Exception ex)
//            {
//                ViewBag.Usuarios = new SelectList(new List<object>(), "Id", "Nombre");
//                ViewBag.Partidos = new SelectList(new List<object>(), "Id", "Nombre");
//                TempData["Warning"] = "Error al cargar listas: " + ex.Message;
//            }
//        }
//    }
//}