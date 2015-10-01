using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace PrimeroMysql
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(  						//CREAR CONEXION
				"Database=bdpruebas;Data Source=localhost;User Id=root; Password=sistemas" 
			);

			mySqlConnection.Open ();														//ABRIR CONEXION

			//updateDatabase (mySqlConnection);

			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();  
			mySqlCommand.CommandText="select * from articulo";								// AQUI VA LA SENTENCIA SQL QUE SE REQUIERA

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader (); 				//CREAR READER

			showColumNames (mySqlDataReader);
			show (mySqlDataReader);

			mySqlDataReader.Close ();														//CERRAR READER
			mySqlConnection.Close ();														//CERRAR CONEXION
																						
		}

		private static void updateDatabase (MySqlConnection mySqlConnection){				// METODO DE ACTUALIZACION
			MySqlCommand mySqlCommand= mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = "update articulo set categoria=2 where id=3";		// SENTENCIA DE UPDATE
			//mySwlCommand.CommandText = "insert, delete...
			mySqlCommand.ExecuteNonQuery ();
		}

		public static void showColumNames (MySqlDataReader mySqlDataReader) {				// METODO QUE MUESTRA NUMERO Y NOMBRE DE LAS COLUMNAS
			Console.WriteLine ("COLUMNAS :");

			//string[] columNames = getColumNames (mySqlDataReader); 
			//Console.WriteLine (string.Join(",",columNames));								// ES LO MISMO QUE EL FOR DE ABAJO

			for (int i=0; i < mySqlDataReader.FieldCount; i++)
				Console.WriteLine ("num columa= " + i + ", nombre= " + mySqlDataReader.GetName (i));
		}

		public static string[] getColumNames (MySqlDataReader mySqlDataReader){				// METODO QUE DEVUELVE UNA LISTA CON LA LINEA DE LA TABLA. (PUEDE HACERSE DEVOLVIENDO UN STRING)
			int count = mySqlDataReader.FieldCount;
			//string[] columNames = new string[count];
			//for(int i=0;i<count;i++){
			//	ColumNames[i]=mySqlDataReader.GetName (i)}
			//return columNames

			List<string> columNames = new List<string> ();
			for(int i =0;i<count;i++)
				columNames.Add (mySqlDataReader.GetName(i));
			return columNames.ToArray();													//PARA PASARLO A ARRAY USAR EL METODO .TOARRAY()
		}

		public static void show (MySqlDataReader mySqlDataReader){ 							// METODO QUE MUESTRA TODO EL CONETNIDO DE LA TABLA
			Console.WriteLine ("FILAS : ");													// EL SHOW MUESTRA EL SHOWROW (FILA). EL SHOWROW MUESTRA LA FILA 
			while (mySqlDataReader.Read())
				showRow (mySqlDataReader);
		}

		public static void showRow(MySqlDataReader mySqlDataReader){						// MUSTRA LA FILA ENTERA EN UNA SOLA LINEA
			int count = mySqlDataReader.FieldCount;
			string line = "";
			for (int i=0; i< count; i++) {
				line = line + mySqlDataReader [i] + " ";
			}
			Console.WriteLine (line);

			
		}
	}
}