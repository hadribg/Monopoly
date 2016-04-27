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
		public bool estHypothequee()			{return estHypothequee;}
		public int getValeurHypothecaire()		{return valeurHypothecaire:}
		public void setProprietaire(Joueur j)	{this.proprietaire = j;}

		public override string ToString ()
		{
			return genre +" "+nom;
		}

	}

}
