using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace monopoly {
	public class Plateau {
		private LinkedList<Case> cases = new LinkedList<Case>();
		private LinkedList<Joueur> joueurs = new LinkedList<Joueur>();
		private Dictionary<string,Groupe> groupes = new Dictionary<string,Groupe>();
		private Prison prison;

		public Plateau (LinkedList<Joueur> uneArrayJoueurs)
		{
			joueurs = uneArrayJoueurs;

			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
			StreamReader monStreamReader = new StreamReader("../../cases.csv", encoding);

			// Chargement du loyer des gares
			string[] loyerGares = monStreamReader.ReadLine().Split(new char[] { ',' });

			// Chargement du des diff»rents groupes
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
					Prison p = new Prison ();
					cases.AddLast (p);
					prison = p;
					break;
				case "caseNeutre":
					cases.AddLast (new CaseNeutre(propCase[0], propCase[1]));
					break;
				case "impot":
					cases.AddLast (new Impot(propCase[1],int.Parse(propCase[2])));
					break;
				case "propriete":
					switch (propCase [1]) {
					case "terrain":
						Terrain t = new Terrain (groupes [propCase [2]], int.Parse (propCase [3]), propCase [4], int.Parse (propCase [5]), int.Parse (propCase [6]), int.Parse (propCase [7]), int.Parse (propCase [8]), int.Parse (propCase [9]), int.Parse (propCase [10]), int.Parse (propCase [11]), int.Parse (propCase [12]));
						cases.AddLast (t);
						groupes [propCase [2]].getPropriete ().Add (t);
						break;
					case "gare":
						Gare g = new Gare (groupes[propCase[1]],int.Parse (loyerGares [0]), propCase [2], int.Parse (loyerGares [1]), int.Parse (loyerGares [2]), int.Parse (loyerGares [3]), int.Parse (loyerGares [4]));
						cases.AddLast (g);
						groupes [propCase [1]].getPropriete ().Add (g);
						break;
					case "compagnie":
						Compagnie c = new Compagnie (groupes [propCase [1]], propCase [2], int.Parse (propCase [3]));
						cases.AddLast (c);
						groupes [propCase [1]].getPropriete ().Add (c);
						break;
					}
					break;
				}
				chaineCase = monStreamReader.ReadLine();
			}
			// Fermeture du StreamReader
			monStreamReader.Close();

			// Placer les joueurs sur la case depart
			foreach (Joueur j in joueurs) {
				j.setCaseCourante (cases.First.Value);
			}
		}

		// Permet ‡ un joueur de se deplacer de nbCases cases sur le plateau suite ‡ un lancer de des
		public void avancer(int nbCases, Joueur joueur) {
			// Cas d'invalidite
			if (nbCases < 1 || nbCases > 12) throw new Exception("Nombre de cases incoherent");

			// Avance case par case le joueur sur le plateau
			for (int i = 0; i< nbCases; i++)
			{
				if (cases.Find (joueur.getCaseCourante ()).Next == null) {
					// Donner de l'argent car pasage sur la case d»part
					joueur.crediter(200); // modifier constante dans fichier de config
					joueur.setCaseCourante (cases.First.Value);
				}
				else
					joueur.setCaseCourante(cases.Find(joueur.getCaseCourante()).Next.Value);
			}
		}

		// Permet ‡ un joueur j d'avancer jusqu'‡ la case c
		public void avancer(Case c, Joueur j) {
			// ContrÙle de la validitÈ du paramËtre
			if (c == null) throw new Exception("Cases incoherente");

			// Avance case par case le joueur sur le plateau
			do {
				if (cases.Find (j.getCaseCourante ()).Next == null) {
					// Donner de l'argent car pasage sur la case d»part
					j.crediter (200); // modifier constante dans fichier de config
					j.setCaseCourante (cases.First.Value);
				} else
					j.setCaseCourante (cases.Find (j.getCaseCourante ()).Next.Value);
			} while  (!j.getCaseCourante ().Equals (c));
		}

		public void reculer(int nbCases, Joueur joueur) {
			// Cas d'invalidite
			if (nbCases < 1) throw new Exception("Nombre de cases incohÈrent");

			// recule case par case le joueur sur le plateau
			for (int i = 0; i< nbCases; i++)
			{
				// Cas de la case depart
				if (cases.Find (joueur.getCaseCourante ()).Previous == null) {
					joueur.setCaseCourante (cases.Last.Value);
				}
				else
					joueur.setCaseCourante(cases.Find(joueur.getCaseCourante()).Previous.Value);
			}
		} 

		public void reculer(Case c, Joueur j) {
			// ContrÙle de la validitÈ du paramËtre
			if (c == null) throw new Exception("Cases incoherente");

			// Cas o˘ on est sur la case dÈpart et qu'on recule
			// On ne reÁoit pas une deuxiËme fois les sous
			Case departTest = new Case("depart");
			if (j.getCaseCourante().Equals(departTest)){
				if(!c.Equals(departTest))
					j.setCaseCourante (cases.Last.Value);
			}

			// Avance case par case le joueur sur le plateau
			do {
				if (cases.Find (j.getCaseCourante ()).Previous == null) {
					// Donner de l'argent car pasage sur la case d»part
					j.crediter (200); // modifier constante dans fichier de config
					j.setCaseCourante (cases.Last.Value);
				} else
					j.setCaseCourante (cases.Find (j.getCaseCourante ()).Previous.Value);
			} while (!j.getCaseCourante ().Equals (c));
		}

		// Permet de savoir qui est le joueur suivant
		public Joueur getJoueurSuivant(Joueur unJoueur) {
			if (joueurs.Find (unJoueur).Next == null)
				return joueurs.First.Value;
			return joueurs.Find(unJoueur).Next.Value;
		}

		public Prison getPrison(){		
			return prison;
		}

		//get&set
		public LinkedList<Case> getCases()		{return this.cases;}
		public LinkedList<Joueur> getJoueurs()	{return joueurs;}
	}

}
