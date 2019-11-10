using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        String str1 = "server=NKTRINH\\AKSSQL;database=DWQueue;UID=sa;password=trinhkhanh";
        String str2 = "server=NKTRINH\\AKSSQL;database=OvertimeDinner;UID=sa;password=trinhkhanh";
        public Form1()
        {
            InitializeComponent();
            
        }

       

        private void removeSupplier(String name, String address, Int32 contact, Array FoodList)
        {

        }

        private void editSupplier(String name, String address, Int32 contact, Array FoodList)
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


        private void testConnection()
        {
            try

            {
                SqlDataReader reader = ExecuteReader(str2, "SELECT * FROM Food", CommandType.Text);
                

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

            catch (Exception es)

            {
                MessageBox.Show(es.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ToolStripButton11_Click(object sender, EventArgs e)
        {
            var myDialog = new View.FRegisterSupplier();
            myDialog.ShowDialog();
            //addNewSupplier("asd", "asdasd", 123123123);
        }

        private void ToolStripButton12_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            testConnection();
        }
    }
}
