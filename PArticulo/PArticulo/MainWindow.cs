using System;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWndow constructor.");
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=bdpruebas;Data Source= localhost;user ID=root;Password=sistemas"
		);

		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from articulo";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();
		while (mySqlDataReader.Read()) {
			Console.WriteLine ("id={0} nombre{1}", mySqlDataReader [0], mySqlDataReader [1]);
		}


		TreeView.AppendColumn("id", new CellRendererText(), "text",0);
		TreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		TreeView.AppendColumn ("precio", new CellRendererText (), "text", 2);

		// ESTABLECER MODELO DE cONTROLADOR DE VISTA (MVC)
		ListStore listStore = new ListStore (typeof(long), typeof (String));
		listStore.AppendValues (1L, "Nombre del primero",);
		TreeView.Model = listStore;
		listStore.AppendValues (2L, "Nombre del segundo");


		mySqlDataReader.Close ();
		mySqlConnection.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
