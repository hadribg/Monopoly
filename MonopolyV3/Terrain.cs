using System;
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
		}

		public override string ToString ()
		{
			return genre +" "+groupe.getCouleur()+" "+nom;
		}

	}

}
