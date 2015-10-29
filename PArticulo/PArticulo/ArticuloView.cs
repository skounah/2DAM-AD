using Gtk;
using System;
using SerpisAd;
using System.Collections;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			entryNombre.Text = "Introduce nombre";
			spinPrecio.Value = 1.0;


			QueryResult queryResult = PersisterHelp.Get ("select * from categoria");
			CellRendererText cellRendererText = new CellRendererText ();
			boxCategoria.PackStart (cellRendererText, false); 								
			boxCategoria.SetCellDataFunc (cellRendererText, delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {	//ESTA Y LA ANTERIOR CREAN LA CAJA PARA DIBUJAR
				IList row =(IList)tree_model.GetValue(iter,0);
				cellRendererText.Text = row[0] +" - "+ row[1].ToString();        //string.Format("{0:-5} - {1}",row[0],row[1].toString
			});
			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows)
				listStore.AppendValues (row);
			boxCategoria.Model = listStore;
		}
	}
}

