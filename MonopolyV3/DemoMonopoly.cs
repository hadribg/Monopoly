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
			// Création du controller.getVue()
			// Mise en place du contexte
			ControllerMonopoly controller = new ControllerMonopoly();

			// Instanciation des objets
			Terrain t = new Terrain (new Groupe ("bleu"), 140, "rue de la paix", 1, 2, 3, 4, 5, 5, 6, 7);
			Terrain t1 = new Terrain (new Groupe ("rouge"), 140, "Boulevard des capucines", 1, 2, 3, 4, 5, 5, 6, 7);

			Joueur j = new Physique ("Loic");
			Joueur j1 = new Physique ("Fanny");

			// Liste de joueurs
			LinkedList<Joueur> joueurs = new LinkedList<Joueur> ();
			joueurs.AddLast (j);
			joueurs.AddLast (j1);

			Console.Clear ();
			Console.WriteLine ("Test methode setCaseCourante et dessinerCase avec un joueur sur un terrain : ");
			j.setCaseCourante (t);
			controller.getVue().dessinerCase(t,joueurs,30,5);

			Console.Clear ();
			j1.setCaseCourante (t1);
			j.setCaseCourante(t1);
			controller.getVue().dessinerCase(t1,joueurs,30,5);

			Console.Clear ();
			Console.WriteLine ("Test methode setCaseCourante et dessinerCase avec plus d'un joueur sur un terrain : ");
			t.construireMaison (j);
			t.construireMaison (j);
			controller.getVue().dessinerCase (t, joueurs,30,5);

			Console.Clear ();
			Console.WriteLine ("Test methode construireMaison et dessinerCase avec deux et quatre maisons, et 1 hotel (noter : Loic s'est déplacé sur un terrain précédant " +
				"ce test, il n'y a donc plus personne sur un terrain)");
			t.construireMaison (j);
			t.construireMaison (j);
			controller.getVue().dessinerCase (t, joueurs,30,5);
			t.construireMaison (j);
			controller.getVue().dessinerCase (t, joueurs,30,5);

			// Bouger le premier joueur joueurs 
			controller.getModele().avancer(12,controller.getModele().getJoueurs().First.Value);

			Console.WriteLine ("Dessiner toutes les cartes du jeu dans l'ordre");
			for (int i = 0; i < controller.getModele ().getCases ().Count (); i++) {
				Console.Clear ();
				controller.getVue().dessinerCase(controller.getModele ().getCases ().ElementAt(i),controller.getModele().getJoueurs(),30,5);
			}

			// Tests Dessiner une case à une position donnée
			Console.Clear ();
			controller.getModele().avancer(10,controller.getModele().getJoueurs().Last.Value);
			controller.getVue().dessinerCase(controller.getModele().getCases().ElementAt(10),controller.getModele().getJoueurs(), 30, 5);
			Console.ReadKey ();

			Console.Clear ();
			controller.getModele().avancer(10,controller.getModele().getJoueurs().Last.Value);
			controller.getVue().dessinerCase(controller.getModele().getCases().ElementAt(20),controller.getModele().getJoueurs(), 30, 5);
			Console.ReadKey ();

			Console.Clear ();
			controller.getModele().avancer(10,controller.getModele().getJoueurs().Last.Value);
			controller.getVue().dessinerCase(controller.getModele().getCases().ElementAt(30),controller.getModele().getJoueurs(), 30, 5);
			Console.ReadKey ();

			Console.Clear ();
			controller.getModele().avancer(7,controller.getModele().getJoueurs().Last.Value);
			controller.getVue().dessinerCase(controller.getModele().getCases().ElementAt(12),controller.getModele().getJoueurs(), 50, 7);
			Console.ReadKey ();

			Console.Clear ();
			controller.getModele().avancer(2,controller.getModele().getJoueurs().Last.Value);
			controller.getVue().dessinerCase(controller.getModele().getCases().ElementAt(14),controller.getModele().getJoueurs(), 50, 7);
			Console.ReadKey ();

			Console.Clear ();
			controller.getModele().avancer(2,controller.getModele().getJoueurs().First.Value);
			controller.getVue().dessinerCase(controller.getModele().getCases().ElementAt(14),controller.getModele().getJoueurs(), 50, 7);
			Console.ReadKey ();
			//Console.WriteLine ("\n\nFin des tests, vous pouvez commencer à jouer. Appuyez sur une touche et une partie de deux joueurs se déroulera sous vos yeux, essayez de la suivre ;) ");

			// Instanciation d'un thread
			// La partie se jouera dans ce thread
			Thread filPrincipal = new Thread(
				new ThreadStart(controller.run));

			// Démarrer le thread
			filPrincipal.Start();

		}
	}
}

