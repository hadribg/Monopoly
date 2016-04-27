using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace monopoly
{
	class ControlerMonopoly
	{
		private Plateau plateau;
		private vueMonopoly view;

		public ControlerMonopoly() {
			// Initialiser les joueurs
			// Créer le plateau de jeu qui sera le modèle
			this.plateau = new Plateau(ControlerMonopoly.initJoueurs ());
			this.view = new vueMonopoly ();
		}

		public void run() {

			// Déterminer le premier joueur
			Joueur joueurCourant = ControlerMonopoly.determinerPremier(this.plateau.getJoueurs());

			// début de la partie
			int score;
			while (this.plateau.getJoueurs().Count != 1) {
				score = ControlerMonopoly.lancerDes();
				Console.WriteLine (joueurCourant.getNom () + " a lancé les dés, il a fait " + score);
				Console.ReadKey ();
				plateau.SeDeplacer (score, joueurCourant);
				Console.WriteLine (joueurCourant.getNom () + " se déplace");
				Console.ReadKey ();
				Console.WriteLine (joueurCourant.getNom () + " est sur la case " + joueurCourant.getCaseCourante ());
				Console.ReadKey ();
				if (joueurCourant.getCaseCourante ().getType () == "propriete") {
					Console.WriteLine ("Voulez vous acheter cette propriete ? o/n");
					if (Console.ReadLine () == "o") {
						joueurCourant.acheter ((Propriete)joueurCourant.getCaseCourante ());
					}
				}				Console.WriteLine (joueurCourant.getNom () + " a " + joueurCourant.getArgent ()+"€");
				Console.ReadKey ();

				joueurCourant = plateau.getJoueurSuivant (joueurCourant);
				Console.WriteLine ("Au tour de " + joueurCourant.getNom ());
				Console.ReadKey ();
			}
			// cocuou
			// TESTPART
			LinkedList<Case> cases = plateau.getCases();
			foreach (Case c in cases){
				Console.WriteLine(c.ToString());
			}
		}

		// Créer des instance de joueurs
		public static LinkedList<Joueur> initJoueurs(){
			/*
			// Demander combien de joueurs
			Console.WriteLine ("Combien de joueurs physiques ?");
			int nbPhysiques = int.Parse(Console.ReadLine ());
			Console.WriteLine ("Combien d'IA ?");
			int nbIA = 0;// int.Parse(Console.ReadLine ());

			// Cas d'invalidités
			if (nbIA == 0 && nbPhysiques == 0)	throw new Exception("Vous ne pouvez pas jouer sans joueur !");
			if (nbIA < 0 || nbPhysiques < 0)	throw new Exception ("Erreur : nombre de joueur négatif");

			LinkedList<Joueur> joueurs = new LinkedList<Joueur>();

			// Demander les noms
			string nom;
			for (int i = 0; i < nbPhysiques; i++) {
				Console.WriteLine ("Un nom pour le joueur physique " + (i+1));
				nom = Console.ReadLine ();
				joueurs.AddLast(new Physique (nom));
			}
			for (int i = 0; i < nbIA; i++) {
				joueurs.AddLast(new IA("un panda moqueur"));
			}

			Console.WriteLine ("Joueurs créés");
			*/
			LinkedList<Joueur> joueurs = new LinkedList<Joueur>();
			joueurs.AddLast (new Physique("Hadrien"));
			joueurs.AddLast (new Physique("Hadrien"));

			return joueurs;

		}

		// Simule un lancer de dés
		public static int lancerDes(){
			Random rnd = new Random();
			// Simule deux entiers pour mieux respecter les probabilités
			int de1 = rnd.Next(6)+1;
			int de2 = rnd.Next(6)+1;
			return de1+de2;
		}

		// Simuler un lancer de des par joueur et retourner le joueur qui a fait le meilleur score
		public static Joueur determinerPremier(LinkedList<Joueur> joueurs){
			Joueur premier= joueurs.First.Value;
			int max=0;
			int score=0;

			foreach (Joueur j in joueurs) {
				score = lancerDes ();
				Console.WriteLine (j.getNom () + " a lancé les dés, il a fait " + score);
				Console.ReadKey ();
				if (score > max) {
					premier = j;
					max = score;
				}
			}
			Console.WriteLine (premier.getNom () + " commence à jouer");
			Console.ReadKey ();
			return premier;
		}

		// Test pattern Observer
		public static void joueurADecouvert(Joueur j,int argent){
			Console.WriteLine (j.getNom()+" a découvert de " + argent+"€ ");
			Thread.Sleep(3000);
			Console.WriteLine ("coucou");
		}

		public static void joueurADecouvert(){
			Console.WriteLine (" a découvert de ");
			Thread.Sleep(3000);
			Console.WriteLine ("coucou");
		}

	}

}
