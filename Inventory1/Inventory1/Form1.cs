using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory1
{
    public partial class frmAddProduct : Form
    {

        class NumberFormatException : Exception
        {
            public NumberFormatException(string quantity) : base(quantity) { }
        }
        class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) { }
        }
        class CurrenceyFormatException : Exception
        {
            public CurrenceyFormatException(string price) : base(price) { }
        }

        private int _Quantity;
        private double _SellPrice;
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        BindingSource showProductList;

        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (StringFormatException sfe)
            {
                MessageBox.Show($"Message: " + sfe.Message);
            }
            catch (NumberFormatException nfe)
            {
                MessageBox.Show($"Message: " + nfe.Message);
            }
            catch (CurrenceyFormatException cfe)
            {
                MessageBox.Show($"Message: " + cfe.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtProductName.Text = "";
            cbCategory.Text = "";
            dtPickerMfgDate.Text = "";
            dtPickerExpDate.Text = "";
            richTxtDescription.Text = "";
            txtQuantity.Text = "";
            txtSellPrice.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]{
                 "Beverages", "Bread / Bakery", "Canned / Jurred Goods", "Dairy", "Frozen Goods",
                 "Meat", "Personal Care", "Other"
                };
            foreach (string variableName in ListOfProductCategory)
            {
                cbCategory.Items.Add(variableName);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                //Exception here
                throw new StringFormatException("Invalid Input for Product name.");
            }
            else
            {
                return name;
            }    
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                //Exception here
                throw new NumberFormatException("Invalid Input for Quantity.");
            }
            else
            {
                return Convert.ToInt32(qty);
            }  
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                //Exception here
                throw new CurrenceyFormatException("Invalid Input for Selling price.");
            }
            else
            {
                return Convert.ToDouble(price);
            } 
        }
    }
}

