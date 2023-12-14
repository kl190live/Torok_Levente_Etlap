using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		etlapDataTable etlapDataTable;
		public MainWindow(etlapDataTable etlapDataTable)
		{
			InitializeComponent();
			this.etlapDataTable = etlapDataTable;
			Read();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{
			Window1 form = new Window1(etlapDataTable);
			form.Closed += (_, _) =>
			{
				Read();
			};
			form.ShowDialog();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			etlapAdatok selected = menuTable.SelectedItem as etlapAdatok;
			if (selected == null)
			{
				MessageBox.Show("Törléshez előbb válasszon ki dolgozót!");
				return;
			}
			MessageBoxResult selectedButton =
			MessageBox.Show($"Törölni szeretné ezt az ételt: {selected.Name}?","Biztos?", MessageBoxButton.YesNo);
			if (selectedButton == MessageBoxResult.Yes)
			{
				if (etlapDataTable.Delete(selected.Id))
				{
					MessageBox.Show("Sikeres törlés!");
				}
				else
				{
					MessageBox.Show("Hiba történt a törlés során!");
				}
				Read();
			}
		}

		private void Read()
		{
			menuTable.ItemsSource = etlapDataTable.GetAll();
		}

	}
}
