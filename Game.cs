
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
		
		private Jugador player1;   // ComputerPlayer
		private Jugador player2;   // HumanPlayer
		private List<int> naipesHuman;
		private List<int> naipesComputer;
		private int limite;
		private bool juegaHumano = false;
		private List<int> colaDeJugadas;    // contendra las jugadas realizadas durante el juego
		
		public Game(){	}
		
		private void reset(){
			// instancia todos los atributos del Game
			
			player1 = new ComputerPlayer();
			player2 = new HumanPlayer();
			naipesHuman = new List<int>();
			naipesComputer = new List<int>();
			juegaHumano = false;
			colaDeJugadas = new List<int>();
			
			var rnd = new Random();
			limite = rnd.Next(LOWER, UPPER);
			//limite = 7;
			
			naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();
			
			for (int i = 1; i <= WIDTH; i++) {
				if (!naipesHuman.Contains(i)) {
					naipesComputer.Add(i);
				}
			}
			// inicializa los jugadores con las cartas elegidas aleatoriamente
			
			player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);
		}
		
		private void printScreen()
		{
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
			colaDeJugadas.Add(carta);
			naipes.Remove(carta);
			limite -= carta;
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}
		
		
		private void printWinner()
		{
			if (!juegaHumano) {
				Console.WriteLine("Ganaste !!");
			} else {
				Console.WriteLine("Gano la Computadora");
			}
			
		}
		
		private void imprimirJugadas(){
			string cadena = "";
			foreach(int x in colaDeJugadas){
				cadena = cadena + x.ToString() + ", ";
			}
			Console.WriteLine("las jugadas realizadas son: " + cadena);
		}
		
		private bool fin()
		{
			return limite < 0;
		}
		
		public void iniciar()
		{
			// menu principal del juego
			
			int opcion = -1;
			while(opcion != 0){
				banner();
				Console.WriteLine("Ingrese una opcion: \n" +
				                  "\n" +
				                  "1) Comenzar una nueva partida\n" +
				                  "0) Salir\n");
				opcion = int.Parse(Console.ReadLine());
				if(opcion == 1){
					// reset reinstanciara todos los atributos del game y creara todo lo necesario 
					// para una nueva partida
					reset();
					Console.Clear();
					play();
				}
				else if(opcion == 0){
					opcion = 0;
				}
				else{
					Console.WriteLine("Opcion incorrecta...");
					Console.ReadKey();
				}
				Console.Clear();
			}
		}
		
		private void play(){
			banner();
			while (!this.fin()) {
				this.printScreen();
				consultas();
			}
			Console.WriteLine("");
			this.printWinner();
			Console.ReadKey();
			Console.Clear();
		}
	
		private void consultas(){
			string opcion;
			Console.WriteLine("ingrese una opcion: \n" +
			                  "\n" +
			                  "a) tirar una carta\n" +
			                  "b) ver todos los resultados posibles\n" +
			                  "c) ver todas las jugadas\n");
			opcion = Console.ReadLine();
			Console.WriteLine("");
			if(opcion == "a"){
				this.turn(player2, player1, naipesHuman);	// Juega el usuario				
				if (!this.fin()) {
					Console.Clear();
					banner();
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
					Console.WriteLine("");
				}
			}
			else if(opcion == "b"){
				ComputerPlayer compu = (ComputerPlayer)player1;
				compu.resultadosPosibles(limite);
				Console.ReadKey();
				Console.Clear();
				banner();
			}
			else if(opcion == "c"){
				imprimirJugadas();
				Console.ReadKey();
				Console.Clear();
				banner();
			}
			else{
				Console.WriteLine("opcion incorrecta");
			}
			Console.WriteLine("");
		}
		
	}
}
