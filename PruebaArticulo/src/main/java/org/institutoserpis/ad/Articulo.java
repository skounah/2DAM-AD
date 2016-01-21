package org.institutoserpis.ad;

import java.math.BigDecimal;

public class Articulo {
	
	private Long id; //EL TIPO LONG DESCIENDE DE LA CLASE LONG QUE SI PERMITE NULOS AL CONTRARIO DE LA long
	private String nombre;
	private Long categoria;
	private BigDecimal precio;
	
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
	public Long getCategoria() {
		return categoria;
	}
	public void setCategoria(Long categoria) {
		this.categoria = categoria;
	}
	public BigDecimal getPrecio() {
		return precio;
	}
	public void setPrecio(BigDecimal precio) {
		this.precio = precio;
	}
	
}
