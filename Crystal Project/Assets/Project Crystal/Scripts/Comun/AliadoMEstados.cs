//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsEnemigo.cs (10/11/2016)                                              	    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Maquina de estados de los aliados               				\\
// Fecha Mod:       10/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
using MoonPincho.Sistemas.MUI;
#endregion

namespace MoonPincho.Sistemas.SFreya
{
    /// <summary>
    /// <para>Maquina de estados de los aliados   </para>
    /// </summary>
    [AddComponentMenu("Moon Pincho/Sistemas/SFreya/Interno/AliadoMEstados")]
    public class AliadoMEstados : MonoBehaviour
    {
        public clsAliado aliado;
        public EstadoTurno estadoTurno;
        private SFreya sFreya;

        private float actualCD = 0;
        private float maxCD = 5f;

        public UIProgressBar progressBar;

        public GameObject selector;

        public GameObject posicionEnemigo;
        private bool iniciarAccion;
        private Vector3 posicionInicial;
        private float velAnimacion = 10.0f;

        private bool estadoSalud = true;

        private PanelAliados valoresAliados;
        public GameObject panelAliados;
        private GameObject gridPanelAliados;


        public void Start()
        {
            // FIX Para testeo
            actualCD = Random.Range(0, 2.5f);

            // Obtener el panel aliados
            gridPanelAliados = GameObject.Find("GridBarAliados");

            // Crear Panel
            CrearPanelAliado();

            // Resetear Selector
            selector.SetActive(false);

            // Encontrar el SFreya
            sFreya = GameObject.Find("SFreya").GetComponent<SFreya>();

            // Inicializar el estado del turno
            estadoTurno = EstadoTurno.Procesando;

            // Obtener la posicion inicial
            posicionInicial = transform.position;
        }

        public void Update()
        {
            switch (estadoTurno)
            {
                case EstadoTurno.Procesando:
                    ActualizarProgressBar();
                    break;

                case EstadoTurno.AgregandoALista:
                    sFreya.ManagerAliado.Add(this.gameObject);
                    estadoTurno = EstadoTurno.Esperando;
                    break;

                case EstadoTurno.Esperando:
                    // estado idle
                    break;

                case EstadoTurno.Accion:
                    StartCoroutine(TiempoDeAccion());
                    if (aliado.actualHP > 0)
                    {
                        StartCoroutine(TiempoDeAccion());
                    }
                    else
                    {
                        estadoTurno = EstadoTurno.Muerte;
                    }
                    break;

                case EstadoTurno.Muerte:
                    if (!estadoSalud)
                    {
                        return;
                    }
                    else
                    {
                        // Cambiar tag
                        this.gameObject.tag = "AliadoMuerto";

                        // No atacable por el enemigo
                        sFreya.aliados.Remove(this.gameObject);

                        // No seleccionable
                        sFreya.ManagerAliado.Remove(this.gameObject);

                        // Desactivar el selector
                        selector.SetActive(false);

                        // Resetear GUI
                        sFreya.panelAcciones.SetActive(false);
                        sFreya.panelSeleccionEnemigo.SetActive(false);

                        // Quitar procesos del proximo turno
                        if (sFreya.aliados.Count > 0)
                        {
                            for (int n = 0; n < sFreya.proxTurno.Count; n++)
                            {
                                if (sFreya.proxTurno[n].prefabEnemigo == this.gameObject)
                                {
                                    sFreya.proxTurno.Remove(sFreya.proxTurno[n]);
                                }

                                if (sFreya.proxTurno[n].objetivoEnemigo == this.gameObject)
                                {
                                    sFreya.proxTurno[n].objetivoEnemigo = sFreya.aliados[Random.Range(0, sFreya.aliados.Count)];
                                }
                            }
                        }

                        // Cambiar color / Animacion
                        this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105,105,105,255);

                        // Resetear AliadoGUI
                        sFreya.estadoBatalla = Freya.ComprobarVida;

                        // Activar estado de salud
                        estadoSalud = false;
                    }
                    break;
            }
        }

        public void ActualizarProgressBar()
        {
            actualCD = actualCD + Time.deltaTime;
            float calculoCD = actualCD / maxCD;
            progressBar.value = Mathf.Clamp(calculoCD, 0, 1);

            if (actualCD >= maxCD)
            {
                estadoTurno = EstadoTurno.AgregandoALista;
                
            }
        }

        private IEnumerator TiempoDeAccion()
        {
            if (iniciarAccion)
            {
                yield break;
            }

            iniciarAccion = true;

            //Movimiento hacia la posicion del objetivo
            Vector3 _posicionEnemigo= new Vector3(posicionEnemigo.transform.position.x + 1.5f,
                                                    posicionEnemigo.transform.position.y,
                                                    posicionEnemigo.transform.position.z);

            while (MoverHaciaEnemigo(_posicionEnemigo)) { yield return null; }

            // Esperando la habilidad
            yield return new WaitForSeconds(0.5f);

            // Pasando valores
            RealizarDamage();

            // Movimiento hacia la posicion inicial
            Vector3 primeraPos = posicionInicial;
            while (MoverHaciaInicio(primeraPos)) { yield return null; }

            // Quitar este proceso de la lista de SFreya
            sFreya.proxTurno.RemoveAt(0);

            // Resetear SFreya
            if (sFreya.estadoBatalla != Freya.Ganar && sFreya.estadoBatalla != Freya.Derrota)
            {
                sFreya.estadoBatalla = Freya.Esperando;

                // Resetear el estado del enemigo
                actualCD = 0.0f;
                estadoTurno = EstadoTurno.Procesando;
            }
            else
            {
                estadoTurno = EstadoTurno.Esperando;
            }
            
            // Fin Corutina
            
            iniciarAccion = false;
        }

        private bool MoverHaciaEnemigo(Vector3 objetivo)
        {
            return objetivo != (transform.position = Vector3.MoveTowards(transform.position, objetivo, velAnimacion * Time.deltaTime));
        }

        private bool MoverHaciaInicio(Vector3 objetivo)
        {
            return objetivo != (transform.position = Vector3.MoveTowards(transform.position, objetivo, velAnimacion * Time.deltaTime));
        }

        /// <summary>
        /// <para>Recibe daño del enemigo</para>
        /// </summary>
        public void RecibirDamage(float damage)// Recibe daño del enemigo
        {
            aliado.actualHP -= damage;
            if (aliado.actualHP <= 0)
            {
                aliado.actualHP = 0;
                estadoTurno = EstadoTurno.Muerte;
            }

            ActualizarPanelAliado();
        }

        /// <summary>
        /// <para>Hace daño al enemigo</para>
        /// </summary>
        private void RealizarDamage()// Hace daño al enemigo
        {
            float calculoDamage = aliado.actualAtack + sFreya.proxTurno[0].ataqueElegido.fuerzaAtaque;
            posicionEnemigo.GetComponent<EnemigoMEstados>().RecibirDamage(calculoDamage);
        }

        public void CrearPanelAliado()
        {
            panelAliados = NGUITools.AddChild(gridPanelAliados, panelAliados);
            valoresAliados = panelAliados.GetComponent<PanelAliados>();
            valoresAliados.txtNombre.text = aliado.nombre;
            valoresAliados.txtHP.text = aliado.actualHP.ToString() + "/" + aliado.maxHP.ToString();
            valoresAliados.progressHP.value = Mathf.Clamp(aliado.actualHP, 0, 1);
            valoresAliados.txtEP.text = aliado.actualEterion.ToString() + "/" + aliado.maxEterion.ToString();
            valoresAliados.progressEP.value = Mathf.Clamp(aliado.actualEterion, 0, 1);

            gridPanelAliados.GetComponent<UIGrid>().Reposition();
        }

        /// <summary>
        /// <para>Actualizar los valores del panel aliado</para>
        /// </summary>
        public void ActualizarPanelAliado()// Actualizar los valores del panel aliado
        {
            valoresAliados.txtNombre.text = aliado.nombre;
            valoresAliados.txtHP.text = aliado.actualHP.ToString() + "/" + aliado.maxHP.ToString();
            valoresAliados.progressHP.value = aliado.actualHP/100;
            valoresAliados.txtEP.text = aliado.actualEterion.ToString() + "/" + aliado.maxEterion.ToString();
            valoresAliados.progressEP.value = aliado.actualEterion/100;
        }
    }
}
