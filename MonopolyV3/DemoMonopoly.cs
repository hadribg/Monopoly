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
			vueMonopoly vue = new vueMonopoly ();
			Terrain t = new Terrain (new Groupe ("bleu"), 140, "rue de la paix", 1, 2, 3, 4, 5, 5, 6, 7);
			Terrain t1 = new Terrain (new Groupe ("bleu"), 140, "Boulevard des capucines", 1, 2, 3, 4, 5, 5, 6, 7);

			Joueur j = new Physique ("Loic");
			Joueur j1 = new Physique ("Fanny");
			ArrayList joueurs = new ArrayList ();
			joueurs.Add (j);
			joueurs.Add (j1);

			j.setCaseCourante (t);
			vue.dessinerCase(t,joueurs);

			j1.setCaseCourante (t1);
			j.setCaseCourante(t1);
			vue.dessinerCase(t1,joueurs);

			t.construireMaison (j);
			t.construireMaison (j);

			vue.dessinerCase (t, joueurs);

			t.construireMaison (j);
			t.construireMaison (j);

			vue.dessinerCase (t, joueurs);

			t.construireMaison (j);

			vue.dessinerCase (t, joueurs);
			Console.ReadKey ();


			// Création du controler
			// Mise en place du contexte
			ControllerMonopoly controler = new ControllerMonopoly();

			// Instanciation d'un thread
			// La partie se jouera dans ce thread
			Thread filPrincipal = new Thread(
				new ThreadStart(controler.run));

			// Démarrer le thread
			filPrincipal.Start();

		}
	}
}

