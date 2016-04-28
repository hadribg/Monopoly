using System;

namespace monopoly {
	public class Propriete : Case  {
		protected String genre;
		protected int prix;
		protected String nom;
		protected Joueur proprietaire;
		protected bool estHypothequee;
		protected int valeurHypothecaire;


		public Propriete(string unGenre, int unPrix, string unNom)
			:base("propriete"){
			genre = unGenre;
			prix = unPrix;
			nom = unNom;
			proprietaire = null;
			estHypothequee = false;
			valeurHypothecaire = unPrix/2;
		}

		public void Acheter(Joueur acheteur) {
			this.proprietaire = acheteur;
		}

		public bool estAchetee(){
			if (this.proprietaire == null)	return false;
			return true;
		}

		// get&set
		public string getGenre() 				{return genre;}
		public int getPrix()     				{return prix;}
		public string getNom()   				{return nom;}
		public Joueur getProprietaire()   		{return proprietaire;}
		public bool getHypothequee()			{return estHypothequee;}
		public int getValeurHypothecaire()		{return valeurHypothecaire;}
		public void setProprietaire(Joueur j)	{this.proprietaire = j;}
		public void setHypotheque(bool h)		{this.estHypothequee = h;}

		public override void callback(Joueur j) {
			
			if (this.proprietaire == null) {
				Console.WriteLine ("Voulez vous acheter cette propriete ? o/n");
				if (Console.ReadLine () == "o") {
					j.acheter ((Propriete)j.getCaseCourante ());
				}
			} else {
				if (this.proprietaire.Equals (j)) {
					Console.WriteLine ("Vous êtes chez vous");
				} else {
					Console.WriteLine("Payez le loyer");
					j.transaction(this.proprietaire,j.getCaseCourante().get
				}
			}
		}

		public override string ToString ()
		{
			return genre +" "+nom;
		}

	}

}
