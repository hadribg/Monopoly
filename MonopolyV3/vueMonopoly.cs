using System;

namespace monopoly
{
	public class vueMonopoly
	{
		public vueMonopoly ()
		{
		}

		public static void dessinerCase(Case c){
			if (c.getType() == "communaute") {
				Console.WriteLine (" ------------");
				Console.WriteLine ("|            |");
				Console.WriteLine ("|            |");
				Console.WriteLine ("|            |");
				Console.WriteLine ("| Communaute |");
				Console.WriteLine ("|            |");
				Console.WriteLine ("|            |");
				Console.WriteLine ("|            |");
				Console.WriteLine (" ------------");
			}
			if (c.getType () == "propriete") {
				Propriete p = (Propriete)c;
				if (p.getGenre() == "terrain") {
					Terrain t = (Terrain)p;
					// t.getGroupe().getCouleur() : obtenir le string de la couleur de la case courante
					Console.WriteLine (" ------------");
					Console.WriteLine ("|            |");
					Console.WriteLine ("|            |");
					Console.WriteLine ("|            |");
					Console.WriteLine ("| "+t.getGroupe().getCouleur()+" |");
					Console.WriteLine ("|            |");
					Console.WriteLine ("|            |");
					Console.WriteLine ("|            |");
					Console.WriteLine (" ------------");
				}
			}
		}

	}
}

