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
			this.plateau = new Plateau(initJoueurs ());
			this.view = new vueMonopoly ();
		}

		public void run() {

			// Déterminer le premier joueur
			Joueur joueurCourant = ControlerMonopoly.determinerPremier(this.plateau.getJoueurs());

			// début de la partie
			int score = 0;
			int lesDoubles = 0;
			while (this.plateau.getJoueurs().Count != 1) {
				// Traitement cas prison
				if (joueurCourant.estEmprisonne() > 0) {
					Console.WriteLine (joueurCourant.getNom () + " est en prison depuis " +joueurCourant.estEmprisonne()+" tour.");
					if (joueurCourant.estEmprisonne() == 1) {
						Console.WriteLine ("Voulez-vous payer 50€ et sortir maintenant? o/n");
						if (Console.ReadLine () == "o") {
							Console.WriteLine ("Vous payez 50€ et vous pouvez avancer");
							joueurCourant.debiter (50);
							plateau.getPrison ().liberer (joueurCourant);
						}
					}
					// Si le joueur n'a pas été libéré
					if (joueurCourant.estEmprisonne() > 0) {
						joueurCourant.setEmprisonne (joueurCourant.estEmprisonne () + 1);
						if (joueurCourant.estEmprisonne () == 4) {
							Console.WriteLine ("Vous n'avez toujours pas fait de double. vous payez 50€ et vous pouvez avancer");
							joueurCourant.debiter (50);
							plateau.getPrison().liberer (joueurCourant);
						}
					}
				}

				// Traitement double
				if (plateau.lanceDes ()) {
					lesDoubles++;
					Console.WriteLine ("Vous avez fait un double");
					// Le joueur a fait un double, on le libère s'il était prisonnier
					if (joueurCourant.estEmprisonne()>0) {
						Console.WriteLine ("Vous sortez de prison");
						plateau.getPrison().liberer (joueurCourant);
						lesDoubles = 0;
					}
				} else
					lesDoubles = 0;
				
				// 3 doubles d'affilée : Le joueur courant va en prison sans jouer
				if (lesDoubles == 3) {
					Console.WriteLine ("3 doubles d'affilée ! Vous allez en prison");
					plateau.getPrison ().emprisonner (joueurCourant, plateau);
					lesDoubles = 0;
					// Fin de tour
				} else {

					// Affecter le score courant
					joueurCourant.setScore(1/*plateau.getSommeDes()*/);
					score = joueurCourant.getScore ();
					Console.WriteLine (joueurCourant.getNom () + " a lancé les dés, il a fait " + score);
					Console.ReadKey ();
						
					if (joueurCourant.estEmprisonne() == 0) {
						plateau.avancer (score, joueurCourant);
						Console.WriteLine (joueurCourant.getNom () + " se déplace");
						Console.ReadKey ();
						Console.WriteLine (joueurCourant.getNom () + " est sur la case " + joueurCourant.getCaseCourante ());
						Console.ReadKey ();

						// Effectuer l'action relative à la case courante
						joueurCourant.getCaseCourante ().callback (joueurCourant, plateau);
						// Vérifier qu'il reste des joueurs
						if (this.plateau.getJoueurs ().Count == 1)
							continue;
					}
					// Fin de tour
				}
				Console.WriteLine (joueurCourant.getNom () + " a " + joueurCourant.getArgent ()+"€");
				Console.ReadKey ();
				if (lesDoubles == 0) {
					joueurCourant = plateau.getJoueurSuivant (joueurCourant);
				}
				Console.WriteLine ("Au tour de " + joueurCourant.getNom ());
				Console.ReadKey ();
			}

			Console.WriteLine(this.plateau.getJoueurs().First.Value.getNom() + " a gagné !");
		}

		// Créer des instance de joueurs
		public LinkedList<Joueur> initJoueurs(){
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
			joueurs.AddLast (new Physique("Fanny"));

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
			int score;
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
	}
}
