using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

[ApiController]
[Route("api/usuarios")]
public class ApiUsuariosController : ControllerBase{

    private readonly IMongoCollection<Usuario> collection;

    public ApiUsuariosController(){
        MongoClient client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("equipo_brandon_isis");
        this.collection = db.GetCollection<Usuario>("Usuarios");
    }

    [HttpGet]
    public IActionResult ListarUsuarios(string? texto){
        var filter = FilterDefinition<Usuario>.Empty;
        if (!string.IsNullOrWhiteSpace(texto))
        {
            var filterNombre = Builders<Usuario>.Filter.Regex(u => u.Nombre, new BsonRegularExpression(texto, "i"));
            var filterCorreo = Builders<Usuario>.Filter.Regex(u => u.Correo, new BsonRegularExpression(texto, "i"));

            filter = Builders<Usuario>.Filter.Or(filterNombre, filterCorreo);
        }
        var list = this.collection.Find(filter).ToList();

        return Ok(list);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(string id){
        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item != null){
            this.collection.DeleteOne(filter);
        }

        return NoContent();
    }

   [HttpPost] 
   public IActionResult Create(UsuarioRequest model )
   {
   
       Usuario bd = new Usuario();
       bd.Nombre = model.Nombre;
       bd.Correo = model.Correo;
       bd.Password = model.Password;

       this.collection.InsertOne(bd); 







        return Ok();

   }

}