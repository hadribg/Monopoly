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
		public static void dessinerCase(Case c){
			if (c.getType() == "communaute") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                 |");
				Console.WriteLine ("|      Caisse     |");
				Console.WriteLine ("|        de       |");
				Console.WriteLine ("|    Communaute   |");
				Console.WriteLine ("|                 |");
				Console.WriteLine ("|                 |");
				Console.WriteLine ("|                 |");
				Console.WriteLine (" -----------------");
			}
			if (c.getType() == "chance") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      Chance      |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
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
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      Impot       |");
				Console.WriteLine ("|   sur le revenu  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      200€        |");
				Console.WriteLine (" ------------------");
			}
			if (c.getType() == "taxe") {
				Console.WriteLine (" ------------------");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|       Taxe       |");
				Console.WriteLine ("|     de luxe      |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|                  |");
				Console.WriteLine ("|      100€        |");
				Console.WriteLine (" ------------------");
			}
			if (c.getType () == "propriete") {
				Propriete p = (Propriete)c;
				if (p.getGenre() == "terrain") {
					Terrain t = (Terrain)p;
					Console.WriteLine (" ------------------");
					Console.WriteLine ("|       "+t.getGroupe().getCouleur()+"       |");
					Console.WriteLine ("|                  |");
					Console.WriteLine (" ------------------");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|  "+t.getNom()+"  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|       "+t.getPrix()+"€       |");
					Console.WriteLine (" -----------------");
				}
				if (p.getGenre() == "compagnie") {
					Compagnie t = (Compagnie)p;
					Console.WriteLine (" ------------------");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|  "+t.getNom()+"  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|       150€       |");
					Console.WriteLine (" ------------------");
				}
				if (p.getGenre() == "gare") {
					Gare g = (Gare)p;
					Console.WriteLine (" ------------------");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|  "+g.getNom()+"  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|                  |");
					Console.WriteLine ("|      200€        |");
					Console.WriteLine (" ------------------");
				}

			}
		}
	}
}