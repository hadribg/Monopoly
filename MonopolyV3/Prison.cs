using System;
using System.Collections;
using System.Collections.Generic;

namespace monopoly {
	public class Prison : Case  {

		ArrayList prisonniers = new ArrayList ();

		public Prison()
			: base ("prison"){

		}

		public void liberer (Joueur j){
			prisonniers.Remove(j);
		}

		public void emprisonner (Joueur j) {
			prisonniers.Add(j);
		}
	}

}
