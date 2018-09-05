//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// UnidadEnemiga.cs (16/12/2016)												\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Controla el comportamiento del enemigo en el campo				\\
// Fecha Mod:		16/12/2016													\\
// Ultima Mod:	Version Inicial													\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
#endregion

namespace MoonPincho.Sistemas.Agne
{
    /// <summary>
    /// <para>Controla el comportamiento del enemigo en el campo</para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SAgne/Interno/UnidadEnemiga")]
    [RequireComponent(typeof(Vision))]
    public class UnidadEnemiga : MonoBehaviour
    {
        #region Variables Publicas
        /// <summary>
        /// <para>Tipo de estado actual del enemigo</para>
        /// </summary>
        public EstadosMovimientos estMov;                                       // Tipo de estado actual del enemigo
        /// <summary>
        /// <para>Waypoint al que se dirijira el enemigo</para>
        /// </summary>
        public GameObject wayPoint;                                             // Waypoint al que se dirijira el enemigo
        #endregion

        #region Variables Privadas
        /// <summary>
        /// <para>Velocidad de movimiento del enemigo</para>
        /// </summary>
        private float velocidad = 2.0f;                                         // Velocidad de movimiento del enemigo
        /// <summary>
        /// <para>Velocidad de rotacion del enemigo</para>
        /// </summary>
        private float velocidadRotacion = 2.0f;                                 // Velocidad de rotacion del enemigo
        /// <summary>
        /// <para>Minima distancia de recorrido</para>
        /// </summary>
        private float minDistancia = -4.0f;                                     // Minima distancia de recorrido
        /// <summary>
        /// <para>Maxima distancia de recorrido</para>
        /// </summary>
        private float maxDistancia = 4.0f;                                      // Maxima distancia de recorrido
        /// <summary>
        /// <para>WayPoint temporal</para>
        /// </summary>
        private GameObject tempWayPoint;                                        // WayPoint temporal
        /// <summary>
        /// <para>Parametro del waypoint para saber si hay alguno creado</para>
        /// </summary>
        private bool wayP = false;                                              // Parametro del waypoint para saber si hay alguno creado
        /// <summary>
        /// <para>Animator del enemigo</para>
        /// </summary>
        private Animator anim;                                                  // Animator del enemigo
        /// <summary>
        /// <para>Tiempo del wp</para>
        /// </summary>
        private float tiempoWP = 0.0f;                                          // Tiempo del wp
        /// <summary>
        /// <para>Tiempo maximo para perseguir</para>
        /// </summary>
        private float tiempoPersiguiendo = 0.0f;                                // Tiempo maximo para perseguir
        /// <summary>
        /// <para>Vision del enemigo</para>
        /// </summary>
        private Vision vision;                                                  // Vision del enemigo
        #endregion

        #region Iniciadores
        /// <summary>
        /// <para>Iniciador de UnidadEnemiga</para>
        /// </summary>
        private void Awake()// Iniciador de UnidadEnemiga
        {
            // Asignar componentes
            anim = GetComponent<Animator>();
            vision = GetComponent<Vision>();
        }

        /// <summary>
        /// <para>Iniciador de UnidadEnemiga</para>
        /// </summary>
        private void Start()// Iniciador de UnidadEnemiga
        {
            // Elegir accion
            StartCoroutine(ElegirAccion());
        }
        #endregion

        #region Actualizadores
        /// <summary>
        /// <para>Actualizador de UnidadEnemiga</para>
        /// </summary>
        private void Update()// Actualizador de UnidadEnemiga
        {
            // Si tiene algun objetivo, perseguirlo
            if (vision.objetivoVision != null)
            {
                estMov = EstadosMovimientos.Persiguiendo;
            }

            // Actualizador del estado de movimiento
            switch (estMov)
            {
                // Si esta parado
                case EstadosMovimientos.Idle:
                    #region Idle
                    // Cambiar animacion a parado
                    anim.SetBool("isMovimiento", false);
                    #endregion
                    break;

                // Si esta moviendose
                case EstadosMovimientos.Moviendose:
                    #region Moviendose
                    // Si no tenemos prox waypoint creado
                    if (wayP == false)
                    {
                        // Crear wp
                        CrearWP();
                    }

                    // Cambiar animacion a corriendo
                    anim.SetBool("isMovimiento", true);
                    #endregion
                    break;

                // Si esta persiguiendo
                case EstadosMovimientos.Persiguiendo:
                    #region Persiguiendo
                    // Si no tenemos prox waypoint creado
                    if (wayP == false)
                    {
                        // Crear wp
                        CrearWPPersecucion();
                    }

                    // Mover al enemigo
                    Mover();

                    // Cambiar animacion a corriendo
                    anim.SetBool("isMovimiento",true);
                    #endregion
                    break;

                // Si esta durmiendo
                case EstadosMovimientos.Descansando:
                    #region Durmiendo
                    //FIX Cambiar animacion a durmiendo
                    // Cambiar animacion a parado
                    anim.SetBool("isMovimiento", false);
                    #endregion
                    break;

                // Expepcion
                default:
                    estMov = EstadosMovimientos.Idle;
                    break;
            }

            // Si esta el waypoint y el estado no es persiguiendo
            if (wayP == true && estMov != EstadosMovimientos.Persiguiendo)
            {
                Mover();
                tiempoWP = tiempoWP + 1 * Time.deltaTime;

                if (tiempoWP >= 5.0f)
                {
                    // Destruir WP, quitar el parametro y poner el estado en Idle
                    Destroy(tempWayPoint);
                    wayP = false;
                    estMov = EstadosMovimientos.Idle;
                    tiempoWP = 0.0f;
                }
            }

            // Si esta mucho tiempo persiguiendo, descansar
            if (estMov == EstadosMovimientos.Persiguiendo)
            {
                tiempoPersiguiendo = tiempoPersiguiendo + 1 * Time.deltaTime;

                if (tiempoPersiguiendo >= 10.0f)
                {
                    estMov = EstadosMovimientos.Descansando;
                    tiempoPersiguiendo = 0.0f;
                }
            }
        }

        /// <summary>
        /// <para>Elegir una accion</para>
        /// </summary>
        /// <returns></returns>
        private IEnumerator ElegirAccion()// Elegir una accion
        {
            while (true)
            {
                // Esperar 3 segundos
                yield return new WaitForSeconds(3.0f);
                // Si no hay wp
                if (tempWayPoint == false)
                {
                    // Sacar un numero para la accion
                    int tempEstad = Random.Range(0, 2);

                    switch (tempEstad)
                    {
                        case 0:
                            estMov = EstadosMovimientos.Idle;
                            break;

                        case 1:
                            estMov = EstadosMovimientos.Moviendose;
                            break;

                        case 2:
                            estMov = EstadosMovimientos.Descansando;
                            break;

                        default:
                            estMov = EstadosMovimientos.Idle;
                            break;
                    }
                }
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Crea un waypoint delante del enemigo</para>
        /// </summary>
        private void CrearWP()// Crea un waypoint delante del enemigo
        {
            // Si no hay ningun waypoint creado, crearlo.
            if (wayP == false)
            {
                // Obtenemos los datos de la distancia
                float distanciaX = transform.position.x + Random.Range(minDistancia, maxDistancia);
                float distanciaZ = transform.position.z + Random.Range(minDistancia, maxDistancia);

                // Instanciamos el wp
                tempWayPoint = Instantiate(wayPoint, new Vector3(distanciaX,0.0f, distanciaZ),Quaternion.identity) as GameObject;

                // Activamos el wp
                wayP = true;
            }
        }

        /// <summary>
        /// <para>Crea un WayPoint en la posicion del objetivo</para>
        /// </summary>
        private void CrearWPPersecucion()// Crea un WayPoint en la posicion del objetivo
        {
            // Si no hay ningun waypoint creado, crearlo.
            if (wayP == false)
            {
                // Instanciamos el wp
                tempWayPoint = Instantiate(wayPoint, new Vector3(vision.objetivoVision.position.x, vision.objetivoVision.position.y, vision.objetivoVision.position.z), Quaternion.identity) as GameObject;

                // Activamos el wp
                wayP = true;
            }
        }

        /// <summary>
        /// <para>Mueve al enemigo</para>
        /// </summary>
        private void Mover()// Mueve al enemigo
        {
            // Mueve al enemigo al wp proximo
            transform.position = Vector3.MoveTowards(transform.position, tempWayPoint.transform.position, velocidad * Time.deltaTime);

            // Asignamos una rotacion y se la pasamos al enemigo
            var rotacion = Quaternion.LookRotation(tempWayPoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, velocidadRotacion);

        }

        /// <summary>
        /// <para>Cuando entras en contacto con un trigger</para>
        /// </summary>
        /// <param name="wayPoint">El otro collider</param>
        private void OnTriggerEnter(Collider col)// Cuando entras en contacto con un trigger
        {
            // Si tiene el tag del WayPoint
            if (col.tag == "WayPointMovEne")
            {
                // Destruir WP, quitar el parametro y poner el estado en Idle
                Destroy(tempWayPoint);
                wayP = false;
                estMov = EstadosMovimientos.Idle;
                tiempoWP = 0.0f;
                tiempoPersiguiendo = 0.0f;
            }

            if (wayPoint.tag == "Jugador")
            {
                // TODO Iniciar batalla
            }
        }
        #endregion
    }

    /// <summary>
    /// <para>Tipo de movimientos de la AI</para>
    /// </summary>
    public enum EstadosMovimientos// Tipo de movimientos de la AI
    {
        Idle,
        Moviendose,
        Persiguiendo,
        Descansando
    }
}