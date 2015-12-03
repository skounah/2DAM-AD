using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{	
		private Articulo articulo;

		//CONTRUCTOR PARA NUEVO ARTICULO
		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			articulo = new Articulo ();
			articulo.Nombre = "";
			init ();
			saveAction.Activated += delegate  { insert(); };
		}

		//COUNSTRUCTOR PARA EDITAR ARTICULO
		public ArticuloView(object id) : base(Gtk.WindowType.Toplevel){
			articulo = ArticuloPersister.Load (id);
			init ();
			saveAction.Activated += delegate  { update(); };
		}

		//RELLENO DE COMBOBOX(PSERPISAD-COMBOBOXHELPER)
		private void init(){
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelp.Get ("select * from categoria");
			ComboBoxHelper.Fill (boxCategoria, queryResult, articulo.Categoria);
			spinPrecio.Value = Convert.ToDouble (articulo.Precio);

		}


		//LEE DE LA GUI
		private void updateModel(){
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (boxCategoria); 
			articulo.Precio = Convert.ToDecimal (spinPrecio.Value);
		}

		//METE LOS DATOS EN BD
		private void insert(){
			Console.WriteLine  ("Insertando");
			updateModel();
			ArticuloPersister.Insert (articulo);
			Destroy ();
		}
	
		//ACTUALIZA LOS DATOS DE LA BD 
		private void update(){
			Console.WriteLine ("Actualizando");
			updateModel ();
			ArticuloPersister.Update (articulo);
			Destroy ();
		}
	}
}

