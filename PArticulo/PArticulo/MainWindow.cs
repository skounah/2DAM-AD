using System;
using System.Data;
using Gtk;
using PArticulo;
using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build (); 

		Console.WriteLine ("MainWndow constructor.");
		QueryResult queryResult = PersisterHelp.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryResult);
	}


	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		new ArticuloView();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


}
