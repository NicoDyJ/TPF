
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPF
{
	public class ComputerPlayer: Jugador
	{
		private ArbolGeneral<int> arbol;
		private int limite;
		
		public ComputerPlayer()
		{
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.limite = limite;
			
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
			
			Console.WriteLine(arbol.ancho());
			Console.WriteLine(arbol.altura());
			Console.WriteLine("------------------");
			arbol.porNiveles();
		}
		
		private ArbolGeneral<int> crearArbol(List<ArbolGeneral<int>> arbolesI, List<ArbolGeneral<int>> arbolesH, ArbolGeneral<int> arbol, bool turno){
			turno = !turno;
			List<ArbolGeneral<int>> arboles;
			if(turno == true){
				arboles = arbolesH;
			}
			else{
				arboles = arbolesI;
			}
			foreach(ArbolGeneral<int> x in arboles){
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
				bool decide = false;
				if(turno%2 == 0){
					decide = true;
				}
				else{
					decide = false;
				}
				foreach(ArbolGeneral<int> x in arbolminmax.getHijos()){
					funcionEuristica(x, limit, turno);
					bool ganador = x.estadoGana();
					if(turno%2 == 0){
						if(ganador == false){
							decide = false;
						}
					}
					else{
						if(ganador == true){
							decide = true;
						}
					}
				}
				if(decide == true){
					arbolminmax.seGana();
				}
				else{
					arbolminmax.sePierde();
				}
			}
		}
		
		public override int descartarUnaCarta()
		{
			int carta = arbol.getHijos()[0].getDatoRaiz();
			foreach(ArbolGeneral<int> x in arbol.getHijos()){
				bool ganar = x.estadoGana();
				if(ganar == true){
					carta = x.getDatoRaiz();
				}
			}
			return carta;
		}
		
		public override void cartaDelOponente(int carta)
		{
			foreach(ArbolGeneral<int> x in this.arbol.getHijos()){
				int dato = x.getDatoRaiz();
				if(dato == carta){
					arbol = x;
				}
			}
			
		}
		
	}
}
