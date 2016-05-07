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

		public static void dessinerNom(Joueur j, int x, int y){
			Console.SetCursorPosition (x, y);
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

		// Dessine le nom des joueurs actuellement sur la case
		// Methode qui renvoit le nombre de lignes qu'elle a dessiné
		public static int dessinerNom(Case c,LinkedList<Joueur> joueurs, int x=30, int y=0){
			int ecartDepart = y;
			foreach (Joueur j in joueurs) {
				if (j.getCaseCourante () == c) {
					Console.SetCursorPosition (x, y++);
					dessinerNom (j,x,y);
				}
			}
			return y-ecartDepart;
		}

		public static int dessinerNom(String[] nom, int x, int y){
			int compteurMot = 0;
			int ecartDepart = y;
			Console.SetCursorPosition (x, y++);
			Console.Write ("| ");
			int compteurLigne = 17;
			// Pour tousles mots
			while(compteurMot<nom.Length){
				// Si le mot courant dépasse de la ligne
				if (nom [compteurMot].Length >= compteurLigne) {
					// On remplit d'espaces 
					while (compteurLigne > 0) {
						Console.Write (" ");
						compteurLigne--;
					}
					// On va à la ligne
					Console.WriteLine ("|");
					Console.SetCursorPosition (x, y++);
					Console.Write ("| ");
					compteurLigne = 17;
				}
				// On écrit le mot courant avec un espace
				Console.Write (nom[compteurMot]+" ");
				compteurLigne -= (nom [compteurMot].Length+1);
				compteurMot++;
			}
			// On remplit d'espaces jusqu'à la fin de la ligne
			while (compteurLigne > 0) {
				Console.Write (" ");
				compteurLigne--;
			}
			Console.WriteLine ("|");
			return y-ecartDepart;
		}

		// Methode pour dessiner les prix au bas des cases propriete
		public static void ecrirePrix(int prix){
			int taille = nombre_chiffre (prix);
			Console.Write ("|");
			for(int i=0;i<((18-taille)/2);i++){
				Console.Write (" ");
			}
			Console.Write(prix.ToString() + "€");
			int retrait = 0;
			if(taille%2==0)
				retrait=1;
			for(int i=0;i<((18-taille)/2)-retrait;i++){
				Console.Write (" ");
			}
			Console.WriteLine ("|");
		}

		public static int nombre_chiffre(int i)	{ 
			int c = 0; 

			if(i < 0)	{ 
				i = -i; 
			} 

			do	{ 
				c++; 
			} 
			while((i /= 10) > 0); 
			return c; 
		}

		public void dessinerCarte(Case c){
			if ((c.getType () != "communaute") && (c.getType () != "chance") && (c.getType () != "propriete"))
				throw new Exception ("Aucunes cartes ne correspond à cette case !");
			else {
				switch (c.getType ()) {
				case "communaute":
					break;
				case "chance":
					break;
				case "propriete":
					Propriete p = (Propriete)c;
					switch (p.getGenre ()) {
					case "terrain":
						break;
					case "compagnie":
						break;
					case "gare":
						break;
					}
					break;
				}
			}
		}

		// Méthode qui prend en paramètre un terrain
		// Dessine la dessine sa couleur dans la partie dédiée
		// V2 : prend en compte les/l' maisons/hotel construit/es sur le terrain
		public int dessinerCouleur(Terrain t, int x, int y){
			int ecartDepart = y;
			ConsoleColor couleur = this.palette [t.getGroupe ().getCouleur ()];
			int compteurLigne = 16;
			int compteurColonne = 3;
			int nbMaisons = t.getNbMaison();
			int maisonDessinee = 0;
			for (int j = 0; j < compteurColonne; j++) {
				Console.SetCursorPosition(x,y++);
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
			return y - ecartDepart;
		}
			

		public void dessinerCase(Case c, LinkedList<Joueur> joueurs, int x, int y){
			if (c.getType() == "communaute") {
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|      Caisse      |");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|        de        |");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|    Communaute    |");
				Console.SetCursorPosition (x, y);  Console.WriteLine ("|                  |");
				y+=dessinerNom (c,joueurs,x,y)+1;
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
			}
			if (c.getType() == "chance") {
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|      Chance      |");
				Console.SetCursorPosition (x, y);  Console.WriteLine ("|                  |");
				y+=dessinerNom (c,joueurs,x,y)+1;
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
			}
			if (c.getType() == "prison") {
				Prison pr = (Prison)c;
				string[] nom = pr.getAffichageCase ().Split (new char[] { ' ' });
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
				y += dessinerNom (nom, x, y);
				Console.SetCursorPosition (x, y);  Console.WriteLine ("|                  |");
				y+=dessinerNom (c,joueurs,x,y)+1;
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
			}
			if (c.getType() == "caseNeutre") {
				CaseNeutre cn = (CaseNeutre)c;
				string[] nom = cn.getNom ().Split (new char[] { ' ' });
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
				y += dessinerNom (nom, x, y);
				Console.SetCursorPosition (x, y);  Console.WriteLine ("|                  |");
				y+=dessinerNom (c,joueurs,x,y)+1;
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
			}
			if (c.getType() == "impot") {
				Impot i = (Impot)c;
				string[] nom = i.getNom().Split (new char[] { ' ' });
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
				y += dessinerNom (nom, x, y);
				Console.SetCursorPosition (x, y);Console.WriteLine ("|                  |");
				y += dessinerNom (c,joueurs,x,y)+1;
				Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
				Console.SetCursorPosition (x, y++);ecrirePrix (i.getPrix ());
				Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
			}
			if (c.getType () == "propriete") {
				Propriete p = (Propriete)c;
				string[] nom;
				switch (p.getGenre ()) {
				case "terrain":
					Terrain t = (Terrain)p;
					nom = t.getNom ().Split (new char[] { ' ' });
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					y+=dessinerCouleur (t,x,y);
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
					y += dessinerNom (nom, x, y);
					Console.SetCursorPosition (x, y);Console.WriteLine ("|                  |");
					y += dessinerNom (c, joueurs, x, y)+1;
					Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
					Console.SetCursorPosition (x, y++);ecrirePrix (t.getPrix ());
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					break;
				case "compagnie":
					Compagnie com = (Compagnie)p;
					nom = com.getNom().Split(new char[] { ' ' });
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
					y += dessinerNom (nom, x, y);
					Console.SetCursorPosition (x, y);Console.WriteLine ("|                  |");
					y += dessinerNom (c, joueurs, x, y)+1;
					Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
					Console.SetCursorPosition (x, y++);ecrirePrix (com.getPrix ());
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					break;
				case "gare":
					Gare g = (Gare)p;
					nom = g.getNom ().Split (new char[] { ' ' });
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
					y += dessinerNom (nom, x, y);
					Console.SetCursorPosition (x, y);Console.WriteLine ("|                  |");
					y += dessinerNom (c, joueurs, x, y)+1;
					Console.SetCursorPosition (x, y++);Console.WriteLine ("|                  |");
					Console.SetCursorPosition (x, y++);ecrirePrix (g.getPrix ());
					Console.SetCursorPosition (x, y++);Console.WriteLine (" ------------------");
					break;
				}
			}
		}
	}
}