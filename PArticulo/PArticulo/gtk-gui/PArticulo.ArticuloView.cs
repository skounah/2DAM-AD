
// This file has been generated by the GUI designer. Do not modify.
namespace PArticulo
{
	public partial class ArticuloView
	{
		private global::Gtk.UIManager UIManager;
		private global::Gtk.Action Guardar;
		private global::Gtk.VBox vbox1;
		private global::Gtk.Toolbar barBotones;
		private global::Gtk.Table table1;
		private global::Gtk.ComboBox boxCategoria;
		private global::Gtk.Label Categoria;
		private global::Gtk.Entry entryNombre;
		private global::Gtk.Label Nombre;
		private global::Gtk.Label Precio;
		private global::Gtk.SpinButton spinPrecio;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget PArticulo.ArticuloView
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
			this.Guardar = new global::Gtk.Action ("Guardar", null, null, "gtk-save");
			w1.Add (this.Guardar, null);
			this.UIManager.InsertActionGroup (w1, 0);
			this.AddAccelGroup (this.UIManager.AccelGroup);
			this.Name = "PArticulo.ArticuloView";
			this.Title = global::Mono.Unix.Catalog.GetString ("ArticuloView");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child PArticulo.ArticuloView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><toolbar name='barBotones'><toolitem name='Guardar' action='Guardar'/></toolbar></ui>");
			this.barBotones = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/barBotones")));
			this.barBotones.Name = "barBotones";
			this.barBotones.ShowArrow = false;
			this.vbox1.Add (this.barBotones);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.barBotones]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.boxCategoria = new global::Gtk.ComboBox ();
			this.boxCategoria.Name = "boxCategoria";
			this.table1.Add (this.boxCategoria);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.boxCategoria]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.Categoria = new global::Gtk.Label ();
			this.Categoria.Name = "Categoria";
			this.Categoria.Xalign = 0F;
			this.Categoria.LabelProp = global::Mono.Unix.Catalog.GetString ("Categoría");
			this.table1.Add (this.Categoria);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.Categoria]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryNombre = new global::Gtk.Entry ();
			this.entryNombre.CanFocus = true;
			this.entryNombre.Name = "entryNombre";
			this.entryNombre.IsEditable = true;
			this.entryNombre.InvisibleChar = '•';
			this.table1.Add (this.entryNombre);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryNombre]));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.Nombre = new global::Gtk.Label ();
			this.Nombre.Name = "Nombre";
			this.Nombre.Xalign = 0F;
			this.Nombre.LabelProp = global::Mono.Unix.Catalog.GetString ("Nombre");
			this.table1.Add (this.Nombre);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.Nombre]));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.Precio = new global::Gtk.Label ();
			this.Precio.Name = "Precio";
			this.Precio.Xalign = 0F;
			this.Precio.LabelProp = global::Mono.Unix.Catalog.GetString ("Precio");
			this.table1.Add (this.Precio);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.Precio]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinPrecio = new global::Gtk.SpinButton (0, 1000, 1);
			this.spinPrecio.CanFocus = true;
			this.spinPrecio.Name = "spinPrecio";
			this.spinPrecio.Adjustment.PageIncrement = 10;
			this.spinPrecio.ClimbRate = 1;
			this.spinPrecio.Digits = ((uint)(2));
			this.spinPrecio.Numeric = true;
			this.table1.Add (this.spinPrecio);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinPrecio]));
			w8.TopAttach = ((uint)(2));
			w8.BottomAttach = ((uint)(3));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add (this.table1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.table1]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 168;
			this.Show ();
		}
	}
}
