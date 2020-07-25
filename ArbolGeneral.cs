using System;
using System.Collections.Generic;

namespace TPF
{
	public class ArbolGeneral<T>
	{
		
		private NodoGeneral<T> raiz;

		public ArbolGeneral(T dato) {
			this.raiz = new NodoGeneral<T>(dato);
		}
	
		private ArbolGeneral(NodoGeneral<T> nodo) {
			this.raiz = nodo;
		}
	
		private NodoGeneral<T> getRaiz() {
			return raiz;
		}
	
		public T getDatoRaiz() {
			return this.getRaiz().getDato();
		}
	
		public List<ArbolGeneral<T>> getHijos() {
			List<ArbolGeneral<T>> temp= new List<ArbolGeneral<T>>();
			foreach (var element in this.raiz.getHijos()) {
				temp.Add(new ArbolGeneral<T>(element));
			}
			return temp;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Add(hijo.getRaiz());
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Remove(hijo.getRaiz());
		}
	
		public bool esVacio() {
			return this.raiz == null;
		}
	
		public bool esHoja() {
			return this.raiz != null && this.getHijos().Count == 0;
		}
		
		public void visitarRaiz(){
			raiz.visitar();
		}
		
		public void desvisitarRaiz(){
			raiz.desvisitar();
		}
		
		public bool estadoVisitado(){
			return raiz.estadoVisitado();
		}
		
		public void seGana(){
			raiz.seGana();
		}
		
		public void sePierde(){
			raiz.sePierde();
		}
		
		public string estadoGana(){
			return raiz.estadoGana();
		}
		
		// Ejercicio 4- A)   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		
		public int altura() {
			int altura = 0;
			if(this.esHoja()){
				return 0;
			}
			else{
				foreach(ArbolGeneral<T> arbol in this.getHijos()){
					int contador = arbol.altura();
					if(contador > altura)
						altura = contador;
				}
				altura++;
			}
			return altura;
		}
		
		//Ejercicio 4- B)      >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		
		public int nivel(T dato) {
			int nivel = 0;
			if(this.getDatoRaiz().ToString() == dato.ToString())
				return 0;
			else{
				foreach(ArbolGeneral<T> arbol in this.getHijos()){
					int encontrado = arbol.nivel(dato);
					if(encontrado < 0)
						continue;
					if(encontrado >= 0)
						nivel = 1 + encontrado;
				}
				if(nivel == 0)
					nivel--;
			}
			return nivel;
		}
		
		// Ejercicio 4- C)       >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		
		public int ancho(){
			int ancho = 0;
			if(this.esHoja())
				return 0;
			else{
				int contador = 0;
				foreach(ArbolGeneral<T> arbol in this.getHijos()){
					arbol.ancho();
					contador++;
					if(contador >= ancho)
						ancho = contador;
				}
			}
			return ancho;
		}
		
		// recorridos        >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		
		public void preOrden(){
			Console.WriteLine(this.getDatoRaiz());
			foreach(ArbolGeneral<T> arbol in this.getHijos()){
				arbol.preOrden();
			}
		}
		
		public void porNiveles(){
			Cola<ArbolGeneral<T>> cola = new Cola<ArbolGeneral<T>>();
			cola.encolar(this);
			while(!cola.esVacia()){
				ArbolGeneral<T> dato = cola.desencolar();
				Console.WriteLine(dato.getDatoRaiz() + dato.estadoGana());
				foreach(ArbolGeneral<T> x in dato.getHijos()){
					cola.encolar(x);
				}
			}
		}
		
		public void resultados(int limite, string cadena, bool raizz){
			// es una modificacion del recorrido preOrden para que pueda imprimir todos los resultados
			// de cada camino del arbol
			
			if(raizz == false){
				cadena =  cadena + this.getDatoRaiz().ToString() + ", ";
				limite = limite - int.Parse(this.getDatoRaiz().ToString());
			}
			else
				raizz = false;
			if(limite < 0){
				Console.WriteLine(cadena + "= " + this.estadoGana());
			}
			else{
				foreach(ArbolGeneral<T> arbol in this.getHijos()){
					arbol.resultados(limite, cadena, raizz);
				}
			}
		}
	
	}
}
