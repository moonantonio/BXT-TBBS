//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsEnemigo.cs (09/12/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Sistema general de batallas               						\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using MoonPincho.Sistemas.MUI;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Sistema general de batallas   </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SFreya/SFreya")]
    public class SFreya : MonoBehaviour
    {
        public Freya estadoBatalla;

        public AliadoGUI aliadoGUI;

        public List<clsControlTurno> proxTurno = new List<clsControlTurno>();
        public List<GameObject> aliados = new List<GameObject>();
        public List<GameObject> enemigos = new List<GameObject>();

        public List<GameObject> ManagerAliado = new List<GameObject>();
        private clsControlTurno eleccionAliado;

        public GameObject btnEnemigo;

        public GameObject padreObjetivo;
        public UIGrid gridBtns;

        public GameObject panelSeleccionEnemigo;

        public GameObject panelAcciones;
        public GameObject gridPanelAcciones;
        public GameObject panelMagia;
        public GameObject gridPanelMagia;
        public GameObject panelEspeciales;
        public GameObject panelObjetos;

        public GameObject btnAcciones;
        public GameObject btnMagias;

        public List<GameObject> botonesAtaque = new List<GameObject>();

        public List<GameObject> enemigosBotones = new List<GameObject>();

        public void Start()
        {
            estadoBatalla = Freya.Esperando;
            enemigos.AddRange(GameObject.FindGameObjectsWithTag("Enemigo"));
            aliados.AddRange(GameObject.FindGameObjectsWithTag("Aliado"));
            aliadoGUI = AliadoGUI.Activado;

            panelAcciones.SetActive(false);
            panelSeleccionEnemigo.SetActive(false);
            panelMagia.SetActive(false);
            panelEspeciales.SetActive(false);
            panelObjetos.SetActive(false);

            CrearBtnEnemigos();
        }

        public void Update()
        {
            switch (estadoBatalla)
            {
                case Freya.Esperando:
                    if (proxTurno.Count > 0)
                    {
                        estadoBatalla = Freya.TomarAccion;
                    }
                    break;

                case Freya.TomarAccion:
                    GameObject proceso = GameObject.Find(proxTurno[0].enemigo);
                    if (proxTurno[0].tipo == "Enemigo")
                    {
                        EnemigoMEstados EME = proceso.GetComponent<EnemigoMEstados>();
                        for (int n = 0; n < aliados.Count; n++)
                        {
                            if (proxTurno[0].objetivoEnemigo == aliados[n])
                            {
                                EME.posicionAliado = proxTurno[0].objetivoEnemigo;
                                EME.estadoTurno = EstadoTurno.Accion;
                                break;
                            }
                            else
                            {
                                proxTurno[0].objetivoEnemigo = aliados[Random.Range(0, aliados.Count)];
                                EME.posicionAliado = proxTurno[0].objetivoEnemigo;
                                EME.estadoTurno = EstadoTurno.Accion;
                            }
                        }
                    }

                    if (proxTurno[0].tipo == "Aliado")
                    {
                        AliadoMEstados AME = proceso.GetComponent<AliadoMEstados>();
                        AME.posicionEnemigo = proxTurno[0].objetivoEnemigo;
                        AME.estadoTurno = EstadoTurno.Accion;
                    }
                    estadoBatalla = Freya.RealizarAccion;
                    break;

                case Freya.RealizarAccion:
                    break;

                case Freya.ComprobarVida:
                    if (aliados.Count < 1)
                    {
                        // Ganar Batalla
                        estadoBatalla = Freya.Derrota;
                    }
                    else if (enemigos.Count < 1)
                    {
                        // Perder Batalla
                        estadoBatalla = Freya.Ganar;
                    }
                    else
                    {
                        // Limpiar panel
                        ClearPanelAtaque();
                        aliadoGUI = AliadoGUI.Activado;
                    }
                    break;

                case Freya.Derrota:
                    NGUIDebug.Log("Has perdido la batalla");
                    for (int i = 0; i < enemigos.Count; i++)
                    {
                        enemigos[0].GetComponent<EnemigoMEstados>().estadoTurno = EstadoTurno.Esperando;
                    }
                    break;

                case Freya.Ganar:
                    NGUIDebug.Log("Has ganado la batalla");
                    for (int i = 0; i < aliados.Count; i++)
                    {
                        aliados[0].GetComponent<AliadoMEstados>().estadoTurno = EstadoTurno.Esperando;
                    }
                    break;
            }

            switch (aliadoGUI)
            {
                case AliadoGUI.Activado:
                    if (ManagerAliado.Count > 0)
                    {
                        ManagerAliado[0].transform.Find("Selector").gameObject.SetActive(true);
                        eleccionAliado = new clsControlTurno();

                        panelAcciones.SetActive(true);

                        CrearBtnAtaques();


                        aliadoGUI = AliadoGUI.Esperando;
                    }
                    
                    break;

                case AliadoGUI.Esperando:
                    Time.timeScale = 0.0f;
                    break;

                case AliadoGUI.Finalizado:
                    AliadoTurnoTerminado();
                    break;
            }
        }

        public void Acciones(clsControlTurno input)
        {
            proxTurno.Add(input);
        }

        public void CrearBtnEnemigos()
        {
            // Limpiar botones
            foreach (GameObject enemigosBtn in enemigosBotones)
            {
                Destroy(enemigosBtn);
            }
            enemigosBotones.Clear();

            // Crear botones
            foreach (GameObject enemigo in enemigos)
            {

                GameObject nuevoBtn = NGUITools.AddChild(padreObjetivo,btnEnemigo);
                enemigosBotones.Add(nuevoBtn);
                SeleccionEnemigo btn = nuevoBtn.GetComponent<SeleccionEnemigo>();

                EnemigoMEstados enemigoActual = enemigo.GetComponent<EnemigoMEstados>();

                UILabel txtBtn = nuevoBtn.transform.Find("Texto").gameObject.GetComponent<UILabel>();
                txtBtn.text = enemigoActual.enemigo.nombre;

                btn.prefabEnemigo = enemigo;

                gridBtns.GetComponent<UIGrid>().Reposition();
            }
        }

        public void AccionAtaque()
        {
            eleccionAliado.enemigo = ManagerAliado[0].name;
            eleccionAliado.prefabEnemigo = ManagerAliado[0];
            eleccionAliado.tipo = "Aliado";
            eleccionAliado.ataqueElegido = ManagerAliado[0].GetComponent<AliadoMEstados>().aliado.ataques[0];
            panelAcciones.SetActive(false);
            panelSeleccionEnemigo.SetActive(true);
        }

        public void AccionEspecial()
        {
            eleccionAliado.enemigo = ManagerAliado[0].name;
            eleccionAliado.prefabEnemigo = ManagerAliado[0];
            eleccionAliado.tipo = "Aliado";

            panelAcciones.SetActive(false);
            panelSeleccionEnemigo.SetActive(true);
        }

        public void AccionDefensa()
        {
            eleccionAliado.enemigo = ManagerAliado[0].name;
            eleccionAliado.prefabEnemigo = ManagerAliado[0];
            eleccionAliado.tipo = "Aliado";

            panelAcciones.SetActive(false);
            panelSeleccionEnemigo.SetActive(true);
        }

        public void AccionMagia(clsAtaques eleMag)
        {
            eleccionAliado.enemigo = ManagerAliado[0].name;
            eleccionAliado.prefabEnemigo = ManagerAliado[0];
            eleccionAliado.tipo = "Aliado";

            eleccionAliado.ataqueElegido = eleMag;
            panelMagia.SetActive(false);
            panelSeleccionEnemigo.SetActive(true);
        }

        public void AccionObjetos()
        {
            eleccionAliado.enemigo = ManagerAliado[0].name;
            eleccionAliado.prefabEnemigo = ManagerAliado[0];
            eleccionAliado.tipo = "Aliado";

            panelAcciones.SetActive(false);
            panelSeleccionEnemigo.SetActive(true);
        }

        public void SeleccionEnemigo(GameObject enemigoSeleccionado)
        {
            eleccionAliado.objetivoEnemigo = enemigoSeleccionado;
            aliadoGUI = AliadoGUI.Finalizado;
        }

        public void CambiarAAtaquesMagicos()
        {
            panelAcciones.SetActive(false);
            panelMagia.SetActive(true);
        }

        public void AliadoTurnoTerminado()
        {
            proxTurno.Add(eleccionAliado);

            // Limpiar el panel de ataque
            ClearPanelAtaque();

            ManagerAliado[0].transform.Find("Selector").gameObject.SetActive(false);
            ManagerAliado.RemoveAt(0);
            aliadoGUI = AliadoGUI.Activado;
        }

        public void ClearPanelAtaque()
        {
            panelSeleccionEnemigo.SetActive(false);
            panelAcciones.SetActive(false);
            panelMagia.SetActive(false);
            panelEspeciales.SetActive(false);
            panelObjetos.SetActive(false);

            foreach (GameObject botonAtaque in botonesAtaque)
            {
                Destroy(botonAtaque);
            }
            botonesAtaque.Clear();
        }

        public void CrearBtnAtaques()
        {
            GameObject btnMagia = NGUITools.AddChild(gridPanelAcciones, btnAcciones);
            UILabel txtBtnMagia = btnMagia.transform.Find("Texto").gameObject.GetComponent<UILabel>();
            txtBtnMagia.text = "Magia";
            btnMagia.GetComponent<UIButton>();
            EventDelegate del2 = new EventDelegate(this, "CambiarAAtaquesMagicos");
            EventDelegate.Set(btnMagia.GetComponent<UIButton>().onClick, del2);
            botonesAtaque.Add(btnMagia);

            if (ManagerAliado[0].GetComponent<AliadoMEstados>().aliado.magias.Count > 0)
            {
                foreach (clsAtaques ataqueMagico in ManagerAliado[0].GetComponent<AliadoMEstados>().aliado.magias)
                {
                    GameObject btnMagico = NGUITools.AddChild(gridPanelMagia, btnMagias);
                    UILabel txtBtnMagia1 = btnMagico.transform.Find("Texto").gameObject.GetComponent<UILabel>();
                    txtBtnMagia1.text = ataqueMagico.nombreAtaque;
                    BtnMagias BMS = btnMagico.GetComponent<BtnMagias>();
                    BMS.procesoAtaqueMagico = ataqueMagico;
                    botonesAtaque.Add(btnMagico);

                    gridPanelMagia.GetComponent<UIGrid>().Reposition();
                }
            }
            else
            {
                btnMagia.GetComponent<UIButton>().enabled = false;
                gridPanelMagia.GetComponent<UIGrid>().Reposition();
            }

            GameObject btnAtaque = NGUITools.AddChild(gridPanelAcciones, btnAcciones);
            UILabel txtBtnAtaque = btnAtaque.transform.Find("Texto").gameObject.GetComponent<UILabel>();
            txtBtnAtaque.text = "Atacar";
            btnAtaque.GetComponent<UIButton>();
            EventDelegate del = new EventDelegate(this, "AccionAtaque");
            EventDelegate.Set(btnAtaque.GetComponent<UIButton>().onClick, del);
            botonesAtaque.Add(btnAtaque);
  
            gridPanelAcciones.GetComponent<UIGrid>().Reposition();
        }
    }

    public enum Freya
    {
        Esperando,
        TomarAccion,
        RealizarAccion,
        ComprobarVida,
        Ganar,
        Derrota
    }

    public enum EstadoTurno
    {
        Procesando,
        AgregandoALista,
        Esperando,
        Seleccionando,
        Accion,
        Muerte,
        EscogerAccion
    }

    public enum AliadoGUI
    {
        Activado,
        Esperando,
        Finalizado
    }
}