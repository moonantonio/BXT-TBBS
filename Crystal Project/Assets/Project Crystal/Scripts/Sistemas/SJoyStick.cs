//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// SJoyStick.cs (14/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Sistema de joystick    											\\
// Fecha Mod:       14/11/2016                                                  \\
// Ultima Mod:  Funcionalidad inicial    										\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
#endregion

namespace MoonPincho.Joystick
{
    /// <summary>
    /// <para>Sistema de joystick general</para>
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Moon Pincho/Sistemas/SJoyStick/SJoyStick")]
    public class SJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        #region Variables Generales
        #region Variables Asignadas
        /// <summary>
        /// <para>Transform del joystick</para>
        /// </summary>
        public RectTransform joystick;                                                  // Transform del joystick
        /// <summary>
        /// <para>Zona del joystick</para>
        /// </summary>
        public RectTransform zonaJoystick;                                              // Zona del joystick
        /// <summary>
        /// <para>Base del joystick</para>
        /// </summary>
        public RectTransform joystickBase;                                              // Base del joystick
        /// <summary>
        /// <para>Auxiliar para la base</para>
        /// </summary>
        private RectTransform baseTrans;                                                // Auxiliar para la base
        /// <summary>
        /// <para>Auxiliar para el centro de la textura del joystick</para>
        /// </summary>
        private Vector2 textureCenter = Vector2.zero;                                   // Auxiliar para el centro de la textura del joystick
        /// <summary>
        /// <para>Posicion por defecto</para>
        /// </summary>
        private Vector2 defaultPos = Vector2.zero;                                      // Posicion por defecto
        /// <summary>
        /// <para>Auxiliar del Centro del joystick</para>
        /// </summary>
        private Vector3 joystickCenter = Vector3.zero;                                  // Auxiliar del Centro del joystick
        /// <summary>
        /// <para>Imagen de la base del joystick</para>
        /// </summary>
        public Image imgBase;                                                           // Imagen de la base del joystick
        /// <summary>
        /// <para>Imagen del joystick</para>
        /// </summary>
        public Image imgJoystick;                                                       // Imagen del joystick
        /// <summary>
        /// <para>Imagen hacia arriba del joystick</para>
        /// </summary>
        public Image imgHaciaUp;                                                        // Imagen hacia arriba del joystick
        /// <summary>
        /// <para>Imagen hacia abajo del joystick</para>
        /// </summary>
        public Image imgHaciaDown;                                                      // Imagen hacia abajo del joystick
        /// <summary>
        /// <para>Imagen hacia la izquierda del joystick</para>
        /// </summary>
        public Image imgHaciaLeft;                                                      // Imagen hacia la izquierda del joystick
        /// <summary>
        /// <para>Imagen hacia la derecha del joystick</para>
        /// </summary>
        public Image imgHaciaRight;                                                     // Imagen hacia la derecha del joystick
        #endregion

        #region Zonas y Longitud
        /// <summary>
        /// <para>Enum de la escala del joystick</para>
        /// </summary>
        public enum Escala                                                              // Enum de la escala del joystick
        {
            Anchura,
            Altura
        }
        /// <summary>
        /// <para>Enum del pivote del joystick</para>
        /// </summary>
        public enum Pivote                                                              // Enum del pivote del joystick
        {
            Izquierda,
            Derecha
        }
        /// <summary>
        /// <para>Enum de zona de pulsacion del joystick</para>
        /// </summary>
        public enum ZonaJoystickTouch                                                   // Enum de zona de pulsacion del joystick
        {
            Default,
            Medio,
            Grande,
            Personalizado
        }
        /// <summary>
        /// <para>Escala del joystick</para>
        /// </summary>
        public Escala escala = Escala.Altura;                                           // Escala del joystick
        /// <summary>
        /// <para>Pivote del joystick</para>
        /// </summary>
        public Pivote pivote = Pivote.Derecha;                                          // Pivote del joystick
        /// <summary>
        /// <para>Zona de pulsacion del joystick</para>
        /// </summary>
        public ZonaJoystickTouch zonajoystickTouch = ZonaJoystickTouch.Default;         // Zona de pulsacion del joystick
        /// <summary>
        /// <para>Tamaño del joystick</para>
        /// </summary>
        public float joystickSize = 1.75f;                                              // Tamaño del joystick
        /// <summary>
        /// <para>Modificador del radio del joystick</para>
        /// </summary>
        public float radioMod = 4.5f;                                                   // Modificador del radio del joystick
        /// <summary>
        /// <para>Auxiliar para el radioMod</para>
        /// </summary>
        private float radio = 1.0f;                                                     // Auxiliar para el radioMod
        /// <summary>
        /// <para>Posicion dinamica del joystick</para>
        /// </summary>
        public bool posicionDinamica = false;                                           // Posicion dinamica del joystick
        /// <summary>
        /// <para>Tamaño personalizado en X</para>
        /// </summary>
        public float customTouchSize_X = 50.0f;                                         // Tamaño personalizado en X
        /// <summary>
        /// <para>Tamaño personalizado en Y</para>
        /// </summary>
        public float customTouchSize_Y = 75.0f;                                         // Tamaño personalizado en Y
        /// <summary>
        /// <para>Tamaño personalizado en X posicion</para>
        /// </summary>
        public float customTouchSizePos_X = 0.0f;                                       // Tamaño personalizado en X posicion
        /// <summary>
        /// <para>Tamaño personalizado en Y posicion</para>
        /// </summary>
        public float customTouchSizePos_Y = 0.0f;                                       // Tamaño personalizado en Y posicion
        /// <summary>
        /// <para>Espaciador en X</para>
        /// </summary>
        public float customEspaciador_X = 5.0f;                                         // Espaciador en X
        /// <summary>
        /// <para>Espaciador en Y</para>
        /// </summary>
        public float customEspaciador_Y = 20.0f;                                        // Espaciador en Y
        #endregion

        #region Estilo y Opciones
        #region Visual
        /// <summary>
        /// <para>Desactiva la personalizacion del joystick</para>
        /// </summary>
        public bool usarPersonalizacion = false;                                        // Desactiva la personalizacion del joystick
        /// <summary>
        /// <para>Desvanece el joystick</para>
        /// </summary>
        public bool usarDesvanecimiento = false;                                        // Desvanece el joystick
        /// <summary>
        /// <para>Grupos en el canvas del joystick</para>
        /// </summary>
        private CanvasGroup grupoJoystick;                                              // Grupos en el canvas del joystick
        /// <summary>
        /// <para>Cuando no se presiona el alpha del joystick</para>
        /// </summary>
        public float desvaNoTouch = 1.0f;                                               // Cuando no se presiona el alpha del joystick
        /// <summary>
        /// <para>Cuando se presiona el alpha del joystick</para>
        /// </summary>
        public float desvaTouch = 0.5f;                                                 // Cuando se presiona el alpha del joystick
        /// <summary>
        /// <para>Duracion del desvacenimiento del joystick hacia fuera</para>
        /// </summary>
        public float desvaDuracionOut = 1.0f;                                           // Duracion del desvacenimiento del joystick hacia fuera
        /// <summary>
        /// <para>Duracion del desvacenimiento del joystick hacia dentro</para>
        /// </summary>
        public float desvaDuracionIn = 1.0f;                                            // Duracion del desvacenimiento del joystick hacia dentro
        /// <summary>
        /// <para>Velocidad del desvacenimiento del joystick hacia fuera</para>
        /// </summary>
        private float desvaVelocidadOut = 1.0f;                                         // Velocidad del desvacenimiento del joystick hacia fuera
        /// <summary>
        /// <para>Velocidad del desvacenimiento del joystick hacia dentro</para>
        /// </summary>
        private float desvaVelocidadIn = 1.0f;                                          // Velocidad del desvacenimiento del joystick hacia dentro
        /// <summary>
        /// <para>Usar animacion en el joystick</para>
        /// </summary>
        public bool usarAnimacion = false;                                              // Usar animacion en el joystick
        /// <summary>
        /// <para>Animator del joystick</para>
        /// </summary>
        public Animator joystickAnimator;                                               // Animator del joystick
        /// <summary>
        /// <para>ID de la animacion del joystick</para>
        /// </summary>
        private int animacionID = 0;                                                    // ID de la animacion del joystick
        /// <summary>
        /// <para>Usar una variacion de color del joystick</para>
        /// </summary>
        public bool usarVariacionColor = false;                                         // Usar una variacion de color del joystick
        /// <summary>
        /// <para>Variacion del color del joystick</para>
        /// </summary>
        public Color variacionColor = new Color(1, 1, 1, 1);                            // Variacion del color del joystick
        /// <summary>
        /// <para>Usar tension del joystick</para>
        /// </summary>
        public bool usarTension = false;                                                // Usar tension del joystick
        /// <summary>
        /// <para>Tension del joystick de base (sin tension)</para>
        /// </summary>
        public Color tensionColorBase = new Color(1, 1, 1, 1);                          // Tension del joystick de base (sin tension)
        /// <summary>
        /// <para>Tension del joystick a tope (con tension)</para>
        /// </summary>
        public Color tensionColorFull = new Color(1, 1, 1, 1);                          // Tension del joystick a tope (con tension)
        #endregion

        #region Funcionalidad
        /// <summary>
        /// <para>Usar retroceso en el joystick</para>
        /// </summary>
        public bool usarRetroceso = false;                                              // Usar retroceso en el joystick
        /// <summary>
        /// <para>Duracion del retroceso del joystick</para>
        /// </summary>
        public float duracionRetroceso = 0.5f;                                          // Duracion del retroceso del joystick
        /// <summary>
        /// <para>Auxiliar para saber si el joystick esta retrocediendo en este instante</para>
        /// </summary>
        private bool isRetrocediendo = false;                                           // Auxiliar para saber si el joystick esta retrocediendo en este instante
        /// <summary>
        /// <para>Usar un joystick desplazable</para>
        /// </summary>
        public bool usarDesplazable = false;                                            // Usar un joystick desplazable
        /// <summary>
        /// <para>Enum que restringe los Axis del joystick</para>
        /// </summary>
        public enum Axis                                                                // Enum que restringe los Axis del joystick
        {
            Ambos,
            X,
            Y
        }
        /// <summary>
        /// <para>Enum limite del joystick</para>
        /// </summary>
        public enum Limite                                                              // Enum limite del joystick
        {
            Circular,
            Cuadrado
        }
        /// <summary>
        /// <para>Enum para forzar la posicion del joystick a (-1,0 y 1)</para>
        /// </summary>
        public enum DeadZone                                                            // Enum para forzar la posicion del joystick a (-1,0 y 1)
        {
            NoUsar,
            UnValor,
            DosValores
        }
        /// <summary>
        /// <para>Restringe los Axis del joystick</para>
        /// </summary>
        public Axis axis = Axis.Ambos;                                                  // Restringe los Axis del joystick
        /// <summary>
        /// <para>Limita el joystick</para>
        /// </summary>
        public Limite limite = Limite.Circular;                                         // Limita el joystick
        /// <summary>
        /// <para>Fuerza la posicion del joystick</para>
        /// </summary>
        public DeadZone deadZone = DeadZone.NoUsar;                                     // Fuerza la posicion del joystick
        /// <summary>
        /// <para>Posicion en X del limitador del joystick</para>
        /// </summary>
        public float xDeadZone = 0.1f;                                                  // Posicion en X del limitador del joystick
        /// <summary>
        /// <para>Posicion en Y del limitador del joystick</para>
        /// </summary>
        public float yDeadZone = 0.1f;                                                  // Posicion en Y del limitador del joystick
        #endregion
        #endregion

        #region Referencias A Scripts
        /// <summary>
        /// <para>Diccionario del SJoystick</para>
        /// </summary>
        public static Dictionary<string, SJoyStick> SJoysticks = new Dictionary<string, SJoyStick>();// Diccionario del SJoystick
        /// <summary>
        /// <para>Nombre del joystick</para>
        /// </summary>
        public string joystickNombre;                                                   // Nombre del joystick
        /// <summary>
        /// <para>En el caso de la carga de scripts, imponer este antes que ninguno.</para>
        /// </summary>
        public bool imponerValores = false;                                             // En el caso de la carga de scripts, imponer este antes que ninguno.
        /// <summary>
        /// <para>Valor horizontal</para>
        /// </summary>
        public float valorHorizontal = 0.0f;                                            // Valor horizontal
        /// <summary>
        /// <para>Valor Vertical</para>
        /// </summary>
        public float valorVertical = 0.0f;                                              // Valor Vertical
        /// <summary>
        /// <para>Estado del joystick</para>
        /// </summary>
        private bool estadoJoystick = false;                                            // Estado del joystick
        /// <summary>
        /// <para>Actualizar posicion del joystick</para>
        /// </summary>
        private bool updatePos = false;                                                 // Actualizar posicion del joystick
        /// <summary>
        /// <para>Puntero del joystick</para>
        /// </summary>
        private int _idPuntero = -1;                                                    // Puntero del joystick
        #endregion
        #endregion

        #region Inicializaciones
        /// <summary>
        /// <para>Cargador inicial de SJoystick</para>
        /// </summary>
        private void Awake()// Cargador inicial de SJoystick
        {
            // Si no se esta ejecutando el juego y se ha asignado el nombre del joystick, registrar el joystick.
            if (Application.isPlaying == true && joystickNombre != string.Empty)
                RegistrarJoystick(joystickNombre);
        }

        /// <summary>
        /// <para>Iniciador base de SJoyStick</para>
        /// </summary>
        private void Start()// Iniciador base de SJoyStick
        {
            // Si el juego no se esta ejecutando, return.
            if (Application.isPlaying == false)
                return;

            // Actualizar el tamaño y la ubicacion del joystick.
            ActualizarPosicion();

            // Comprobar todas las opciones del joystick.
            CompruebaOpcionesJoystick();

            // Comprobar si se usa color variable y actualizarlo
            if (usarVariacionColor == true)
                ActualizarColor(variacionColor);

            // Comprobar si se usa la tension y actualizarlo
            if (usarTension == true)
                ResetearTension();

            // Comprobar si se usa el desvanecimiento y actualizarlo
            if (usarDesvanecimiento == true)
            {
                // Configurar el desvanecimiento.
                desvaVelocidadIn = 1.0f / desvaDuracionIn;
                desvaVelocidadOut = 1.0f / desvaDuracionOut;
            }

            // Comprobar si se usa animaciones y obtener el ID de hash de la animacion.
            if (usarAnimacion == true)
                animacionID = Animator.StringToHash("Touch");

            // Si no hay ninguna secuencia de comandos en el Updater, adjuntar una secuencia de comandos de Updater.
            if (!GetPadreCanvas().GetComponent<JoystickUpdater>())
                GetPadreCanvas().gameObject.AddComponent(typeof(JoystickUpdater));
        }
        #endregion

        #region Actualizador
#if UNITY_EDITOR
        /// <summary>
        /// <para>Actualizador base de SJoyStick</para>
        /// </summary>
        private void Update()// Actualizador base de SJoyStick
        {
            // Mantener el joystick actualizado mientras el editor no se esta ejecutando.
            if (Application.isPlaying == false)
                ActualizarPosicion();
        }
#endif
        #endregion

        #region Metodos Comunes
        /// <summary>
        /// <para>Registra el nombre del joystick</para>
        /// </summary>
        /// <param name="joystickNombre"></param>
        private void RegistrarJoystick(string joystickNombre)// Registra el nombre del joystick
        {
            // Comprueba en el diccionario si existe el nombre y sino lo borra.
            if (SJoysticks.ContainsKey(joystickNombre))
                SJoysticks.Remove(joystickNombre);

            // Agrega al diccionario el nuevo nombre.
            SJoysticks.Add(joystickNombre, GetComponent<SJoyStick>());
        }

        /// <summary>
        /// <para>Este metodo comprueba cada opcion y componente en relacion con el color del joystick. Actualiza el bool de updatePos de acuerdo con las opciones establecidas.</para>
        /// </summary>
        private void CompruebaOpcionesJoystick()// Este metodo comprueba cada opcion y componente en relacion con el color del joystick. Actualiza el bool de updatePos de acuerdo con las opciones establecidas.
        {
            if (usarVariacionColor == false)
                updatePos = false;
            else if (imgJoystick == null)
                updatePos = false;
            else if (joystick.GetComponent<Image>() == imgJoystick)
                updatePos = false;
            else
                updatePos = true;
        }
        #endregion

        #region API
        /// <summary>
        /// <para>Metodo de Unity3D para detectar eventos de touch hacia abajo</para>
        /// </summary>
        /// <param name="touchInfo">Evento del touch</param>
        public void OnPointerDown(PointerEventData touchInfo)// Metodo de Unity3D para detectar eventos de touch hacia abajo
        {
            // Si el joystick ya esta en uso, return.
            if (estadoJoystick == true)
                return;

            // Establecer el estado del joystick en true.
            estadoJoystick = true;

            // Asignar el punteroId para que las otras funciones puedan saber si el puntero que llama a la funcion es el correcto.
            _idPuntero = touchInfo.pointerId;

            // Si la opcion usarRetroceso y isRetrocediendo esta seleccionada, entonces detener el movimiento actual.
            if (usarRetroceso == true && isRetrocediendo == true)
                StopCoroutine("MovimientoRetroceso");

            // Si posicionDinamica o usarPersonalizacion estan habilitados ...
            if (posicionDinamica == true || usarPersonalizacion == true)
            {
                // Mover la zonaJoystick a la posicion del toque.
                zonaJoystick.position = touchInfo.position - textureCenter;

                // Ajustar el joystickCenter para que la posicion se pueda calcular correctamente.
                joystickCenter = touchInfo.position;
            }

            // Si usarAnimacion esta activado, inicializar.
            if (usarAnimacion == true)
                joystickAnimator.SetBool(animacionID, true);

            // Si usarDesvanecimiento esta activado, inicializar.
            if (usarDesvanecimiento == true && grupoJoystick != null)
                StartCoroutine("MovimientoDesvanecimiento");

            // Inicializar UpdateJoystick con la informacion del PointerEventData actual.
            ActualizarJoystick(touchInfo);
        }

        /// <summary>
        /// <para>Metodo de Unity3D para detectar el arrastre del touch</para>
        /// </summary>
        /// <param name="touchInfo">Evento del touch.</param>
        public void OnDrag(PointerEventData touchInfo)// Metodo de Unity3D para detectar el arrastre del touch
        {
            // Si el evento de pointer que esta llamando a esta funcion no es el mismo que el que inicio el joystick, return.
            if (touchInfo.pointerId != _idPuntero)
                return;

            // Inicializar UpdateJoystick con la informacion del PointerEventData actual.
            ActualizarJoystick(touchInfo);
        }

        /// <summary>
        /// <para>Metodo de Unity3D para detectar eventos de touch hacia arriba</para>
        /// </summary>
        /// <param name="touchInfo">Evento de touch.</param>
        public void OnPointerUp(PointerEventData touchInfo)// Metodo de Unity3D para detectar eventos de touch hacia arriba
        {
            // Si el evento de pointer que esta llamando a esta funcion no es el mismo que el que inicio el joystick, return.
            if (touchInfo.pointerId != _idPuntero)
                return;

            // Puesto que el touch se ha levantado, estado en false y reestablecer el _idPuntero.
            estadoJoystick = false;
            _idPuntero = -1;

            // Si posicionDinamica, usarPersonalizacion o usarDesplazable estan habilitados ...
            if (posicionDinamica == true || usarPersonalizacion == true || usarDesplazable == true)
            {
                // La zonaJoystick debe restablecerse de nuevo a la posicion predeterminada.
                zonaJoystick.position = defaultPos;

                // Restablecer el joystickCenter desde que se ha soltado.
                joystickCenter = joystickBase.position;
            }

            // Si usarRetroceso esta activado, iniciar MovimientoRetroceso().
            if (usarRetroceso == true)
                StartCoroutine("MovimientoRetroceso");
            else
            {
                // Resetear la posicion del joystick en el centro.
                joystick.position = joystickCenter;

                // Actualizar la posicion del joystick.
                if (updatePos == true)
                    imgJoystick.transform.position = joystickCenter;
            }

            // Si usarTension esta activo, resetear tension.
            if (usarTension == true && usarRetroceso == false)
                ResetearTension();

            // Si usarAnimacion esta activada, inicializar.
            if (usarAnimacion == true)
                joystickAnimator.SetBool(animacionID, false);

            // Establecer la referencia a la posición xy para que puedan referenciarse.
            if (imponerValores == true && usarRetroceso == false)
            {
                valorHorizontal = 0.0f;
                valorVertical = 0.0f;
            }
        }

        /// <summary>
        /// <para>Actualizar Sistema del joystick</para>
        /// </summary>
        /// <param name="touchInfo">Evento del touch.</param>
        public void ActualizarJoystick(PointerEventData touchInfo)// Actualizar Sistema del joystick
        {
            // Crear un nuevo Vector2 temporal para igualar el vector del toque actual al centro del joystick.
            Vector2 tempVector = touchInfo.position - (Vector2)joystickCenter;

            // Si la configuracion solo esta para un axis, cerrar el opuesto.
            if (axis == Axis.X)
                tempVector.y = 0;
            else if (axis == Axis.Y)
                tempVector.x = 0;

            // Si se desea un limite circular para el joystick, ajustar la magnitud por el radio.
            if (limite == Limite.Circular)
                tempVector = Vector2.ClampMagnitude(tempVector, radio);
            // Sino se selecciona un limite cuadrado, por lo que se establece un X e Y maximo.
            else if (limite == Limite.Cuadrado)
            {
                tempVector.x = Mathf.Clamp(tempVector.x, -radio, radio);
                tempVector.y = Mathf.Clamp(tempVector.y, -radio, radio);
            }

            // Aplicar el tempVector a la posicion del joystick.
            joystick.transform.position = (Vector2)joystickCenter + tempVector;

            // Actualizar la posicion de la imagen del joystick.
            if (updatePos == true)
                imgJoystick.transform.position = joystick.transform.position;

            // Si usarTension esta activado, iniciarla.
            if (usarTension == true)
                MostrarTension();

            // Si se quiere arrastrar el joystick junto con el touch ...
            if (usarDesplazable == true)
            {
                // Almacenar la posicion del toque actual.
                Vector3 currentTouchPosition = touchInfo.position;

                // Si no se usa el Axis Ambos, alinear la posicion de toque actual.
                if (axis != Axis.Ambos)
                {
                    if (axis == Axis.X)
                        currentTouchPosition.y = joystickCenter.y;
                    else
                        currentTouchPosition.x = joystickCenter.x;
                }
                // Encontrar la distancia del touch al centro.
                float touchDistancia = Vector3.Distance(joystickCenter, currentTouchPosition);

                // Si touchDistancia es mayor que el radio ...
                if (touchDistancia >= radio)
                {
                    // Conseguir la posicion actual del joystick.
                    Vector2 joystickPosition = (joystick.position - joystickCenter) / radio;

                    // Mover zonaJoystick en la direccion en que se encuentra el joystick, multiplicado por la diferencia de distancia del radio maximo.
                    zonaJoystick.position += new Vector3(joystickPosition.x, joystickPosition.y, 0) * (touchDistancia - radio);

                    // Reconfigurar el joystickCenter ya que el joystick ya se ha movido.
                    joystickCenter = joystickBase.position;
                }
            }

            if (imponerValores == true)
            {
                valorHorizontal = GetHorizontalAxis();
                valorVertical = GetVerticalAxis();
            }
        }

        /// <summary>
        /// <para>Actualiza el tamaño y la ubicacion del Joystick. Util para las rotaciones de la pantalla o para cambiar el tamaño de la pantalla.</para>
        /// </summary>
        public void ActualizarPosicion()// Actualiza el tamaño y la ubicacion del Joystick. Util para las rotaciones de la pantalla o para cambiar el tamaño de la pantalla
        {
            // Si alguno de los componentes necesarios se deja sin asignar, tirar error.
            if (zonaJoystick == null || joystickBase == null || joystick == null)
            {
                Debug.LogError("Hay algunos componentes necesarios que no estan asignados actualmente. Comprobar la seccion Variables asignadas y asegurarse de asignar todos los componentes.");
                return;
            }

            // Establezca el tamaño de referencia actual para la escala.
            float referenciaSize = escala == Escala.Altura ? Screen.height : Screen.width;

            // Configurar el tamaño del target para el tamaño del joystick.
            float textureSize = referenciaSize * (joystickSize / 10);

            // Si baseTrans es null, almacenar RectTrans de este objeto para que pueda ser colocado.
            if (baseTrans == null)
                baseTrans = GetComponent<RectTransform>();

            // Obtener una posicion para el joystick basandose en las variables de posicion.
            Vector2 imagePosition = ConfigurarPosicionImg(new Vector2(textureSize, textureSize), new Vector2(customEspaciador_X, customEspaciador_Y));

            // Si la zona es personalizada
            if (zonajoystickTouch == ZonaJoystickTouch.Personalizado)
            {
                // Corregir las variables de tamaño personalizado.
                float fixedFBPX = customTouchSize_X / 100;
                float fixedFBPY = customTouchSize_Y / 100;

                // Dependiendo de las opciones de ZonaJoystickTouch, configurar el tamaño.
                baseTrans.sizeDelta = new Vector2(Screen.width * fixedFBPX, Screen.height * fixedFBPY);

                // Calcular el tamaño y el posicionamiento personalizado a la funcion ConfigureImagePosition para obtener la posicion exacta.
                Vector2 imagePos = ConfigurarPosicionImg(baseTrans.sizeDelta, new Vector2(customTouchSizePos_X, customTouchSizePos_Y));

                // Aplicar la nueva posicion.
                baseTrans.position = imagePos;
            }
            else
            {
                // Floar temporal para almacenar un modificador para el tamaño del area tactil.
                float fixedTouchSize = zonajoystickTouch == ZonaJoystickTouch.Grande ? 2.0f : zonajoystickTouch == ZonaJoystickTouch.Medio ? 1.51f : 1.01f;

                // Vector2 temporal para almacenar el tamaño predeterminado del joystick.
                Vector2 tempVector = new Vector2(textureSize, textureSize);

                // Aplicar el tamaño del joystick multiplicado por el fixedTouchSize.
                baseTrans.sizeDelta = tempVector * fixedTouchSize;

                // Aplicar la posicion de la imagen modificada con la diferencia del sizeDelta dividido por 2, multiplicado por la escala del canvas principal.
                baseTrans.position = imagePosition - ((baseTrans.sizeDelta - tempVector) / 2);
            }

            // Si la opcion dinamica esta on, la posicion predeterminada debe almacenarse ...
            if (posicionDinamica == true || usarPersonalizacion == true || usarDesplazable == true)
            {
                // Ajustar el centro de textura de forma que el joystick pueda moverse a la posicion de touch correctamente.
                textureCenter = new Vector2(textureSize / 2, textureSize / 2);

                // Almacenar la posicion predeterminada para que pueda volver despues de levantar el dedo.
                defaultPos = imagePosition;
            }

            // Aplicar el tamaño y la posicion al zonaJoystick.
            zonaJoystick.sizeDelta = new Vector2(textureSize, textureSize);
            zonaJoystick.position = imagePosition;

            // Configurar el tamaño del radio del Joystick.
            radio = zonaJoystick.sizeDelta.x * (radioMod / 10);

            // Guardar el centro del joystick para que la Posicion del Joystick se pueda configurar correctamente.
            joystickCenter = joystick.position;

            // Si esta activo el desvanecimiento, y el joystickGroup no esta asignado, buscar el CanvasGroup.
            if (usarDesvanecimiento == true && grupoJoystick == null)
                grupoJoystick = GetCanvasGroup();
        }

        /// <summary>
        /// <para>Actualiza el color resaltado del joystick con un color elegido</para>
        /// </summary>
        /// <param name="colorElegido">Nuevo color.</param>
        public void ActualizarColor(Color colorElegido)// Actualiza el color resaltado del joystick con un color elegido
        {
            if (usarVariacionColor == false)
                return;

            variacionColor = colorElegido;

            // Compruebe si cada variable esta asignada por lo que no hay una excepcion de referencia null al aplicar el color.
            if (imgBase != null)
                imgBase.color = variacionColor;
            if (imgJoystick != null)
                imgJoystick.color = variacionColor;
        }

        /// <summary>
        /// <para>Actualiza los colores de la tension.</para>
        /// </summary>
        /// <param name="targetvacio">Color normal.</param>
        /// <param name="targetlleno">Color con tension.</param>
        public void ActualizarColorTension(Color targetvacio, Color targetlleno)// Actualiza los colores de la tension
        {
            if (usarTension == false)
                return;

            tensionColorBase = targetvacio;
            tensionColorFull = targetlleno;
        }

        /// <summary>
        /// <para>Muestra la tension del joystick</para>
        /// </summary>
        public void MostrarTension()// Muestra la tension del joystick
        {
            // Crea un Vector2 temporal para la posicion actual del joystick.
            Vector2 tension = (joystick.position - joystickCenter) / radio;

            // Si el joystick esta a la derecha ...
            if (tension.x > 0)
            {
                // Leer el color segun la posicion X de la tension.
                if (imgHaciaRight != null)
                    imgHaciaRight.color = Color.Lerp(tensionColorBase, tensionColorFull, tension.x);

                // Si la tension opuesta no es tensionColorBase, se implementa.
                if (imgHaciaLeft != null && imgHaciaLeft.color != tensionColorBase)
                    imgHaciaLeft.color = tensionColorBase;
            }
            // Si el joystick esta a la izquierda ...
            else
            {
                // Mathf.Abs da un numero positivo a lerp.
                tension.x = Mathf.Abs(tension.x);

                // Repetir los pasos anteriores ...
                if (imgHaciaLeft != null)
                    imgHaciaLeft.color = Color.Lerp(tensionColorBase, tensionColorFull, tension.x);
                if (imgHaciaRight != null && imgHaciaRight.color != tensionColorBase)
                    imgHaciaRight.color = tensionColorBase;
            }

            // Si el joystick esta hacia arriba ...
            if (tension.y > 0)
            {
                // Leer el color segun la posicion Y de la tension.
                if (imgHaciaUp != null)
                    imgHaciaUp.color = Color.Lerp(tensionColorBase, tensionColorFull, tension.y);

                // Si la tension opuesta no es tensionColorBase, se implementa.
                if (imgHaciaDown != null && imgHaciaDown.color != tensionColorBase)
                    imgHaciaDown.color = tensionColorBase;
            }
            // Si el joystick esta hacia abajo ...
            else
            {
                // Mathf.Abs da un numero positivo a lerp.
                tension.y = Mathf.Abs(tension.y);

                if (imgHaciaDown != null)
                    imgHaciaDown.color = Color.Lerp(tensionColorBase, tensionColorFull, tension.y);

                // Repetir los pasos anteriores ...
                if (imgHaciaUp != null && imgHaciaUp.color != tensionColorBase)
                    imgHaciaUp.color = tensionColorBase;
            }
        }

        /// <summary>
        /// <para>Resetea los valores de la tension</para>
        /// </summary>
        public void ResetearTension()// Resetea los valores de la tension
        {
            if (imgHaciaUp != null)
                imgHaciaUp.color = tensionColorBase;

            if (imgHaciaDown != null)
                imgHaciaDown.color = tensionColorBase;

            if (imgHaciaLeft != null)
                imgHaciaLeft.color = tensionColorBase;

            if (imgHaciaRight != null)
                imgHaciaRight.color = tensionColorBase;
        }

        /// <summary>
        /// <para>Devuelve un valor entre -1 y 1 que representa el valor horizontal del Joystick.</para>
        /// </summary>
        public float GetHorizontalAxis()// Devuelve un valor entre -1 y 1 que representa el valor horizontal del Joystick
        {
            return GetPosicion().x;
        }

        /// <summary>
        /// <para>Devuelve un valor entre -1 y 1 que representa el valor vertical del Joystick</para>
        /// </summary>
        public float GetVerticalAxis()// Devuelve un valor entre -1 y 1 que representa el valor vertical del Joystick
        {
            return GetPosicion().y;
        }

        /// <summary>
        /// <para>Devuelve un valor Vector2 que contiene los valores horizontales y verticales del joystick.</para>
        /// </summary>
        public Vector2 GetPosicion()// Devuelve un valor Vector2 que contiene los valores horizontales y verticales del joystick.
        {
            if (deadZone != DeadZone.NoUsar)
                return GetPosicionJoystickDeadZone();

            return (joystick.position - joystickCenter) / radio;
        }

        /// <summary>
        /// <para>Devuelve un valor entre 0 y 1 que representa la distancia del joystick desde la base.</para>
        /// </summary>
        public float GetDistancia()// Devuelve un valor entre 0 y 1 que representa la distancia del joystick desde la base.
        {
            return Vector3.Distance(joystick.position, joystickCenter) / radio;
        }

        /// <summary>
        /// <para>Restablece el Joystick Entero.</para>
        /// </summary>
        public void ResetJoystick()// Restablece el Joystick Entero
        {
            OnPointerUp(new PointerEventData(FindObjectOfType<EventSystem>()));
        }

        #region API Estatica
        /// <summary>
        /// <para>Devuelve el joystick si existe dentro de la escena.</para>
        /// </summary>
        /// <param name="joystickNombre">El nombre del Joystick.</param>
        public static SJoyStick GetJoystick(string joystickNombre)// Devuelve el joystick si existe dentro de la escena.
        {
            if (!ConfirmarJoystick(joystickNombre))
                return null;

            return SJoysticks[joystickNombre];
        }

        /// <summary>
        /// <para>Devuelve un valor Vector2 que contiene los valores horizontales y verticales del joystick.</para>
        /// </summary>
        /// <param name="joystickNombre">El nombre del Joystick.</param>
        public static Vector2 GetPosicion(string joystickNombre)// Devuelve un valor Vector2 que contiene los valores horizontales y verticales del joystick.
        {
            if (!ConfirmarJoystick(joystickNombre))
                return Vector2.zero;

            return SJoysticks[joystickNombre].GetPosicion();
        }

        /// <summary>
        /// <para>Devuelve un valor entre 0 y 1 que representa la distancia del joystick desde la base.</para>
        /// </summary>
        /// <param name="joystickNombre">El nombre del Joystick.</param>
        public static float GetDistancia(string joystickNombre)// Devuelve un valor entre 0 y 1 que representa la distancia del joystick desde la base.
        {
            if (!ConfirmarJoystick(joystickNombre))
                return 0.0f;

            return SJoysticks[joystickNombre].GetDistancia();
        }

        /// <summary>
        /// <para>Devuelve el estado de interaccion actual del Joystick.</para>
        /// </summary>
        /// <param name="joystickNombre">El nombre del Joystick.</param>
        public static bool GetEstadosJoystick(string joystickNombre)// Devuelve el estado de interaccion actual del Joystick.
        {
            if (!ConfirmarJoystick(joystickNombre))
                return false;

            return SJoysticks[joystickNombre].estadoJoystick;
        }

        /// <summary>
        /// <para>Devuelve un valor entre -1 y 1 que representa el valor horizontal del Joystick.</para>
        /// </summary>
        /// <param name="joystickName">El nombre del Joystick.</param>
        public static float GetHorizontalAxis(string joystickNombre)// Devuelve un valor entre -1 y 1 que representa el valor horizontal del Joystick.
        {
            if (!ConfirmarJoystick(joystickNombre))
                return 0.0f;

            return SJoysticks[joystickNombre].GetHorizontalAxis();
        }

        /// <summary>
        /// <para>Devuelve un valor entre -1 y 1 que representa el valor vertical del Joystick.</para>
        /// </summary>
        /// <param name="joystickName">El nombre del Joystick.</param>
        public static float GetVerticalAxis(string joystickName)// Devuelve un valor entre -1 y 1 que representa el valor vertical del Joystick.
        {
            if (!ConfirmarJoystick(joystickName))
                return 0.0f;

            return SJoysticks[joystickName].GetVerticalAxis();
        }
        #endregion
        #endregion

        #region Funcionalidad
        /// <summary>
        /// <para>Configura la posicion de una imagen segun el tamaño y el espaciado personalizado.</para>
        /// </summary>
        /// <param name="texturaSize">Tamaño de la imagen.</param>
        /// <param name="espaciado">Espaciado de la imagen.</param>
        /// <returns>Posicion de la imagen.</returns>
        private Vector2 ConfigurarPosicionImg(Vector2 texturaSize, Vector2 espaciado)// Configura la posicion de una imagen segun el tamaño y el espaciado personalizado.
        {
            // Corregir el espaciado para que el valor sea entre 0.0f y 1.0f
            Vector2 fixedCustomEspaciado = espaciado / 100;

            // Configurar los espaciados de posicion de acuerdo con las dimensiones de la pantalla, el espaciado fijo y el tamaño de la textura.
            float posicionSpacerX = Screen.width * fixedCustomEspaciado.x - (texturaSize.x * fixedCustomEspaciado.x);
            float posicionSpacerY = Screen.height * fixedCustomEspaciado.y - (texturaSize.y * fixedCustomEspaciado.y);

            // Vectro 2 temporal para el return
            Vector2 tempVector;

            // Aplicar la posicionxSpacerX, de lo contrario calcular hacia fuera desde el lado derecho y aplicar la posicionSpaceX.
            tempVector.x = pivote == Pivote.Izquierda ? posicionSpacerX : (Screen.width - texturaSize.x) - posicionSpacerX;

            // Aplicar la posicionSpacerY.
            tempVector.y = posicionSpacerY;

            // Devolver la variable temporal modificada.
            return tempVector;
        }

        /// <summary>
        /// <para>Obtener el grupo de canvas del joystick</para>
        /// </summary>
        /// <returns>El grupo al que pertenece el joystick</returns>
        private CanvasGroup GetCanvasGroup()// Obtener el grupo de canvas del joystick
        {
            if (GetComponent<CanvasGroup>())
                return GetComponent<CanvasGroup>();
            else
            {
                gameObject.AddComponent<CanvasGroup>();
                return GetComponent<CanvasGroup>();
            }
        }

        /// <summary>
        /// <para>Obtener el padre del canvas.</para>
        /// </summary>
        /// <returns>Devuelve el padre del canvas o en su defecto un null.</returns>
        private Canvas GetPadreCanvas()// Obtener el padre del canvas o en su defecto un null.
        {
            Transform padre = transform.parent;
            while (padre != null)
            {
                if (padre.transform.GetComponent<Canvas>())
                    return padre.transform.GetComponent<Canvas>();

                padre = padre.transform.parent;
            }
            return null;
        }

        /// <summary>
        /// <para>Obtener la zona dead del joystick</para>
        /// </summary>
        /// <returns>El vector2 de la zona en x y de la zonadead.</returns>
        private Vector2 GetPosicionJoystickDeadZone()// Obtener la zona dead del joystick
        {
            Vector2 tempVec = (joystick.position - joystickCenter) / radio;

            // Si el valor de X esta a la izquierda, actualizar la deadZone del vector2.x a -1.
            if (tempVec.x < -xDeadZone)
                tempVec.x = -1;
            // Sino comprobar si esta a la derecha y actualizar deadZone del vector2.x a 1.
            else if (tempVec.x > xDeadZone)
                tempVec.x = 1;
            // Sino poner a 0 el valor de x.
            else
                tempVec.x = 0;

            // Si el valor de Y esta abajo, actualizar la deadZone del vector2.y a -1.
            if (tempVec.y < -yDeadZone)
                tempVec.y = -1;
            // Sino comprobar si esta arriba y actualizar deadZone del vector2.y a 1.
            else if (tempVec.y > yDeadZone)
                tempVec.y = 1;
            // Sino poner a 0 el valor de y.
            else
                tempVec.y = 0;

            return tempVec;
        }

        /// <summary>
        /// <para>Devuelve el joystick al centro suavemente.</para>
        /// </summary>
        /// <returns>Devuelve la transicion.</returns>
        private IEnumerator MovimientoRetroceso()// Devuelve el joystick al centro suavemente
        {
            // Iniciar retroceso
            isRetrocediendo = true;

            // Fijar la velocidad temporal del retroceso
            float auxRetrovelo = 1.0f / duracionRetroceso;

            // Almacenar temporalmente la posicion de donde esta el joystick actualmente.
            Vector3 startJoyPos = joystick.position;

            for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * auxRetrovelo)
            {
                // Inicia el retroceso suavemente del joystick.
                joystick.position = Vector3.Lerp(startJoyPos, joystickCenter, i);

                // Actualizar la posicion del joystick.
                if (updatePos == true)
                    imgJoystick.transform.position = joystick.position;

                // Si se tiene habilitado usarTension, mostrar la tension mientras se mueve el joystick.
                if (usarTension == true)
                    MostrarTension();

                if (imponerValores == true)
                {
                    valorHorizontal = GetHorizontalAxis();
                    valorVertical = GetVerticalAxis();
                }

                yield return null;
            }

            isRetrocediendo = false;

            // Resetea la posicion del joystick al centro.
            joystick.position = joystickCenter;

            // Actualizar la posicion del joystick.
            if (updatePos == true)
                imgJoystick.transform.position = joystick.position;

            // Resetea la tension.
            if (usarTension == true)
                ResetearTension();

            if (imponerValores == true)
            {
                valorHorizontal = 0.0f;
                valorVertical = 0.0f;
            }
        }

        /// <summary>
        /// <para>Ejecuta el desvacenimiento del joystick</para>
        /// </summary>
        /// <returns>El desvacenimiento.</returns>
        private IEnumerator MovimientoDesvanecimiento()// Ejecuta el desvacenimiento del joystick
        {
            // Almacene el valor actual para el alfa del grupoJoystick.
            float actualDesva = grupoJoystick.alpha;

            // Si desvaVelocidadIn es NaN, ajustar el alfa al desvacenimiento deseado.
            if (float.IsInfinity(desvaVelocidadIn))
                grupoJoystick.alpha = desvaTouch;
            // sino ejecutar el bucle para desvanecerse al alfa con el tiempo.
            else
            {
                for (float desvaIn = 0.0f; desvaIn < 1.0f && estadoJoystick == true; desvaIn += Time.deltaTime * desvaVelocidadIn)
                {
                    grupoJoystick.alpha = Mathf.Lerp(actualDesva, desvaTouch, desvaIn);
                    yield return null;
                }
                if (estadoJoystick == true)
                    grupoJoystick.alpha = desvaTouch;
            }

            // Mientras que el estado este activo ...
            while (estadoJoystick == true)
                yield return null;

            // Ajustar el valor de desvanecimiento actual.
            actualDesva = grupoJoystick.alpha;

            // Si el valor de desvaVelocidadOut es NaN, ajustar el alfa deseado.
            if (float.IsInfinity(desvaVelocidadOut))
                grupoJoystick.alpha = desvaNoTouch;
            // Sino ejecutar el bucle para retroceder el desvanecimiento.
            else
            {
                for (float fadeOut = 0.0f; fadeOut < 1.0f && estadoJoystick == false; fadeOut += Time.deltaTime * desvaVelocidadOut)
                {
                    grupoJoystick.alpha = Mathf.Lerp(actualDesva, desvaNoTouch, fadeOut);
                    yield return null;
                }
                if (estadoJoystick == false)
                    grupoJoystick.alpha = desvaNoTouch;
            }
        }

        /// <summary>
        /// <para>Devuelve con una confirmacion si se encuenta el joystick.</para>
        /// </summary>
        static bool ConfirmarJoystick(string joystickNombre)// Devuelve con una confirmacion si se encuenta el joystick.
        {
            if (!SJoysticks.ContainsKey(joystickNombre))
            {
                Debug.LogWarning("No se encuentra " + joystickNombre + " como un joystick registrado.");
                return false;
            }
            return true;
        }
        #endregion
    }

    /// <summary>
    /// <para>Actualizador del joystick general</para>
    /// </summary>
    public class JoystickUpdater : UIBehaviour
    {
        #region Metodos
        /// <summary>
        /// <para>Metodo interno de Unity, para saber si cambia las dimensiones de un rect</para>
        /// </summary>
        protected override void OnRectTransformDimensionsChange()// Metodo interno de Unity, para saber si cambia las dimensiones de un rect
        {
            StartCoroutine("YieldPosicionamiento");
        }
        #endregion

        #region Funcionalidad
        /// <summary>
        /// <para>Actualiza la posicion de los joysticks en tiempo de ejecucion</para>
        /// </summary>
        /// <returns>Devuelve la actualizacion de posicion de los joysticks</returns>
        IEnumerator YieldPosicionamiento()// Actualiza la posicion de los joysticks en tiempo de ejecucion
        {
            yield return new WaitForEndOfFrame();

            // Buscar todos los objetos de tipo SJoystick
            SJoyStick[] allJoysticks = FindObjectsOfType(typeof(SJoyStick)) as SJoyStick[];

            // Actualizar la posicion de todos los Sjoysticks encontrados
            for (int i = 0; i < allJoysticks.Length; i++)
                allJoysticks[i].ActualizarPosicion();
        }
        #endregion
    }
}
