using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

[ApiController]
[Route("api/usuarios")]
public class ApiUsuariosController : ControllerBase{

    private readonly IMongoCollection<Usuario> collection;

    public ApiUsuariosController(){
        MongoClient client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Escuela_Joshua_Uriel");
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
    public IActionResult Delete(string id)
    {
        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item != null)
        {
            this.collection.DeleteOne(filter);
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Create(UsuarioRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Correo))
        {
            return BadRequest("El correo es REQUERIDO");
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest("La clave es REQUERIDA");
        }
         if (string.IsNullOrWhiteSpace(model.Nombre))
        {
            return BadRequest("El nombre es REQUERIDO");
        }

        var filter = Builders<Usuario>.Filter.Eq(x => x.Correo, model.Correo);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item != null)
        {
            return BadRequest("El correo " + model.Correo + " ya existe en la base de datos");
        }

        Usuario bd = new Usuario();
        bd.Nombre = model.Nombre;
        bd.Correo = model.Correo;
        bd.Password = model.Password;

        this.collection.InsertOne(bd);
          
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item == null)
        {
            return NotFound("No existe un usuario con el Id proporcionado");
        }

        return Ok(item);
    }

     [HttpPut("{id}")]
    public IActionResult Update(string id, UsuarioRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Correo))
        {
            return BadRequest("El correo es REQUERIDO");
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest("La clave es REQUERIDA");
        }
         if (string.IsNullOrWhiteSpace(model.Nombre))
        {
            return BadRequest("El nombre es REQUERIDO");
        }

        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item == null)
        {
            return NotFound("No existe un usuario con el Id proporcionado");
        }

        var filterCorreo = Builders<Usuario>.Filter.Eq(x => x.Correo, model.Correo);
        var itemExistente = this.collection.Find(filterCorreo).FirstOrDefault();
        if(itemExistente != null && itemExistente.Id != id)
        {
            return BadRequest("El correo " + model.Correo + " ya existe en la base de datos");
        }

        var updateOptions = Builders<Usuario>.Update
        .Set(x => x.Correo, model.Correo)
        .Set(x => x.Nombre, model.Nombre)
        .Set(x => x.Password, model.Password);

        this.collection.UpdateOne(filter, updateOptions);
          
        return Ok();
    }
}