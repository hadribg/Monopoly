using System;
namespace monopoly {
	public class Chance : Case  {

		public Chance ()
			: base ("chance"){

		}

		public override void callback(Joueur j, Plateau p) {
			Console.WriteLine ("Tirez une carte chance !");
		}
	}

}
