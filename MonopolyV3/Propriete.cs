using System;

namespace monopoly {
	public class Propriete : Case  {
		protected String genre;
		protected int prix;
		protected String nom;
		protected Joueur proprietaire;

		public Propriete(string unGenre, int unPrix, string unNom)
			:base("propriete"){
			genre = unGenre;
			prix = unPrix;
			nom = unNom;
			proprietaire = null;
		}

		public void Acheter(Joueur acheteur) {
			this.proprietaire = acheteur;
		}

		public override string ToString ()
		{
			return genre;
		}

	}

}
