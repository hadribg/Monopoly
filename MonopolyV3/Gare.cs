using System;
namespace monopoly {
	public class Gare : Propriete  {
		private int loyer1gare;
		private int loyer2gares;
		private int loyer3gares;
		private int loyer4gares;

		public Gare(int unPrix, string unNom, int l1, int l2, int l3, int l4)
			:base("gare",unPrix,unNom){
			loyer1gare = l1;
			loyer2gares = l2;
			loyer3gares = l3;
			loyer4gares = l4;
		}

	}

}
