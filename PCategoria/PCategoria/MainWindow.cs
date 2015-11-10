using System;
using System.Data;
using Gtk;
using SerpisAd;
using PCategoria;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWndow constructor.");
		QueryResult queryResult = PersisterHelp.Get ("select * from categoria");
		TreeViewHelper.Fill (treeView, queryResult);

		/*newAction.Activated += delegate{
			new ArticuloView();
		};*/
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
