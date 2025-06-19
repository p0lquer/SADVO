//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SADVO.Application.Interface.Service;
//using SADVO.Application.DTOs.Usuarios;
//using SADVO.Application.ViewModels.Usuario;
//using SADVO.Domain.Entities;
//using SADVO.Domain.Enumns;

//namespace SADVOWeb.Controllers
//{
//    public class UsuarioController : Controller
//    {
//        private readonly IUsuariosService _userServices;
//        private readonly IUsuariosSeccion _userSeccion;

//        public UsuarioController(IUsuariosService usuariosService, IUsuariosSeccion usuariosSeccion)
//        {
//            _userServices = usuariosService ?? throw new ArgumentNullException(nameof(usuariosService));
//            _userSeccion = usuariosSeccion ?? throw new ArgumentNullException(nameof(usuariosSeccion));
//        }

//        // GET: Usuario
//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                var usuarios = await _userServices.GetAllAsync();
//                var usuariosViewModel = usuarios.Select(u => new UserViewModel()
//                {
//                    Id = u.Id,
//                    Nombre = u.Nombre,
//                    EsActivo = u.EsActivo,
//                    Apellido = u.Apellido,
//                    Email = u.Email,
//                    Telefono = u.Telefono,
//                    Password = u.Password,
//                    ConfirmationPassword = u.ConfirmationPassword,
//                    Foto = u.Foto,
//                    Role = u.Role
//                }).ToList();

//                return View(usuariosViewModel);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar los usuarios: " + ex.Message;
//                return View(new List<UserViewModel>());
//            }
//        }

//        // GET: Usuario/Details/5
//        public async Task<ActionResult> Details(int id)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }

//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID inválido";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuario = await _userServices.GetUsuarioConRolesAsync(id);
//                if (usuario == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuarioDto = new UsuarioDto
//                {
//                    Id = usuario.Id,
//                    Nombre = usuario.Nombre, 
//                    EsActivo = usuario.EsActivo,
//                    Apellido = usuario.Apellido,
//                    Email = usuario.Email,
//                    Password = usuario.Password,
//                    ConfirmationPassword = usuario.ConfirmationPassword,
//                    Telefono = usuario.Telefono,
//                    Foto = usuario.Foto,
//                    Role = (int)usuario.Role
//                };

//                return View(usuarioDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar los detalles del usuario: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // GET: Usuario/Create
//        public ActionResult Create()
//        {
//            if (!_userSeccion.HasUser())
//            {
//                return RedirectToRoute(new { controller = "Login", action = "Index" });
//            }
//            if (!_userSeccion.IsAdmin())
//            {
//                return RedirectToRoute(new { controller = "Home", action = "Index" });
//            }

//            var createDto = new CreateUsuarioDto
//            {
//                Nombre = string.Empty, // Fix: Set the required 'Nombre' property
//                EsActivo = true,       // Fix: Set the required 'EsActivo' property
//                Apellido = string.Empty,
//                Email = string.Empty,
//                Password = string.Empty,
//                ConfirmationPassword = string.Empty,
//                Role = (int)RolUsuario.UsuarioComun 
//            };

//            ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                .Cast<RolUsuario>()
//                .Select(r => new { Value = (int)r, Text = r.ToString() })
//                .ToList();

//            return View(createDto);
//        }

//        // POST: Usuario/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(CreateUsuarioDto createDto)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                if (!ModelState.IsValid)
//                {
//                    ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                        .Cast<RolUsuario>()
//                        .Select(r => new { Value = (int)r, Text = r.ToString() })
//                        .ToList();
//                    return View(createDto);
//                }

//                // Validar email único
//                var existeEmail = await _userServices.ValidarEmailUnicoAsync(createDto.Email);
//                if (existeEmail)
//                {
//                    ModelState.AddModelError("Email", "Ya existe un usuario con este email");
//                    ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                        .Cast<RolUsuario>()
//                        .Select(r => new { Value = (int)r, Text = r.ToString() })
//                        .ToList();
//                    return View(createDto);
//                }

//                // Validar que las contraseñas coincidan
//                var passwordsCoinciden = await _userServices.ValidarPasswordsCoincidanAsync(createDto.Password, createDto.ConfirmationPassword);
//                if (!passwordsCoinciden)
//                {
//                    ModelState.AddModelError("ConfirmationPassword", "Las contraseñas no coinciden");
//                    ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                        .Cast<RolUsuario>()
//                        .Select(r => new { Value = (int)r, Text = r.ToString() })
//                        .ToList();
//                    return View(createDto);
//                }

//                // Crear nueva entidad
//                var nuevoUsuario = new Usuarios
//                {
//                    Nombre = createDto.Nombre ?? string.Empty,
//                    Apellido = createDto.Apellido,
//                    Email = createDto.Email,
//                    Password = createDto.Password, // En producción, hashear la contraseña
//                    ConfirmationPassword = createDto.ConfirmationPassword,
//                    Telefono = createDto.Telefono,
//                    Foto = createDto.Foto,
//                    Role = (RolUsuario)createDto.Role,
//                    RolUsuario = (RolUsuario)createDto.Role,
//                    EsActivo = true
//                };

//                await _userServices.CreateAsync(nuevoUsuario);
//                TempData["Success"] = "Usuario creado exitosamente";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al crear el usuario: " + ex.Message;
//                ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                    .Cast<RolUsuario>()
//                    .Select(r => new { Value = (int)r, Text = r.ToString() })
//                    .ToList();
//                return View(createDto);
//            }
//        }

//        // GET: Usuario/Edit/5
//        public async Task<ActionResult> Edit(int id)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID inválido";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuario = await _userServices.GetByIdAsync(id);
//                if (usuario == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuarioDto = new UsuarioDto
//                {
//                    Id = usuario.Id,
//                    Nombre = usuario.Nombre, // Fix: Set the required 'Nombre' property
//                    EsActivo = usuario.EsActivo,
//                    Apellido = usuario.Apellido,
//                    Email = usuario.Email,
//                    Password = usuario.Password,
//                    ConfirmationPassword = usuario.ConfirmationPassword,
//                    Telefono = usuario.Telefono,
//                    Foto = usuario.Foto,
//                    Role = (int)usuario.Role
//                };

//                ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                    .Cast<RolUsuario>()
//                    .Select(r => new { Value = (int)r, Text = r.ToString() })
//                    .ToList();

//                return View(usuarioDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar el usuario: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Usuario/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit(int id, UsuarioDto usuarioDto)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                if (id != usuarioDto.Id)
//                {
//                    TempData["Error"] = "ID inconsistente";
//                    return View(usuarioDto);
//                }

//                if (!ModelState.IsValid)
//                {
//                    ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                        .Cast<RolUsuario>()
//                        .Select(r => new { Value = (int)r, Text = r.ToString() })
//                        .ToList();
//                    return View(usuarioDto);
//                }

//                var usuarioActual = await _userServices.GetByIdAsync(id);
//                if (usuarioActual == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Validar email único solo si cambió
//                if (!string.Equals(usuarioActual.Email, usuarioDto.Email, StringComparison.OrdinalIgnoreCase))
//                {
//                    var existeEmail = await _userServices.ValidarEmailUnicoAsync(usuarioDto.Email);
//                    if (existeEmail)
//                    {
//                        ModelState.AddModelError("Email", "Ya existe un usuario con este email");
//                        ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                            .Cast<RolUsuario>()
//                            .Select(r => new { Value = (int)r, Text = r.ToString() })
//                            .ToList();
//                        return View(usuarioDto);
//                    }
//                }

//                // Validar que las contraseñas coincidan
//                var passwordsCoinciden = await _userServices.ValidarPasswordsCoincidanAsync(usuarioDto.Password, usuarioDto.ConfirmationPassword);
//                if (!passwordsCoinciden)
//                {
//                    ModelState.AddModelError("ConfirmationPassword", "Las contraseñas no coinciden");
//                    ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                        .Cast<RolUsuario>()
//                        .Select(r => new { Value = (int)r, Text = r.ToString() })
//                        .ToList();
//                    return View(usuarioDto);
//                }

//                // Actualizar entidad
//                usuarioActual.Apellido = usuarioDto.Apellido;
//                usuarioActual.Email = usuarioDto.Email;
//                usuarioActual.Password = usuarioDto.Password; // En producción, hashear si cambió
//                usuarioActual.ConfirmationPassword = usuarioDto.ConfirmationPassword;
//                usuarioActual.Telefono = usuarioDto.Telefono;
//                usuarioActual.Foto = usuarioDto.Foto;
//                usuarioActual.Role = (RolUsuario)usuarioDto.Role;
//                usuarioActual.RolUsuario = (RolUsuario)usuarioDto.Role;

//                await _userServices.UpdateAsync(usuarioActual);
//                TempData["Success"] = "Usuario actualizado exitosamente";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al actualizar el usuario: " + ex.Message;
//                ViewBag.Roles = Enum.GetValues(typeof(RolUsuario))
//                    .Cast<RolUsuario>()
//                    .Select(r => new { Value = (int)r, Text = r.ToString() })
//                    .ToList();
//                return View(usuarioDto);
//            }
//        }

//        // GET: Usuario/Delete/5
//        public async Task<ActionResult> Delete(int id)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID inválido";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuario = await _userServices.GetByIdAsync(id);
//                if (usuario == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuarioDto = new UsuarioDto
//                {
//                    Id = usuario.Id,
//                    Nombre = usuario.Nombre, // Fix: Set the required 'Nombre' property
//                    EsActivo = usuario.EsActivo,
//                    Apellido = usuario.Apellido,
//                    Email = usuario.Email,
//                    Password = usuario.Password,
//                    ConfirmationPassword = usuario.ConfirmationPassword,
//                    Telefono = usuario.Telefono,
//                    Foto = usuario.Foto,
//                    Role = (int)usuario.Role
//                };

//                return View(usuarioDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar el usuario: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Usuario/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID inválido";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuario = await _userServices.GetByIdAsync(id);
//                if (usuario == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Soft delete - desactivar usuario
//                usuario.EsActivo = false;
//                await _userServices.UpdateAsync(usuario);

//                TempData["Success"] = "Usuario desactivado exitosamente";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al desactivar el usuario: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // GET: Usuario/ChangePassword/5
//        public async Task<ActionResult> ChangePassword(int id)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }

//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID inválido";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuario = await _userServices.GetByIdAsync(id);
//                if (usuario == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                var changePasswordDto = new ChangePasswordDto
//                {
//                    UsuarioId = id,
//                    PasswordActual = string.Empty,
//                    PasswordNuevo = string.Empty,
//                    ConfirmarPasswordNuevo = string.Empty
//                };

//                return View(changePasswordDto);
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cargar el formulario de cambio de contraseña: " + ex.Message;
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Usuario/ChangePassword/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ChangePassword(int id, ChangePasswordDto changePasswordDto)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }

//                if (id != changePasswordDto.UsuarioId)
//                {
//                    TempData["Error"] = "ID inconsistente";
//                    return View(changePasswordDto);
//                }

//                if (!ModelState.IsValid)
//                {
//                    return View(changePasswordDto);
//                }

//                // Validar que las contraseñas nuevas coincidan
//                var passwordsCoinciden = await _userServices.ValidarPasswordsCoincidanAsync(
//                    changePasswordDto.PasswordNuevo,
//                    changePasswordDto.ConfirmarPasswordNuevo);

//                if (!passwordsCoinciden)
//                {
//                    ModelState.AddModelError("ConfirmarPasswordNuevo", "Las contraseñas nuevas no coinciden");
//                    return View(changePasswordDto);
//                }

//                var cambioExitoso = await _userServices.CambiarPasswordAsync(
//                    id,
//                    changePasswordDto.PasswordActual,
//                    changePasswordDto.PasswordNuevo);

//                if (!cambioExitoso)
//                {
//                    ModelState.AddModelError("PasswordActual", "La contraseña actual es incorrecta");
//                    return View(changePasswordDto);
//                }

//                TempData["Success"] = "Contraseña cambiada exitosamente";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al cambiar la contraseña: " + ex.Message;
//                return View(changePasswordDto);
//            }
//        }

//        // GET: Usuario/Activate/5
//        public async Task<ActionResult> Activate(int id)
//        {
//            try
//            {
//                if (!_userSeccion.HasUser())
//                {
//                    return RedirectToRoute(new { controller = "Login", action = "Index" });
//                }
//                if (!_userSeccion.IsAdmin())
//                {
//                    return RedirectToRoute(new { controller = "Home", action = "Index" });
//                }

//                if (id <= 0)
//                {
//                    TempData["Error"] = "ID inválido";
//                    return RedirectToAction(nameof(Index));
//                }

//                var usuario = await _userServices.GetByIdAsync(id);
//                if (usuario == null)
//                {
//                    TempData["Error"] = "Usuario no encontrado";
//                    return RedirectToAction(nameof(Index));
//                }

//                usuario.EsActivo = true;
//                await _userServices.UpdateAsync(usuario);

//                TempData["Success"] = "Usuario activado exitosamente";
//            }
//            catch (Exception ex)
//            {
//                TempData["Error"] = "Error al activar el usuario: " + ex.Message;
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}