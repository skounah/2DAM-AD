using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SerpisAd{

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
			get {return dbConnection;}
			set { dbConnection = value; }
		}
	}
}

