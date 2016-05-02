using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace monopoly
{
	public class vueMonopoly
	{
		Dictionary<String, ConsoleColor> palette = new Dictionary<String, ConsoleColor>();
		public vueMonopoly ()
		{
			// Définir palette couleur
			Dictionary<String, ConsoleColor> mapCouleur = new Dictionary<String, ConsoleColor>();
			mapCouleur.Add ("violet", ConsoleColor.Magenta);
			mapCouleur.Add ("cyan",ConsoleColor.Cyan);
			mapCouleur.Add ("magenta",ConsoleColor.Magenta);
			mapCouleur.Add ("orange",ConsoleColor.DarkYellow);
			mapCouleur.Add ("rouge",ConsoleColor.Red);
			mapCouleur.Add ("jaune",ConsoleColor.Yellow);
			mapCouleur.Add ("vert",ConsoleColor.Green);
			mapCouleur.Add ("bleu",ConsoleColor.Blue);
			this.palette = mapCouleur;
		}
		public static void dessinerNom(Joueur j){
			String joueur = j.getNom ();
			int taille = joueur.Length;
			Console.Write ("|");
			for(int i=0;i<((18-taille)/2);i++){
				Console.Write (" ");
			}
			Console.Write(joueur);
			int retrait = 0;
			if(taille%2==1)
				retrait=1;
			for(int i=0;i<((18-taille)/2)+retrait;i++){
				Console.Write (" ");
			}
			Console.WriteLine ("|"); 
		}
		public static void dessinerNom(Case c,ArrayList joueurs){
			foreach (Joueur j in joueurs) {
				if (j.getCaseCourante () == c) {
					dessinerNom (j);
				} else {
					Console.WriteLine ("|                  |");
				}
			}
			for (int i = 0; i < 5 - joueurs.Count; i++) {
				Console.WriteLine ("|                  |");
			}
		}
		public static void dessinerNom(String[] nom){
			int compteurMot = 0;
			Console.Write ("| ");
			int compteurLigne = 17;
			while(compteurMot<nom.Length){
				if (nom [compteurMot].Length > compteurLigne) {
					while (compteurLigne > 0) {
						Console.Write (" ");
						compteurLigne--;
					}
					Console.WriteLine ("|");
					Console.Write ("| ");
					compteurLigne = 17;
				}
				Console.Write (nom[compteurMot]+" ");
				compteurLigne -= nom [compteurMot].Length+1;
				compteurMot++;
			}
			while (compteurLigne > 0) {
				Console.Write (" ");
				compteurLigne--;
			}
			Console.WriteLine ("|");
		}

		// Méthode qui prend en paramètre un terrain
		// Dessine la dessine sa couleur dans la partie dédiée
		// V2 : prend en compte les/l' maisons/hotel construit/es sur le terrain
		public void dessinerCouleur(Terrain t){
			ConsoleColor couleur = this.palette [t.getGroupe ().getCouleur ()];
			int compteurLigne = 16;
			int compteurColonne = 3;
			int nbMaisons = t.getNbMaison();
			int maisonDessinee = 0;
			for (int j = 0; j < compteurColonne; j++) {
				Console.Write ("| ");
				Console.BackgroundColor = couleur;
				for (int i = 0; i < compteurLigne; i++){
					// Traitement de l'écart entre les maisons
					if (j == 1 && i % 3 == 2 && maisonDessinee < nbMaisons) {
						Console.Write ("■");
						maisonDessinee++;
					}
					else
						Console.Write (" ");
				}
				Console.ResetColor();
				Console.WriteLine (" |");
			}
		}

		public void dessinerCase(Case c, ArrayList joueurs){
			if (c.getType() == "communaute") {
				Console.WriteLine (" -------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      Caisse      |");
				Console.WriteLine ("|        de        |");
				Console.WriteLine ("|    Communaute    |");
				Console.WriteLine ("|                  |");
				dessinerNom (c,joueurs);
				Console.WriteLine (" -------------------");
			}
			if (c.getType() == "chance") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      Chance      |");
				Console.WriteLine ("|                  |");
				dessinerNom (c,joueurs);
				Console.WriteLine (" ------------------");
			}
			if (c.getType() == "prison") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      Prison      |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine (" ------------------");
			}
			if (c.getType() == "parc") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|       Parc       |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine (" ------------------");
			}
			if (c.getType() == "impot") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      Impot       |");
				Console.WriteLine ("|   sur le revenu  |");
				Console.WriteLine ("|                  |");
				dessinerNom (c,joueurs);
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|       200€       |");
				Console.WriteLine (" ------------------");
			}
			if (c.getType() == "taxe") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|       Taxe       |");
				Console.WriteLine ("|     de luxe      |");
				Console.WriteLine ("|                  |");
				dessinerNom (c,joueurs);
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|       100€       |");
				Console.WriteLine (" ------------------");
			}
			if (c.getType () == "propriete") {
				Propriete p = (Propriete)c;
				if (p.getGenre() == "terrain") {
					Terrain t = (Terrain)p;
					string[] nom = t.getNom().Split(new char[] { ' ' });
					Console.WriteLine (" ------------------");
					dessinerCouleur (t);
					Console.WriteLine (" ------------------");
					Console.WriteLine ("|                  |");
					dessinerNom (nom);
					Console.WriteLine ("|                  |");
					dessinerNom (c, joueurs);
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|       " + t.getPrix () + "€       |");
					Console.WriteLine (" ------------------");

				}
				if (p.getGenre() == "compagnie") {
					Compagnie t = (Compagnie)p;
					string[] nom = t.getNom().Split(new char[] { ' ' });
					if (nom.Length == 4) {
						Console.WriteLine (" ------------------");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|     " + nom [0] + " " + nom [1] + "       |");
						Console.WriteLine ("|     " + nom [2] + " " + nom [3] + "       |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|       200€       |");
						Console.WriteLine (" ------------------");
					}
					if (nom.Length == 3) {
						Console.WriteLine (" ------------------");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|     " + nom [0] + " " + nom [1] + "       |");
						Console.WriteLine ("|     " + nom [2] + "   |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|       200€       |");
						Console.WriteLine (" ------------------");
					}
					if (nom.Length == 2) {
						Console.WriteLine (" ------------------");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|     " + nom [0] + "   |");
						Console.WriteLine ("|     " + nom [1] + "   |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|       200€       |");
						Console.WriteLine (" ------------------");
					}
					if (nom.Length == 1) {
						Console.WriteLine (" ------------------");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|     " + nom [0] + "   |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|                  |");
						Console.WriteLine ("|       200€       |");
						Console.WriteLine (" ------------------");
					}
				}

			}
		}
	}
}