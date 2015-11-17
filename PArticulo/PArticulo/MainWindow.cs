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
		Title = "Artículo";
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
			object id = TreeViewHelper.GetId(TreeView);
			Console.WriteLine("click en delete action id={0}", id);
			delete(id);
		};

		TreeView.Selection.Changed += delegate(object sender, EventArgs e) {
			Console.WriteLine("Cambio en el Selection action");
			deleteAction.Sensitive = TreeViewHelper.IsSelected(TreeView); // SI NO HAY NADA SELECCIONADO NO DEJA EJECUTAR LA ACCION DE BORRAR
		};
		deleteAction.Sensitive = false;
	}


	private void delete(object id) {
		if (!WindowHelper.ConfirmDelete(this))
				Console.WriteLine ("Dice que eliminar NO");
		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "delete from articulo where id = @id";
		DbCommandHelper.addParameter (dbCommand, "id", id);
		int filasEliminadas = dbCommand.ExecuteNonQuery (); //SI NO DEVUELVE UN 1 ES POR QUE ALGUN EVENTO HA OCURRIDO ANTES CON ESE ID
		fillTreeview ();
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
