//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SADVO.Application.Interface.Service;
//using SADVO.Application.DTOs.Ciudadano;
//using SADVO.Domain.Entities;

//namespace SADVOWeb.Controllers
//{
//    public class CiudadanoController : Controller
//    {
//        private readonly ICiudadanoService _ciudadanoService;

//        public CiudadanoController(ICiudadanoService ciudadanoService)
//        {
//            _ciudadanoService = ciudadanoService ?? throw new ArgumentNullException(nameof(ciudadanoService));
//        }

//        // GET: Ciudadano
//        public async Task<ActionResult> Index()
//        {
//            try
//            {
//                var ciudadanos = await _ciudadanoService.GetAllAsync();
//                var ciudadanosDto = ciudadanos.Select(c => new CiudadanoDto
//                {
//                    Id = c.Id,
//                    Nombre = c.Nombre,
//                    Apellido = c.Apellido,
//                    NumeroIdentificacion = c.NumeroIdentificacion,
//                    Email = c.Email,
//                    EsActivo = c.EsActivo
//                }).ToList();

//                return View(ciudadanosDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al cargar los ciudadanos: {ex.Message}";
//                return View(new List<CiudadanoDto>());
//            }
//        }

//        // GET: Ciudadano/Details/5
//        public async Task<ActionResult> Details(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID de ciudadano inválido.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadano = await _ciudadanoService.GetByIdAsync(id);
//                if (ciudadano == null)
//                {
//                    TempData["Error"] = "Ciudadano no encontrado.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadanoDto = new CiudadanoDto
//                {
//                    Id = ciudadano.Id,
//                    Nombre = ciudadano.Nombre,
//                    Apellido = ciudadano.Apellido,
//                    NumeroIdentificacion = ciudadano.NumeroIdentificacion,
//                    Email = ciudadano.Email,
//                    EsActivo = ciudadano.EsActivo,
//                };

//                return View(ciudadanoDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al cargar el ciudadano: {ex.Message}";
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // GET: Ciudadano/Create
//        public ActionResult Create()
//        {
//            return View(new CreateCiudadanoDto
//            {
//                Nombre = string.Empty,
//                EsActivo = true,
//                Apellido = string.Empty,
//                NumeroIdentificacion = string.Empty,
//                Email = string.Empty
//            });
//        }

//        // POST: Ciudadano/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(CreateCiudadanoDto createCiudadanoDto)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    return View(createCiudadanoDto);
//                }

//                // Validar que el ciudadano sea único
//                var esUnico = await _ciudadanoService.ValidarCiudadanoUnicoAsync(
//                    createCiudadanoDto.NumeroIdentificacion,
//                    createCiudadanoDto.Email);

//                if (!esUnico)
//                {
//                    ModelState.AddModelError("", "Ya existe un ciudadano con este número de identificación o email.");
//                    return View(createCiudadanoDto);
//                }

//                var ciudadano = new Ciudadano
//                {
//                    Nombre = createCiudadanoDto.Nombre,
//                    Apellido = createCiudadanoDto.Apellido,
//                    NumeroIdentificacion = createCiudadanoDto.NumeroIdentificacion,
//                    Email = createCiudadanoDto.Email,
//                    EsActivo = true,
//                };

//                await _ciudadanoService.AddAsync(ciudadano);
//                TempData["Success"] = "Ciudadano creado exitosamente.";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al crear el ciudadano: {ex.Message}";
//                return View(createCiudadanoDto);
//            }
//        }

//        // GET: Ciudadano/Edit/5
//        public async Task<ActionResult> Edit(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID de ciudadano inválido.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadano = await _ciudadanoService.GetByIdAsync(id);
//                if (ciudadano == null)
//                {
//                    TempData["Error"] = "Ciudadano no encontrado.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var updateCiudadanoDto = new UpdateCiudadanoDto
//                {
//                    Id = ciudadano.Id,
//                    Nombre = ciudadano.Nombre,
//                    Apellido = ciudadano.Apellido,
//                    NumeroIdentificacion = ciudadano.NumeroIdentificacion,
//                    Email = ciudadano.Email,
//                    EsActivo = ciudadano.EsActivo // Added this line to satisfy the required member

//                };

//                return View(updateCiudadanoDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al cargar el ciudadano para edición: {ex.Message}";
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Ciudadano/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit(int id, UpdateCiudadanoDto updateCiudadanoDto)
//        {
//            try
//            {
//                if (id != updateCiudadanoDto.Id)
//                {
//                    TempData["Error"] = "ID de ciudadano no coincide.";
//                    return View(updateCiudadanoDto);
//                }

//                if (!ModelState.IsValid)
//                {
//                    return View(updateCiudadanoDto);
//                }

//                var ciudadanoExistente = await _ciudadanoService.GetByIdAsync(id);
//                if (ciudadanoExistente == null)
//                {
//                    TempData["Error"] = "Ciudadano no encontrado.";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Verificar si cambió el número de identificación o email y validar unicidad
//                if (ciudadanoExistente.NumeroIdentificacion != updateCiudadanoDto.NumeroIdentificacion ||
//                ciudadanoExistente.Email != updateCiudadanoDto.Email)
//                {
//                    // Validar que no exista otro ciudadano con estos datos
//                    var ciudadanoPorNumero = await _ciudadanoService.GetByNumeroIdentificacionAsync(updateCiudadanoDto.NumeroIdentificacion);
//                    var ciudadanoPorEmail = (await _ciudadanoService.BuscarCiudadanosAsync(updateCiudadanoDto.Email))
//                        .FirstOrDefault(c => c.Email == updateCiudadanoDto.Email);

//                    if ((ciudadanoPorNumero != null && ciudadanoPorNumero.Id != id) ||
//                        (ciudadanoPorEmail != null && ciudadanoPorEmail.Id != id))
//                    {
//                        ModelState.AddModelError("", "Ya existe otro ciudadano con este número de identificación o email.");
//                        return View(updateCiudadanoDto);
//                    }
//                }

//                // Actualizar los datos
//                ciudadanoExistente.Nombre = updateCiudadanoDto.Nombre;
//                ciudadanoExistente.Apellido = updateCiudadanoDto.Apellido;
//                ciudadanoExistente.NumeroIdentificacion = updateCiudadanoDto.NumeroIdentificacion;
//                ciudadanoExistente.Email = updateCiudadanoDto.Email;

//                await _ciudadanoService.UpdateAsync(ciudadanoExistente);
//                TempData["Success"] = "Ciudadano actualizado exitosamente.";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al actualizar el ciudadano: {ex.Message}";
//                return View(updateCiudadanoDto);
//            }
//        }

//        // GET: Ciudadano/Delete/5
//        public async Task<ActionResult> Delete(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID de ciudadano inválido.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadano = await _ciudadanoService.GetByIdAsync(id);
//                if (ciudadano == null)
//                {
//                    TempData["Error"] = "Ciudadano no encontrado.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadanoDto = new CiudadanoDto
//                {
//                    Id = ciudadano.Id,
//                    Nombre = ciudadano.Nombre,
//                    Apellido = ciudadano.Apellido,
//                    NumeroIdentificacion = ciudadano.NumeroIdentificacion,
//                    Email = ciudadano.Email,
//                    EsActivo = ciudadano.EsActivo
//                };

//                return View(ciudadanoDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al cargar el ciudadano para eliminación: {ex.Message}";
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Ciudadano/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID de ciudadano inválido.";
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadano = await _ciudadanoService.GetByIdAsync(id);
//                if (ciudadano == null)
//                {
//                    TempData["Error"] = "Ciudadano no encontrado.";
//                    return RedirectToAction(nameof(Index));
//                }

//                await _ciudadanoService.DeleteAsync(id);
//                TempData["Success"] = "Ciudadano eliminado exitosamente.";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al eliminar el ciudadano: {ex.Message}";
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Ciudadano/ActivarDesactivar/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ActivarDesactivar(int id, bool estado)
//        {
//            try
//            {
//                var resultado = await _ciudadanoService.ActivarDesactivarCiudadanoAsync(id, estado);
//                if (resultado)
//                {
//                    TempData["Success"] = $"Ciudadano {(estado ? "activado" : "desactivado")} exitosamente.";
//                }
//                else
//                {
//                    TempData["Error"] = "No se pudo cambiar el estado del ciudadano.";
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al cambiar el estado del ciudadano: {ex.Message}";
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        // GET: Ciudadano/Buscar
//        public async Task<ActionResult> Buscar(string criterio)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(criterio))
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                var ciudadanos = await _ciudadanoService.BuscarCiudadanosAsync(criterio);
//                var ciudadanosDto = ciudadanos.Select(c => new CiudadanoDto
//                {
//                    Id = c.Id,
//                    Nombre = c.Nombre,
//                    Apellido = c.Apellido,
//                    NumeroIdentificacion = c.NumeroIdentificacion,
//                    Email = c.Email,
//                    EsActivo = c.EsActivo
//                }).ToList();

//                ViewBag.CriterioBusqueda = criterio;
//                return View("Index", ciudadanosDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al buscar ciudadanos: {ex.Message}";
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // GET: Ciudadano/Activos
//        public async Task<ActionResult> Activos()
//        {
//            try
//            {
//                var ciudadanos = await _ciudadanoService.GetCiudadanosActivosAsync();
//                var ciudadanosDto = ciudadanos.Select(c => new CiudadanoDto
//                {
//                    Id = c.Id,
//                    Nombre = c.Nombre,
//                    Apellido = c.Apellido,
//                    NumeroIdentificacion = c.NumeroIdentificacion,
//                    Email = c.Email,
//                    EsActivo = c.EsActivo
//                }).ToList();

//                ViewBag.SoloActivos = true;
//                return View("Index", ciudadanosDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = $"Error al cargar ciudadanos activos: {ex.Message}";
//                return View("Index", new List<CiudadanoDto>());
//            }
//        }
//    }
//}