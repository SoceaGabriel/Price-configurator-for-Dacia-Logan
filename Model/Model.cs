/**************************************************************************
 *                                                                        *
 *  Aceasta este clasa Model (una din componentele sablonului MVC).       *
 *  In aceasta clasa sunt descrise modalitati de a aduce datele necesare  *
 *  din baza de date spre a le folosi mai departe in program              *
 *  Autori:                                                               *
 *         Socea Gabriel 1410B                                            *
 *         Stoican Marius Catalin 1409B                                   *
 *         Ursache Emanuel 1410A                                          *
 *         Condriea Stefan 1410A                                          *
 *                                                                        *
 **************************************************************************/

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model
{
    public class Model
    {
        #region Private Members Variable
        /*
         * Variabile de conexiune
         */
        private OracleConnection _conn;
        public string _connectionString;

        /*
         * Liste de echipamente
         */
        private List<string> _motoare;
        private List<string> _de_culoare;
        private List<string> _de_jante;
        private List<string> _design_interior;
        private List<string> _echipamente;

        #endregion
        /*
         * Constructorul fara argumente in care se creeaza conexiunea cu baza de date
         */
        #region Constructor
        public Model()
        {
            _connectionString = "User Id=proiect_ip;Password=proiectip;Data Source=localhost:1521/xe";
            _conn = new OracleConnection(_connectionString);
            _conn.Open();
            
        }
        #endregion

        #region  Public Methods
        /*
         * Inchiderea conexiunii cu baza de date
         */
        public void ConnClose()
        {
            _conn.Dispose();
        }

        /*
         * Extragem lista de motoare din baza de date
         */
        public List<string> GetListaMotoare(string tabela)
        {
            try
            {
                _motoare = new List<string>();
                string queryString = "SELECT MOTOR FROM " + tabela + " WHERE MOTOR IS NOT NULL";
                
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _motoare.Add(dr.GetString(0));
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Nu s-a putut extrage lista de motoare din tabela " + tabela);
            }
            
            return _motoare;
        }

        /*
         * Extragem lista de culori din baza de date
         */
        public List<string> GetListaCulori(string tabela)
        {
            try
            {
                _de_culoare = new List<string>();
                string queryString = "SELECT DESIGN_EXTERIOR FROM " + tabela + " WHERE DESIGN_EXTERIOR IS NOT NULL AND DESIGN_EXTERIOR LIKE 'Culoare%'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _de_culoare.Add(dr.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage lista de culori din tabela " + tabela);
            }

            return _de_culoare;
        }

        /*
         * Extragem lista de jante din baza de date
         */
        public List<string> GetListaJante(string tabela)
        {
            try
            {
                _de_jante = new List<string>();
                string queryString = "SELECT DESIGN_EXTERIOR FROM " + tabela + " WHERE DESIGN_EXTERIOR IS NOT NULL AND DESIGN_EXTERIOR LIKE 'Jante%'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _de_jante.Add(dr.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage lista de jante din tabela " + tabela);
            }

            return _de_jante;
        }

        /*
         * Extragem lista de componente de design interior din baza de date
         */
        public List<string> GetListaDI(string tabela)
        {
            try
            {
                _design_interior = new List<string>();
                string queryString = "SELECT DESIGN_INTERIOR FROM " + tabela + " WHERE DESIGN_INTERIOR IS NOT NULL";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _design_interior.Add(dr.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage lista de de componente de design interior din tabela " + tabela);
            }

            return _design_interior;
        }

        /*
         * Extragem lista de echipamente din baza de date
         */
        public List<string> GetListaEchipamente(string tabela)
        {
            try
            {
                _echipamente = new List<string>();
                string queryString = "SELECT ECHIPAMENTE FROM " + tabela + " WHERE ECHIPAMENTE IS NOT NULL";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _echipamente.Add(dr.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage lista de echipamente din tabela " + tabela);
            }

            return _echipamente;
        }

        /*
         * Extragem pretul unui anumit motor dat ca parametru din tabela data ca parametru din baza de date
         */
        public int GetPretMotor(string tabela,string numeMotor)
        {

            int pret_motor = 0;
            try
            {
                string queryString = "SELECT PRET_MOTOR FROM " + tabela + " WHERE PRET_MOTOR IS NOT NULL AND MOTOR = '"+numeMotor+"'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pret_motor= (int)dr.GetOracleDecimal(0);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage pretul motorului " + numeMotor + " din tabela " + tabela);
            }

            return pret_motor;
        }


        /*
         * Extragem pretul unei anumite culori data ca parametru din tabela data ca parametru din baza de date
         */
        public int GetPretCuloare(string tabela,string culoare)
        {
            int pret_culoare=0;
            try
            {
                string queryString = "SELECT PRET_DE FROM " + tabela + " WHERE PRET_DE IS NOT NULL AND DESIGN_EXTERIOR = '"+culoare+"'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pret_culoare = (int)dr.GetOracleDecimal(0);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage pretul culorii " + culoare + " din tabela " + tabela);
            }

            return pret_culoare;
        }

        /*
         * Extragem pretul unui anumit set de jante dat ca parametru din tabela data ca parametru din baza de date
         */
        public int GetPretJante(string tabela,string jante)
        {
            int pret_jante = 0;
            try
            {
                string queryString = "SELECT PRET_DE FROM " + tabela + " WHERE PRET_DE IS NOT NULL AND DESIGN_EXTERIOR ='" + jante + "'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pret_jante = (int)dr.GetOracleDecimal(0);
                        
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage pretul jantelor " + jante + " din tabela " + tabela);
            }

            return pret_jante;
        }

        /*
         * Extragem pretul unei anumite componente de design interior data ca parametru din tabela data ca parametru din baza de date
         */
        public int GetPretDesignInterior(string tabela,string design)
        {
            int pret_design_interior = 0;
            try
            {
                string queryString = "SELECT PRET_DI FROM " + tabela + " WHERE DESIGN_INTERIOR='" + design + "'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pret_design_interior = (int)dr.GetOracleDecimal(0);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage pretul tapiseriei " + design + " din tabela " + tabela);
            }

            return pret_design_interior;
        }

        /*
         * Extragem pretul unui anumit echipament optional dat ca parametru din tabela data ca parametru din baza de date
         */
        public int GetPretEchipament(string tabela,string echipament)
        {
            int pret_echipament = 0;
            try
            {
                string queryString = "SELECT PRET_ECHIPAMENTE FROM " + tabela + " WHERE ECHIPAMENTE ='" + echipament + "'";
                OracleCommand cmd = new OracleCommand(queryString, _conn);
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pret_echipament = (int)dr.GetOracleDecimal(0);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nu s-a putut extrage pretul echipamentului " + echipament + " din tabela " + tabela);
            }

            return pret_echipament;
        }
        #endregion
    }
}
