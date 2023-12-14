using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Etlap
{
	public class Program
	{
		[STAThread]
		
		public static void Main(string[] args)
		{
			etlapDataTable etlapDataTable = new etlapDataTable();
			Application application = new Application();
			application.Run(new MainWindow(etlapDataTable));
		}
	}
}
