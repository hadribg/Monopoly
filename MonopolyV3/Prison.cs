using System;
using System.Collections;
using System.Collections.Generic;

namespace monopoly {
	public class Prison : Case  {

		ArrayList prisonniers = new ArrayList ();

		public Prison()
			: base ("prison"){

		}

		// Permet de savoir si un joueur est actuellement en prison
		public bool estEmprisonne(Joueur j){
			if (prisonniers.Contains (j))
				return true;
			return false;
		}

		public void liberer (Joueur j){
			prisonniers.Remove(j);
			j.setEmprisonne (0);
		}

		public void emprisonner (Joueur j, Plateau p) {
			prisonniers.Add(j);
			j.setEmprisonne (1);
			j.setCaseCourante (p.getPrison ());
		}

		public override void callback (Joueur j, Plateau p)
		{
			emprisonner (j,p);
			Console.WriteLine ("Vous allez en prison");
		}
	}
}
