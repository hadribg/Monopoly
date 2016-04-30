using System;
namespace monopoly {
	public class Communaute : Case  {

		public Communaute()
			:base("communaute"){
		}

		public override void callback(Joueur j, Plateau p) {
			Console.WriteLine ("Tirez une carte Caisse de Communaute !");
		}

	}

}
