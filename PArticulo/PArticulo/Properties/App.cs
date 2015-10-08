using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PArticulo{

	public class App{

		private App (){
		}

		private static App instance = new App ();
		public static App Instance {
			get {return instance;}
		}

		//CLASE PRIVADA CON METODO PUBLICO QUE LA DEVUELVE
		private IDbConnection dbConnection;
		public IDbConnection DbConnection {
			get {
				if(dbConnection == null){
					dbConnection = new MySqlConnection(
					"Database=bdpruebas;Data Source= localhost;user ID=root;Password=sistemas"
					);
					dbConnection.Open ();
				}
				return dbConnection;}
		}


	}
}

