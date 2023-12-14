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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		private etlapDataTable etlapDataTable;
		private etlapAdatok etlapUpdate;
		public Window1(etlapDataTable etlapDataTable)
		{
			InitializeComponent();
			this.etlapDataTable = etlapDataTable;
		}

		public Window1(etlapDataTable etlapDataTable, etlapAdatok etlapUpdate)
		{
			InitializeComponent();
			this.etlapDataTable = etlapDataTable;
			this.etlapUpdate = etlapUpdate;
			tbName.Text = etlapUpdate.Name;
			tbDescription.Text = etlapUpdate.description;
			tbPrice.Text = etlapUpdate.price.ToString();
		}


		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				etlapAdatok etlapAdatok = CreateEmployeeFromInputData();
				if (etlapDataTable.Create(etlapAdatok))
				{
					MessageBox.Show("Sikeres hozzáadás");
					tbName.Text = "";
					tbDescription.Text = "";
					tbPrice.Text = "";

				}
				else
				{
					MessageBox.Show("Hiba történt a hozzáadás során próbálja meg újra.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private etlapAdatok CreateEmployeeFromInputData()
		{
			string name = tbName.Text.Trim();
			string description = tbDescription.Text.Trim();
			string comboxBoxSelected = comboxBox.Text.Trim();
			string pricetext = tbPrice.Text.Trim();

			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Név megadása kötelező");
			}
			if (string.IsNullOrEmpty(description))
			{
				throw new Exception("A leírás megadása kiválasztása kötelező");
			}

			if (string.IsNullOrEmpty(description))
			{
				throw new Exception("Kor megadása kötelező");
			}

			if (string.IsNullOrEmpty(comboxBoxSelected))
			{
				throw new Exception("A kategória megadása kötelező");
			}

			if (!int.TryParse(pricetext, out int price))
			{
				throw new Exception("A ár csak szám lehet");
			}

			if (string.IsNullOrEmpty(pricetext))
			{
				throw new Exception("Adja meg az árat mert az kötelező kitölteni kötelező!");
			}

			etlapAdatok etlap = new etlapAdatok();
			etlap.Name = name;
			etlap.description = description;
			etlap.price = price;
			etlap.category = comboxBoxSelected;
			return etlap;
		}
	}
}
