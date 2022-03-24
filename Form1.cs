using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SofDev_SAC_One_Task_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //initialise the number of columns
            grdvwData.ColumnCount = 7;
            //add columns to datagridview
            grdvwData.Columns[0].Name = @"Name";
            grdvwData.Columns[1].Name = "Category";
            grdvwData.Columns[2].Name = "Seller";
            grdvwData.Columns[3].Name = "Purchaser";
            grdvwData.Columns[4].Name = "Purchase price";
            grdvwData.Columns[5].Name = "Sale price";
            grdvwData.Columns[6].Name = "Profit";
        }

        public void loadDataGrid()
        {
            float totProfit = 0f;

            //initialise lines variable
            var lines = new List<string>();

            //create list of the file from a file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = openFileDialog.FileName;
                lines = File.ReadAllLines(filePath).ToList();
            }

            foreach(string line in lines)
            {
                //create list of lines split by comma
                List<string> splitData = line.Split(',').ToList();

                float profit = 0f;

                //parse the purchase price value
                float.TryParse(splitData[4], out float purchasePrice);
                //create a bool of the sale price value's parse success
                //to make an if statement simple and easy to read
                bool saleValRet = float.TryParse(splitData[5], out float salePrice);


                //calculate the profit using these variables,
                //if sale value is N/A, set the profit to a negative

                if (saleValRet == true)
                {

                    profit = salePrice - purchasePrice;

                }
                else
                {
                    profit = purchasePrice * -1;

                }


                //add profit to the total
                totProfit += profit;
                //add profit to the list in a currency form
                splitData.Add(profit.ToString("c"));
                //add list to datagridview
                grdvwData.Rows.Add(splitData.ToArray());


            }
            //ensure total profit is displayed correctly
            grdvwData.Rows.Add();
            int rowCount = grdvwData.Rows.Count;
            grdvwData.Rows[rowCount - 1].Cells[5].Value = "Total profit";
            //display the total profit in a currency format
            grdvwData.Rows[rowCount - 1].Cells[6].Value = totProfit.ToString("c");

        }


        private void btnCalc_Click(object sender, EventArgs e)
        {
            loadDataGrid();

        }
    }
}
