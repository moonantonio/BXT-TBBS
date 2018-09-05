//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// TrelloException.cs (15/11/2016)                                              \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Extension de la clase Exception para Trello   					\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

#region Librerias
using System;
#endregion

namespace MoonPincho.MEditor.ToDo.Editor
{
    /// <summary>
    /// <para>Extension de la clase Exception para Trello</para>
    /// </summary>
    public class TrelloException : Exception
    {
        #region Metodos
        public TrelloException() : base()
		{
			
		}
		
		public TrelloException(string message) : base(message)
		{
		
		}
		
		public TrelloException(string format, params object[] args) : base(string.Format(format, args))
		{
		
		}
		
		public TrelloException(string message, Exception innerException) : base(message, innerException) 
		{
		
		}
		
		public TrelloException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) 
		{
		
		}
        #endregion
    }
}