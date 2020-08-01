/**************************************************************************
 *                                                                        *
 *  Aceasta clasa reprezinta o componenta a aplicatiei noastre.           *
 *  Aici avem metode de setare a selectiilor utilizatorilor si            *
 *  inca o metoda care calculeaza suma totala a unei masini folosind      *
 *  selectiile utilizatorului si datele din Model                         *
 *  Autori:                                                               *
 *         Socea Gabriel 1410B                                            *
 *         Stoican Marius Catalin 1409B                                   *
 *         Ursache Emanuel 1410A                                          *
 *         Condriea Stefan 1410A                                          *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controller
{
    public class Controller
    {
        #region Private Member Variable
        /*
         * Campul Model
         */
        private Model.Model _model;
        /*
         * Variabile in care se seteaza selectiile utilizatorului din interfata grafica
         */
        private string _tip;
        private string _motor;
        private string _culoare;
        private string _jante;
        private string _designInterior;
        private List<string> _echipamenteOptionale;
        /*
         * Pretul total al tuturor echipamentelor selectate de utilizator
         */
        private int _pretTotal;
        #endregion

        /*
         * Functia de initializare a variabilelor
         */

        #region Public Method
        public void Init()
        {
            _tip = "";
            _motor = "";
            _culoare = "";
            _jante = "";
            _designInterior = "";
            _echipamenteOptionale = new List<string>();
            _pretTotal = 0;
        }
        #endregion
        #region Constructor
        /*
         * Constructorul in care se initializeaza Modelul
         */
        public Controller(Model.Model model)
        {
            _model = model;
            Init();
        }
        #endregion

        /*
         * Functia get pentru pretTotal
         */
        #region Public Methods
        public int GetPret()
        {
            return _pretTotal;
        }

        /*
         * Setare tip 
         */
        public void SetTip(string tip)
        {
            _tip = tip;
        }

        /*
         * Setare motor
         */
        public void SetMotor(string motor)
        {
            _motor = motor;
        }

        /*
         * Setare culoare
         */
        public void SetCuloare(string culoare)
        {
            _culoare = culoare;
        }

        /*
         * Setare jante
         */
        public void SetJante(string jante)
        {
            _jante = jante;
        }

        /*
         * Setare componenta d.i.
         */
        public void SetDesignInterior(string designInterior)
        {
            _designInterior = designInterior;
        }

        /*
         * Setare lista de echipamente optionale
         */
        public void SetEchipamenteOptionale(List<string> echipamenteOptionale)
        {
            _echipamenteOptionale = echipamenteOptionale;
        }

        /*
         * Functia care calculeaza pretul total pe baza selectiilor utilizatorului
         */
        public void ComputePret()
        {
            try
            {
                _pretTotal = 0;
                _pretTotal += _model.GetPretMotor(_tip, _motor);
                _pretTotal += _model.GetPretCuloare(_tip, _culoare);
                _pretTotal += _model.GetPretJante(_tip, _jante);
                _pretTotal += _model.GetPretDesignInterior(_tip, _designInterior);
                for (int i = 0; i < _echipamenteOptionale.Count; i++)
                {
                    _pretTotal += _model.GetPretEchipament(_tip, _echipamenteOptionale[i]);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Nu s-a putut calcula pretul total deoarece nu s-au putut extrage preturile din BD. A fost detectata urmatoarea eroare: " + e);
            }
        }
        #endregion
    }
}
