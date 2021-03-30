using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Competicao.Models.Infra;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Competicao.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public readonly ILogger _logger;

        public InfraController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<InfraController> Logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = Logger;
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrarNovoUsuario(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNovoUsuario(RegistrarNovoUsuarioViewModel model, IFormFile foto, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var stream = new MemoryStream();
                await foto.CopyToAsync(stream);
                model.Foto = stream.ToArray();
                model.FotoMimeType = foto.ContentType;

                var user = new Usuario { UserName = model.Email, Email = model.Email, Foto = model.Foto, FotoMimeType = model.FotoMimeType };
                var result = await _userManager.CreateAsync(user, model.Senha);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário criou uma nova conta com senha.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var logado = await _signInManager.PasswordSignInAsync(user.Email, model.Senha, false, false);
                    _logger.LogInformation("logado " + logado.Succeeded);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("Usuário acessou com a conta criada.");

                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            _logger.LogInformation("returURL" + returnUrl);
            _logger.LogInformation("isLocal" + Url.IsLocalUrl(returnUrl));

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuário realizou logout.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acessar(AcessarViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.ManterConectado, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário Autenticado.");
                    return RedirectToLocal(returnUrl);
                }
            }
            ModelState.AddModelError(string.Empty, "Falha na tentativa de login.");
            return View(model);
        }

        public async Task<FileContentResult> GetFoto()
        {
            var id = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(id);

            return File(user.Foto, user.FotoMimeType);
        }
    }
}