package org.institutoserpis.ad;


import java.math.BigDecimal;
import java.util.Date;
import java.util.InputMismatchException;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.Scanner;
import java.util.List;
import javax.persistence.Persistence;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

public class ArticuloPrueba {
	private static EntityManagerFactory entityManagerFactory;
	
	//MOSTRAR ARTICULO EN FILA SEPARADO POR LOS %
	private static void show(Articulo articulo) {
		System.out.printf("%5s %20s %15s %20s\n", 
				articulo.getId(), 
				articulo.getNombre(), 
				format(articulo.getCategoria()),
				//articulo.getCategoria(), 
				articulo.getPrecio()
		);
	}
	
	private static String format(Categoria categoria) {
		if(categoria == null)
			return null;
		//DEVUELVE UN STRING CON FORMATO
		return String.format("%5s - %-10s", categoria.getId(),categoria.getNombre());
	}
	//SELECT ALL
	private static void query() {
		System.out.println("query:");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos)
			show(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	//INSERT
	private static Long persist() {
		System.out.println("persist:");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = new Articulo();
		articulo.setNombre("nuevo " + new Date());
		entityManager.persist(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
		return articulo.getId();
	}

	//SELECT POR ID 
	private static void find(Long id) {
		System.out.println("find: " + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Articulo articulo = entityManager.find(Articulo.class, id);
		
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	
	//DELETE POR ID 
	private static void remove(Long id) {
		System.out.println("remove: " + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Articulo articulo = entityManager.find(Articulo.class, id);
		entityManager.remove(articulo);
		
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	//UPDATE POR ID 
	private static void update(Long id) {
		System.out.println("update: " + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Articulo articulo = entityManager.find(Articulo.class, id);
		articulo.setNombre("modificado " +  new Date());
		
		entityManager.getTransaction().commit();//CON EL COMMIT HACE EL UPDATE 
		entityManager.close();
		show(articulo);
	}
	
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
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
				//find(id);
				break;
			case 2:
				persist();
				break;
			case 3:
				//update(id);
				break;
			case 4:
				//remove(id);
				break;
			case 5:
				query();
				break;
			case 6:
				System.out.println("Cerrando conexión con la BD.");
				//cerrarStatements();//CIERRA STATEMENTS 
				//conection().close();//CIERRA CONEXION
				System.out.println("Finalizada la conexión con BDpruebas.");
				break;
			default:
				System.out.println("Opcion Incorrecta. Introduce una accion valida.");
			}
			
		}while(opcion !=6);
		
	}

}
