using System;
using System.Data;
using Gtk;
using System.Collections;
using System.Collections.Generic;

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


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
