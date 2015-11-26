using System;
using Gtk;
using System.Collections;


namespace SerpisAd
{
	public class ComboBoxHelper
	{
		public static void Fill(ComboBox comboBox,QueryResult queryResult, object id){
			CellRendererText cellRendererText = new CellRendererText ();
			comboBox.PackStart (cellRendererText, false); 								
			comboBox.SetCellDataFunc (cellRendererText, delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {	//ESTA Y LA ANTERIOR CREAN LA CAJA PARA DIBUJAR
				IList row =(IList)tree_model.GetValue(iter,0);
				cellRendererText.Text = /*row[0] +" - "+*/ row[1].ToString();       // Id - Categoria
			});
			ListStore listStore = new ListStore (typeof(IList));
			IList first = new object[] {null, "<sin asignar>"};
			TreeIter treeIterAsignado =listStore.AppendValues (first); // lo guardo en un treeIter Para utilizarlo como activo en el comboiBox
			foreach (IList row in queryResult.Rows) {
				TreeIter treeIter = listStore.AppendValues (row);
				if (row[0].Equals(id))
					treeIterAsignado = treeIter;
			}
			comboBox.Model = listStore;
			comboBox.SetActiveIter(treeIterAsignado);
		}
		public static object GetId(ComboBox comboBox) {
			TreeIter treeIter;
			comboBox.GetActiveIter (out treeIter);
			IList row = (IList)comboBox.Model.GetValue (treeIter, 0); //ILIST 0 por que es el unico elemento aunque dentro vallan las columnas
			return row [0];
		}
	}
}

