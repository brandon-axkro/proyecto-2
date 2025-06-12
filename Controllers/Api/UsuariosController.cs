
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    // Metodos para hacer las operaciones CRUD
    // C = Create
    // R = Read
    // U = Update
    // D = Delate
    
    private readonly IMongoCollection<Usuario> collection;
    public UsuariosController()
    {
     var db = client.GetDatabase("Escuela_Isis_brandon");
     this.var collection = db.GetCollection<Usuario>("Equipo");

    }    
    
    [HttpGet]
    public IActionResult ListarUsiarios()
    {
        var filter = FilterDefinition<Usuario>.Empty;
        var list = this.collection.Find(filter).Tolist();
        return Ok(list);
    }
}