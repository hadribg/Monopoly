using System;
namespace monopoly {
	public abstract class Joueur {
		private String nom;
		private int argent;
        private Case caseCourante;

		public Joueur(string unNom){
			nom = unNom;
			argent = 1500;
		}

		public void payer(int somme) {
			this.setArgent(this.getArgent()-somme);
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
        }
        public int getArgent()
        {
            return this.argent;
        }
	}

}
