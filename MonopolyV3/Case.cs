using System;
namespace monopoly {
	public class Case {
		protected String type;

        public Case(String unType)
        {
            type = unType;
        }

		// Méthode qui s'exécute quand le joueur j s'arrête sur la case
		public virtual void callback(Joueur j){
			Console.WriteLine (j.getNom () + " est sur une case");
		}

		public override string ToString(){
			return type;
		}

		public string getType() { return type; }

		public bool Equals (Case c)
		{
			if (this.type == c.type)
				return true;
			return false;
		}
	}

}