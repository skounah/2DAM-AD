using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;
namespace PArticulo
{
	public class ArticuloPersister
	{

		public ArticuloPersister ()
		{
		}
	
		// CARGA LOS DATOS DE LA BD DEL ARTICULO SELECCIONADO
		public static Articulo Load(object id){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader();
			if (!dataReader.Read())
				return null;

			Articulo.Nombre = (string)dataReader ["nombre"];

			Articulo.Categoria = dataReader ["categoria"];
			if (Articulo.Categoria is DBNull)
				Articulo.Categoria = null;

			try {
				Articulo.Precio= (decimal)dataReader["precio"];
			}catch{
				Articulo.Precio=0;	
			}
		
			dataReader.Close ();

			throw new NotImplementedException ();
		}

		//INSERTA EN LA BD
		public static void Insert(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values(@nombre, @categoria, @precio)";
			//string nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", Articulo.Nombre);

			//object categoria = ComboBoxHelper.GetId (boxCategoria); 
			DbCommandHelper.AddParameter (dbCommand, "categoria", Articulo.Categoria);

			//decimal precio = Convsert.ToDecimal (spinPrecio.Value);
			DbCommandHelper.AddParameter (dbCommand, "precio", Articulo.Precio);

			dbCommand.ExecuteNonQuery ();
			//Destroy ();
		}

		//ACTUALIZA EN LA BD
		public static void Update(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre,categoria=@categoria, precio=@precio where id=@id";

			DbCommandHelper.AddParameter (dbCommand, "id", id);

			//string nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", Articulo.Nombre);

			//object categoria = ComboBoxHelper.GetId (boxCategoria); 
			DbCommandHelper.AddParameter (dbCommand, "categoria", Articulo.Categoria);

			//decimal precio = Convert.ToDecimal (spinPrecio.Value);
			DbCommandHelper.AddParameter (dbCommand, "precio", Articulo.Precio);


			dbCommand.ExecuteNonQuery ();
			//Destroy ();

		}
	}
}

