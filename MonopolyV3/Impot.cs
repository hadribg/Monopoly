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

		public override void callback(Joueur j, Plateau p) {
			j.debiter (this.prix);
			Console.WriteLine ("Vous payez les impots");
		}

		//get&set
		public string getNom()	{return this.nom;}
		public int getPrix()	{return this.prix;}
	}

}
