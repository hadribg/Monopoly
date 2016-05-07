using System;
namespace monopoly {
	public class CaseNeutre : Case  {

		private string nom;

		public CaseNeutre(string unType, string unNom)
			:base(unNom){
			type = unType;
			nom = unNom;
		}

		public override void callback(Joueur j, Plateau p) {
			Console.WriteLine ("Reposez vous !");
		}

		public string getNom()	{return this.nom;}
	}
}
