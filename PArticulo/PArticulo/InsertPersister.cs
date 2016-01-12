using System;

namespace PArticulo
{
	public class InsertPersister
	{
		public static int Insert(object cualkiera){
			Console.WriteLine("Persister.Insert");
			Type type = cualkiera.GetType ();
			string[] fieldNames = getFieldNames (type);
			string[] paramNames = getParamNames (fieldNames);
			string sentenciainsert = "insert into {0} ({1}) values ({2})";
			Console.WriteLine (sentenciainsert, tableName, string.Join (",", fieldNames), string.Join (",", paramNames));
		}

		
	}
}

