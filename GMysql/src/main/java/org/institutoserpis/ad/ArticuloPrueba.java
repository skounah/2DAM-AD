package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;
import com.mysql.jdbc.PreparedStatement;
import java.sql.Statement;


public class ArticuloPrueba {
	//CONEXION
	static Connection connection;
	//SENTENCIAS SQL
	static PreparedStatement insertPreparedStatement; //INSERT
	static PreparedStatement updatePreparedStatement; //UPDATE
	static PreparedStatement deletePreparedStatement; //DELETE
	static PreparedStatement selectPreparedStatement; //SELECT
	static PreparedStatement selectAllPreparedStatement;//SELECTALL
	
	//SCANNER
	private static Scanner scan = new Scanner(System.in);
	
	// DATOS DE ARTICULO + METODO MOSTRAR ARTICULO
	String nombre;
	int categoria;
	double precio;
	int id;
	
	//MOSTRAR ARTICULO
	public static void showArticulo(ArticuloPrueba articulo){
 	System.out.println("Id: " +articulo.id);
	System.out.println("Nombre: " +articulo.nombre);
	System.out.println("Categoria: " +articulo.categoria);
	System.out.println("Precio: " +articulo.precio);
	}
	
	//METODO PARA CONECTARSE A LA BD
	public static Connection conection() throws SQLException{
		 connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/bdpruebas", "root", "sistemas");
		Statement stat = connection.createStatement();
		return connection;
	}
	
	//METODO PARA PODER CERRAR LAS SENTENCIAS SQL
	public static void cerrarStatements() throws SQLException{
		if(insertPreparedStatement!=null)
			insertPreparedStatement.close();
		
		if(updatePreparedStatement!=null)
			insertPreparedStatement.close();
		
		if(deletePreparedStatement!=null)
			insertPreparedStatement.close();
		
		if(selectPreparedStatement!=null)
			insertPreparedStatement.close();
		
	}
	
	//METODO SELECCION //REVISAR!!!
	public static void select() throws SQLException{
		
		
		System.out.println("--------------------------------------------------");
		System.out.println("Introduce el id del Articulo que quieres consultar :");
		int id= scan.nextInt();
		String selectsql = "select * from articulo where id = ?" ;
		if(selectPreparedStatement == null)
			selectPreparedStatement = (PreparedStatement) connection.prepareStatement(selectsql);
		selectPreparedStatement.setInt(1, id);
		ResultSet result= selectPreparedStatement.executeQuery(selectsql);
		//ACABAR
		System.out.println("-----------------------");
		System.out.println("    ID    : " +result.getInt("id"));
		System.out.println("  Nombre  : " +result.getString("nombre"));
		System.out.println("Categoria : " +result.getInt("categoria"));
		System.out.println("  Precio  : " +result.getDouble("precio"));
		System.out.println("-----------------------");
	
		
	}
	
	//METODO PARA INSERTAR A BD
	public static void insert() throws SQLException{
		try{
			
			String nuevosql = "insert into articulo (nombre, categoria, precio) values (?,?,?)";
			
			System.out.println("------------------------------------------");
			String nombre;
			System.out.print("Introduzca nombre del nuevo articulo: ");	
			nombre = scan.nextLine();
		
			int categoria;
			System.out.print("Introduzca categoria del nuevo articulo. [1, 2, 3]: ");
			categoria = scan.nextInt();
	
			double precio;
			System.out.print("Introduzca precio del nuevo articulo: ");
			precio = scan.nextDouble();
			
			//CAMBIAMOS LOS ??? DE NUEVOSQL POR LOS VALORES INTRODUCIDOS POR TECLADO //MODELO PARA SENTENCIAS SQL
			if(insertPreparedStatement==null)
				insertPreparedStatement = (PreparedStatement) connection.prepareStatement(nuevosql);
			insertPreparedStatement.setString(1, nombre);
			insertPreparedStatement.setInt(2, categoria);
			insertPreparedStatement.setDouble(3, precio);
			insertPreparedStatement.executeUpdate();
			System.out.println("Articulo Guardado Correctamente");
		}catch (InputMismatchException e){ // EXCEPCION QUE SALTA AL INTRODUCIR UN TIPO INCORRECTO EN UN SCANNER
			System.out.println("Introduce un tipo de dato valido");
		}	
	}
	
	//METODO PARA ACTUALIZAR LA BD
	public static void update()throws Throwable{ //LOS TRHOWS DE EXCEPCIONES SON PARA TIPOS DE DATOS INTRODUCIDOS(SCAN) INCORRECTOS.
		String updatesql ="update articulo set nombre = ?, categoria = ?, precio = ? where id = ?";
		
		
		//System.out.println("--------------------------------------------------------------");
		try{
			int id;
			System.out.println("Introduce el id del Articulo que quieres modificar :");
			id= scan.nextInt();
			scan.nextLine();
			
			String nombre;
			System.out.println("Introduzca nuevo nombre del articulo: ");	
			nombre = scan.nextLine();
	
			int categoria;
			System.out.println("Introduzca nueva categoria del articulo. [1, 2, 3]: ");
			categoria = scan.nextInt();
			
			double precio;
			System.out.println("Introduzca nuevo precio del nuevo articulo: ");
			precio = scan.nextDouble();
		
			if(updatePreparedStatement==null)
				updatePreparedStatement = (PreparedStatement) connection.prepareStatement(updatesql);
			updatePreparedStatement.setString(1, nombre);
			updatePreparedStatement.setInt(2, categoria);
			updatePreparedStatement.setDouble(3, precio);
			updatePreparedStatement.setInt(4, id);
			int count = updatePreparedStatement.executeUpdate();
			
			if (count == 1)
				System.out.println("Articulo Actualizado Correctamente");
			else 
				System.out.println("No hay ningun Articulo con ese ID");
		}catch (InputMismatchException e){ // EXCEPCION QUE SALTA AL INTRODUCIR UN TIPO INCORRECTO EN UN SCANNER
			System.out.println("Introduce un tipo de dato valido");
		}	
	}
	
	//METODO PARA BORRAR DE LA BD
	public static void delete()throws Throwable{ //LOS TRHOWS DE EXCEPCIONES SON PARA TIPOS DE DATOS INTRODUCIDOS(SCAN) INCORRECTOS.
		String deletesql= "delete from articulo where id = ?";
		Scanner scann = new Scanner(System.in);
		
		try{
			int id;
			System.out.println("Introduce el id del Articulo que quieres Borrar :");
			id= scann.nextInt();
			
			deletePreparedStatement.setInt(1, id);
			int count = deletePreparedStatement.executeUpdate();
			
			if (count == 1)
				System.out.println("Articulo Borrado Correctamente");
			else 
				System.out.println("No hay ningun Articulo con ese ID");
		}catch (InputMismatchException e){ // EXCEPCION QUE SALTA AL INTRODUCIR UN TIPO INCORRECTO EN UN SCANNER
			System.out.println("Introduce un tipo de dato valido");
		}	
			
	}
	
	//METODO PARA VER TODA UNA TABLA DE LA BD
	public static void selectAll() throws SQLException{
		System.out.println("Todos los articulos disponibles :");
		String selectAllsql = "select * from articulo";
		selectAllPreparedStatement = (PreparedStatement) connection.prepareStatement(selectAllsql);
		ResultSet result= selectAllPreparedStatement.executeQuery(selectAllsql);
		//ACABAR
		System.out.println("   ID		NOMBRE 		  CATEGORIA 	 	PRECIO");
		System.out.println("---------------------------------------------------------------");
		/*for (ArticuloPrueba articulo : articulos)
			System.out.printf("%5s %20s %15s %20s\n", 
					articulo.getId(), 
					articulo.getNombre(), 
					articulo.getCategoria(), 
					articulo.getPrecio()
			);*/
	}
	
	
	public static void main(String[] args) throws Throwable {
		conection(); 
		//// MENU PRINCIPAL - CONTROL SOBRE LA BD ////
		int opcion=-1;
		do{
			System.out.println("");
			System.out.println("----MENU DE CONTROL BD----");
			System.out.println("1-Ver Articulo.");
			System.out.println("2-Nuevo.");
			System.out.println("3-Editar.");
			System.out.println("4-Eliminar.");
			System.out.println("5-Ver todos.");
			System.out.println("6-Salir.");
			System.out.println("-----------------------");
			System.out.print("SELECCIONE UNA OPCION : ");

			try{
				opcion = scan.nextInt();
	 		}catch (InputMismatchException e){
				System.out.println("DEBES INTRODUCIR NUMEROS ENTEROS");
			}
			scan.nextLine();
			switch (opcion) {
			case 1:
				select();
				break;
			case 2:
				insert();
				break;
			case 3:
				update();
				break;
			case 4:
				delete();
				break;
			case 5:
				selectAll();
				break;
			case 6:
				System.out.println("Cerrando conexión con la BD.");
				cerrarStatements();//CIERRA STATEMENTS 
				conection().close();//CIERRA CONEXION
				System.out.println("Finalizada la conexión con BDpruebas.");
				break;
			default:
				System.out.println("Opcion Incorrecta. Introduce una accion valida.");
			}
			
		}while(opcion !=6);
	}
}
