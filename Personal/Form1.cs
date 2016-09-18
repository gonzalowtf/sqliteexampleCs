using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Personal
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Conn c = new Conn();
                c.conn.Open();
                c.sqlcm = c.conn.CreateCommand();
                string nombre = this.textBox1.Text;
                string apellido = this.textBox2.Text;
                long telefono = Convert.ToInt64(this.textBox3.Text);
                long dni = Convert.ToInt64(this.textBox4.Text);
                c.sqlcm.CommandText = "INSERT INTO PERSONAL (nombre , apellido ,telefono , dni ) values ('" + nombre + "','" + apellido + "'," + telefono + "," + dni + ")";
                c.sqlcm.ExecuteNonQuery();
                c.conn.Close();
                MessageBox.Show("Alta realizada");
            }
           catch(SQLiteException ex)
            {
                MessageBox.Show("error de sql  " + ex.ToString());
            }
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conn c = new Conn();
            c.conn.Open();
            c.sqlcm = c.conn.CreateCommand();
            c.sqlcm.CommandText = "select * from personal";
            c.reader = c.sqlcm.ExecuteReader();
            while (c.reader.Read())
            {
                MessageBox.Show(c.reader.GetString(0) + "||" + c.reader.GetString(1) + "||" + c.reader.GetInt64(2).ToString() + "||" + c.reader.GetInt64(3).ToString());
            }
            c.conn.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Conn c = new Conn();
                c.conn.Open();
                c.sqlcm = c.conn.CreateCommand();
                c.sqlcm.CommandText = "CREATE TABLE IF NOT EXISTS PERSONAL (NOMBRE VARCHAR(100),APELLIDO VARCHAR(100) , TELEFONO BIGINT, DNI BIGINT )";
                c.sqlcm.ExecuteNonQuery();
                c.conn.Close();
                MessageBox.Show("Base de datos creada");
            }
            catch (SQLiteException exe)
            {
                MessageBox.Show("La base de datos ya existe  o Error al crear la base de datos" + exe.ToString());
            }
        }
    }
    public class Conn
    {
        public SQLiteConnection conn;
        public SQLiteCommand sqlcm;
        public SQLiteDataReader reader;
        public Conn()
        {
            try
            {
                conn = new SQLiteConnection("Data Source = C:\\Users\\gonzalowtf\\Google Drive\\Vs\\Programas\\Personal\\datapersonal.db");
                //MessageBox.Show("connected to SQLite database");
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("cannot connect to database : " + e);
            }
        }
    }
}
