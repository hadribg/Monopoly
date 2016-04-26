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
		}

		// Echange d'argent entre 2 joueurs
		public void transaction(Joueur debite, Joueur credite, int somme){
			// Cas d'invalidité
			if (somme < 0)	throw new Exception("Somme invalide");

			debite.debiter (somme);
			credite.crediter (somme);
		}

		// test observer
		public void notifyObserver(int argent){
			ControlerMonopoly.joueurADecouvert (this, argent);
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
