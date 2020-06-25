
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
			
			Console.WriteLine(arbol.ancho());
			Console.WriteLine(arbol.altura());
		}
		
		private ArbolGeneral<int> crearArbol(List<ArbolGeneral<int>> arbolesI, List<ArbolGeneral<int>> arbolesH, ArbolGeneral<int> arbol, bool turno){
			turno = !turno;
			List<ArbolGeneral<int>> arboles;
			if(turno == true){
				arboles = arbolesI;
			}
			else{
				arboles = arbolesH;
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
		
		public override int descartarUnaCarta()
		{
			//Implementar
			return 0;
		}
		
		public override void cartaDelOponente(int carta)
		{
			//implementar
			
		}
		
	}
}
