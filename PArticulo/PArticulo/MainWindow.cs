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
		fillTreeview ();
		
		//TreeView.Columns();
		//TreeView.Refresh ();

		newAction.Activated += delegate{
			new ArticuloView();
		};

		refreshAction.Activated += delegate {
			fillTreeview();
		};

	}

	protected void fillTreeview(){
		QueryResult queryResult = PersisterHelp.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryResult);
	}
	/*protected void OnNewActionActivated (object sender, EventArgs e) {  ESTE METODO SIRVE SI HAS ACITVADO LA SEÑAL EN EL BOTON
		new ArticuloView();
	}*/

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


}
