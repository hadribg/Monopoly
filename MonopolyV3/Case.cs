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
	}

}