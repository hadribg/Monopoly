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
		private int valeurHypothecaire;
		private int prixMaison;
		private int prixHotel;
		private int nbMaison;
		private Groupe groupe;

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

			ArrayList zone = this.groupe.getTerrains();
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

			ArrayList zone = this.groupe.getTerrains();
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

		public int getLoyer(){
			switch (nbMaison) {
			case 0:
				if (proprietaire.PossedeTousLesTerrains (this))
					return (loyerTerrain * 2);
				else
					return loyerTerrain;
				break;
			case 1:
				return loyer1maison;
				break;
			case 2:
				return loyer2maisons;
				break;
			case 3:
				return loyer3maisons;
				break;
			case 4:
				return loyer4maisons;
				break;
			case 5:
				return loyerHotel;
				break;
			}
		}

		// get&set
		public int getNbMaison()	{return this.nbMaison;}
		public Groupe getGroupe()	{return groupe;}

	}

}
