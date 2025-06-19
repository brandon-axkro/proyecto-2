using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("api/mi-proyecto")]
public class MiProyectoController : ControllerBase {

    [HttpGet("integrantes")]
    public ActionResult<MiProyecto> Integrantes() {
        var integrantes = new MiProyecto {
            NombreIntegrante1 = "Brandon",
            NombreIntegrante2 = "Isis"
        };
        return Ok(integrantes);
    }

    [HttpGet("presentacion")]
    public ActionResult Presentacion() {
       MongoClient client = new MongoClient(CadenaConexion.MONGO_DB);
       var db = client.GetDatabase("Equipo_isis_brandon");
       var collection = db.GetCollection<Equipo>("Equipo");

      var item = collection.Find(FilterDefinition<Equipo>.Empty).FirstOrDefault();

        return Ok(item);
    }

}