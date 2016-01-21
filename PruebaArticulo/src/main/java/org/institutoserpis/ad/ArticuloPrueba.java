package org.institutoserpis.ad;

import java.util.List;
import javax.persistence.Persistence;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

public class ArticuloPrueba {
	
	public static void main(String[] args) {
		
		System.out.println("Inicio");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		System.out.println("   ID		NOMBRE 		  CATEGORIA 	 	PRECIO");
		System.out.println("---------------------------------------------------------------");
		for (Articulo articulo : articulos)
			System.out.printf("%5s %20s %15s %20s\n", 
					articulo.getId(), 
					articulo.getNombre(), 
					articulo.getCategoria(), 
					articulo.getPrecio()
			);
		entityManager.getTransaction().commit();
		entityManager.close();
		
		entityManagerFactory.close();
	}

}
