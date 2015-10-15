using System;
using Gtk;
using System.Collections;

namespace PArticulo
{
	public class TreeViewHelper
	{
		public static void Fill(TreeView treeView,QueryResult queryResult )
		{
			string[] columnNames = queryResult.ColumnNames;
			CellRendererText cellRendererText = new CellRendererText ();

			//ESTABLECEMOS EL MODELO CON DELEGADO PARA LUEGO PODER TRATAR EL TEXTO RENDERIZADO
			for (int index=0; index<columnNames.Length; index++) {
				int column = index;
				treeView.AppendColumn (columnNames [index], cellRendererText, 
				delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
					IList row = (IList)tree_model.GetValue(iter,0);
					if (row[column] == DBNull.Value)
						cellRendererText.Text = "sin asignar" ;
					else 
						cellRendererText.Text= row[column].ToString();
				});
			}

			ListStore listStore = new ListStore (typeof(IList));

			foreach (IList row in (queryResult.Rows))
				listStore.AppendValues (row);
			treeView.Model= listStore;
		}
	}
}

