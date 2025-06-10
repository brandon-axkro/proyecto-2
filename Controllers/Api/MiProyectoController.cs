using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("api/mi-proyecto")]
public class MiProyectoController : ControllerBase {

    [HttpGet("integrantes")]
    public ActionResult<MiProyecto> Integrantes() {
        var integrantes = new MiProyecto {
            NombreIntegrante1 = "IsisMg",
            NombreIntegrante2 = "brandon-axkro"
        };
        return Ok(integrantes);
    }

    [HttpGet("presentacion")]
    public ActionResult Presentacion() {
       MongoClient client = new MongoClient(CadenaConexion.MONGO_DB);
       var db = client.GetDatabase("Escuela_Isis_brandon");
       var collection = db.GetCollection<Equipo>("Equipo");

       var lista = collection.Find(FilterDefinition<Equipo>.Empty).ToList();

       return Ok(lista);
    }

}