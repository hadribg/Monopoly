using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace monopoly
{
	public class DemoMonopoly
	{

		static void Main(string[] args)
		{
			/*Terrain t = new Terrain (new Groupe ("bleu"), 140, "rue de la paix", 1, 2, 3, 4, 5, 5, 6, 7);
			vueMonopoly.dessinerCase (t);
			Console.ReadKey ();*/


			// Création du controler
			// Mise en place du contexte
			ControlerMonopoly controler = new ControlerMonopoly();

			// Instanciation d'un thread
			// La partie se jouera dans ce thread
			Thread filPrincipal = new Thread(
				new ThreadStart(controler.run));

			// Démarrer le thread
			filPrincipal.Start();

		}
	}
}

