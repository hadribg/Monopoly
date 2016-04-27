using System;
namespace monopoly {
	public class Case {
		protected String type;

        public Case(String unType)
        {
            type = unType;
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