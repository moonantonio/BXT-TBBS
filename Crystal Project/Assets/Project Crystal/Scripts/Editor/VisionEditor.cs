//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// VisionEditor.cs (16/12/2016)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Editor de Vision                        						\\
// Fecha Mod:		16/12/2016													\\
// Ultima Mod:	Version Inicial													\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
#endregion

namespace MoonPincho.Sistemas.Agne
{
    /// <summary>
    /// <para>Editor de Vision </para>
    /// </summary>
    [CustomEditor(typeof(Vision))]
    public class VisionEditor : Editor
    {
        public void OnSceneGUI()
        {
            Vision visio = (Vision)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(visio.transform.position, Vector3.up, Vector3.forward, 360, visio.radio);
            Vector3 anguloA = visio.dirDelAngulo(-visio.angulo / 2, false);
            Vector3 anguloB = visio.dirDelAngulo(visio.angulo / 2, false);

            Handles.DrawLine(visio.transform.position, visio.transform.position + anguloA * visio.radio);
            Handles.DrawLine(visio.transform.position, visio.transform.position + anguloB * visio.radio);

            Handles.color = Color.red;
            if (visio.objetivoVision != null)
            {
                Handles.DrawLine(visio.transform.position, visio.objetivoVision.transform.position);
            }
        }
    }
}