using System;
namespace monopoly {
	public class CaseNeutre : Case  {

		public CaseNeutre(string unType, string unNom)
			:base(unNom){
			type = unType;
		}

		public override void callback(Joueur j, Plateau p) {
			Console.WriteLine ("Reposez vous !");
		}

	}

}
