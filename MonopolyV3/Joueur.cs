using System;
using System.Collections;
using System.Collections.Generic;

namespace monopoly {
	public abstract class Joueur {
		private String nom;
		private int argent;
        private Case caseCourante;
		private int dernierScore;	// Représente la somme aux dés qu'a fait le joueur la dernière fois
		private int tourEmprisonne;
		private Plateau plateau;

		public Joueur(string unNom){
			nom = unNom;
			argent = 1500;
			dernierScore = 0;
			caseCourante = null;
			tourEmprisonne = 0;
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

			// Test construction
			if (p.getGenre() == "terrain") {
				Terrain t = (Terrain)p;
				if (this.PossedeTousLesTerrains (t)) {
					Console.WriteLine ("Vous possédez tous les terrains de ce groupe, voulez-vous acheter une maison ?");
					if (Console.ReadLine () == "o") {
						t.construireMaison (this);
						Console.WriteLine ("Maison construite !");
					}				
				}
			}
		}

		// Hypothequer une maison à la banque pour gagner un peu d'argent.
		// Impossible de percevoir un loyer d'un propriété hypothequée.
		public void hypothequer (Propriete p) {
			// Cas d'invalidité
			if (p.getProprietaire() != this)	throw new Exception("Cette propriété n'est pas à vous, vous ne pouvez pas l'hypothequer");

			p.setHypotheque(true);
			this.crediter (p.getValeurHypothecaire());
		}

		// Permettre à un joueur de vendre sa propriété à un autre joueur à un prix p
		public void vendre(Propriete p, Joueur j, int somme){
			// Cas d'invalidité
			if (p.getProprietaire() != this)	throw new Exception("Cette propriété n'est pas à vous, vous ne pouvez pas la vendre");

			p.setProprietaire (j);
			j.transaction (this, somme);
		}

		// Méthode qui renvoit le nombre de propriétés du même groupe possédées par le joueur support
		public int nbProprietePossedees(Groupe g){
			int possede = 0;
			ArrayList zone = g.getPropriete ();
			foreach (Propriete t in zone) {
				if (t.getProprietaire () != null) {
					if (t.getProprietaire ().Equals (this) || (t.getProprietaire ().Equals (this) && t.getHypothequee()))
						possede++;
				}
			}
			return possede;
		}

		public int nbProprietePossedees(Propriete p){
			return nbProprietePossedees (p.getGroupe ());
		}

		// Renvoie vrai si le joueur support possède tous les terrains du groupe g
		public bool PossedeTousLesTerrains(Groupe g){
			ArrayList zone = g.getPropriete ();
			foreach (Propriete t in zone) {
				if (t.getProprietaire() == null)
					return false;
				
				if (!t.getProprietaire ().Equals (this) || (t.getProprietaire ().Equals (this) && t.getHypothequee()))
					return false;

			}
			return true;
		}

		public bool PossedeTousLesTerrains(Terrain t){
			return PossedeTousLesTerrains (t.getGroupe ());
		}

		// Redéfinition Equals
		public bool Equals(Joueur j){
			if (this.nom == j.nom)
				return true;
			return false;
		}
			
        // get&set
		public int getScore()						{return this.dernierScore;}
        public Case getCaseCourante(){return this.caseCourante;}        
        public int getArgent()						{return this.argent;}
		public string getNom()						{return this.nom;}
		public int estEmprisonne()					{return this.tourEmprisonne;}
		public Plateau getPlateau()					{return this.plateau;}

		public void setScore(int unScore)			{this.dernierScore = unScore;}
		public void setCaseCourante(Case uneCase)	{this.caseCourante = uneCase;}
		public void setEmprisonne(int b)			{this.tourEmprisonne = b; }
		public void setPlateau(Plateau p)			{this.plateau = p;}
		public void setArgent(int somme){
			this.argent = somme;
			if (argent < 0)
				plateau.joueurADecouvert (this, argent);
		}
	}
}
