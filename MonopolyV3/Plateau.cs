using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;


namespace monopoly {
	public class Plateau {
		private LinkedList<Case> cases = new LinkedList<Case>();
		private LinkedList<Joueur> joueurs = new LinkedList<Joueur>();
		private Dictionary<string,Groupe> groupes = new Dictionary<string,Groupe>();
		private Prison prison;
		private ArrayList des = new ArrayList();

		public Plateau (LinkedList<Joueur> uneArrayJoueurs)
		{
			joueurs = uneArrayJoueurs;

			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
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
					Prison p = new Prison (propCase[1]);
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
				j.setPlateau (this);
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
		// Permet de stocker un score de dÈs
		// Vrai si le joueur a fait un double
		public bool lanceDes() {
			des.Clear ();
			Random rnd = new Random();
			// Simule deux entiers pour mieux respecter les probabilitÈs
			int de1 = rnd.Next(6)+1;
			int de2 = rnd.Next(6)+1;
			des.Add (de1);
			des.Add (de2);
			if (de1 == de2)
				return true;
			return false;
		}

		public int getSommeDes(){
			int somme = 0;
			foreach (int i in des)
				somme += i;
			return somme;
		}

		// pattern Observer
		public void joueurADecouvert(Joueur j,int argent){
			ArrayList test = this.listeProprietesPossedees (j);
			test = this.possibilitesHypotheque (test, j);

			// Cas o˘ le joueur n'a plus de propriÈtÈ ‡ hypothequer
			if (test.Count == 0) {
				this.faillite(j);
				return;
			}

			int choix = 0;
			Terrain t;
			Console.WriteLine (j.getNom () + " a decouvert de " + (argent*(-1)) + "Ä ");
			Console.WriteLine ("Vous pouvez :");
			Console.WriteLine (choix + " - Abandonner");

			// Parcourir toutes les propriÈtÈs possÈdÈes par le joueur
			// On propose d'hypothequer les terrains, de vendre des maisons, ou d'abandonner
			foreach (Propriete p in test) {
				if (!p.getHypothequee()) {
					// Regarder si c'est un terrain pour proposer de vendre les maisons/hotel d'abord
					if (p is Terrain){
						t = (Terrain)p;
						switch (t.getNbMaison ()) {
						case 0:
							choix++;
							Console.WriteLine (choix + " - hypothequer " + p.getNom ());
							Console.WriteLine("(+"+(t.getValeurHypothecaire())+" Ä)");
							break;
						case 5:
							choix++;
							Console.WriteLine (choix + " - vendre l'hotel " + p.getNom ());
							Console.WriteLine("(+"+(t.getPrixHotel()/2)+" Ä)");
							break;
						default:
							choix++;
							Console.WriteLine (choix + " - vendre une maison " + p.getNom ());
							Console.WriteLine ("(+" +(t.getPrixMaison ()/2) + " Ä)");
							break;
						}
					} else {
						choix++;
						Console.WriteLine (choix + " - hypothequer " + p.getNom ());
						Console.WriteLine("(+"+(p.getValeurHypothecaire())+" Ä)");
					}
				}
			}
			// Effectuer l'action dÈcidÈe par le joueur
			choix = 1;//int.Parse(Console.ReadLine());
			if (choix == 0) {
				this.faillite(j);
				return;
			} else {
				if (test[choix-1] is Terrain){
					t = (Terrain)test[choix-1];
					t.vendre (j);
				} else
					j.hypothequer((Propriete)test[choix-1]);
			}
		}

		// MÈthode qui prend en paramËtres une Arraylist de PropriÈtÈs possÈdÈe par le Joueur j
		// Renvoit une ArrayList des propriÈtÈs qui peuvent actuellement Ítre hypothÈquÈes ou privÈes d'une maison/hotel
		public ArrayList possibilitesHypotheque(ArrayList proprietes, Joueur j){
			ArrayList res = new ArrayList ();
			Terrain t;
			// Le joueur doit vendre en prioritÈ les hotels
			// Le joueur doit vendre uniformÈment les maisons
			// Si le joueur possËde tous les terrains d'un groupe, il ne peut hypothequer un des terrains seulement s'il n'y a pas de maisons sur les terrains du groupe
			// S'il ne possËde pas tous les terrains du groupe OU si la propriÈtÈ n'est pas un terrain, il peut hypothequer
			foreach (Propriete p in proprietes) {
				if (p is Terrain) {
					t = (Terrain)p;
					if (j.PossedeTousLesTerrains (t)) {
						if (t.peutVendre())	res.Add(p);
					} else {
						res.Add (p);
					}
				} else {
					res.Add (p);
				}
			}
			return res;
		}

		// Renvoit une collection de PropriÈtÈs qui ont pour propriÈtaire le joueur passÈ en paramËtre
		// Ne tient pas compte des propritetes hypothequees
		public ArrayList listeProprietesPossedees(Joueur j){
			ArrayList res = new ArrayList ();
			Propriete p;
			foreach (Case c in cases) {
				if (c.getType () == "propriete") {
					p = (Propriete)c;
					if (p.getProprietaire () != null) {
						if (p.getProprietaire ().Equals (j) && !p.getHypothequee())
							res.Add (p);
					}
				}
			}
			return res;
		}

		// Joueur fait faillite, il donne toutes ses propriÈtÈs ‡ la banque
		public void faillite(Joueur j) {
			ArrayList proprietes = j.getPlateau().listeProprietesPossedees (j);
			Terrain t;
			foreach (Propriete p in proprietes) {
				p.setProprietaire (null);
				if (p is Terrain) {
					t = (Terrain)p;
					t.setNbMaisons (0);
				}
			}
			this.joueurs.Remove (j);
			Console.WriteLine (j.getNom()+" a fait faillite !");
		}

		// Renvoit une instance sous forme de collection decrivant l'issue d'une enchËre
		// Prend en paramËtre la propriete mise en jeu
		/*public OrderedDictionary encheres(Propriete p) {
			// TODO
			return;
		}*/

		//get&set
		public LinkedList<Case> getCases()		{return this.cases;}
		public LinkedList<Joueur> getJoueurs()	{return joueurs;}
		public Prison getPrison()				{return prison;}
		public Case getPrisonVisite() {
			foreach (Case c in cases) {
				if (c.getType () == "caseNeutre")
					return c;
			}
			throw new Exception ("ERROR");
		}
	}
}