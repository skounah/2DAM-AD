using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{

	public partial class ArticuloView : Gtk.Window
	{
		private object id = null;
		private string nombre = "";
		private object categoria = null;
		private decimal precio =0;

		private Articulo articulo;

		//CONTRUCTOR PARA NUEVO ARTICULO
		public ArticuloView () : base(Gtk.WindowType.Toplevel){
			init ();
			saveAction.Activated += delegate  { insert(); };
		}

		//COUNSTRUCTOR PARA EDITAR ARTICULO
		public ArticuloView(object id) : base(Gtk.WindowType.Toplevel){
			this.id = id;
			//load ();
			articulo = ArticuloPersister.Load (id);
			init ();
			//saveAction.Activated += delegate  { update(); };
		}

		//RELLENO DE COMBOBOX(PSERPISAD-COMBOBOXHELPER)
		private void init(){
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelp.Get ("select * from categoria");
			ComboBoxHelper.Fill (boxCategoria, queryResult, categoria);
			spinPrecio.Value = Convert.ToDouble (precio);
			//saveAction.Activated += delegate  {save(); };
		}


		//CARGA DE LA BD LOS DATOS DEL ARTICULO SELECCIONADO
		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader();
			if (!dataReader.Read())
				//TODO Excepcion
				return;

			nombre = (string)dataReader ["nombre"];
			//entryNombre.Text = nombre;

			categoria = dataReader ["categoria"];
			//TODO poscionamiento en el combobox
			if (categoria is DBNull)
				categoria = null;

			try {
				precio= (decimal)dataReader["precio"];
			}catch{
				precio=0;	
			}
			//precio = (decimal)dataReader ["precio"];
			//spinPrecio.Value = Convert.ToDouble (precio);

			dataReader.Close ();
		}

		//METE LOS DATOS EN BD
		private void insert(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values(@nombre, @categoria, @precio)";
			string nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);

			object categoria = ComboBoxHelper.GetId (boxCategoria); 
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);

			decimal precio = Convert.ToDecimal (spinPrecio.Value);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

	
		//ACTUALIZA LOS DATOS DE LA BD 
		private void update(){
			Console.WriteLine ("update");
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre,categoria=@categoria, precio=@precio where id=@id";

			DbCommandHelper.AddParameter (dbCommand, "id", id);

			string nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);

			object categoria = ComboBoxHelper.GetId (boxCategoria); 
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);

			decimal precio = Convert.ToDecimal (spinPrecio.Value);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);


			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}
	}
}

