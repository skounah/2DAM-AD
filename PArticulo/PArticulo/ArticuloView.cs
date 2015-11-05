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
			addParameter (dbCommand, "nombre", nombre);
		
			object categoria = GetId (boxCategoria); 
			addParameter (dbCommand, "categoria", categoria);

			decimal precio = Convert.ToDecimal(spinPrecio.Value);
			addParameter (dbCommand, "precio", precio);

		
			dbCommand.ExecuteNonQuery();
		}

		private static void addParameter(IDbCommand dbCommand, string name, object value){
			IDbDataParameter dbDataParameter = dbCommand.CreateParameter ();
			dbDataParameter.ParameterName = name;
			dbDataParameter.Value = value;
			dbCommand.Parameters.Add (dbDataParameter);
		}

		public static object GetId(ComboBox comboBox) {
			TreeIter treeIter;
			comboBox.GetActiveIter (out treeIter);
			IList row = (IList)comboBox.Model.GetValue (treeIter, 0); //ILIST 0 por que es el unico elemento aunque dentro vallan las columnas
			return row [0];
		}
	}
}

