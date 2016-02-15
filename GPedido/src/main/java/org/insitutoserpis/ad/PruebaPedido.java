package org.insitutoserpis.ad;

import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.persistence.Persistence;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

public class PruebaPedido {
	private static EntityManagerFactory entityManagerFactory;
	
	public static void main (String[] args){
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		System.out.println("Inicio");
		entityManagerFactory = Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		
		
		entityManagerFactory.close();
		System.out.println("Fin");
	}
	//METODO PARA IMPRIMIR LOS PEDIDOS CON SUS LINEASPEDIDO
	public static void showPedido(Pedido pedido){
		System.out.println(pedido);
		for (PedidoLinea pedidoLinea : pedido.getPedidoLineas())//IMPRIME TODAS LINEAS PEDIDO
			System.out.println(pedidoLinea);
	}
	
	//INSERT 
	private static Long persist() {
		System.out.println("Persist: ");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		//COMINEZA LA INSERCION DEL PEDIDO
		entityManager.getTransaction().begin();
		Cliente cliente = entityManager.find(Cliente.class, 1L);
		Pedido pedido = new Pedido();
		pedido.setCliente(cliente);
		pedido.setFecha(Calendar.getInstance());
		for (Long articuloId = 1L; articuloId <= 3; articuloId++) {
			Articulo articulo = entityManager.find(Articulo.class, articuloId);
			PedidoLinea pedidoLinea = new PedidoLinea();
			pedidoLinea.setPedido(pedido);
			pedidoLinea.setArticulo(articulo);
			
			//OJO
			pedido.getPedidoLineas().add(pedidoLinea);
		}
		entityManager.persist(pedido);
		entityManager.getTransaction().commit();
		entityManager.close();
		showPedido(pedido);
		return pedido.getId();
		
		
	}
	//SELECTALL
	private static void query() {
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Pedido> pedidos = entityManager.createQuery("from Pedido", Pedido.class).getResultList();
		for(Pedido pedido : pedidos){
			System.out.println(pedido);
		}
		entityManager.getTransaction().commit();
		entityManager.close();
	}

}
