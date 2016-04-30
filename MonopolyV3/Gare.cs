using System;
namespace monopoly {
	public class Gare : Propriete  {
		private int loyer1gare;
		private int loyer2gares;
		private int loyer3gares;
		private int loyer4gares;


		public Gare(Groupe unGroupe, int unPrix, string unNom, int l1, int l2, int l3, int l4)
			:base("gare",unPrix,unNom){
			loyer1gare = l1;
			loyer2gares = l2;
			loyer3gares = l3;
			loyer4gares = l4;
			groupe = unGroupe;
		}

		public string getNom()	{return nom;}

		// get
		public int getLoyer1gare()	{return loyer1gare;}
		public int getLoyer2gares()	{return loyer2gares;}
		public int getLoyer3gares()	{return loyer3gares;}
		public int getLoyer4gares()	{return loyer4gares;}

	}

}
