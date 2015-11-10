using System;
using Gtk;
using MySql.Data.MySqlClient;
using SerpisAd;

namespace PCategoria
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			App.Instance.DbConnection = new MySqlConnection (
				"Database=bdpruebas;Data Source=Localhost;User Id=root;Password=sistemas"
				);
			App.Instance.DbConnection.Open ();

			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
			App.Instance.DbConnection.Close();

		}
	}
}
