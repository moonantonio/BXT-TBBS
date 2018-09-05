//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// CamaraMotor.cs (00/00/0000)                                              	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:     													       	\\
// Fecha Mod:       00/00/0000                                                  \\
// Ultima Mod:      												            \\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
#endregion

namespace MoonPincho.Sistemas.Camaras
{
    public class CamaraMotor : MonoBehaviour
    {
        public Transform target;
        public float smoothPosition = 0F;
        public float SmoothRotation = 0F;

        #region Inicializaciones
        /// <summary>
        /// <para>Iniciador base de CamaraMotor</para>
        /// </summary>
        private void Start() { }
        #endregion

        #region Actualizador
        /// <summary>
        /// <para>Actualizador base de CamaraMotor</para>
        /// </summary>
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * smoothPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * SmoothRotation);
        }
        #endregion
    }
}
