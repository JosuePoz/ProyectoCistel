﻿using System;
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
    }
}