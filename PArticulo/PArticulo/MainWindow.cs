using System;
using System.Data;
using Gtk;
using PArticulo;
using SerpisAd;
using System.Collections;

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
		deleteAction.Activated += delegate{
			object id = GetId(TreeView);
			Console.WriteLine("click en delete action id={0}", id);
		};

		TreeView.Selection.Changed += delegate(object sender, EventArgs e) {
			Console.WriteLine("Cambio en el Selection action");
			deleteAction.Sensitive = GetId(TreeView) !=null; // SI NO HAY NADA SELECCIONADO NO DEJA EJECUTAR LA ACCION DE BORRAR
		};

	}

	public static object GetId(TreeView treeView) {
		TreeIter treeIter;
			if (!treeView.Selection.GetSelected (out treeIter)) {
				return null;
			}
		treeView.Selection.GetSelected(out treeIter);
		treeView.Model.GetValue(treeIter, 0);
		IList row  = (IList)treeView.Model.GetValue(treeIter, 0);
		return row [0];
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
