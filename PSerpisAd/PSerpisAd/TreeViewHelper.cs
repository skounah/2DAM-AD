using System;
using Gtk;
using System.Collections;

namespace SerpisAd
{
	public class TreeViewHelper
	{	//RELLENO DEL TREEVIEW
		public static void Fill(TreeView treeView,QueryResult queryResult )
		{
			removeAllColumns (treeView);
			string[] columnNames = queryResult.ColumnNames;
			CellRendererText cellRendererText = new CellRendererText ();

			//ESTABLECEMOS EL MODELO CON DELEGADO PARA LUEGO PODER TRATAR EL TEXTO RENDERIZADO
			for (int index=0; index<columnNames.Length; index++) {
				int column = index;
				treeView.AppendColumn (columnNames [index], cellRendererText, 
				delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
					IList row = (IList)tree_model.GetValue(iter,0);
					if (row[column] == DBNull.Value)
						cellRendererText.Text = null;//"sin asignar" ;LO QUE MUESTRA SI NO HAY NADA 
					else 
						cellRendererText.Text= row[column].ToString();//LO QUE MUESTRA SI SI QUE HAY ALGO 
				});
			}
			//RELLENO DE DATOS SEGUN EL MODELO.
			ListStore listStore = new ListStore (typeof(IList));

			foreach (IList row in (queryResult.Rows))
				listStore.AppendValues (row);
			treeView.Model= listStore;
		}
		//METODO PARA BORRAR COLUMNAS 
		private static void removeAllColumns(TreeView treeView) {
			TreeViewColumn[] treeViewColumns = treeView.Columns;
			foreach (TreeViewColumn treeViewColumn in treeViewColumns)
				treeView.RemoveColumn(treeViewColumn);
		}

		public static object GetId(TreeView treeView) {
				TreeIter treeIter;
				if (!treeView.Selection.GetSelected (out treeIter))
						return null;
				IList row = (IList)treeView.Model.GetValue(treeIter, 0);
						return row[0];
		}

		public static bool IsSelected(TreeView treeView) {
				TreeIter treeIter;
				return treeView.Selection.GetSelected (out treeIter);
				//o bien
				//return treeView.Selection.CountSelectedRows() != 0; 
		}
	}
}

