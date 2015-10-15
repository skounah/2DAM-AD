using System;
using System.Data;
using Gtk;
using System.Collections;
using System.Collections.Generic;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build (); 

		Console.WriteLine ("MainWndow constructor.");
		QueryResult queryResult = PersisterHelp.Get ("select * from articulo");
		string[] columnNames = queryResult.ColumnNames;
		CellRendererText cellRendererText = new CellRendererText ();


		for (int index=0; index<columnNames.Length; index++) {
	
		// ESTABLECEMOS EL MODELO CON DELEGADO PARA LUEGO PODER TRATAR EL TEXTO RENDERIZADO
			int column = index;
			TreeView.AppendColumn (columnNames [index], cellRendererText, 
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
		TreeView.Model= listStore;
		
	
		App.Instance.DbConnection.Close ();
	}



	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
