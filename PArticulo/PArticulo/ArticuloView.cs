using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id;
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			//entryNombre.Text = "Introduce nombre";
			//spinPrecio.Value = 1.0;

			//RELLENO DE COMBOBOX(PSERPISAD-COMBOBOXHELPER)
			QueryResult queryResult = PersisterHelp.Get ("select * from categoria");
			ComboBoxHelper.Fill (boxCategoria, queryResult);

			Guardar.Activated += delegate { save(); };
		}

		public ArticuloView(object id) : this(){
			this.id = id;
			load ();
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader();
			if (!dataReader.Read()) 
				//TODO throw exception
				return;

			string nombre = (string)dataReader ["nombre"];
			object categoria = dataReader ["categoria"];
			decimal precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
			entryNombre.Text = nombre;
			//TODO poscionamiento en el combobox
			spinPrecio.Value = Convert.ToDouble (precio);
		}

		//METODO QUE GUARDA LOS DATOS DEL ARTICULOVIEW EN BD
		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values(@nombre, @categoria, @precio)";
			  //"values('nuevo articulo 69', 69, 69.5)"; PRUEBA DE FUNCIONAMIENTO EFECTUADA CON EXITO 


			string nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
		
			object categoria = ComboBoxHelper.GetId (boxCategoria); 
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);

			decimal precio = Convert.ToDecimal(spinPrecio.Value);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

		
			dbCommand.ExecuteNonQuery();
			Destroy ();
		}

	}
}

