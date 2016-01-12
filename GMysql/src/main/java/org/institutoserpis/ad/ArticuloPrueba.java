package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.*;

import com.mysql.jdbc.PreparedStatement;

import java.sql.Statement;
//import com.mysql.jdbc.Statement;


public class ArticuloPrueba {
	static Connection connection;
	static PreparedStatement insertPreparedStatement;
	static PreparedStatement updatePreparedStatement;
	static PreparedStatement deletePreparedStatement;
	static PreparedStatement selectPreparedStatement;
	
	/* DATOS DE ARTICULO + METODO MOSTRAR
	String nombre;
	int categoria;
	double precio;
	int id;
	
	MOSTRAR ARTICULO
	public static void showArticulo(ArticuloPrueba articulo){
 	System.out.println("Id: " +articulo.id);
	System.out.println("Nombre: " +articulo.nombre);
	System.out.println("Categoria: " +articulo.categoria);
	Sytem.out.println("Precio: " +articulo.precio);
	}*/
	
	//METODO CONEXION CON BD
	public static Connection conection() throws SQLException{
		 connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/bdpruebas", "root", "sistemas");
		Statement stat = connection.createStatement();
		return connection;
	}
	
	//METODO SELECCION
	public static void select() throws Throwable{ //LOS TRHOWS DE EXCEPCIONES SON PARA TIPOS DE DATOS INTRODUCIDOS(SCAN) INCORRECTOS.
		Scanner scan = new Scanner(System.in);
		
		System.out.println("Introduce el id del Articulo que quieres consultar :");
		int id= scan.nextInt();
		String consulta = "select * from articulo where id=" +id;
	}
	
	//METODO PARA INSERTAT A BD
	public static void insert() throws SQLException{
		Scanner scann = new Scanner(System.in);
		String nuevosql = "insert into articulo (nombre, categoria, precio) values (?,?,?)";
		
		String nombre;
		System.out.print("Introduzca nombre del nuevo articulo: ");	
		nombre = scann.nextLine();
		
		int categoria;
		System.out.print("Introduzca categoria del nuevo articulo. [1, 2, 3]: ");
		categoria = scann.nextInt();
	
		double precio;
		System.out.print("Introduzca precio del nuevo articulo: ");
		precio = scann.nextDouble();
			
		insertPreparedStatement = (PreparedStatement) connection.prepareStatement(nuevosql);
		insertPreparedStatement.setString(1, nombre);
		insertPreparedStatement.setInt(2, categoria);
		insertPreparedStatement.setDouble(3, precio);
		insertPreparedStatement.executeUpdate();
		System.out.println("Articulo Guardado Correctamente");
		
	}
	
	//METODO PARA ACTUALIZAR LA BD
	public static void update()throws Throwable{ //LOS TRHOWS DE EXCEPCIONES SON PARA TIPOS DE DATOS INTRODUCIDOS(SCAN) INCORRECTOS.
		System.out.println("Introduce el id del Articulo que quieres modificar :");
	}
	
	//METODO PARA BORRAR DE LA BD
	public static void delete()throws Throwable{ //LOS TRHOWS DE EXCEPCIONES SON PARA TIPOS DE DATOS INTRODUCIDOS(SCAN) INCORRECTOS.
		System.out.println("Introduce el id del Articulo que quieres eliminar :");
	}
	
	//METODO PARA VER TODA UNA TABLA DE LA BD
	public static void selectAll(){
		System.out.println("Todos los articulos disponibles :");
		String vertodos = "select * from articulo";
	}
	
	
	public static void main(String[] args) throws Throwable {
		// SIN METODO CONECT
		/*Connection connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/bdpruebas", "root", "sistemas");
		Statement stat = connection.createStatement();*/
		
		conection(); // CON METODO CONNECT
		
		//// MENU PRINCIPAL - CONTROL SOBRE LA BD ////
		Scanner leer = new Scanner(System.in);
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
			System.out.println("-------------------");
			System.out.print("SELECCIONE UNA OPCION : ");
			try{
				opcion = leer.nextInt();
			}catch (InputMismatchException e){
				System.out.println("DEBES INTRODUCIR NUMEROS ENTEROS");
			}
			leer.nextLine();
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
				//connection.close(); SIN METODO CONECT
				conection().close();//CON METODO CONECT
				//PreparedStatement.close();
				System.out.println("Finalizada la conexión con BDpruebas.");
				break;
			default:
				System.out.println("Opcion Incorrecta. Introduce una accion valida.");
			}
			
		}while(opcion !=6);
	}
}
