using Gtk;
using System;

namespace SerpisAd
{
	public class WindowHelper
	{
		public static bool ConfirmDelete(Window window) {
		//TODO localización del ¿Quieres eliminar...
			MessageDialog messageDialog = new MessageDialog (
				window,
				DialogFlags.DestroyWithParent,
				MessageType.Question,
				ButtonsType.YesNo,
				"¿Quieres eliminar el elemento seleccionado?"
				);
			messageDialog.Title = window.Title; 
			ResponseType response = (ResponseType)messageDialog.Run ();
			messageDialog.Destroy ();
			return response == ResponseType.Yes;
		}
	}
}
