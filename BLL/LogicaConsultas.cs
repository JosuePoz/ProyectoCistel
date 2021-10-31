using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using DAL;

namespace BLL
{
    public class LogicaConsultas
    {
        public DataTable ListarClientes()
        {
            ClassConsultas muestra = new ClassConsultas();
            string sql = "Select * From cliente";
            DataTable Tabla = muestra.Consulta(sql, null);
            return Tabla;
        }

        public DataTable BuscarCliente(string busca)
        {
            ClassConsultas Consul = new ClassConsultas();
            MySqlParameter[] parametros = new[]
            {
                new MySqlParameter("@nombre", busca)
            };
            string sql = "Select * From cliente where nombre_completo = @nombre";
            DataTable Tabla = Consul.Consulta(sql, parametros);
            return Tabla;
        }//Fin 
        public string GuardarCliente(string nombre, string direccionFactura, string remitente, string NIT, string Telefono)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "Insert Into cliente (nombre_completo, direccion_factura, direccion_remitente, nit, telefono) Values (@nombre, @direccionFactura, @remitente, @NIT, @Telefono)";
            MySqlParameter[] parametros = new[]
            { 
                    new MySqlParameter("@nombre", nombre),
                    new MySqlParameter("@direccionFactura", direccionFactura),
                    new MySqlParameter("@remitente", remitente),
                    new MySqlParameter("@NIT", NIT),
                    new MySqlParameter("@Telefono", Telefono)
            };
            resultado = Accion.Acciones(sql, parametros); 
            return resultado;
        }//fin GuardarCliente
        public string EliminarCliente(int id)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "DELETE FROM cliente where ClienteId = @id";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id.ToString()),
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }
        public string EditarCliente(int id, string nombre, string direccionFactura, string remitente, string telefono, string NIT)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "UPDATE cliente SET nombre_completo = @nombre_completo, direccion_factura= @direccion_factura, direccion_remitente= @direccion_remitente, nit= @nit, telefono= @telefono Where ClienteId = @id";

            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id.ToString()),
                    new MySqlParameter("@nombre_completo", nombre),
                    new MySqlParameter("@direccion_factura", direccionFactura),
                    new MySqlParameter("@direccion_remitente", remitente),
                    new MySqlParameter("@nit", NIT),
                    new MySqlParameter("@telefono", telefono)
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }

        public DataTable ListarDepartamentos()
        {
            ClassConsultas muestra = new ClassConsultas();
            string sql = "Select * From departamento";
            DataTable Tabla = muestra.Consulta(sql, null);
            return Tabla;
        }

        public DataTable BuscarDepartamento(string busca)
        {
            ClassConsultas Consul = new ClassConsultas();
            MySqlParameter[] parametros = new[]
            {
                new MySqlParameter("@nombre", busca)
            };
            string sql = "Select * From departamento where nombre_departamento = @nombre";
            DataTable Tabla = Consul.Consulta(sql, parametros);
            return Tabla;
        }//Fin 

        public string GuardarDepartamento(string nombre)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "Insert Into departamento (nombre_departamento) Values (@nombre)";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@nombre", nombre),
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }//fin GuardarDepartmento

        public string EliminarDepartamento(int id)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = " DELETE FROM municipio where DepartamentoId = @id; DELETE FROM departamento where DepartamentoId = @id;";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id.ToString()),
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }

        public string EditarDepartamento(int id, string nombre)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "UPDATE departamento SET nombre_departamento = @nombre_completo Where DepartamentoId = @id";

            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id.ToString()),
                    new MySqlParameter("@nombre_completo", nombre),
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }
        public DataTable ListarMunicipios()
        {
            ClassConsultas muestra = new ClassConsultas();
            string sql = "SELECT municipio.MunicipioId, municipio.nombre_municipio, departamento.nombre_departamento FROM municipio INNER JOIN departamento ON departamento.DepartamentoId = municipio.DepartamentoId;";
            DataTable Tabla = muestra.Consulta(sql, null);
            return Tabla;
        }
        public DataTable BuscarMunicipio(string busca)
        {
            ClassConsultas Consul = new ClassConsultas();
            MySqlParameter[] parametros = new[]
            {
                new MySqlParameter("@nombre", busca)
            };
            string sql = "SELECT municipio.MunicipioId, municipio.nombre_municipio, departamento.nombre_departamento FROM municipio INNER JOIN departamento ON departamento.DepartamentoId = municipio.DepartamentoId WHERE nombre_municipio =  @nombre OR nombre_departamento = @nombre;";
            DataTable Tabla = Consul.Consulta(sql, parametros);
            return Tabla;
        }//Fin 

        public string GuardarMunicipio(string nombre, string idDepartamento)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "Insert Into municipio (nombre_municipio, DepartamentoId) Values (@nombre, @DepartamentoId)";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@nombre", nombre),
                    new MySqlParameter("@DepartamentoId", idDepartamento)

            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }//fin GuardarDepartmento

        public string EditarMunicipio(string id, string nombre, string idDepartamento)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "UPDATE municipio SET nombre_municipio = @nombre_completo, DepartamentoId = @DepartamentoId Where MunicipioId = @id";

            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@nombre_completo", nombre),
                    new MySqlParameter("@DepartamentoId", idDepartamento),
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }
        public string EliminarMunicipio(string id)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "DELETE FROM municipio where MunicipioId = @id";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id),
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }
        public DataTable ListarEnvios()
        {
            ClassConsultas muestra = new ClassConsultas();
            string sql = "SELECT envio.EnvioId, cliente.nombre_completo, paquete.nombre_destinatario, envio.fecha_envio, envio.valor_envio, envio.estado FROM envio INNER JOIN cliente ON cliente.ClienteId = envio.ClienteId INNER JOIN paquete ON paquete.PaqueteId = envio.PaqueteId;";
            DataTable Tabla = muestra.Consulta(sql, null);
            return Tabla;
        }

        public DataTable BuscarEnvios(string busca)
        {
            string sql = "SELECT envio.EnvioId, cliente.nombre_completo, paquete.nombre_destinatario, envio.fecha_envio, envio.valor_envio, envio.estado FROM envio INNER JOIN cliente ON cliente.ClienteId = envio.ClienteId INNER JOIN paquete ON paquete.PaqueteId = envio.PaqueteId WHERE  (fecha_envio = @fecha_envio OR @fecha_envio IS NULL);";

            ClassConsultas Consul = new ClassConsultas();
            MySqlParameter[] parametros = new[]
            {
                new MySqlParameter("@fecha_envio", busca)
            };
            DataTable Tabla = Consul.Consulta(sql, parametros);
            return Tabla;
        }//Fin 

        public string GuardarEnvios(string ClienteId, string PaqueteId, string fecha, string valor, string estado)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "Insert Into envio (ClienteId, PaqueteId, fecha_envio, valor_envio, estado) Values (@ClienteId, @PaqueteId, @fecha_envio, @valor_envio, @estado)";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@ClienteId", ClienteId),
                    new MySqlParameter("@PaqueteId", PaqueteId),
                    new MySqlParameter("@fecha_envio", fecha),
                    new MySqlParameter("@valor_envio", valor),
                    new MySqlParameter("@estado", estado)

            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }//fin GuardarDepartmento

        public string EditarEnvios(string id, string ClienteId, string PaqueteId, string fecha, string valor, string estado)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "UPDATE envio SET ClienteId = @ClienteId, PaqueteId = @PaqueteId, fecha_envio = @fecha_envio, valor_envio = @valor_envio, estado = @estado  Where EnvioId = @id";

            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@ClienteId", ClienteId),
                    new MySqlParameter("@PaqueteId", PaqueteId),
                    new MySqlParameter("@fecha_envio", fecha),
                    new MySqlParameter("@valor_envio", valor),
                    new MySqlParameter("@estado", estado)
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }

        public DataTable ListarPaquetes()
        {
            ClassConsultas muestra = new ClassConsultas();
            string sql = "SELECT paquete.PaqueteId, municipio.nombre_municipio, paquete.descripcion, paquete.peso_libras, paquete.nombre_destinatario, paquete.direccion_destino FROM paquete INNER JOIN municipio ON municipio.MunicipioId = paquete.MunicipioId;";
            DataTable Tabla = muestra.Consulta(sql, null);
            return Tabla;
        }

        public DataTable BuscaPaquetes(string busca)
        {
            string sql = "SELECT paquete.PaqueteId, municipio.nombre_municipio, paquete.descripcion, paquete.peso_libras, paquete.nombre_destinatario, paquete.direccion_destino FROM paquete INNER JOIN municipio ON municipio.MunicipioId = paquete.MunicipioId WHERE nombre_destinatario = @nombre_destinatario";

            ClassConsultas Consul = new ClassConsultas();
            MySqlParameter[] parametros = new[]
            {
                new MySqlParameter("@nombre_destinatario", busca),
            };
            DataTable Tabla = Consul.Consulta(sql, parametros);
            return Tabla;
        }//Fin 

        public string GuardarPaquetes(string MunicipioId, string descripcion, string destinatario, string direccion, string peso_libras)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "Insert Into paquete (MunicipioId, descripcion, nombre_destinatario, direccion_destino, peso_libras) Values (@MunicipioId, @descripcion, @nombre_destinatario, @direccion_destino, @peso_libras)";
            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@MunicipioId", MunicipioId),
                    new MySqlParameter("@descripcion", descripcion),
                    new MySqlParameter("@nombre_destinatario", destinatario),
                    new MySqlParameter("@direccion_destino", direccion),
                    new MySqlParameter("@peso_libras", peso_libras)

            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }//fin GuardarDepartmento

        public string EditarPaquete(string id, string MunicipioId, string descripcion, string destinatario, string direccion, string peso_libras)
        {
            string resultado, sql;
            ClassConsultas Accion = new ClassConsultas();
            sql = "UPDATE paquete SET MunicipioId = @MunicipioId, descripcion = @descripcion, nombre_destinatario = @nombre_destinatario, direccion_destino = @direccion_destino, peso_libras = @peso_libras  Where PaqueteId = @id";

            MySqlParameter[] parametros = new[]
            {
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@MunicipioId", MunicipioId),
                    new MySqlParameter("@descripcion", descripcion),
                    new MySqlParameter("@nombre_destinatario", destinatario),
                    new MySqlParameter("@direccion_destino", direccion),
                    new MySqlParameter("@peso_libras", peso_libras)
            };
            resultado = Accion.Acciones(sql, parametros);
            return resultado;
        }
    }
}
