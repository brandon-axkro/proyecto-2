import { useEffect, useState } from "react"

type Usuario = {
    "Id": string,
    "Nombre": string,
    "Correo": string,
    "Clave": string
}
const Usuario = () => {
    const [registro, setRegistros] = useState<Usuario[]>([]);
    const [texto, setTexto] = useState("");

    const listarRegistro = async () => {
        let url = "/api/usuarios";    
        if(texto){
            url += "?texto=" + texto;
        }

        const resp = await fetch(url);
        if(resp.ok){
            const datos = await resp.json();
            setRegistros(datos);
        }
    }

    useEffect(()=>{
        listarRegistro();
    }, []);

    const Buscar = () => {
        listarRegistro();
    }

    const eliminar = async(item: Usuario) => {
        if(!confirm("¿Desea eliminar el usuario " + item.Nombre + "?")){
            return;
        }

        const resp = await fetch("/api/usuarios/" + item.Id, {
            method: "DELETE"
        });

        if (resp.ok) {
            listarRegistro();
        }
        else{
            const error = await resp.text();
            console.log(error);
            alert("Ocurrio un error al eliminar el registro");
        }
    }

    return(
        <>
        <div className="container">
            <h1>Usuarios</h1>
        </div>
        <div className="container">
            <div className="card">
                <div className="card-header">Busqueda</div>
                <div className="card-body">
                    <div className="row">
                        <div className="col-12">
                            <div className="mb-3">
                                <label>Busqueda de Usuarios</label>
                                <input type="text" className="form-control" 
                                placeholder="Introduce el nombre o el correo"
                                onChange={(e) => setTexto(e.target.value)} 
                                />
                            </div>
                        </div>
                        <div className="col-12">
                            <button className="btn btn-primary" onClick={Buscar}>Buscar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div className="container mt-4">
            <div className="card">
                <div className="card-header">Usuarios Existentes</div>
                <div className="card-body">
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th>No.</th>
                                <th>Nombre</th>
                                <th>Correo</th>
                                <th>Clave</th>
                            </tr>
                        </thead>
                        {
                            registro.length === 0 &&
                            <tbody>
                                <tr>
                                    <td colSpan={5}>No hay registros para mostrar</td>
                                </tr>
                            </tbody>
                        }
                        {
                            registro.length > 0 &&
                            <tbody>
                                {
                                    registro.map((item, index) =>
                                    <tr>
                                        <td>{index + 1}</td>
                                        <td>{item.Nombre}</td>
                                        <td>{item.Correo}</td>
                                        <td>{item.Clave}</td>
                                        <td className="d-flex gap-2">
                                            <a className="btn btn-primary" href={"/usuarios/" + item.Id}>Editar</a>
                                            <button className="btn btn-danger" onClick={() => eliminar(item)}>Eliminar</button>
                                        </td>
                                    </tr>)
                                }
                            </tbody>
                        }
                    </table>
                </div>
            </div>
        </div>
        </>
    )
}
export default Usuario;