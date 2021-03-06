﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Capa_Datos
{
    public class Conexion_Bases_Datos
    {
        public static NpgsqlConnection conexion;
        public static NpgsqlCommand cmd;
        string resultado_Query = string.Empty;

        public static void Conexion_General()
        {
            string servidor = "localhost";
            int puerto = 5432;
            string usuario = "postgres";
            string claveAnthonny = "1414250816ma";
            string claveBryan = "bryan2748245"; 
            string claveRoger = "Saborio17";
            string baseDatos = "postgres";

            string cadenaConexion = "Server=" + servidor + ";" + "Port=" + puerto + ";" + "User Id=" + usuario + ";" + "Password=" + claveRoger + ";" + "Database=" + baseDatos;
            conexion = new NpgsqlConnection(cadenaConexion);

            if (conexion != null)
            {

                Console.WriteLine("Conexion con la DB nombre : " + baseDatos + " , Exitosa!!");
            }
            else
            {

                Console.WriteLine("Error en la conexion con la DB");
            }
        }

        public static void Conexion_Tablas(string db )
        {
            string servidor = "localhost";
            int puerto = 5432;
            string usuario = "postgres";
            string claveAnthonny = "1414250816ma";
            string claveBryan = "bryan2748245";
            string claveRoger = "Saborio17";
 
            string cadenaConexion = "Server=" + servidor + ";" + "Port=" + puerto + ";" + "User Id=" + usuario + ";" + "Password=" + claveRoger + ";" + "Database=" + db;
            conexion = new NpgsqlConnection(cadenaConexion);
            if (conexion != null)
            {
                Console.WriteLine("Conexion con la DB nombre : " + db+ " , Exitosa!!");
            }
            else
            {
                Console.WriteLine("Error en la conexion con la DB");
            }
        }

        //Bases de datos
        public static ArrayList ConsultarInformacionDBSistema()
        {
            ArrayList informacionDB = new ArrayList();
            Conexion_General();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT datname FROM pg_database", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    if ((lectorConsulta.GetString(0).Equals("template0") == false) && (lectorConsulta.GetString(0).Equals("template1") == false))
                    {
                        informacionDB.Add(lectorConsulta.GetString(0));
                    }
                }
            }
            conexion.Close();
            return informacionDB;
        }

        //Users
        public static ArrayList ConsultarInformacionUsersSistema()
        {
            ArrayList informacionUsers = new ArrayList();
            Conexion_General();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT rolname FROM pg_roles", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionUsers.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionUsers;
        }

        //TableSpaces
        public static ArrayList ConsultarInformacionTableSpaceSistema()
        {
            ArrayList informacionTS = new ArrayList();
            Conexion_General();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT spcname FROM pg_tablespace", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionTS.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionTS;
        }

        //Schemas
        public static ArrayList ConsultarInformacionSchemas(string daba)
        {
            ArrayList informacionSchemas = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT nspname FROM pg_catalog.pg_namespace WHERE nspname like 'pg_%' = false AND nspname != 'information_schema'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionSchemas.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionSchemas;
        }

        //Tables
        public static ArrayList ConsultarInformacionTablasDB(string daba, string schema)
        {
            ArrayList informacionDB = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = '"+ schema + "' AND table_type='BASE TABLE'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionDB.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionDB;
        }
        //SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';

        //Triggers
        public static ArrayList ConsultarInformacionTrigger(string daba, string schema)
        {
            ArrayList informacionTrigger = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT trigger_name FROM information_schema.triggers WHERE trigger_schema = '" + schema + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionTrigger.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionTrigger;
        }

        //Fuctions
        public static ArrayList ConsultarInformacionFuctions(string daba, string schema)
        {
            ArrayList informacionFuctions = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT proname FROM pg_catalog.pg_namespace n JOIN pg_catalog.pg_proc p ON pronamespace = n.oid WHERE nspname = '" + schema +"'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionFuctions.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionFuctions;
        }

        //Sequences
        public static ArrayList ConsultarInformacionSequences(string daba, string schema)
        {
            ArrayList informacionSequence = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT sequence_name FROM information_schema.sequences WHERE sequence_schema = '" + schema + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionSequence.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionSequence;
        }

        //Views
        public static ArrayList ConsultarInformacionViews(string daba, string schema)
        {
            ArrayList informacionViews = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT table_name FROM information_schema.views WHERE table_schema ='" + schema + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionViews.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionViews;
        }

        //Columns
        public static ArrayList ConsultarInformacionColumns(string daba, string schema, string table)
        {
            ArrayList informacionColumns = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT column_name FROM information_schema.columns WHERE table_schema ='" + schema + "' AND table_name = '" + table + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionColumns.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionColumns;
        }

        //Index
        public static ArrayList ConsultarInformacionIndex(string daba, string schema, string table)
        {
            ArrayList informacionIndex = new ArrayList();
            Conexion_Tablas(daba);
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT indexname FROM pg_indexes WHERE schemaname = '" + schema + "' AND tablename = '" + table + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    informacionIndex.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();
            return informacionIndex;
        }

        /// <summary>
        /// This metod create data bases
        /// </summary>
        /// <param name="nombreDB">name of the future data base</param>
        public void Crear_Base_Datos(string nombreDB)
        {
            Conexion_General();
            try
            {
                conexion.Open();
                //cmd = new NpgsqlCommand("CREATE DATABASE " + nombreDB + "", conexion);
                //cmd.ExecuteNonQuery();
                //conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error--- \n" + error);
            }

            cmd = new NpgsqlCommand("CREATE DATABASE " + nombreDB + "", conexion);
            bool realizacionConsulta = Convert.ToBoolean(cmd.ExecuteNonQuery());

            if (realizacionConsulta)
            {
                MessageBox.Show("Consulta Ejecutada Con Exito!!");

            }
            else
            {
                MessageBox.Show("Ha ocurrido un Error en la ejecucion de la Consulta");
            }
        }

        /// <summary>
        /// This metod delete data bases
        /// </summary>
        /// <param name="nombreDB">name of the data base that will be deleted</param>
        public void Eliminar_Base_Datos(string nombreDB)
        {

            try
            {
                Conexion_General();
                try
                {
                    conexion.Open();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error--- \n" + error);
                }

                cmd = new NpgsqlCommand("DROP DATABASE " + nombreDB + "", conexion);
                bool realizacionConsulta = Convert.ToBoolean(cmd.ExecuteNonQuery());

                if (realizacionConsulta)
                {
                    MessageBox.Show("Se Elimino la DB con Exito!!");
                    resultado_Query = "Consulta Ejecutada Con Exito!!";
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un Error en la ejecucion de la Eliminacion", "¡¡ERROR!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultado_Query = "Ha ocurrido un Error en la ejecucion de la Consulta";
                }
                conexion.Close();

            }
            catch (Exception error)
            {
                MessageBox.Show("Error--- \n" + error);
            }
        }

        /// <summary>
        /// This method execute whatever kind of query
        /// </summary>
        /// <param name="scripts">the scripts</param>
        public void Consulta_Cualquiera(string db,string scripts)
        {
            try
            {
                Conexion_Tablas(db);
                try
                {
                    conexion.Open();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error--- \n" + error);
                }

                cmd = new NpgsqlCommand(scripts, conexion);
                bool realizacionConsulta = Convert.ToBoolean(cmd.ExecuteNonQuery());

                if (realizacionConsulta)
                {
                    MessageBox.Show("Consulta Ejecutada Con Exito!!");
                    resultado_Query= "Consulta Ejecutada Con Exito!!";
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un Error en la ejecucion de la Consulta", "¡¡ERROR!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultado_Query="Ha ocurrido un Error en la ejecucion de la Consulta";
                }
            }
            catch (Exception errorQuery)
            {
                MessageBox.Show("Error se origina como :\n"+errorQuery);
            }
        }

    
        public void Consulta_Query(string db, string scripts,DataGridView dtg)
        {
            try
            {
                Conexion_Tablas(db);
                try
                {
                    conexion.Open();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error--- \n" + error);
                }
                cmd = new NpgsqlCommand(scripts, conexion);
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(scripts, conexion);
                adapter.Fill(dataset);
                dtg.DataSource = dataset.Tables[0];
                
                bool realizacionConsulta = Convert.ToBoolean(cmd.ExecuteNonQuery());

                if (realizacionConsulta)
                {
                    MessageBox.Show("Query Ejecutada Con Exito!!");
                    resultado_Query = "Consulta Ejecutada Con Exito!!";
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un Error en la ejecucion del Query", "¡¡ERROR!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultado_Query = "Ha ocurrido un Error en la ejecucion de la Consulta";
                }
                conexion.Close();
            }
            catch (Exception errorQuery)
            {
                MessageBox.Show("Error se origina como:\n" + errorQuery);
            }
        }

    }
}
