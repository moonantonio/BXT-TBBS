//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoEditor.cs (15/11/2016)                                              	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Editor de MTodo    												\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonPincho.MEditor.ToDo.Extensiones;
using System.Collections;
using MiniJSON;
#endregion

namespace MoonPincho.MEditor.ToDo.Editor
{
    /// <summary>
    /// <para>Editor de MTodo </para>
    /// </summary>
    public class MTodoEditor : EditorWindow
    {
        #region Propiedades
        /// <summary>
        /// <para>Propiedad que contiene la palabra a buscar</para>
        /// </summary>
        private string _buscarPalabra = "";                                                 // Propiedad que contiene la palabra a buscar
        /// <summary>
        /// <para>Propiedad que contiene la palabra a buscar</para>
        /// </summary>
        public string BuscarPalabra                                                         // Propiedad que contiene la palabra a buscar
        {
            get { return _buscarPalabra; }
            set
            {
                if (value != _buscarPalabra)
                {
                    _buscarPalabra = value;
                    RefrescarTickets();
                }
            }
        }
        /// <summary>
        /// <para>Anchura del sidebar</para>
        /// </summary>
        private float SidebarWidth                                                          // Anchura del sidebar
        {
            get { return position.width / 3f; }
        }
        /// <summary>
        /// <para>Tags de MTodo</para>
        /// </summary>
        private string[] Tags                                                               // Tags de MTodo
        {
            get
            {
                if (_data != null && _data.ListaTags.Count > 0)
                    return _data.ListaTags.ToArray();
                else
                    return new string[] { "TODO", "BUG" };
            }
        }
        #endregion

        #region Variables Privadas
        #region TODO
        /// <summary>
        /// <para>Variable de sistema que observa</para>
        /// </summary>
        private FileSystemWatcher _observador;                                              // Variable de sistema que observa
        /// <summary>
        /// <para>Variable de sistema que contiene los archivos</para>
        /// </summary>
        private FileInfo[] _archivos;                                                       // Variable de sistema que contiene los archivos
        /// <summary>
        /// <para>Data contenedora de todos los tickets</para>
        /// </summary>
        private MTodoData _data;                                                            // Data contenedora de todos los tickets
        /// <summary>
        /// <para>Nuevo nombre de la tag</para>
        /// </summary>
        private string _nuevoNombreTag = "";                                                // Nuevo nombre de la tag
        /// <summary>
        /// <para>Scroll del sidebar</para>
        /// </summary>
        private Vector2 _sidebarScroll;                                                     // Scroll del sidebar
        /// <summary>
        /// <para>Scroll del area</para>
        /// </summary>
        private Vector2 _mainAreaScroll;                                                    // Scroll del area
        /// <summary>
        /// <para>Contador de la tag actual</para>
        /// </summary>
        private int _actualTag = -1;                                                        // Contador de la tag actual
        /// <summary>
        /// <para>Todos los tickets a mostrar</para>
        /// </summary>
        private MTodoTickets[] _ticketsAMostrar;                                            // Los tickets a mostrar
        #endregion

        #region Tareas
        /// <summary>
        /// <para>Data contenedora de todas las tareas</para>
        /// </summary>
        private MTodoTareasData _dataTarea;                                                  // Data contenedora de todas las tareas

        private string nuevoNombreCat;
        private List<string> tareasRealizadas = new List<string>();
        private Color nuevoColorCat;
        private bool cambioNombre = false;
        private bool cambioColor = false;
        string tempNombre = "";
        Color tempColor = Color.white;
        private int tabTareas = 0;
        /// <summary>
        /// <para>Lista de las categorias</para>
        /// </summary>
        internal static string[] categorias = new string[1] { "Normal" };                                       // Lista de las categorias
        /// <summary>
        /// <para>Filtro de las categorias</para>
        /// </summary>
        internal static string[] filtroCategorias = new string[2] { "Todas las Categorias", "Normal" };         // Filtro de las categorias
        /// <summary>
        /// <para>Filtro</para>
        /// </summary>
        private int indexFiltro = 0;                                                                // Filtro
        private int indexCat = 0;
        string _Titulo;
        string _Descripcion;
        string _Categoria;
        #endregion

        #region Trello (Beta)
        private bool trelloAutentificacion = false;
        private string token;
        private string key;
        private List<object> boards;
        private List<string> nombresBoards = new List<string>();
        private List<object> lists;
        private List<string> nombresListas = new List<string>();
        private const string memberBaseUrl = "https://api.trello.com/1/members/me";
        private const string boardBaseUrl = "https://api.trello.com/1/boards/";
        private const string cardBaseUrl = "https://api.trello.com/1/cards/";
        private string currentBoardId = "";
        private string currentListId = "";
        Vector2 scrollPosTrelloBoard;
        Vector2 scrollPosTrelloLista;
        private int paraBoard;
        private string boardActual = "";
        private string listaActual = "";
        private string cardTitulo;
        private string cardDescripcion;
        private string cardFecha;
        private bool envioTrello;
        #endregion

        #region Ajustes
        /// <summary>
        /// <para>Ruta de acceso de los datos</para>
        /// </summary>
        private string _dataPath = @"Assets/Moon Pincho/Dev/MToDo/Alexandria/MTodo.asset";                          // Ruta de acceso de los datos
        /// <summary>
        /// <para>Condicional para el autoescaneo</para>
        /// </summary>
        private bool _autoScan;                                                                                     // Condicional para el autoescaneo
        /// <summary>
        /// <para>Nombre del archivo</para>
        /// </summary>
        private const string _dataPathTareaArchivo = @"MTodoTareas.asset";                                          // Nombre del archivo
        /// <summary>
        /// <para>Ruta de la carpeta</para>
        /// </summary>
        private const string _dataPathTareaCarpeta = @"Moon Pincho/Dev/MToDo/Alexandria";                           // Ruta de la carpeta
        /// <summary>
        /// <para>Ruta de acceso de los datos</para>
        /// </summary>
        private const string _dataPathTarea = @"Assets/" + _dataPathTareaCarpeta + "/" + _dataPathTareaArchivo;     // Ruta de acceso de los datos
        #endregion
        #endregion

        #region Inicializadores
        /// <summary>
        /// <para>Inicializa MTodo</para>
        /// </summary>
        [MenuItem("MTODO",false,0)]
        public static void Init()// Inicializa MTodo
        {
            Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Moon Pincho/Dev/MToDo/Iconos/trello.png");
            var window = GetWindow<MTodoEditor>();
            window.minSize = new Vector2(400, 250);
            GUIContent titleContent = new GUIContent("MTodo", icon);
            window.titleContent = titleContent;
            window.Show();
        }

        /// <summary>
        /// <para>Cuando se activa MTodo</para>
        /// </summary>
        private void OnEnable()// Cuando se activa MTodo
        {
            Cargar();

            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            RefrescarArchivos();

            _data = ScriptableObjectUtils.LoadOrCreateAsset<MTodoData>(_dataPath);

            _dataTarea = AssetDatabase.LoadAssetAtPath(_dataPathTarea, typeof(MTodoTareasData)) as MTodoTareasData;

            if (_dataTarea == null)
            {
                if (AssetDatabase.IsValidFolder("Assets/" + _dataPathTareaCarpeta))
                {
                    AssetDatabase.CreateFolder("Assets", _dataPathTareaCarpeta);
                    _dataTarea = ScriptableObject.CreateInstance<MTodoTareasData>();
                    AssetDatabase.CreateAsset(_dataTarea, _dataPathTarea);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

            }

            RefrescarTickets();

            if (!_autoScan)
                return;

            _observador = new FileSystemWatcher(Application.dataPath, "*.cs");
            _observador.Changed += OnChanged;
            _observador.Deleted += OnDeleted;
            _observador.Renamed += OnRenamed;
            _observador.Created += OnCreated;

            _observador.EnableRaisingEvents = true;
            _observador.IncludeSubdirectories = true;
        }
        #endregion

        #region GUI
        /// <summary>
        /// <para>Interfaz Grafica</para>
        /// </summary>
        private void OnGUI()// Interfaz Grafica
        {


            if (_dataTarea == null)
            {
                _dataTarea = ScriptableObjectUtils.LoadAsset<MTodoTareasData>(_dataPathTarea);
            }
            if (boards == null)
            {
                boardActual = "";
            }
            if (lists == null)
            {
                listaActual = "";
            }

            if (MTodoAlexandria.sistemaMTodo == Sistemas.Todo)
            {
                if (_data == null)
                {
                    GUILayout.Label("No carga la data", EditorStyles.centeredGreyMiniLabel);
                    return;
                }

                Undo.RecordObject(_data, "tododata");

                ToolbarMTodo();
                using (new HorizontalBlock())
                {
                    Sidebar();
                    MainArea();
                }

                EditorUtility.SetDirty(_data);
            }

            if (MTodoAlexandria.sistemaMTodo == Sistemas.Tareas)
            {
                ToolbarMTodo();
                using (new HorizontalBlock())
                {
                    SidebarTareas();
                    AreaTareas();
                }
            }

            if (MTodoAlexandria.sistemaMTodo == Sistemas.Trello)
            {
                ToolbarMTodo();
                using (new HorizontalBlock())
                {
                    SidebarTrello();
                    AreaTrello();
                }
            }
            Repaint();
        }
        #endregion

        #region API
        /// <summary>
        /// <para>Carga los ajustes de MTodo</para>
        /// </summary>
        private void Cargar()// Carga los ajustes de MTodo
        {
            _autoScan = EditorPrefs.GetBool("MP_ToDo_Auto_Scan", true);
            _dataPath = EditorPrefs.GetString("MP_ToDo_Data_Path", @"Assets/Moon Pincho/Dev/MToDo/Alexandria/MTodo.asset");
            MTodoAlexandria.sistemaMTodo = CargarSistema();
        }

        /// <summary>
        /// <para>Refresca los tickets</para>
        /// </summary>
        private void RefrescarTickets()// Refresca los tickets
        {
            if (_actualTag == -1)
                _ticketsAMostrar = _data.Tickets.ToArray();
            else if (_actualTag >= 0)
                _ticketsAMostrar = _data.Tickets.Where(e => e.Tag == _data.ListaTags[_actualTag]).ToArray();
            if (!string.IsNullOrEmpty(BuscarPalabra))
            {
                var etmp = _ticketsAMostrar;
                _ticketsAMostrar = etmp.Where(e => e.Texto.Contains(_buscarPalabra)).ToArray();
            }
        }

        /// <summary>
        /// <para>Fija la tag actual</para>
        /// </summary>
        /// <param name="index">Posicion de la tag</param>
        private void SetActualTag(int index)// Fija la tag actual
        {
            EditorApplication.delayCall += () =>
            {
                _actualTag = index;
                RefrescarTickets();
                Repaint();
            };
        }

        /// <summary>
        /// <para>Obtiene la ruta relativa</para>
        /// </summary>
        /// <param name="pathAbsoluta">Ruta absoluta</param>
        /// <returns>La Ruta Relativa</returns>
        public static string AssetsRelativePath(string pathAbsoluta)// Obtiene la ruta relativa
        {
            if (pathAbsoluta.StartsWith(Application.dataPath))
                return "Assets" + pathAbsoluta.Substring(Application.dataPath.Length);
            else
                throw new System.ArgumentException("La ruta completa no contiene la carpeta de assets del proyecto actual",
                    "absolutePath");
        }

        /// <summary>
        /// <para>Busca una palabra</para>
        /// </summary>
        /// <param name="txtBuscar">Palabra que buscar</param>
        /// <param name="opciones">Opciones especiales</param>
        /// <returns>La palabra buscada</returns>
        private string Buscar(string txtBuscar, params GUILayoutOption[] opciones)// Busca una palabra
        {
            txtBuscar = GUILayout.TextField(txtBuscar, "ToolbarSeachTextField", opciones);
            if (GUILayout.Button("", "ToolbarSeachCancelButton"))
            {
                txtBuscar = "";
                GUI.FocusControl(null);
            }
            return txtBuscar;
        }
        #endregion

        #region Funcionalidad GUI
        /// <summary>
        /// <para>Toolbar de MTodo</para>
        /// </summary>
        private void ToolbarMTodo()// Toolbar de MTodo
        {
            using (new HorizontalBlock(EditorStyles.toolbar))
            {

                if (GUILayout.Button("ToDo", EditorStyles.toolbarButton))
                    MTodoAlexandria.sistemaMTodo = Sistemas.Todo;
                    EditorPrefs.SetInt("SistemaMTodo",GuardarSistema());

                if (GUILayout.Button("Tareas", EditorStyles.toolbarButton))
                    MTodoAlexandria.sistemaMTodo = Sistemas.Tareas;
                    EditorPrefs.SetInt("SistemaMTodo", GuardarSistema());

                if (GUILayout.Button("Trello(βeta)", EditorStyles.toolbarButton))
                    MTodoAlexandria.sistemaMTodo = Sistemas.Trello;
                    EditorPrefs.SetInt("SistemaMTodo", GuardarSistema());

                GUILayout.Label("   ||   ");

                switch (MTodoAlexandria.sistemaMTodo)
                {
                    case Sistemas.Todo:
                        if (GUILayout.Button("Forzar scan", EditorStyles.toolbarButton))
                            ScanTodosLosArchivos();

                            GUILayout.FlexibleSpace();
                            BuscarPalabra = Buscar(BuscarPalabra, GUILayout.Width(250));
                        break;

                    case Sistemas.Tareas:
                        if (GUILayout.Button("X!", EditorStyles.toolbarButton)) { }

                        DibujarProgressBar(tareasRealizadas.Count,_dataTarea.Tareas.Count);

                        GUILayout.FlexibleSpace();
                        BuscarPalabra = Buscar(BuscarPalabra, GUILayout.Width(250));
                        break;

                    case Sistemas.Trello:
                        if (GUILayout.Button("X", EditorStyles.toolbarButton)) { }

                        if (GUILayout.Button("Tablero Actual >> " + boardActual, EditorStyles.toolbarButton))
                        {}
                            

                        GUILayout.FlexibleSpace();
                        BuscarPalabra = Buscar(BuscarPalabra, GUILayout.Width(250));
                        break;

                    default:
                        GUILayout.Label("Error al cargar la barra de tareas");
                        break;
                }


            }
        }

        /// <summary>
        /// <para>Sidebar de MTodo</para>
        /// </summary>
        private void Sidebar()// Sidebar de MTodo
        {
            using (new VerticalBlock(GUI.skin.box, GUILayout.Width(SidebarWidth), GUILayout.ExpandHeight(true)))
            {
                using (new ScrollviewBlock(ref _sidebarScroll))
                {
                    TagField(-1);
                    for (var i = 0; i < _data.GetTagsCount; i++)
                        TagField(i);
                }
                AddTagField();
            }
        }

        /// <summary>
        /// <para>Sidebar de tareas</para>
        /// </summary>
        private void SidebarTareas()// Sidebar de tareas
        {
            using (new VerticalBlock(GUI.skin.box, GUILayout.Width(SidebarWidth), GUILayout.ExpandHeight(true)))
            {
                DibujarCategoria();
            }
        }

        /// <summary>
        /// <para>Dibuja las categorias de las tareas</para>
        /// </summary>
        private void DibujarCategoria()// Dibuja las categorias de las tareas
        {
            EditorGUILayout.BeginVertical("box");
            for (int n = 0; n < _dataTarea.Categorias.Count; n++)
            {
                tempNombre = _dataTarea.Categorias[n].Nombre;
                tempColor = _dataTarea.Categorias[n].Color;

                if (tempNombre != _dataTarea.Categorias[n].Nombre)
                    cambioNombre = true;
                if (nuevoColorCat != _dataTarea.Categorias[n].Color)
                    cambioColor = true;

               
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _dataTarea.Categorias.RemoveAt(n);
                }  
                _dataTarea.Categorias[n].Nombre = EditorGUILayout.TextField(_dataTarea.Categorias[n].Nombre);
                _dataTarea.Categorias[n].Color = EditorGUILayout.ColorField(_dataTarea.Categorias[n].Color, GUILayout.Width(50));
                if (cambioNombre || cambioColor)
                {

                    if (GUILayout.Button("S", GUILayout.Width(50)))
                    {
                        if (cambioNombre)
                        {
                            _dataTarea.Categorias[n].Nombre = tempNombre;
                        }

                        if (cambioColor)
                        {
                            _dataTarea.Categorias[n].Color = tempColor;
                        }

                        cambioColor = false;
                        cambioNombre = false;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Agregar Categoria", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();

            nuevoNombreCat = EditorGUILayout.TextArea(nuevoNombreCat);
            nuevoColorCat = EditorGUILayout.ColorField(nuevoColorCat, GUILayout.Width(50));

            if (GUILayout.Button("+", GUILayout.Width(50)) && nuevoNombreCat != "")
            {
                _dataTarea.AddCategoria(new MTodoTareaCategoria(nuevoNombreCat,nuevoColorCat));
                nuevoNombreCat = "";
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// <para>Sidebar de trello</para>
        /// </summary>
        private void SidebarTrello()// Sidebar de trello
        {
            using (new VerticalBlock(GUI.skin.box, GUILayout.Width(SidebarWidth), GUILayout.ExpandHeight(true)))
            {
                EditorGUILayout.BeginVertical("box");

                if (trelloAutentificacion == false)
                {
                    GUILayout.Label("Configuracion Inicial");
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Key");
                    key = EditorGUILayout.TextField(key);
                    if (key == "")
                    {
                        EditorGUILayout.HelpBox("https://trello.com/1/appKey/generate", MessageType.Info);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Token");
                    token = EditorGUILayout.TextField(token);
                    if (token == "")
                    {
                        EditorGUILayout.HelpBox("https://trello.com/1/connect?key=[yourkeygoeshere]&name=Your%20App&response_type=token&scope=read,write&expiration=never", MessageType.Info);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Guardar Config"))
                    {
                        GuardarTrello();
                    }

                    if (GUILayout.Button("Cargar Config"))
                    {
                        CargarTrello();
                    }

                    if (GUILayout.Button("Conectar"))
                    {
                        AutentificarTrello(key, token);
                        CargarBoards();
                    }
 
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    GUILayout.Label("Configuracion Finalizada");
                    if (GUILayout.Button("Editar Config"))
                        trelloAutentificacion = false;

                    EditorGUILayout.Space();

                    if (GUILayout.Button("Conectar"))
                    {
                        AutentificarTrello(key, token);
                        CargarBoards();
                    }

                }
                    
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();

                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.BeginHorizontal();

                
                if (boardActual != "")
                {
                    GUILayout.Label("Tableros cargados: " + nombresBoards.Count + "/" + nombresListas.Count);
                    if (GUILayout.Button("<-"))
                    {
                        boardActual = "";
                    }
                }

                EditorGUILayout.EndHorizontal();
                if (trelloAutentificacion == true)
                {
                    if (boardActual != "")
                    {
                        DibujarGUITrelloList();
                    }
                    else
                    {
                        DibujarGUITrelloBoard();
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }

        /// <summary>
        /// <para>Area de las tareas</para>
        /// </summary>
        private void AreaTareas()// Area de las tareas
        {
            using (new VerticalBlock(GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                CargarNombresCategorias();
                EditorGUILayout.BeginVertical();
                GUILayout.Label("Tareas", EditorStyles.boldLabel);
                int OldFilterIndex = indexFiltro;
                indexFiltro = EditorGUILayout.Popup(indexFiltro, filtroCategorias, GUILayout.Width(100));
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();
                if (tabTareas == 2)
                {


                    if (GUILayout.Button("Agregar", EditorStyles.miniButton))
                    {
                        _dataTarea.Tareas.Add(new MTodoTarea(_Titulo, _Descripcion, _Categoria, "Fecha Inicio " + System.DateTime.Today + "||" + System.DateTime.Today.Hour, false, Color.white));
                    }
                    _Categoria = EditorGUILayout.TextField(_Categoria);
                    _Titulo = EditorGUILayout.TextField(_Titulo);
                    if (GUILayout.Button("0", EditorStyles.miniButton, GUILayout.MaxWidth(20)))
                    {
                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();

                    _Descripcion = EditorGUILayout.TextArea(_Descripcion, GUILayout.ExpandWidth(true), GUILayout.Height(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("Fecha Inicio " + System.DateTime.Today);
                    EditorGUILayout.LabelField("Estado Abierto");


                }
                DibujarTareas();
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DibujarTareas()
        {


            using (new ScrollviewBlock(ref _mainAreaScroll))
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Activas", EditorStyles.miniButton))
                {
                    tabTareas = 0;
                }
                if (GUILayout.Button("Completadas", EditorStyles.miniButton))
                {
                    tabTareas = 1;
                }
                if (GUILayout.Button("+", EditorStyles.miniButton))
                {
                    tabTareas = 2;
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginVertical("box");

                for (int n = 0; n < _dataTarea.Tareas.Count; n++)
                {
                    EditorGUILayout.BeginVertical("box");

                    EditorGUILayout.BeginHorizontal();

                    if (tabTareas == 0)
                    {
                        if (_dataTarea.Tareas[n].Terminada == false)
                        {
                            _dataTarea.Tareas[n].Terminada = EditorGUILayout.Toggle(_dataTarea.Tareas[n].Terminada);
                            _dataTarea.Tareas[n].Categoria = EditorGUILayout.TextField("Categoria");
                            _dataTarea.Tareas[n].Titulo = EditorGUILayout.TextField(_dataTarea.Tareas[n].Titulo);
                            if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.MaxWidth(20)))
                            {
                                _dataTarea.Tareas.RemoveAt(n);
                            }
                            EditorGUILayout.EndHorizontal();


                            EditorGUILayout.BeginHorizontal();

                            _dataTarea.Tareas[n].Descripcion = EditorGUILayout.TextArea(_dataTarea.Tareas[n].Descripcion, GUILayout.ExpandWidth(true), GUILayout.Height(100));
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("Fecha Inicio " + _dataTarea.Tareas[n].Fecha);
                            if (_dataTarea.Tareas[n].Terminada == true)
                            {
                                EditorGUILayout.LabelField("Estado Cerrado");
                            }
                            else
                            {
                                EditorGUILayout.LabelField("Estado Abierto");
                            }
                        }

                    }

                    if (tabTareas == 1)
                    {
                        if (_dataTarea.Tareas[n].Terminada == true)
                        {
                            _dataTarea.Tareas[n].Terminada = EditorGUILayout.Toggle(_dataTarea.Tareas[n].Terminada);
                            _dataTarea.Tareas[n].Categoria = EditorGUILayout.TextField("Categoria");
                            EditorGUILayout.LabelField(StrikeThrough(_dataTarea.Tareas[n].Titulo));
                            if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.MaxWidth(20)))
                            {
                                _dataTarea.Tareas.RemoveAt(n);
                            }
                            EditorGUILayout.EndHorizontal();


                            EditorGUILayout.BeginHorizontal();

                            _dataTarea.Tareas[n].Descripcion = EditorGUILayout.TextArea(_dataTarea.Tareas[n].Descripcion, GUILayout.ExpandWidth(true), GUILayout.Height(100));
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("Fecha Inicio " + _dataTarea.Tareas[n].Fecha);
                            if (_dataTarea.Tareas[n].Terminada == true)
                            {
                                EditorGUILayout.LabelField("Estado Cerrado");
                            }
                            else
                            {
                                EditorGUILayout.LabelField("Estado Abierto");
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }


                EditorGUILayout.EndVertical();

            }
        }

        /// <summary>
        /// <para>MainArea de MTodo</para>
        /// </summary>
        private void MainArea()// MainArea de MTodo
        {
            using (new VerticalBlock(GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                using (new ScrollviewBlock(ref _mainAreaScroll))
                    for (var i = 0; i < _ticketsAMostrar.Length; i++)
                        TicketField(i);
            }
        }

        /// <summary>
        /// <para>Area de trello</para>
        /// </summary>
        private void AreaTrello()// Area de trello
        {

            using (new VerticalBlock(GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                EditorGUILayout.BeginVertical();
                if (listaActual != "")
                {
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.BeginHorizontal("box");

                    EditorGUILayout.LabelField("Enviar al tablero " + boardActual + " en la lista " + listaActual + " la proxima card.");

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.BeginHorizontal("box");

                    EditorGUILayout.LabelField("Titulo de la tarjeta");
                    cardTitulo = EditorGUILayout.TextField(cardTitulo);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal("box");

                    EditorGUILayout.LabelField("Descripcion de la tarjeta");
                    cardDescripcion = EditorGUILayout.TextArea(cardDescripcion);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal("box");

                    EditorGUILayout.LabelField("Fecha de la tarjeta");
                    cardFecha = EditorGUILayout.TextField(cardFecha);

                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginHorizontal("box");

                    if (GUILayout.Button("Mandar"))
                    {
                        MandarTrello();
                    }

                    if (envioTrello == true)
                    {
                        EditorGUILayout.HelpBox("Tarjeta Enviada.", MessageType.Info);
                    }

                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.BeginHorizontal("box");

                    EditorGUILayout.HelpBox("Seleccionar primero Tablero y luego lista",MessageType.Error);

                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
        
        }

        /// <summary>
        /// <para>Zona de las Tags</para>
        /// </summary>
        /// <param name="index">Posicion</param>
        private void TagField(int index)// Zona de las Tags
        {
            Event e = Event.current;
            var tag = index == -1 ? "Todos los Tags" : _data.ListaTags[index];
            using (new HorizontalBlock(EditorStyles.helpBox))
            {
                using (new ColoredBlock(index == _actualTag ? Color.green : Color.white))
                {
                    GUILayout.Label(tag);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("(" + _data.GetCountPorTag(index) + ")");
                }
                if (index != -1 && index != 0 && index != 1)
                {
                    if (GUILayout.Button("x", EditorStyles.miniButton))
                        EditorApplication.delayCall += () =>
                        {
                            _data.RemoveTag(index);
                            Repaint();
                        };
                }
            }
            var rect = GUILayoutUtility.GetLastRect();
            if (e.isMouse && e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
                SetActualTag(index);
        }

        /// <summary>
        /// <para>Agrega una tag a la zona de tags</para>
        /// </summary>
        private void AddTagField()// Agrega una tag a la zona de tags
        {
            using (new HorizontalBlock(EditorStyles.helpBox))
            {
                _nuevoNombreTag = EditorGUILayout.TextField(_nuevoNombreTag);
                if (GUILayout.Button("Agregar", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
                {
                    _data.AddTag(_nuevoNombreTag);
                    _nuevoNombreTag = "";
                    GUI.FocusControl(null);
                }
            }
        }

        /// <summary>
        /// <para>Zona de los tickets</para>
        /// </summary>
        /// <param name="index">Posicion</param>
        private void TicketField(int index)// Zona de los tickets
        {
            var entry = _ticketsAMostrar[index];
            using (new VerticalBlock(EditorStyles.helpBox))
            {
                using (new HorizontalBlock())
                {
                    GUILayout.Label(entry.Tag, EditorStyles.boldLabel);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(entry.PathAMostrar, EditorStyles.miniBoldLabel);
                }
                GUILayout.Space(5f);
                GUILayout.Label(entry.Texto, EditorStyles.largeLabel);
            }
            Event e = Event.current;
            var rect = GUILayoutUtility.GetLastRect();
            if (e.isMouse && e.type == EventType.MouseDown && rect.Contains(e.mousePosition) && e.clickCount == 2)
                EditorApplication.delayCall += () =>
                {
                    UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(entry.Archivo, entry.Linea);
                };
        }

        /// <summary>
        /// <para>Dibuja la progress bar</para>
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="maxEstado"></param>
        private void DibujarProgressBar(float estado, float maxEstado)// Dibuja la progress bar
        {

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));
            EditorGUI.ProgressBar(new Rect(190, 1, 350, 15), 1 - (estado / maxEstado), estado == 0 ? "Completado" : "Completado " + (maxEstado - estado) + "/" + maxEstado);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(15);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Carga el sistema de variables y convierne int en Sistema</para>
        /// </summary>
        /// <returns>El Sistema</returns>
        private Sistemas CargarSistema()// Carga el sistema de variables y convierne int en Sistema
        {
            int sist = EditorPrefs.GetInt("SistemaMTodo");

            switch (sist)
            {
                case 0:
                    return Sistemas.Todo;

                case 1:
                    return Sistemas.Tareas;

                case 2:
                    return Sistemas.Trello;

                default:
                    return Sistemas.Todo;
            }
        }

        /// <summary>
        /// <para>Guardar el sistema de variables y convierne sistema en int</para>
        /// </summary>
        /// <returns>El Sistema</returns>
        private int GuardarSistema()// Guardar el sistema de variables y convierne sistema en int
        {
            Sistemas sist = MTodoAlexandria.sistemaMTodo;

            switch (sist)
            {
                case  Sistemas.Todo:
                    return 0;

                case  Sistemas.Tareas:
                    return 1;

                case Sistemas.Trello:
                    return 2;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// <para>Cuando Cambia</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void OnChanged(object obj, FileSystemEventArgs e)// Cuando Cambia
        {
            EditorApplication.delayCall += () => EscaneaArchivo(e.FullPath);
        }

        /// <summary>
        /// <para>Cuando se Crea</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void OnCreated(object obj, FileSystemEventArgs e)// Cuando se Crea
        {
            EditorApplication.delayCall += () => EscaneaArchivo(e.FullPath);
        }

        /// <summary>
        /// <para>Cuando se Borra</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void OnDeleted(object obj, FileSystemEventArgs e)// Cuando se Borra
        {
            EditorApplication.delayCall += () => _data.Tickets.RemoveAll(en => en.Archivo == e.FullPath);
        }

        /// <summary>
        /// <para>Cuando se Renombra</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void OnRenamed(object obj, FileSystemEventArgs e)// Cuando se Renombra
        {
            EditorApplication.delayCall += () => EscaneaArchivo(e.FullPath);
        }

        /// <summary>
        /// <para>Escanea todos los archivos del proyecto</para>
        /// </summary>
        private void ScanTodosLosArchivos()// Escanea todos los archivos del proyecto
        {
            RefrescarArchivos();
            foreach (var file in _archivos.Where(file => file.Exists))
            {
                EscaneaArchivo(file.FullName);
            }
        }

        /// <summary>
        /// <para>Escanea un archivo</para>
        /// </summary>
        /// <param name="pathArchivo">Archivo a escanear</param>
        private void EscaneaArchivo(string pathArchivo)// Escanea un archivo
        {
            var file = new FileInfo(pathArchivo);
            if (!file.Exists)
                return;

            var entries = new List<MTodoTickets>();
            _data.Tickets.RemoveAll(e => e.Archivo == pathArchivo);

            var parser = new MTodoParse(pathArchivo, Tags);

            entries.AddRange(parser.Parse());
            var temp = entries.Except(_data.Tickets);
            _data.Tickets.AddRange(temp);
        }

        /// <summary>
        /// <para>Refresca los archivos</para>
        /// </summary>
        private void RefrescarArchivos()// Refresca los archivos
        {
            var assetsDir = new DirectoryInfo(Application.dataPath);

            _archivos =
                assetsDir.GetFiles("*.cs", SearchOption.AllDirectories)
                    .Concat(assetsDir.GetFiles("*.js", SearchOption.AllDirectories))
                    .ToArray();
        }

        /// <summary>
        /// <para>Guarda la configuracion de trello</para>
        /// </summary>
        private void GuardarTrello()// Guarda la configuracion de trello
        {
            //trelloAutentificacion = true;
            EditorPrefs.SetBool("TRELLO_AUTEN",trelloAutentificacion);
            EditorPrefs.SetString("TRELLO_KEY", key);
            EditorPrefs.SetString("TRELLO_TOKEN", token);
        }

        /// <summary>
        /// <para>Carga la configuracion de trello</para>
        /// </summary>
        private void CargarTrello()// Carga la configuracion de trello
        {
            //trelloAutentificacion = EditorPrefs.GetBool("TRELLO_AUTEN");
            key = EditorPrefs.GetString("TRELLO_KEY");
            token = EditorPrefs.GetString("TRELLO_TOKEN");
            AutentificarTrello(key, token);
            CargarBoards();
        }

        /// <summary>
        /// <para>Logea en trello</para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        public void AutentificarTrello(string key, string token)// Logea en trello
        {
            this.key = key;
            this.token = token;
        }

        /// <summary>
        /// <para>Manda una card a Trello</para>
        /// </summary>
        public void MandarTrello()// Manda una card a Trello
        {
            AutentificarTrello(key, token);

            CargarBoards();
            SetActualBoard(boardActual);

            CargarList();
            setActualList(listaActual);

            var card = nuevaCard();
            card.nombre = cardTitulo;
            card.descripcion = cardDescripcion;
            card.fecha = cardFecha;

            uploadCard(card);

            cardTitulo = "";
            cardDescripcion = "";
            cardFecha = "";

            Debug.Log("Enviada la tarjeta, envio completado.");
            envioTrello = true;

            // FIX Quitar la exception 
            /*try
            {
                throw new UnityException("Prueba");
            }
            catch (UnityException e)
            {
                uploadExceptionCard(e);
            }*/

        }

        #region Trello (Beta)
        /// <summary>
        /// <para>Comprueba si un objeto WWW ha tenido un error y, si es asi, lanza una excepcion para tratarlo.</para>
        /// </summary>
        /// <param name="errorMsj">Mensaje del error.</param>
        /// <param name="www">El objeto requerido.</param>
        private void checkWwwStatus(string errorMsj, WWW www)// Comprueba si un objeto WWW ha tenido un error y, si es asi, lanza una excepcion para tratarlo.
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                throw new TrelloException(errorMsj + ": " + www.error);
            }
        }

        /// <summary>
        /// <para>Descargar la lista de tableros disponibles para el usuario, estas se almacenan en cache y permiten que CargarBoards() funcione.</para>
        /// </summary>
        /// <returns>Una lista de tableros en JSON.</returns>
        public List<object> CargarBoards()// Descargar la lista de tableros disponibles para el usuario, estas se almacenan en cache y permiten que CargarBoards() funcione
        {
            envioTrello = false;
            //nombresBoards.Clear();

            boards = null;
            WWW www = new WWW(memberBaseUrl + "?" + "key=" + key + "&token=" + token + "&boards=all");

            while (!www.isDone)
            {
                checkWwwStatus("La conexion a los servidores Trello no fue posible ", www);
            }

            var dict = Json.Deserialize(www.text) as Dictionary<string, object>;

            Debug.Log(www.text);

            boards = (List<object>)dict["boards"];
            for (int i = 0; i < boards.Count; i++)
            {
                var board = (Dictionary<string, object>)boards[i];
                if (nombresBoards.Count < boards.Count)
                {
                    nombresBoards.Add(board["name"].ToString());
                }
            }
            trelloAutentificacion = true;

            return boards;
        }

        /// <summary>
        /// <para>Establece el tablero actual para buscar las listas en el.</para>
        /// </summary>
        /// <param name="nombre">Nombre del tablero.</param>
        public void SetActualBoard(string nombre)// Establece el tablero actual para buscar las listas en el
        {
            if (boards == null)
            {
                CargarBoards();
                DibujarGUITrelloBoard();
                throw new TrelloException("No se ha seleccionado el tablero.");
            }

            for (int i = 0; i < boards.Count; i++)
            {
                var board = (Dictionary<string, object>)boards[i];
                if ((string)board["name"] == nombre)
                {
                    currentBoardId = (string)board["id"];
                    boardActual = board["name"].ToString();
                    CargarList();
                    return;
                }
            }

            currentBoardId = "";
            throw new TrelloException("No se encontro el tablero.");
        }

        /// <summary>
        /// <para>Descargar las listas del tablero actual, estas se almacenan en cache para cargar la tarjeta mas facilmente.</para>
        /// </summary>
        /// <returns>Una lista de listas en JSON.</returns>
        public List<object> CargarList()// Descargar las listas del tablero actual, estas se almacenan en cache para cargar la tarjeta mas facilmente
        {
            nombresListas.Clear();
            //lists = null;

            if (currentBoardId == "")
            {
                throw new TrelloException("No se puede encontrar la lista.");
            }

            WWW www = new WWW(boardBaseUrl + currentBoardId + "?" + "key=" + key + "&token=" + token + "&lists=all");


            while (!www.isDone)
            {
                checkWwwStatus("La conexion a los servidores Trello no fue posible ", www);
            }

            var dict = Json.Deserialize(www.text) as Dictionary<string, object>;

            //Debug.Log(www.text);

            lists = (List<object>)dict["lists"];
            for (int i = 0; i < lists.Count; i++)
            {
                var list = (Dictionary<string, object>)lists[i];
                //Debug.Log("Nombre: " + board["name"] + " BoardsCount: " + boards.Count);
                if (nombresListas.Count < lists.Count)
                {
                    nombresListas.Add(list["name"].ToString());
                }
            }
            return lists;
        }

        /// <summary>
        /// <para>Actualiza la lista actual para cargar tarjetas.</para>
        /// </summary>
        /// <param name="nombre">Nombre de las listas.</param>
        public void setActualList(string nombre)// Actualiza la lista actual para cargar tarjetas
        {
            if (lists == null)
            {
                throw new TrelloException("No se ha seleccionado la lista.");
            }

            for (int i = 0; i < lists.Count; i++)
            {
                var list = (Dictionary<string, object>)lists[i];
                if ((string)list["name"] == nombre)
                {
                    currentListId = (string)list["id"];
                    listaActual = list["name"].ToString();
                    //Debug.Log(currentListId);
                    return;
                }
            }

            currentListId = "";
            throw new TrelloException("No se encontro la lista.");
        }

        /// <summary>
        /// <para>Crea una nueva tarjeta de trello.</para>
        /// </summary>
        /// <returns>La Tarjeta.</returns>
        public TrelloCard nuevaCard()// Crea una nueva tarjeta de trello
        {
            if (currentListId == "")
            {
                throw new TrelloException("No se puede crear una tarjeta cuando no se ha seleccionado una lista. ");
            }

            var card = new TrelloCard();
            card.idList = currentListId;
            return card;
        }

        /// <summary>
        /// <para>Dado un objeto de excepcion, se crea una tarjeta TrelloCard y se rellena con la informacion relevante de la excepcion. Esto se carga en el servidor Trello.</para>
        /// </summary>
        /// <returns>La exception de la tarjeta.</returns>
        /// <param name="e">E.</param>
        public TrelloCard uploadExceptionCard(Exception e)// Dado un objeto de excepcion, se crea una tarjeta TrelloCard y se rellena con la informacion relevante de la excepcion. Esto se carga en el servidor Trello.
        {
            TrelloCard card = new TrelloCard();
            card.nombre = e.GetType().ToString();
            card.fecha = DateTime.Now.ToString();
            card.descripcion = e.Message;
            card.idList = currentListId;

            return uploadCard(card);
        }

        /// <summary>
        /// <para>Carga una tarjeta a los servidores de Trello.</para>
        /// </summary>
        /// <returns>Tu Tarjeta.</returns>
        /// <param name="card">Tu Tarjeta.</param>
        public TrelloCard uploadCard(TrelloCard card)// Carga una tarjeta a los servidores de Trello
        {
            WWWForm post = new WWWForm();
            post.AddField("name", card.nombre);
            post.AddField("desc", card.descripcion);
            post.AddField("due", card.fecha);
            post.AddField("idList", card.idList);
            post.AddField("urlSource", card.urlSource);

            WWW www = new WWW(cardBaseUrl + "?" + "key=" + key + "&token=" + token, post);

            while (!www.isDone)
            {
                checkWwwStatus("No se pudo cargar la tarjeta Trello", www);
            }

            return card;
        }

        /// <summary>
        /// <para>Dibuja la UI de los Boards.</para>
        /// </summary>
        public void DibujarGUITrelloBoard()// Dibuja la UI de los Boards
        {
            scrollPosTrelloBoard =  EditorGUILayout.BeginScrollView(scrollPosTrelloBoard, GUILayout.Width(SidebarWidth), GUILayout.ExpandHeight(true));
            if (nombresBoards.Count <= 202)
            {
                for (int n = 0; n < nombresBoards.Count; n++)
                {
                    if (nombresBoards[n] == boardActual)
                    {
                        GUI.backgroundColor = Color.green;
                        if (GUILayout.Button(nombresBoards[n], GUILayout.Width(SidebarWidth - 30), GUILayout.Height(50)))
                        {
                            AutentificarTrello(key, token);
                            CargarBoards();
                            SetActualBoard(nombresBoards[n]);
                            CargarList();
                        }
                    }
                    else
                    {
                        GUI.backgroundColor = Color.white;
                        if (GUILayout.Button(nombresBoards[n], GUILayout.Width(SidebarWidth - 30), GUILayout.Height(50)))
                        {
                            AutentificarTrello(key, token);
                            CargarBoards();
                            SetActualBoard(nombresBoards[n]);
                            CargarList();
                        }
                    }
                }
            }else
            {
                nombresBoards.Clear();
            }


            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// <para>Dibuja la UI de las listas.</para>
        /// </summary>
        public void DibujarGUITrelloList()// Dibuja la UI de las listas.
        {
            scrollPosTrelloLista = EditorGUILayout.BeginScrollView(scrollPosTrelloLista, GUILayout.Width(SidebarWidth), GUILayout.ExpandHeight(true));
            if (nombresListas.Count <= 100)
            {
                for (int n = 0; n < nombresListas.Count; n++)
                {
                    if (nombresListas[n] == listaActual)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUI.backgroundColor = Color.green;
                        if (GUILayout.Button(nombresListas[n], GUILayout.Width(SidebarWidth - 30), GUILayout.Height(50)))
                        {
                            setActualList(nombresListas[n]);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUI.backgroundColor = Color.white;
                        if (GUILayout.Button(nombresListas[n], GUILayout.Width(SidebarWidth - 30), GUILayout.Height(50)))
                        {
                            setActualList(nombresListas[n]);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                nombresListas.Clear();
            }

            EditorGUILayout.EndScrollView();
        }
        #endregion
        #endregion

        #region Funcionalidad
        public string StrikeThrough(string s)
        {
            string strikethrough = "";
            foreach (char c in s)
            {
                strikethrough = strikethrough + c + '\u0336';
            }
            return strikethrough;
        }

        /// <summary>
        /// <para>Carga los nombres de las categorias</para>
        /// </summary>
        private void CargarNombresCategorias()// Carga los nombres de las categorias
        {
            categorias = new string[_dataTarea.Categorias.Count];
            filtroCategorias = new string[_dataTarea.Categorias.Count + 1];
            filtroCategorias[0] = "Todas las Categorias";
            for (int i = 0; i < _dataTarea.Categorias.Count; i++)
            {
                categorias[i] = _dataTarea.Categorias[i].Nombre;
                filtroCategorias[i + 1] = _dataTarea.Categorias[i].Nombre;
            }
        }
        #endregion
    }
}
