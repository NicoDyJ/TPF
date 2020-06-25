
using System;
using System.Collections.Generic;

namespace TPF
{
	class Juego
	{
		public static void Main(string[] args)
		{
			Game game = new Game();
			game.iniciar();
			
			
			/*
				List<int> cartasIA = new List<int>();
			List<int> cartasHumano = new List<int>();
			cartasHumano.Add(1);
			cartasHumano.Add(2);
			cartasIA.Add(3);
			cartasIA.Add(4);
			
			ComputerPlayer compu = new ComputerPlayer();
			compu.incializar(cartasIA, cartasHumano, 6);
			*/
			
			
		    Console.ReadKey();
		}
	}
}