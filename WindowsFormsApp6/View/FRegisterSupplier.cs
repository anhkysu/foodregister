using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6.View
{
    public partial class FRegisterSupplier : Form
    {
        private String SupplierName;
        private String SupplierAddress;
        private Int32 SupplierContact;
        private Int32 Count = 0;
        private String str2 = "server=NKTRINH\\AKSSQL;database=OvertimeDinner;UID=sa;password=trinhkhanh";
        public FRegisterSupplier()
        {
            InitializeComponent();
        }

        private void FRegisterSupplier_Load(object sender, EventArgs e)
        {
            
        }

        public static SqlDataReader ExecuteReader(String connectionString, String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);
                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.  
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
        }

        private void addNewSupplier(String name, String address, Int32 contact)
        {
            String queryString = "INSERT INTO Suppliers (SupplierName, SupplierAddress, SupplierContact) VALUES (@Name, @Address, @Contact)";
            SqlParameter[] paramInput =
            {
                new SqlParameter("@Name", SqlDbType.VarChar, 255) {Value = name},
                new SqlParameter("@Address", SqlDbType.VarChar, 255) {Value = address},
                new SqlParameter("@Contact", SqlDbType.Int) {Value = contact}
            };
            SqlDataReader myReader = ExecuteReader(str2, queryString, CommandType.Text, paramInput);
            if (myReader.RecordsAffected > 0)
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        private void addFoodToSupplier(Int32 supplierid, String name)
        {
            String queryString = "INSERT INTO Food (FoodName, FoodSupplier) VALUES (@Name, @SupplierId)";
            SqlParameter[] paramInput =
            {
                new SqlParameter("@Name", SqlDbType.VarChar, 255) {Value = name},
                new SqlParameter("@SupplierId", SqlDbType.Int) {Value = supplierid},
            };
            SqlDataReader myReader = ExecuteReader(str2, queryString, CommandType.Text, paramInput);
            if (myReader.RecordsAffected > 0)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }

        private void AddToDataGridView (String foodName)
        {
            Count = dataGridView1.Rows.Count;
            String[] row = {Count.ToString(), foodName };
            dataGridView1.Rows.Add(row);
        }

        private void TbxAddFood_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbxSupplierName_TextChanged(object sender, EventArgs e)
        {
            if (tbxSupplierName.Text == "") return;
            SupplierName = tbxSupplierName.Text;
        }

        private void TbxSupplierAddress_TextChanged(object sender, EventArgs e)
        {
            if (tbxSupplierAddress.Text == "") return;
            SupplierAddress = tbxSupplierAddress.Text;
        }

        private void BtnAddSupplierFood_Click(object sender, EventArgs e)
        {
            if (tbxAddFood.Text == "") return;
            AddToDataGridView(tbxAddFood.Text);
        }

        private void GetLastIdOfFoodSupplier()
        {
            SqlDataReader reader = ExecuteReader(str2, "SELECT COUNT(*) FROM Suppliers", CommandType.Text);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

        }

        private void BtnSubmitSupplier_Click(object sender, EventArgs e)
        {
            if (tbxSupplierName.Text != "" && tbxSupplierAddress.Text != "" && tbxSupplierContact.Text != "")
            {
                Int32 SupplierContact = Int32.Parse(tbxSupplierContact.Text);
                addNewSupplier(tbxSupplierName.Text, tbxSupplierAddress.Text, SupplierContact);
            }

            addFoodToSupplier(1, "Busn Bo");
        }
    }
}
