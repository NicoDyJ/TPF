
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPF
{

	public class Game
	{
		public static int WIDTH = 12;
		public static int UPPER = 35;
		public static int LOWER = 25;
		
		private Jugador player1 = new ComputerPlayer();
		private Jugador player2 = new HumanPlayer();
		private List<int> naipesHuman = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool juegaHumano = false;
		
		
		public Game()
		{
			var rnd = new Random();
			limite = rnd.Next(LOWER, UPPER);
			//limite = 7;
			
			naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();
			
			for (int i = 1; i <= WIDTH; i++) {
				if (!naipesHuman.Contains(i)) {
					naipesComputer.Add(i);
				}
			}
			
			/*
			List<int> cartasIA = new List<int>();
			List<int> cartasHumano = new List<int>();
			cartasHumano.Add(1);
			cartasHumano.Add(2);
			cartasHumano.Add(3);
			cartasIA.Add(4);
			cartasIA.Add(5);
			cartasIA.Add(6);
			
			player1.incializar(cartasIA, cartasHumano, limite);
			player2.incializar(cartasHumano, cartasIA, limite);
			*/
			
			player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);
			
		}
		
		
		private void printScreen()
		{
			Console.WriteLine();
			Console.WriteLine("Limite: " + limite.ToString());
			Console.WriteLine();
		}
		
		private void banner(){
			Console.WriteLine(
				"********************************************************************************\n" +
				"****************                 CTEDyA - Juego               ******************\n" +
				"********************************************************************************\n"
			);
		}
		
		private void turn(Jugador jugador, Jugador oponente, List<int> naipes)
		{
			int carta = jugador.descartarUnaCarta();
			naipes.Remove(carta);
			limite -= carta;
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}
		
		
		
		private void printWinner()
		{
			if (!juegaHumano) {
				Console.WriteLine("Gano el Ud");
			} else {
				Console.WriteLine("Gano Computer");
			}
			
		}
		
		private bool fin()
		{
			return limite < 0;
		}
		
		public void iniciar()
		{
			int opcion = -1;
			while(opcion != 0){
				banner();
				Console.WriteLine("ingrese una opcion: \n" +
				                  "\n" +
				                  "1) Comenzar una nueva partida\n" +
				                  "0) salir\n");
				opcion = int.Parse(Console.ReadLine());
				if(opcion == 1){
					Console.Clear();
					play();
				}
				else if(opcion == 0){
					opcion = 0;
				}
				else{
					Console.WriteLine("opcion incorrecta..");
					Console.ReadKey();
				}
				Console.Clear();
			}
		}
		
		public void play(){
			banner();
			while (!this.fin()) {
				this.printScreen();
				this.turn(player2, player1, naipesHuman);	// Juega el usuario				
				if (!this.fin()) {
					Console.Clear();
					banner();
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
				}
			}
			Console.Clear();
			banner();
			this.printWinner();
			Console.ReadKey();
		}
		
		
	}
}
