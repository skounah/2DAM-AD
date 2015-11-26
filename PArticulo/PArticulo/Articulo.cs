using System;

namespace PArticulo
{
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

		public string Nombre {
			get{ return nombre; }
			set{ nombre = value; }
		}

		
		public object Id {
			get{ return id; }
			set{ id = value; }
		}

		public object Categoria {
			get{ return categoria; }
			set{ categoria = value; }
		}

		public decimal Precio {
			get{ return precio; }
			set{ id = value; }
		}
	}
}
