using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monopoly
{
	class Program
	{
		static void Main(string[] args)
		{
			Physique j1 = new Physique ("Hadrien");
			IA i1  = new IA("un panda moqueur");
			ArrayList joueurs = new ArrayList();
			joueurs.Add (j1);
			joueurs.Add (i1);

			Plateau plateau = new Plateau(joueurs);
			LinkedList<Case> cases = plateau.getCases();
			foreach (Case c in cases){
				Console.WriteLine(c.ToString());
			}
		}
	}
}
