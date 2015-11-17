using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			//entryNombre.Text = "Introduce nombre";
			spinPrecio.Value = 1.0;


			QueryResult queryResult = PersisterHelp.Get ("select * from categoria");
			ComboBoxHelper.Fill (boxCategoria, queryResult);

			Guardar.Activated += delegate {
				save();
			};
		}


		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values(@nombre, @categoria, @precio)";
			  //"values('nuevo articulo 69', 69, 69.5)"; PRUEBA DE FUNCIONAMIENTO EFECTUADA CON EXITO 


			string nombre = entryNombre.Text;
			DbCommandHelper.addParameter (dbCommand, "nombre", nombre);
		
			object categoria = ComboBoxHelper.GetId (boxCategoria); 
			DbCommandHelper.addParameter (dbCommand, "categoria", categoria);

			decimal precio = Convert.ToDecimal(spinPrecio.Value);
			DbCommandHelper.addParameter (dbCommand, "precio", precio);

		
			dbCommand.ExecuteNonQuery();
			Destroy ();
		}

	}
}

