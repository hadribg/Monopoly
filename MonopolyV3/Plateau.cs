using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace monopoly {
	public class Plateau {
		private LinkedList<Case> cases = new LinkedList<Case>();
        private ArrayList joueurs = new ArrayList();
		private Dictionary<string,Groupe> groupes = new Dictionary<string,Groupe>();

		public Plateau (ArrayList uneArrayJoueurs)
        {
			joueurs = uneArrayJoueurs;

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader monStreamReader = new StreamReader("../../cases.txt", encoding);

			// Chargement du loyer des gares
			string[] loyerGares = monStreamReader.ReadLine().Split(new char[] { ',' });

			// Chargement du des différents groupes
			string[] couleurs = monStreamReader.ReadLine().Split(new char[] { ',' });
			for (int i = 0; i < couleurs.Length; i++)
				groupes.Add(couleurs[i],new Groupe(couleurs[i]));

			// Lecture de toutes les cases (un par lignes)
			string chaineCase = monStreamReader.ReadLine();
			string[] propCase = chaineCase.Split(new char[] { ',' });

			while (chaineCase != null)
            {
				propCase = chaineCase.Split(new char[] { ',' });

				switch (propCase [0]) {
				case "depart":
					cases.AddLast (new Depart());
					break;
				case "communaute":
					cases.AddLast (new Communaute());
					break;
				case "chance":
					cases.AddLast (new Chance());
					break;
				case "prison":
					cases.AddLast (new Prison());
					break;
				case "parc":
					cases.AddLast (new Parc());
					break;
				case "impot":
					cases.AddLast (new Impot(propCase[1],int.Parse(propCase[2])));
					break;
				case "propriete":
					switch (propCase [1]) {
					case "terrain":
						cases.AddLast(new Terrain(groupes[propCase[2]],int.Parse(propCase[3]),propCase[4],int.Parse(propCase[5]),int.Parse(propCase[6]),int.Parse(propCase[7]),int.Parse(propCase[8]),int.Parse(propCase[9]),int.Parse(propCase[10]),int.Parse(propCase[10]),int.Parse(propCase[10]),int.Parse(propCase[10])));
						break;
					case "gare":
						cases.AddLast (new Gare (int.Parse (loyerGares [0]), propCase [2], int.Parse (loyerGares [1]), int.Parse (loyerGares [2]), int.Parse (loyerGares [3]), int.Parse (loyerGares [4])));
						break;
					case "compagnie":
						cases.AddLast (new Compagnie (propCase [2], int.Parse (propCase [3])));
						break;
					}
					break;
				}
				chaineCase = monStreamReader.ReadLine();
            }
            // Fermeture du StreamReader
            monStreamReader.Close();

			// Placer les joueurs sur la case départ
			foreach (Joueur j in joueurs) {
				j.setCaseCourante (cases.First.Value);
			}
        }

        // Permet à un joueur de se déplacer de nbCases cases sur le plateau
        public void SeDeplacer(int nbCases, Joueur joueur) {
            // Cas d'invalidité
            if (nbCases < 1 || nbCases > 12) throw new Exception("Nombre de case incohérent");

            // Avance case par case le joueur sur le plateau
            for (int i = 0; i< nbCases; i++)
            {
                joueur.setCaseCourante(cases.Find(joueur.getCaseCourante()).Next.Value);
            }
        }

        //get&set
        public LinkedList<Case> getCases()
        {
            return this.cases;
        }

    }

}
