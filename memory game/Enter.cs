using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public partial class Enter : Form
    {
        int altura = Screen.PrimaryScreen.Bounds.Height;
        int largura = Screen.PrimaryScreen.Bounds.Width;
        string path = Directory.GetCurrentDirectory();
        int controle = 0;
        string testando = "funcionou";

        public Enter()
        {
            InitializeComponent();
        }

        private void Enter_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;

            this.BackgroundImage = Image.FromFile(path + @"\formdesign\BACKGROUND_N1.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.MaximizeBox = false;
            nivelbutton();
        }


        private void nivelbutton()
        {
            for(int i = 0; i < 4;i++)
            {
                PictureBox nivel = new PictureBox();

                nivel.Size = new System.Drawing.Size(Convert.ToInt32(largura * 0.22276), Convert.ToInt32(altura * 0.18074));
                nivel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                nivel.BackColor = Color.Transparent;

                if(controle == 0)
                {
                    nivel.Image = Image.FromFile(path + @"\formdesign\BOTÕES DE NIVEL1.png");
                    nivel.Location = new System.Drawing.Point(Convert.ToInt32(largura - (largura * 0.07384615 + ((largura * 0.22276) * 0.8))), Convert.ToInt32((altura * 0.4719444)/2));
                    nivel.Tag = 1;
                    nivel.Click += new System.EventHandler(this.ImagensClick_Click);

                }
                else if(controle == 1)
                {
                    nivel.Image = Image.FromFile(path + @"\formdesign\BOTÕES DE NIVEL2.png");
                    nivel.Location = new System.Drawing.Point(Convert.ToInt32(largura -(largura* 0.07384615 + ((largura * 0.22276)*0.8))), Convert.ToInt32(altura * 0.4519444));
                    nivel.Tag = 2;
                    nivel.Click += new System.EventHandler(this.ImagensClick_Click);
                }
                else if (controle == 2)
                {
                    nivel.Image = Image.FromFile(path + @"\formdesign\DIZERES TELA 1_Prancheta 1(2).png");
                    nivel.Location = new System.Drawing.Point(Convert.ToInt32((largura/2) -((largura * 0.6615384) / 2)), Convert.ToInt32((altura*0.90)- (altura * 0.095083)));
                    nivel.Size = new System.Drawing.Size(Convert.ToInt32(largura * 0.6615384), Convert.ToInt32(altura * 0.095083));
                }

                else if (controle == 3)
                {
                    nivel.Image = Image.FromFile(path + @"\formdesign\roletanivel.png");
                    nivel.Location = new System.Drawing.Point(Convert.ToInt32(largura * 0.045083), Convert.ToInt32(altura * 0.042592592));
                    nivel.Size = new System.Drawing.Size(Convert.ToInt32(largura * 0.658333), Convert.ToInt32(altura * 0.7046296));
                }
                this.Controls.Add(nivel);
                controle += 1;

            }
        }

        private void ImagensClick_Click(object sender, EventArgs e)
        {

            PictureBox pic = (PictureBox)sender;
            int tagIndex = int.Parse(string.Format("{0}", pic.Tag));

            Form1 meu = new Form1(tagIndex);
            meu.ShowDialog();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }
    }
}
