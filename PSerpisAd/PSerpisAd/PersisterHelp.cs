using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace SerpisAd
{
	public class PersisterHelp // BASICAMENTE SE USA PARA TRANSFORMAR LOS DATOS DE LA BD EN ILIST
	{
		public static QueryResult Get(string selectText){

			IDbConnection dbConnection = App.Instance.DbConnection;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = selectText;
			IDataReader dataReader = dbCommand.ExecuteReader (); 
			QueryResult queryResult = new QueryResult ();

			queryResult.ColumnNames =  getColumnNames(dataReader);
			List<IList> rows = new List<IList> ();
			//rows.Add (new object[] { 1, "art. uno","1","1,5" }); Para añadir de forma manual
			while (dataReader.Read()) {
				IList row = getRows (dataReader);
				rows.Add (row);
			}
			queryResult.Rows=rows;
			dataReader.Close();
			return queryResult;
		
		}

		//METODO QUE COJE EL NOMBRES DE LAS COLUMNAS DE LA BD Y LAS PASA A UN ARRAY.
		private static string[] getColumnNames(IDataReader dataReader){
			List<string> columnNames = new List<string> ();
			int contador = dataReader.FieldCount;
			for (int i=0;i<contador;i++)
				columnNames.Add (dataReader.GetName (i));
			return columnNames.ToArray();
		}

		//METODO QUE DEVUELVE UN ILIST CON LOS VALORES DE LAS FILAS.
		private static IList getRows(IDataReader dataReader){
			List<object> values = new List<object> ();
			int count = dataReader.FieldCount;
			for (int i=0;i<count;i++)
				values.Add (dataReader [i]);
			return values;
		}



	}
}

