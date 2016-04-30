using System;
namespace monopoly {
	public class Compagnie : Propriete  {

		public Compagnie(Groupe unGroupe,string unNom, int unPrix)
			: base ("compagnie",unPrix,unNom){
			groupe = unGroupe;
		}

		public override string ToString ()
		{
			return this.genre;
		}
	}
}