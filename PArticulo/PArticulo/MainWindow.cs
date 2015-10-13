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
		IDbConnection dbconnection = App.Instance.DbConnection;
		IDbCommand dbCommand = dbconnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";

		//CREA EL DATAREADER
		IDataReader dataReader = dbCommand.ExecuteReader ();


		//COJE LAS COLUMNAS DEL TREEVIEW DE FORMA AUTOMATICA CON EL METODO GETCOLUMNAMES
		string[] columnNames = getColumnNames (dataReader);
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
					//string value = tree_model.GetValue(iter, column).ToString();
					cellRendererText.Text= row[column].ToString();
			});
		}



		//Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (typeof(IList));


		//listStore = new ListStore (typeof(String), typeof(String), typeof (String), typeof (String));


		//COJE LAS FILAS AUTOMATICAMENTE CON EL METODO GETVALUES
		while (dataReader.Read()) {
			//string[] values = getValues (dataReader);
			IList values= getValues (dataReader);
			listStore.AppendValues (values);

		}
		dataReader.Close ();
		TreeView.Model = listStore;
		App.Instance.DbConnection.Close ();
	}


	//METODO QUE COJE EL NOMBRES DE LAS COLUMNAS DE LA BD Y LAS PASA A UN ARRAY
	private string[] getColumnNames(IDataReader dataReader){
		List<string> columnNames = new List<string> ();
		int contador = dataReader.FieldCount;
		for (int i=0;i<contador;i++)
			columnNames.Add (dataReader.GetName (i));
		return columnNames.ToArray();
	}


	//METODO QUE COJE LOS TIPOS DE LA BD Y LOS PASA A UN ARRAY DE TIPOS CON TODO STRING
	private Type [] getTypes(int count){
			List<Type> types = new List<Type>();
		for (int i=0; i<count; i++) 
			types.Add (typeof(String));
	    return types.ToArray ();
	}
	//METODO QUE DEVUELVE UN STRING CON LOS VALORES DE LAS FILAS TRANSFORMADOS A ARRAY 
	private IList getValues(IDataReader dataReader){
		List<object> values = new List<object> ();
		int count = dataReader.FieldCount;
		for (int i=0;i<count;i++)
			values.Add (dataReader [i].ToString ());
		return values;
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
