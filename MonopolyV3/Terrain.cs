using System;
using System.Collections;
using System.Collections.Generic;

namespace monopoly {
	public class Terrain : Propriete  {
		private int loyerTerrain;
		private int loyer1maison;
		private int loyer2maisons;
		private int loyer3maisons;
		private int loyer4maisons;
		private int loyerHotel;
		private int prixMaison;
		private int prixHotel;
		private int nbMaison;

		public Terrain(Groupe unGroupe, int unPrix, string unNom, int l0, int l1, int l2, int l3, int l4, int lh, int prixM, int prixH)
			:base("terrain",unPrix,unNom){

			loyerTerrain = l0;
			loyer1maison = l1;
			loyer2maisons = l2;
			loyer3maisons = l3;
			loyer4maisons = l4;
			loyerHotel = lh;
			valeurHypothecaire = unPrix/2;
			prixMaison = prixM;
			prixHotel = prixH;
			groupe = unGroupe;
			nbMaison = 0;
		}

		public override string ToString ()
		{
			return genre +" "+groupe.getCouleur()+" "+nom;
		}

		// Contrôler que les maisons ont bien été construites uniformément
		// Renvoie vrai si il est possible de faire une nouvelle sur le terrrain support
		public bool peutConstruireMaison(){
			// Cas d'invalidité : il y a déjà les 4 maisons sur le terrain support
			if (this.getNbMaison()>4)	return false;

			ArrayList zone = this.groupe.getPropriete();
			bool uniforme = true;
			foreach (Terrain t in zone) {
				if (t.getNbMaison() != this.getNbMaison() && t.getNbMaison() != (this.getNbMaison()-1))
					uniforme = false;
			}
			return uniforme;
		}

		public void construireMaison(Joueur j){
			// Contrôler que le joueur possède bien tous les terrains du groupe
			if (j.PossedeTousLesTerrains(this)) {
				// Contrôler que le joueur a bien construit ses maisons uniformément
				if(this.peutConstruireMaison()){
					this.nbMaison++;
					j.debiter (this.prixMaison);
				} else
					throw new Exception("Vous ne pouvez pas construire de maison sur ce terrain : construisez des maisons sur les autres terrains avant");
			}
		}

		// Contrôler qu'il y a bien 4 maisons sur toute la zone
		// Renvoie vrai si il est possible de faire un hotel sur le terrrain support
		public bool peutConstruireHotel(){
			ArrayList zone = this.groupe.getPropriete();
			bool peutConstruire = true;
			foreach (Terrain t in zone) {
				if (t.getNbMaison() != 4)
					peutConstruire = false;
			}
			return peutConstruire;
		}

		public void construireHotel(Joueur j) {
			// Contrôler que le joueur a bien construit ses maisons
			if(this.peutConstruireHotel()){
				this.nbMaison++;
				j.debiter (this.prixHotel);
			} else
				throw new Exception("Vous ne pouvez pas construire d'hotel sur ce terrain : construisez des maisons sur les autres terrains avant");
		}

		// Renvoit vrai si le terrain support est destructible tout en préservant l'uniformité entre les terrains de la zone
		public bool peutVendre(){
			ArrayList zone = this.groupe.getPropriete();
			bool uniforme = true;
			foreach (Terrain t in zone) {
				if (t.getNbMaison() > this.getNbMaison())
					uniforme = false;
			}
			return uniforme;
		}

		// Vendre un bien à la banque
		// Maison/hotel/terain
		public void vendre(Joueur j){
			if (nbMaison > 0 && nbMaison < 6) {
				this.nbMaison--;
				if (nbMaison == 4) {
					j.crediter (this.prixHotel / 2);
				} else {
					j.crediter (this.prixMaison / 2);
				}
			} else {
				j.hypothequer (this);
			}
		}

		// get&set
		public int getNbMaison()		{return this.nbMaison;}
		public int getLoyerTerrain()	{return loyerTerrain;}
		public int getLoyer1maison()	{return loyer1maison;}
		public int getLoyer2maisons()	{return loyer2maisons;}
		public int getLoyer3maisons()	{return loyer3maisons;}
		public int getLoyer4maisons()	{return loyer4maisons;}
		public int getLoyerHotel()		{return loyerHotel;}
		public int getPrixMaison()		{return prixMaison;}
		public int getPrixHotel()		{return prixHotel;}

		public void setNbMaisons(int i)			{this.nbMaison = i;}
			
	}

}
