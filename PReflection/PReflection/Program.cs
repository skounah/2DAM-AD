using System;
using System.Reflection;

namespace PReflection
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*int i=11;
			Type tipo = i.GetType ();
			//Console.WriteLine ("tipo.Name={0}, {1}", tipo.Name,tipo.FullName); ES PARA HACER REFERENCIA A LO DE DESOPUES DE LA COMA [0,1,2,3...]
			showType (tipo);

			string cadena="Hola skoner";
			Type tipo2 = cadena.GetType ();
			//Console.WriteLine ("tipo2.Name={0}", tipo2.Name);
			showType (tipo2);

			Type tipostring = typeof(string);
			//Console.WriteLine ("tipostring.Name={0}", tipostring.Name);
			showType (tipostring);

			Type tipobase = typeof(object);
			showType (tipobase);
		
			Type tipoFoo = typeof(Foo);
			showType (tipoFoo);
			Foo foo = new Foo ();
			//showObject (foo);*/

			Articulo articulo = new Articulo ();
			/*articulo.Nombre="articulo 1";
			articulo.Categoria=2;
			articulo.Precio = decimal.Parse("7.5");*/
			setValues (articulo, new object[] { 33L, "nuevo articulo 1", 3L, decimal.Parse("11,11") }); // DEBEN IR EN EL ORDEN PUESTO EN LA CLASE DEL OBJETO
			showType (articulo.GetType ()); // =  Type tipoFoo = typeof(Foo);  showType (tipoFoo);
			showObject (articulo);

		}//DEL MAIN

		public static void showType(Type type){
			Console.WriteLine ("TIPO DE OBJETO :");
			Console.WriteLine ("tipo= {0}, Nombre completo= {1}, Clase antecesora= {2}",
			                   type.Name, type.FullName,type.BaseType.Name);
			Console.WriteLine ("INFO DE PROPIEDADES :");
			PropertyInfo[] infopropiedades = type.GetProperties ();
			foreach (PropertyInfo infopropiedad in infopropiedades)
				Console.WriteLine("Nombre de propiedad={0}, Informacion de propiedad={1}",infopropiedad.Name, infopropiedad.PropertyType); 
		}//DE SHOWTYPE

		private static void showObject(object objeto){
			Type type = objeto.GetType ();
			PropertyInfo[] infopropiedades = type.GetProperties ();
			Console.WriteLine ("VALORES DE LAS PROPIEDADES : ");
			foreach (PropertyInfo infopropiedad in infopropiedades)
				Console.WriteLine("{0}={1}",infopropiedad.Name, infopropiedad.GetValue(objeto, null));
		}//DE SHOW OBJECT

		private static void setValues(object objeto, object[] values){
			Type type = objeto.GetType ();
			int index = 0;
			PropertyInfo[] infopropiedades = type.GetProperties ();
			foreach (PropertyInfo infopropiedad in infopropiedades)
				infopropiedad.SetValue(objeto, values [index++],null);
		}//DE SETVALUES

	}//DE LA CLASE

	public class Foo{
		private string name;
		private object id;
		private double precio;

		public double Precio {
			get { return precio; }
			set { precio = value; }
		}
	
		public string Name {
			get { return name; }
			set { name = value; }
		}
		public object Id {
			get { return id; }
			set { id = value; }
		}
	}

	public class Articulo
	{
		public Articulo ()
		{
		}

		//FORMA 1
		private object id;
		private string nombre;
		private object categoria;
		private decimal precio;


		public object Id {
			get{ return id; }
			set{ id = value; }
		}

		public string Nombre {
			get{ return nombre; }
			set{ nombre = value; }
		}


		public object Categoria {
			get{ return categoria; }
			set{ categoria = value; }
		}

		public decimal Precio {
			get{ return precio; }
			set{ precio = value; }
		}
	}
}
