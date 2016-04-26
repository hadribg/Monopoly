using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace monopoly
{
	public class DemoMonopoly
	{
		public DemoMonopoly ()
		{
		}

		static void Main(string[] args)
		{

			// Création du controler
			ControlerMonopoly controler = new ControlerMonopoly();

			// Instanciation d'un thread
			Thread filPrincipal = new Thread(
				new ThreadStart(controler.run));

			// Démarrer le thread.
			filPrincipal.Start();

		}
		// boloss
	}
}

