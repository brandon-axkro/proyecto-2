using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("api/mi-proyecto")]
public class MiProyectoController : ControllerBase {

    [HttpGet("integrantes")]
    public ActionResult<MiProyecto> Integrantes() {
        var integrantes = new MiProyecto {
            NombreIntegrante1 = "Uriel Fermin",
            NombreIntegrante2 = "Joshua Ricardo"
        };
        return Ok(integrantes);
    }

    [HttpGet("presentacion")]
    public ActionResult Presentacion() {
       MongoClient client = new MongoClient(CadenaConexion.MONGO_DB);
       var db = client.GetDatabase("Escuela_Joshua_Uriel");
       var collection = db.GetCollection<Equipo>("Equipo");

      var item = collection.Find(FilterDefinition<Equipo>.Empty).FirstOrDefault();

        return Ok(item);
    }

}