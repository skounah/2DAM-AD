using System;
using System.Data;
using Gtk;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		// ESTABLECER CONEXION CON BD 
		Console.WriteLine ("MainWndow constructor.");

		IDbConnection dbConnection = new MySqlConnection (
			"Database=bdpruebas;Data Source= localhost;user ID=root;Password=sistemas"
		);
		// ABRIR CONEXION 
		dbConnection.Open ();


		// SELECCIONAR EN BASE DE DATOS
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";

		//CREA EL DATAREADER
		IDataReader dataReader = dbCommand.ExecuteReader ();

		// AÑADO LAS COLUMNAS DEL TREEVIEW DE FORMA MANUAL 
		/*TreeView.AppendColumn("categoria", new CellRendererText(), "text",0);
		TreeView.AppendColumn("id", new CellRendererText (), "text", 1);
		TreeView.AppendColumn("nombre", new CellRendererText (), "text", 2);
		TreeView.AppendColumn ("precio", new CellRendererText (), "text", 3);*/

		//COJE LAS COLUMNAS DEL TREEVIEW DE FORMA AUTOMATICA CON EL METODO GETCOLUMNAMES
		string[] columnNames = getColumnNames (dataReader);
		for (int index=0; index<columnNames.Length; index++) {
			TreeView.AppendColumn (columnNames [index], new CellRendererText (), "text", index);
		}

		Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (types);

		// ESTABLECER MODELO DE CONTROLADOR DE VISTA (MVC)
		listStore = new ListStore (typeof(String), typeof(String), typeof (String), typeof (String));
		TreeView.Model = listStore;



		// COJE LAS FILAS AUTOMATICAMENTE CON EL METODO GETVALUES
		while (dataReader.Read()) {
			string[] values = getValues (dataReader);
			listStore.AppendValues (values);

		}
		// COJE DIFERENTES FILAS MANUALMENTE
		//listStore.AppendValues (dataReader [0].ToString(), dataReader[1], dataReader[2].ToString(), dataReader [3].ToString()); ESTA LLLENDO 
		//Console.WriteLine ("categoria={0} id={1} nombre{2} precio{3}",dataReader[0], dataReader [1], dataReader [2],dataReader [3]); ESO SOLO ESCRIBE 
		//listStore.AppendValues (2L, "Nombre del segundo", 2.00); ESTAS ESCRITAS MANUALMENTE
		//listStore.AppendValues (3L, "Nombre del tercero", 3.20); "						"


		dataReader.Close ();
		dbConnection.Close ();
	}
	// METODO QUE COJE EL NOMBRES DE LAS COLUMNAS DE LA BD Y LAS PASA A UN ARRAY
	private string[] getColumnNames(IDataReader dataReader){
		List<string> columnNames = new List<string> ();
		int contador = dataReader.FieldCount;
		for (int i=0;i<contador;i++)
			columnNames.Add (dataReader.GetName (i));
		return columnNames.ToArray();
	}
	// METODO QUE COJE LOS TIPOS DE LA BD Y LOS PASA A UN ARRAY DE TIPOS CON TODO STRING
	private Type [] getTypes(int count){
			List<Type> types = new List<Type>();
		for (int i=0; i<count; i++) 
			types.Add (typeof(String));
	    return types.ToArray();
	}
	// METODO QUE DEVUELVE UN STRING CON LOS VALORES DE LAS FILAS TRANSFORMADOS A ARRAY 
	private string[]getValues(IDataReader dataReader){
		List<string> values = new List<string> ();
		int count = dataReader.FieldCount;
		for (int i=0;i<count;i++)
			values.Add (dataReader [i].ToString ());
		return values.ToArray();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
