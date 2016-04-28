using System;
using System.Collections;
using System.Collections.Generic;

namespace monopoly {
	public abstract class Joueur {
		private String nom;
		private int argent;
        private Case caseCourante;

		public Joueur(string unNom){
			nom = unNom;
			argent = 1500;
		} 

		// Vrai si le joueur peut payer la somme, faux sinon
		public bool peutPayer(int somme){
			if (this.getArgent() >= somme)
				return true;
			return false;
		}

		// Permet à un joueur de verser somme à la banque, somme > 0
		public void debiter(int somme) {
			// Cas d'invalidité
			if (somme < 0)	throw new Exception("Somme invalide");

			this.setArgent (this.getArgent () - somme);
		}

		// Permet à la banque de verser somme à un joueur, somme > 0
		public void crediter(int somme) {
			// Cas d'invalidité
			if (somme < 0)	throw new Exception("Somme invalide");
			this.setArgent(this.getArgent()+somme);
			Console.WriteLine ("Passage à la case départ : +200€");
		}

		// Echange d'argent entre 2 joueurs
		public void transaction(Joueur credite, int somme){
			// Cas d'invalidité
			if (somme < 0)	throw new Exception("Somme invalide");

			this.debiter (somme);
			credite.crediter (somme);
		}

		// Permettre à un joueur d'acheter une propriété
		public void acheter(Propriete p){
			this.debiter (p.getPrix ());
			p.setProprietaire (this); 
		}

		// Hypothequer une maison à la banque pour gagner un peu d'argent.
		// Impossible de percevoir un loyer d'un propriété hypothequée.
		public void hypothequer (Propriete p) {
			// Cas d'invalidité
			if (p.getProprietaire() != this)	throw new Exception("Cette propriété n'est pas à vous, vous ne pouvez pas l'hypothequer");

			this.crediter (p.getValeurHypothecaire());
			p.setHypotheque(true);
		}

		// Permettre à un joueur de vendre sa propriété à un autre joueur à un prix p
		public void vendre(Propriete p, Joueur j, int somme){
			// Cas d'invalidité
			if (p.getProprietaire() != this)	throw new Exception("Cette propriété n'est pas à vous, vous ne pouvez pas la vendre");

			p.setProprietaire (j);
			j.transaction (this, somme);
		}

		public bool PossedeTousLesTerrains(Groupe g){
			bool possede = true;
			ArrayList zone = g.getTerrains ();
			foreach (Terrain t in zone) {
				if (!t.getProprietaire ().Equals (this))
					possede = false;
			}
			return possede;
		}

		public bool PossedeTousLesTerrains(Terrain t){
			return PossedeTousLesTerrains (t.getGroupe ());
		}

		// test observer
		public void notifyObserver(int argent){
			ControlerMonopoly.joueurADecouvert (this, argent);
		}

		// Redéfinition Equals
		public bool Equals(Joueur j){
			if (this.nom == j.nom)
				return true;
			return false;
		}
			
        // get&set
        public void setCaseCourante(Case uneCase)
        {
            this.caseCourante = uneCase;
        }
        public Case getCaseCourante()
        {
            return this.caseCourante;
        }
        public void setArgent(int somme)
        {
            this.argent = somme;
			if (argent < 0)
				notifyObserver (argent);
        }
        public int getArgent()
        {
            return this.argent;
        }
		public string getNom(){
			return this.nom;
		}
	}

}
