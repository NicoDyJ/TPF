
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPF
{
	public class ComputerPlayer: Jugador
	{
		private ArbolGeneral<int> arbol;
		
		public ComputerPlayer()
		{
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			// crea listas de arboles a partir de las cartas propias y del oponente
			// para luego pasarlas por parametro a la funcion crearArbol()
			
			List<ArbolGeneral<int>> arbolesIA = new List<ArbolGeneral<int>>();
			List<ArbolGeneral<int>> arbolesHumano = new List<ArbolGeneral<int>>();
			
			foreach(int x in cartasPropias){
				ArbolGeneral<int> nuevoArbol = new ArbolGeneral<int>(x);
				arbolesIA.Add(nuevoArbol);
			}
			foreach(int x in cartasOponente){
				ArbolGeneral<int> nuevoArbol = new ArbolGeneral<int>(x);
				arbolesHumano.Add(nuevoArbol);
			}
			
			arbol = crearArbol(arbolesIA, arbolesHumano, new ArbolGeneral<int>(0), false);
			funcionEuristica(arbol, limite, -1);
			
		}
		
		public void resultadosPosibles(int limite){
			arbol.resultados(limite, "", true);
		}
		
		private ArbolGeneral<int> crearArbol(List<ArbolGeneral<int>> arbolesI, List<ArbolGeneral<int>> arbolesH, ArbolGeneral<int> arbol, bool turno){
			turno = !turno;      // cada llamada a crearArbol cambiara el turno de cartas a usar en el algoritmo
			List<ArbolGeneral<int>> arboles; 
			if(turno == true){
				arboles = arbolesH;
			}
			else{
				arboles = arbolesI;
			}
			foreach(ArbolGeneral<int> x in arboles){
				//cada arbol que no este visitado, se clonara y se agregara como hijo del arbol pasado 
				//por parametro. luego se marcara como visitado para ir descartando en futuras llamadas
				//recursivas. 
				
				if(x.estadoVisitado() == false){
					ArbolGeneral<int> nuevoArbol = new ArbolGeneral<int>(x.getDatoRaiz());
					arbol.agregarHijo(nuevoArbol);
					x.visitarRaiz();
					crearArbol(arbolesI, arbolesH, nuevoArbol, turno);
					x.desvisitarRaiz();
				}
			}
			return arbol;
		}
		
		private void funcionEuristica(ArbolGeneral<int> arbolminmax, int limit, int turno){
			// turno ira turnando al IA y al usuario. si turno es par, es turno de IA. impar para el usuario
			
			turno++;
			int dato = arbolminmax.getDatoRaiz();
			limit = limit - dato;
			if(limit < 0){
				if(turno%2 == 0){
					arbolminmax.sePierde();
				}
				else{
					arbolminmax.seGana();
				}
			}
			else{
				// si es turno de IA, entonces la funcion euristica minimiza, si es turno del usuario maximiza
				// por defecto si es turno de IA, se implementa que gana IA a menos que haya un solo resultado
				// negativo obtenido de la recursion, entonces se pone en el arbol que pierde (minimizar). 
				// lo contrario con el turno del usuario.
				
				string decide = "";
				if(turno%2 == 0){
					decide = "gana IA";
				}
				else{
					decide = "pierde IA";
				}
				foreach(ArbolGeneral<int> x in arbolminmax.getHijos()){
					funcionEuristica(x, limit, turno);
					string ganador = x.estadoGana();
					if(turno%2 == 0){
						if(ganador == "pierde IA"){
							decide = "pierde IA";
						}
					}
					else{
						if(ganador == "gana IA"){
							decide = "gana IA";
						}
					}
				}
				if(decide == "gana IA"){
					arbolminmax.seGana();
				}
				else{
					arbolminmax.sePierde();
				}
			}
		}
		
		public override int descartarUnaCarta()
		{
			//busca en los hijos del arbol el estado "gana IA" y elige esa carta
			int carta = arbol.getHijos()[0].getDatoRaiz();
			foreach(ArbolGeneral<int> x in arbol.getHijos()){
				string ganar = x.estadoGana();
				if(ganar == "gana IA"){
					carta = x.getDatoRaiz();
				}
			}
			//elige el camino del arbol que posee la carta elegida previamente 
			foreach(ArbolGeneral<int> x in arbol.getHijos()){
				if(x.getDatoRaiz() == carta){
					arbol = x;
				}
			}
			// imprime y retorna la carta
			Console.WriteLine("Naipe elegido por la Computadora: " + carta);
			return carta;
		}
		
		public override void cartaDelOponente(int carta)
		{
			//busca en los hijos del arbol, la carta jugada por el usuario y ese subarbol sera el nuevo
			//arbol del computer player asi tomando ese camino 
			foreach(ArbolGeneral<int> x in this.arbol.getHijos()){
				int dato = x.getDatoRaiz();
				if(dato == carta){
					arbol = x;
				}
			}
			
		}
		
	}
}
