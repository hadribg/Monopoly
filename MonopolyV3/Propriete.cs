using System;

namespace monopoly {
	public class Propriete : Case  {
		protected String genre;
		protected int prix;
		protected String nom;
		protected Joueur proprietaire;
		protected bool estHypothequee;
		protected int valeurHypothecaire;
		protected Groupe groupe;


		public Propriete(string unGenre, int unPrix, string unNom)
			:base("propriete"){
			genre = unGenre;
			prix = unPrix;
			nom = unNom;
			proprietaire = null;
			estHypothequee = false;
			valeurHypothecaire = unPrix/2;
		}

		public bool estAchetee(){
			if (this.proprietaire == null)	return false;
			return true;
		}
			
		public override void callback(Joueur j, Plateau plateau) {

			// Propriété n'appartenant à personne
			if (this.proprietaire == null) {
				Console.WriteLine ("Voulez vous acheter cette propriete ? o/n");
				if (Console.ReadLine () == "o") {
					j.acheter ((Propriete)j.getCaseCourante ());
				} else {
					// ENCHERES
				}
			} else {
				if (this.proprietaire.Equals (j)) {
					Console.WriteLine ("Vous êtes chez vous");
				} else {
					if (this.getHypothequee ()) {
						Console.WriteLine ("Propriete hypothequee, vous ne payez aucun loyer.");
					} else {
						Console.WriteLine ("Payez le loyer");
						Propriete p = (Propriete)j.getCaseCourante ();
						j.transaction (this.proprietaire, (p.getLoyer (j.getScore ())));
					}
				}
			}
		}

		public virtual int getLoyer(int score){
			switch (this.getGenre ()){
			case "terrain":
				Terrain t = (Terrain)this;
				switch (t.getNbMaison()) {
				case 0:
					if (proprietaire.PossedeTousLesTerrains (t))
						return (t.getLoyerTerrain () * 2);
					else
						return t.getLoyerTerrain ();
				case 1:
					return t.getLoyer1maison ();
				case 2:
					return t.getLoyer2maisons ();
				case 3:
					return t.getLoyer3maisons();
				case 4:
					return t.getLoyer4maisons();
				case 5:
					return t.getLoyerHotel();
				}
				throw new Exception ("Erreur nombre de maisons");
			case "gare":
				Gare g = (Gare)this;
				switch (g.getProprietaire().nbProprietePossedees(g)) {
				case 1:
					return g.getLoyer1gare ();
				case 2:
					return g.getLoyer2gares ();
				case 3:
					return g.getLoyer3gares ();
				case 4:
					return g.getLoyer4gares ();
				}
				throw new Exception ("Erreur nombre de gares");
			case "compagnie":
				switch (this.getProprietaire ().nbProprietePossedees (this)) {
				case 1:
					return 4*score;
				case 2:
					return 10*score;
				}
				throw new Exception ("Erreur nombre de compagnies");
			}
			throw new Exception ("propriete non conforme");
		}

		public override string ToString ()
		{
			return genre +" "+nom;
		}

		// get&set
		public string getGenre() 				{return genre;}
		public int getPrix()     				{return prix;}
		public string getNom()   				{return nom;}
		public Joueur getProprietaire()   		{return proprietaire;}
		public bool getHypothequee()			{return estHypothequee;}
		public int getValeurHypothecaire()		{return valeurHypothecaire;}
		public Groupe getGroupe ()				{return groupe;}

		public void setProprietaire(Joueur j)	{this.proprietaire = j;}
		public void setHypotheque(bool h)		{this.estHypothequee = h;}
	}

}