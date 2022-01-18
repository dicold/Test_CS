using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Test_CS
{
    public partial class Form1 : Form
    {
        // строка для подключения к БД
        private string connectionString = ConfigurationManager.ConnectionStrings["ShipmentDB"].ConnectionString;
        private SqlConnection SqlConnection = null;


        public Form1()
        {
            InitializeComponent();
        }

        // подключение к базе данных
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
            
            // заполнение таблицы данными с БД
            this.shipmentTableTableAdapter.Fill(this.dataSet1.ShipmentTable);

            // перечень всех столбиков ДБ в чек-лист
            for (int i = 0; i < dataSet1.Tables[0].Columns.Count; i++)
                checkedListBox1.Items.Add(dataSet1.Tables[0].Columns[i].ColumnName);
        }

        // группировка по выбранным в чек-листе значениям
        private void button1_Click(object sender, EventArgs e)
        {
            // проверка на выбор хотя бы одного значения
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("No items to work with!" +
                    "\nChoose some grouping criterias from the check-list and try again.",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // массив выбраных значений
            string[] str = checkedListBox1.CheckedItems.Cast<string>().ToArray();
            
            // строим запрос в БД на основе вышеупомянутого списка
            string query = "SELECT ";
            string a1 = "";
            for (int i = 0; i < str.Length; i++)
            {
                a1 += str[i];
                if (i != str.Length - 1)
                    a1 += ", ";
            }
            query += a1 + ", SUM(Count) AS Count, SUM(Sum) AS Sum FROM ShipmentTable GROUP BY " + a1;

            // отправляем команду в БД
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, SqlConnection);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            shipmentTableDataGridView.DataSource = dataSet.Tables[0];
        }

        // возвращение исходных данных в таблицу
        private void button2_Click(object sender, EventArgs e)
        {
            shipmentTableDataGridView.DataSource = dataSet1.Tables[0];
        }
    }
}
