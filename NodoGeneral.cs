﻿using System;
using System.Collections.Generic;

namespace TPF
{
	/// <summary>
	/// Description of NodoGeneral.
	/// </summary>
	public class NodoGeneral<T>
	{
		private T dato;
		private List<NodoGeneral<T>> hijos;
		private bool visitado = false;
		private string gana = "";
			
		public NodoGeneral(T dato){		
			this.dato = dato;
			this.hijos = new List<NodoGeneral<T>>();
		}
	
		public T getDato(){		
			return this.dato; 
		}
		
		public List<NodoGeneral<T>> getHijos(){		
			return this.hijos;
		}

		public void setDato(T dato){		
			this.dato = dato;
		}
		
		public void setHijos(List<NodoGeneral<T>> hijos){		
			this.hijos = hijos;
		}
		
		public void visitar(){
			visitado = true;
		}
		
		public void desvisitar(){
			visitado = false;
		}
		
		public bool estadoVisitado(){
			return visitado;
		}
		
		public void seGana(){
			gana = "gana IA";
		}
		public void sePierde(){
			gana = "pierde IA";
		}
		
		public string estadoGana(){
			return gana;
		}
	}
}
