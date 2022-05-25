using barcode_reader.Engine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace barcode_reader
{
    public partial class ProductPage : Form
    {
        Dao daoEngine = new Dao();
        PojoOggetto oggettoform2;
        int secret = 0;
        public ProductPage(PojoOggetto oggetto)
        {
            
            InitializeComponent();
            this.oggettoform2 = oggetto;
            textBox1b.Text = this.oggettoform2.isbn;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           this.Hide();
            MainPage form1 = new MainPage();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Equals("")) {MessageBox.Show("Inserire titolo"); return;}
            if (maskedTextBox2.Text.Equals("")) {MessageBox.Show("Inserire Categoria"); return; }
            if (numericUpDown1.Value < 0) {MessageBox.Show("Inserire una quantità"); return; }
            oggettoform2.definizione = maskedTextBox1.Text;
            oggettoform2.categoria = maskedTextBox2.Text.Split(',', ' ', ';');
            oggettoform2.descrizione = textBox1.Text;
            oggettoform2.data = dateTimePicker1.Value;
            oggettoform2.quantità = (int)numericUpDown1.Value;
            oggettoform2.iseritoDa = StaticLocalStorage.userpojo.username;
            MessageBox.Show(oggettoform2.ToString());
            //Utility.registerItem(oggettoform2);
            button1_Click(sender, e);


        }

        private void button3_Click(object sender, EventArgs e)
        {

            MessageBox.Show(daoEngine.getFirstDocumentFrom("ITEMS", "ITEMS"));
            /*
            MongoClient dbclient = new MongoClient("mongodb+srv://admin:admin@cluster0.j5w1g.mongodb.net/?retryWrites=true&w=majority");
            var dat = dbclient.GetDatabase("ITEMS");
            var collection = dat.GetCollection<BsonDocument>("ITEMS");
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            MessageBox.Show(Utility.toJson(firstDocument));*/
        }
    }

}
