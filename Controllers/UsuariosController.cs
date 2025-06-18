using Microsoft.AspNetCore.Mvc;

[Route("usuarios")]
public class UsuariosController : Controller
{
   public IActionResult index()
   {
      return View();
   }

   [Route("editar/{id}")]
   public IActionResult Editar(string? id)
   {

      ViewBag.ID = id;
      return View();
   }
}