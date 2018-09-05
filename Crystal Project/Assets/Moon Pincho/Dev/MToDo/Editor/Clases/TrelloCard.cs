//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// TrelloCard.cs (15/11/2016)                                                 	\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion: Clase de cards(Trello) para MTodo   							\\
// Fecha Mod:       15/11/2016                                                  \\
// Ultima Mod: Version Inicial     												\\
//******************************************************************************\\

namespace MoonPincho.MEditor.ToDo.Editor
{
    /// <summary>
    /// <para>Clase de cards(Trello) para MTodo  </para>
    /// </summary>
    public class TrelloCard
    {
        #region Variables
        public string nombre = "";
		public string descripcion = "";
		public string fecha = "null";
		public string idList = "";
		public string urlSource = "null";
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor de TrelloCard</para>
        /// </summary>
        public TrelloCard(){}
        #endregion
    }
}
