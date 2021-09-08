using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Concesionario
{
    class Datos
    {
        OleDbConnection Conexion = new OleDbConnection();
        OleDbCommand Comando = new OleDbCommand();
        OleDbDataReader Lector;
        string cadenaConexion = @"Provider=SQLNCLI11;Data Source=DESKTOP-O00CJ4U;Integrated Security=SSPI;Initial Catalog=Concesionario";

        public Datos()
        {
            Conexion = new OleDbConnection();
            Conexion.ConnectionString = CadenaConexion;
        }

        public Datos(string cadenaConexion)
        {
            Conexion = new OleDbConnection();
            Comando = new OleDbCommand();
        }

        public OleDbDataReader Lector1 { get => Lector; set => Lector = value; }
        public string CadenaConexion { get => cadenaConexion; set => cadenaConexion = value; }

        public void conectar()
        {
            Conexion.ConnectionString = cadenaConexion;
            Conexion.Open();
            Comando.Connection = Conexion;
            Comando.CommandType = CommandType.Text;
        }

        public void desconectar()
        {
            Conexion.Close();
            Conexion.Dispose();
        }

        public DataTable consultarTabla(string nombreTabla)
        {
            conectar();
            Comando.CommandText = "select * from " + nombreTabla;
            DataTable tabla = new DataTable();
            tabla.Load(Comando.ExecuteReader());

            desconectar();
            return tabla;
        }

        public void leerTabla(string nombreTabla)
        {
            conectar();

            Comando.CommandText = "select * from " + nombreTabla;
            Lector = Comando.ExecuteReader();
        }

        public void actualizar(string consultaSQL)
        {
            conectar();
            Comando.CommandText = consultaSQL;
            Comando.ExecuteNonQuery();//cantidad de filas afectadas
            desconectar();
        }
    }
}
