package org.insitutoserpis.ad;

import java.math.BigDecimal;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

import org.hibernate.annotations.GenericGenerator;

@Entity
public class PedidoLinea {
	private Long id;
	private Pedido pedido;
	private Articulo articulo;
	private BigDecimal precio;
	private BigDecimal unidades;
	private BigDecimal importe;
	
	
	@Id
	@GeneratedValue(generator="increment")
	@GenericGenerator(name="increment", strategy = "increment")
	public Long getId() { return id;}
		
	public void setId(Long id) {this.id = id;}

	@ManyToOne
	@JoinColumn(name="pedido")
	public Pedido getPedido() {return pedido;}

	public void setPedido(Pedido pedido) {this.pedido = pedido;}
	
	@ManyToOne
	@JoinColumn(name="articulo")
	public Articulo getArticulo() {return articulo;}

	public void setArticulo(Articulo articulo) {this.articulo = articulo;}

	@Override
	public String toString() {
		return String.format("$s,%s,%s,%s,%s,%s", id,
												  pedido == null ? null : pedido.getId(),
												  articulo == null ? null : articulo.getId(),
												  precio,
												  unidades,
												  importe);
				
	}
}
