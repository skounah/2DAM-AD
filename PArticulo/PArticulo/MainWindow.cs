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
	
		//LANZADOR NUEVO ARTICULO
		newAction.Activated += delegate{
			new ArticuloView();
		};
		//LANZADOR ACTRUALIZAR TABLA
		refreshAction.Activated += delegate {
			fillTreeview();
		};
		//LANZADOR BORRAR ARTICULO
		deleteAction.Activated += delegate{
			object id = TreeViewHelper.GetId(TreeView);
			Console.WriteLine("click en delete action id={0}", id);
			delete(id);
		};
		//LANZADOR EDITAR ARTICULO (LANZA LA MISMA VENTANA QUE EL NUEVO ARTICULO)
		editAction.Activated += delegate{
			object id = TreeViewHelper.GetId(TreeView);

			new ArticuloView(id);
		};

		//SELECCION DEL TREEVIEW
		TreeView.Selection.Changed += delegate(object sender, EventArgs e) {
			Console.WriteLine("Cambio en el Selection action");
			deleteAction.Sensitive = TreeViewHelper.IsSelected(TreeView); // SI NO HAY NADA SELECCIONADO NO DEJA EJECUTAR LA ACCION DE BORRAR
			editAction.Sensitive = TreeViewHelper.IsSelected(TreeView);   // SI NO HAY NADA SELECCIONADO NO DEJA EJECUTAR LA ACCION DE EDITAR
		};
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;
	}

	//METODOS FUERA DEL MAIN 

		//METODO DE BORRADO
	private void delete(object id) {
		if (!WindowHelper.ConfirmDelete (this)) {
			Console.WriteLine ("Cancelado el borrado");
		} else {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "delete from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			dbCommand.ExecuteNonQuery (); //SI NO DEVUELVE UN 1 ES POR QUE ALGUN EVENTO HA OCURRIDO ANTES CON ESE ID
			fillTreeview ();
		}
	}
		//RELLENO DE TABLA (REFRESH)
	protected void fillTreeview(){
		QueryResult queryResult = PersisterHelp.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryResult);
	}

		//CERRADO DE VENTANA
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


}
