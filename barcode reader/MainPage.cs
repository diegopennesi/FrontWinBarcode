using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace barcode_reader
{
    public partial class MainPage : Form
    {

        public MainPage()
        {
            InitializeComponent();
            button2.Enabled = StaticLocalStorage.isUserLoggedIn;
            button6.Enabled = StaticLocalStorage.isUserLoggedIn;
            button1.Enabled = false;   
            button3.Enabled= false;
            button4.Enabled = !StaticLocalStorage.isUserLoggedIn;
            Utility.initUtility();

        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPG|*.jpg" })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    var destRect = new Rectangle(12, 12, 500, 300);
                    //var destImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    var destImage= new Bitmap(500,300);
                    var image = Image.FromFile(ofd.FileName);
                    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                    using (var graphics = Graphics.FromImage(destImage))
                    {
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            
                            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                        }
                    }
                    pictureBox1.Image=destImage;
                    
                    BarcodeReader reader = new BarcodeReader();
                        var result = reader.Decode((Bitmap)image); 
                    if (result != null)
                    {
                        textBox1.Text = result.ToString();
                        button3.Enabled = true;
                        button1.Enabled = true;
                    }
                }
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            // ricerca google basica 101 TEST
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                string url = "https://www.google.com/search?q=" + textBox1.Text;
                System.Diagnostics.Process.Start(url);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PojoOggetto ogg = new PojoOggetto();
            ogg.isbn = textBox1.Text;
            ProductPage secondForm = new ProductPage(ogg);
            this.Hide();
            secondForm.Show();
        }
        //LOGIN
        private void button4_Click(object sender, EventArgs e)
        {
            UserPojo opg = new UserPojo();
            opg.username=usernamebox.Text;
            opg.Password=passwordbox.Text;
            opg.loginAttempt = Utility.getUnixTime();
            //MessageBox.Show(opg.ToString());
            opg = Utility.getUserLogin(opg);
            if (opg.username.Equals("")){ 
                MessageBox.Show("Username o Password errati"); 
            }
            else { 
                MessageBox.Show("Login Effettuata");
                button4.Hide();
                StaticLocalStorage.isUserLoggedIn = true;
                button2.Enabled = StaticLocalStorage.isUserLoggedIn;
                opg.lastlogin = Utility.getUnixTime();
                StaticLocalStorage.userpojo = opg;
                StaticLocalStorage.pojoOggettoList = Utility.getAllItemfromUser(StaticLocalStorage.userpojo);
                button6.Enabled = StaticLocalStorage.isUserLoggedIn;
                MessageBox.Show(opg.ToString());


            }
        }
        //REGISTER
        private void button5_Click(object sender, EventArgs e)
        {
            _ = Utility.register(new UserPojo(usernamebox.Text, passwordbox.Text,true,0)) ? MessageBox.Show("Registrazione effettuta") : MessageBox.Show("Errore, Username gia presente");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            foreach (PojoOggetto item in StaticLocalStorage.pojoOggettoList)
            {
  

                string x = JsonSerializer.Deserialize<String>(item.ToString());
                MessageBox.Show(x);
               
            }
            /*
             *         public InTabella(String ret)
        {
            String[] righe=ret.Split('\n');
            List<string[]> list = new List<string[]>();
            for (int i = 0; i < righe.Length; i++) {
                String[] exp = righe[i].Split('\t');
                list.Add(exp);
            }
             

            // Convert to DataTable.
         table   = this.ConvertListToDataTable(list);
           // dataGridView1.DataSource = table;
            
         }*/
           
        }
    }
}
