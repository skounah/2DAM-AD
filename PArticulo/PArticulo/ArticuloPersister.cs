using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{
	public class ArticuloPersister
	{
		// CARGA LOS DATOS DE LA BD DEL ARTICULO SELECCIONADO
		public static Articulo Load(object id){
			Articulo articulo = new Articulo ();
			articulo.Id = id;

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader();
			if (!dataReader.Read())
				//TODO 
				return null;

			articulo.Nombre = (string)dataReader ["nombre"];
			articulo.Categoria = get (dataReader ["categoria"], null);
			articulo.Precio= (decimal) get (dataReader["precio"],decimal.Zero);

			dataReader.Close ();
			return articulo;
		}
		//ESTABLECE LOS VALORES POR DEFECTO
		private static object get(object value,object defaultValue){
			return value is DBNull ? defaultValue : value;
		}

		//INSERTA EN LA BD
		public static int Insert(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) values(@nombre, @categoria, @precio)";

			DbCommandHelper.AddParameter (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", articulo.Precio);

			return dbCommand.ExecuteNonQuery ();
		}

		//ACTUALIZA EN LA BD
		public static int Update(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre,categoria=@categoria, precio=@precio where id=@id";

			DbCommandHelper.AddParameter (dbCommand, "id", articulo.Id);
			DbCommandHelper.AddParameter (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", articulo.Precio);

			return dbCommand.ExecuteNonQuery ();
			//Destroy ();

		}
		//METODO QUE GUARDA DE DOS FORMAS SEGUN EL ID HACE UPDATE O INSERT id=NULL
		public static int Save (Articulo articulo) {
			return articulo.Id == null ? Insert (articulo) : Update (articulo);
		}
	}
}

