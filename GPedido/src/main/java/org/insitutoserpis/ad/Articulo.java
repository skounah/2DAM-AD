package org.insitutoserpis.ad;

import java.math.BigDecimal;

import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

import org.hibernate.annotations.GenericGenerator;

@Entity
public class Articulo {
	
	private Long id; //EL TIPO LONG DESCIENDE DE LA CLASE LONG QUE SI PERMITE NULOS AL CONTRARIO DE LA long
	private String nombre;
	private Categoria categoria;
	private BigDecimal precio;
	
	@Id
	@GeneratedValue(generator="increment")
	@GenericGenerator(name="increment", strategy = "increment")
	public Long getId() {
		return id;
	}
	public void setId(Long id) {
		this.id = id;
	}
	public String getNombre() {
		return nombre;
	}
	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
	@ManyToOne(fetch=FetchType.LAZY) //LAZY SOLO LEE SI LO NECESITA. EL OTRO TPO ES EAGUER Y LEE SIEMPRE AUNQUE NO LO NECESITE
	@JoinColumn(name="categoria")
	public Categoria getCategoria() {
		return categoria;
	}
	public void setCategoria(Categoria categoria) {
		this.categoria = categoria;
	}
	public BigDecimal getPrecio() {
		return precio;
	}
	public void setPrecio(BigDecimal precio) {
		this.precio = precio;
	}
	
	public String toString(){
		return String.format("%s %s %s %s", id,nombre,categoria,precio);
	}
	
}
