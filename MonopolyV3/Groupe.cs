using System;
using System.Collections;
using System.Collections.Generic;

namespace monopoly {
	public class Groupe {
		private String couleur;
		private ArrayList proprietes = new ArrayList();

		public Groupe(string uneCouleur){
			this.couleur = uneCouleur;
		}
			
		// get&set
		public String getCouleur(){
			return this.couleur;
		}
		public ArrayList getPropriete()	{
			return proprietes;
		}
	}
}
