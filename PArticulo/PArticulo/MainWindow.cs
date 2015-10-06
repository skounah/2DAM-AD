using System;
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
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=bdpruebas;Data Source= localhost;user ID=root;Password=sistemas"
		);
		// ABRIR CONEXION 
		mySqlConnection.Open ();


		// SELECCIONAR EN BASE DE DATOS
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from articulo";

		//CREA EL DATAREADER
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		// AÑADO LAS COLUMNAS DEL TREEVIEW DE FORMA MANUAL 
		/*TreeView.AppendColumn("categoria", new CellRendererText(), "text",0);
		TreeView.AppendColumn("id", new CellRendererText (), "text", 1);
		TreeView.AppendColumn("nombre", new CellRendererText (), "text", 2);
		TreeView.AppendColumn ("precio", new CellRendererText (), "text", 3);*/

		//COJE LAS COLUMNAS DEL TREEVIEW DE FORMA AUTOMATICA CON EL METODO GETCOLUMNAMES
		string[] columnNames = getColumnNames (mySqlDataReader);
		for (int index=0; index<columnNames.Length; index++) {
			TreeView.AppendColumn (columnNames [index], new CellRendererText (), "text", index);
		}

		Type[] types = getTypes (mySqlDataReader.FieldCount);
		ListStore listStore = new ListStore (types);

		// ESTABLECER MODELO DE CONTROLADOR DE VISTA (MVC)
		listStore = new ListStore (typeof(String), typeof(String), typeof (String), typeof (String));
		TreeView.Model = listStore;



		// COJE LAS FILAS AUTOMATICAMENTE CON EL METODO GETVALUES
		while (mySqlDataReader.Read()) {
			string[] values = getValues (mySqlDataReader);
			listStore.AppendValues (values);

		}
		// COJE DIFERENTES FILAS MANUALMENTE
		//listStore.AppendValues (mySqlDataReader [0].ToString(), mySqlDataReader[1], mySqlDataReader[2].ToString(), mySqlDataReader [3].ToString()); ESTA LLLENDO 
		//Console.WriteLine ("categoria={0} id={1} nombre{2} precio{3}",mySqlDataReader[0], mySqlDataReader [1], mySqlDataReader [2],mySqlDataReader [3]); ESO SOLO ESCRIBE 
		//listStore.AppendValues (2L, "Nombre del segundo", 2.00); ESTAS ESCRITAS MANUALMENTE
		//listStore.AppendValues (3L, "Nombre del tercero", 3.20); "						"


		mySqlDataReader.Close ();
		mySqlConnection.Close ();
	}
	// METODO QUE COJE EL NOMBRES DE LAS COLUMNAS DE LA BD Y LAS PASA A UN ARRAY
	private string[] getColumnNames(MySqlDataReader mySqlDataReader){
		List<string> columnNames = new List<string> ();
		int contador = mySqlDataReader.FieldCount;
		for (int i=0;i<contador;i++)
			columnNames.Add (mySqlDataReader.GetName (i));
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
	private string[]getValues(MySqlDataReader mySqlDataReader){
		List<string> values = new List<string> ();
		int count = mySqlDataReader.FieldCount;
		for (int i=0;i<count;i++)
			values.Add (mySqlDataReader [i].ToString ());
		return values.ToArray();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
