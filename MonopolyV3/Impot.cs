using System;
namespace monopoly {
	public class Impot : Case  {
		private String nom;
		private int prix;

		public Impot(string unNom, int unPrix)
			: base ("impot"){
			nom = unNom;
			prix = unPrix;
		}

	}

}
