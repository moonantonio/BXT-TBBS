//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// Alexandria.cs (07/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: GameManager del proyecto crystal    							\\
// Fecha Mod:       07/12/2016                                                  \\
// Ultima Mod:  Version Inicial    												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.SceneManagement;
using MoonPincho.Sistemas.Clases;
#endregion

namespace MoonPincho.Sistemas
{
    /// <summary>
    /// <para>GameManager del proyecto crystal</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/Alexandria/Alexandria")]
    public class Alexandria : MonoBehaviour
    {
        /// <summary>
        /// <para>Instancia de alexandria</para>
        /// </summary>
        public static Alexandria instance;                                              // Instancia de alexandria
        /// <summary>
        /// <para>Personaje controlable</para>
        /// </summary>
        public GameObject personaje;                                                    // Personaje controlable

        public Vector3 proxPosicion;
        public Vector3 ultimaPosicion;

        public string proxEscena;
        public string ultimaEscena;

        public bool isAndando = false;

        public EstadosManager estadoJuego;


        #region Biblioteca Alexandria
        #endregion

        #region Inicializaciones
        /// <summary>
        /// <para>Awake de Alexandria</para>
        /// </summary>
        public void Awake()// Awake de Alexandria
        {
            // # SINGLETON #

            // Si no existe la instancia
            if (instance == null)
            {
                // Asignarla
                instance = this;
            }
            else if (instance != this)// Si existe pero no es esta
            {
                // Destruirla
                Destroy(gameObject);
            }

            // No destruir nunca
            DontDestroyOnLoad(this.gameObject);

            if (!GameObject.Find("Prototipo"))
            {
                GameObject personaj = Instantiate(personaje, proxPosicion, Quaternion.identity) as GameObject;
                personaj.name = "Personaje Prototipo";
                Debug.Log("Instanciando prototipo");
            }
        }

        /// <summary>
        /// <para>Iniciador base de Alexandria</para>
        /// </summary>
        private void Start()// Iniciador base de Alexandria
        {
            if (!GameObject.Find("Personaje Prototipo"))
            {
                GameObject personaj = Instantiate(personaje, proxPosicion, Quaternion.identity) as GameObject;
                personaj.name = "Personaje Prototipo";
                Debug.Log("Instanciando prototipo");
            }
        }
        #endregion

        #region Actualizador
        /// <summary>
        /// <para>Actualizador base de Alexandria</para>
        /// </summary>
        private void Update()// Actualizador base de Alexandria
        {
            switch (estadoJuego)
            {
                case EstadosManager.Intro:
                    break;

                case EstadosManager.MenuPrincipal:
                    break;

                case EstadosManager.Mundo:
                    break;

                case EstadosManager.Interiores:
                    break;

                case EstadosManager.Batalla:
                    InstanciarBatalla();
                    estadoJuego = EstadosManager.Idle;
                    break;

                case EstadosManager.Idle:
                    // Estado de reposo
                    break;
            }
        }
        #endregion

        #region API
        /// <summary>
        /// <para>Carga una nueva escena</para>
        /// </summary>
        public void CargarNuevaEscena()// Carga una nueva escena
        {
            SceneManager.LoadScene(proxEscena);

        }

        /// <summary>
        /// <para>Inicia una batalla</para>
        /// </summary>
        public void InstanciarBatalla()// Inicia una batalla
        {
            // TODO Obtener enemigos para la batalla

            // TODO Obtener aliados para la batalla

            // Obtener la posicion y escena actual
            ultimaPosicion = GameObject.Find("Personaje Prototipo").gameObject.transform.position;
            ultimaEscena = SceneManager.GetActiveScene().name;

            // Cargar escena
            SceneManager.LoadScene("00_Batalla");
        }
        #endregion
    }

    public enum EstadosManager
    {
        Intro,
        MenuPrincipal,
        Mundo,
        Interiores,
        Batalla,
        Idle
    }
}
