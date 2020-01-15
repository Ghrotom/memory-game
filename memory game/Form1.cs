using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace memory_game
{
    public partial class Form1 : Form
    {
        int movimentos, cliques, cartasEncontradas, tagIndex;

        Image[] img = new Image[28];
        List<string> lista = new List<string>(0);
        int[] tags = new int[2];
        int[] rev = new int[2];

        string[] nome = new string[2];

        WindowsMediaPlayer audio = new WindowsMediaPlayer();
        List<string> caminho = new List<string>(0);


        Label lblMovimentos = new Label();
        Timer timer1 = new Timer();


        // E inicia a operacao passando o diretorio inicial:
        // manipular de diretorios
        DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Audios");

        // procurar arquivos


            int altura = Screen.PrimaryScreen.Bounds.Height;
            int largura = Screen.PrimaryScreen.Bounds.Width;


        string path = Directory.GetCurrentDirectory();

        Criar pictu = new Criar();
        int tag = 0;

        int nivel;

        List<PictureBox> listaimg = new List<PictureBox>(0);
        List<PictureBox> listaimg2 = new List<PictureBox>(0);

        public Form1(int niv)
        {
            InitializeComponent();
            nivel = niv;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            BuscaArquivos(dirInfo);

            fazer();



            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            
        }


        private void Inicio()
        {

                foreach (PictureBox item in listaimg)
                {
                    int tagIndex = int.Parse(String.Format("{0}", item.Tag));
                    img[tagIndex] = item.Image;
                    item.Image = Image.FromFile(path + @"\Cards\carta.png");
                    item.Enabled = true;
                }
            
            posicoes();
            nivel1();
        }
        private void posicoes()
        {
            int pictulargura = Convert.ToInt32(largura * 0.07);
            int pictualtura = Convert.ToInt32(altura * 0.187);

            //int x = Convert.ToInt32((largura * 0.356) - pictulargura / 2);
            int x = Convert.ToInt32((largura * 0.3563)*1.27);
            int y = Convert.ToInt32(altura * 0.1419);
            // int y = Convert.ToInt32((altura * 0.141) - pictualtura / 2);
            int espax = Convert.ToInt32((largura * 0.0109)*0.25);
            //int espay = Convert.ToInt32((altura * 0.0258)*0.25);
            int espay = espax;



            foreach (PictureBox item in listaimg)
            {
                Random rdn = new Random();

                int[] xP = {x,x+espax+pictulargura, x + ((espax + pictulargura)*2), x + ((espax + pictulargura) * 3),
                x + ((espax + pictulargura)*4), x + ((espax + pictulargura)*5), x + ((espax + pictulargura)*6)};
                int[] yP = {y, y+espay+pictualtura, y + ((espay + pictualtura)*2), y + ((espay + pictualtura) * 3)};

                Repete:
                var X = xP[rdn.Next(0, xP.Length)];
                var Y = yP[rdn.Next(0, yP.Length)];

                string verificacao = X.ToString() + Y.ToString();

                if (lista.Contains(verificacao))
                {
                    goto Repete;
                }
                else
                {
                    item.Location = new Point(X, Y);
                    lista.Add(verificacao);
                }
            }
        }

        private void ImagensClick_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 2000;
            bool parEncontrado = false;

            PictureBox pic = (PictureBox)sender;
            cliques++;
            tagIndex = int.Parse(string.Format("{0}", pic.Tag));

            
            pic.Image = img[tagIndex];
            pic.Refresh();
            if (cliques == 1)
            {
                audio.controls.pause();
                {

                    tags[0] = int.Parse(string.Format("{0}", pic.Tag));
                    rev[0] = int.Parse(string.Format("{0}", pic.Tag));

                    nome[0] = pic.Name;
                    pic.Enabled = false;

                    try
                    {
                        // audio.URL = caminho[tags[0]];
                        if (nivel == 1)
                        {
                            audio.URL = path + @"\Audios\" + nome[0] + ".mp3";
                            audio.controls.play();
                        }
                        else if(nivel == 2 && tags[0]%2!=0)
                                {
                                audio.URL = path + @"\Audios\" + nome[0] + ".mp3";
                                audio.controls.play();
                            rev[0] = tags[0] - 1;
                                }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex);
                    }
                }

            }
            else if (cliques == 2)
            {

                movimentos++;
                lblMovimentos.Text = "MOVIMENTOS: " + movimentos.ToString();
                tags[1] = int.Parse(string.Format("{0}", pic.Tag));
                rev[1] = int.Parse(string.Format("{0}", pic.Tag));

                nome[1] = pic.Name;

                pic.Enabled = false;

                try
                {
                    if (tags[0] != tags[1] && nivel == 1)
                    {
                        //audio.URL = caminho[tags[1]];
                        audio.URL = path + @"\Audios\" + nome[1] + ".mp3";
                        audio.controls.play();
                    }
                    else if(tags[0] != tags[1] && nivel == 2 && tags[1]%2!=0)
                    {
                        audio.URL = path + @"\Audios\" + nome[1] + ".mp3";
                        audio.controls.play();
                        rev[1] = tags[1] - 1;
                    }

                    timer1.Interval = 5000;
                    timer1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);

                }


                parEncontrado = ChecagemPares();
                Desvirar(parEncontrado);

            }
        }

        private bool ChecagemPares()
        {
            cliques = 0;
            if (nivel == 1)
            {
                if (tags[0] == tags[1])
                {
                    return true;
                }
                else
                {
                    timer1.Enabled = true;
                    return false;
                }
            }

            else
            {
                if (rev[0] == rev[1])
                {
                    return true;
                }
                else
                {
                    timer1.Enabled = true;
                    return false;
                }
            }

        }

        private void Desvirar(bool check)
        {
            Thread.Sleep(1000);
            foreach (PictureBox item in listaimg)
            {
                if (int.Parse(String.Format("{0}", item.Tag)) == tags[0] ||
                    int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        cartasEncontradas++;
                    }
                    else
                    {
                        item.Enabled = true;
                        item.Image = Image.FromFile(path + @"\Cards\carta.png");
                        item.Refresh();
                    }
                }

            }
            FinalJogo();
        }

        private void FinalJogo()
        {
            if (cartasEncontradas == listaimg.Count)
            {
                MessageBox.Show("Parabéns, você terminou o jogo com" + movimentos.ToString() + "movimentos");
                DialogResult msg = MessageBox.Show("Deseja continuar o jogo?", "Caixa de perguntas", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {

                    cliques = 0; movimentos = 0; cartasEncontradas = 0;
                    lista.Clear();
                    Inicio();
                }
                else if (msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigado por jogar!");
                    Application.Exit();
                }
            }
        }


        private void BuscaArquivos(DirectoryInfo dir)
        {
            // lista arquivos do diretorio corrente
            foreach (FileInfo file in dir.GetFiles())
            {
                // aqui no caso estou guardando o nome completo do arquivo em em controle ListBox
                // voce faz como quiser
                caminho.Add(file.FullName);
            }

            // busca arquivos do proximo sub-diretorio
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                BuscaArquivos(subDir);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
            audio.controls.pause();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            audio.controls.pause();
            timer1.Enabled = false;
        }




        private void fazer()
        {

            for (int i = 0; i < 28; i++)
            {
                int tags = 0;
                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.Click += new System.EventHandler(this.ImagensClick_Click);

                if (nivel == 1)
                {
                    tags = tag / 2;
                    
                }
                else if (nivel == 2)
                {
                    tags = tag;
                    if (tags % 2 == 0)
                    {
                        listaimg2.Add(pictureBox1);
                    }
                }
                listaimg.Add(pictureBox1);
                tag++;
                this.Controls.Add(pictu.criar(pictureBox1, tags, nivel));

            }
            Inicio();
        }


        private void nivel1()
        {

            for (int i = 0; i < 3; i++)
            {
                PictureBox roleta = new PictureBox();
                if(i == 0)
                {
                    roleta.Image = Image.FromFile(path + @"\formdesign\roletanivel.png");
                    roleta.Location = new System.Drawing.Point(Convert.ToInt32(largura* 0.045083), Convert.ToInt32(altura * 0.042592592));
                    roleta.Size = new System.Drawing.Size(Convert.ToInt32(largura * 0.388333), Convert.ToInt32(altura *0.3346296));
                }
                else if(i == 1)
                {
                    roleta.Image = Image.FromFile(path + @"\formdesign\BOTÕES DE NIVEL" + nivel + ".png");
                    roleta.Location = new System.Drawing.Point(Convert.ToInt32(largura * 0.065083), Convert.ToInt32(altura*0.3919444));
                    roleta.Size = new System.Drawing.Size(Convert.ToInt32(largura * 0.23276), Convert.ToInt32(altura * 0.15574));
                }

                else if (i == 2)
                {
                    roleta.Image = Image.FromFile(path + @"\formdesign\PranchetaNivel" + nivel + ".png");
                    roleta.Location = new System.Drawing.Point(Convert.ToInt32(largura * 0.065083), Convert.ToInt32(altura* 0.56398));
                    roleta.Size = new System.Drawing.Size(Convert.ToInt32(largura * 0.2588), Convert.ToInt32(altura * 0.3124999999));
                }

                roleta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                roleta.BackColor = Color.Transparent;
                this.Controls.Add(roleta);
            }


            this.BackgroundImage = Image.FromFile(path + @"\formdesign\BACKGROUND_N" + nivel + ".png");


            lblMovimentos.Text = "MOVIMENTOS: 0";
            lblMovimentos.BackColor = Color.Transparent;
            lblMovimentos.Size = new System.Drawing.Size(Convert.ToInt32((altura * 0.187)*0.33), Convert.ToInt32((altura * 0.187)*3));
            lblMovimentos.Font = new Font("Times New Roman", largura*0.02f);
            lblMovimentos.AutoSize = true;
            lblMovimentos.Font = new Font(lblMovimentos.Font, FontStyle.Bold);
            lblMovimentos.Location = new Point (Convert.ToInt32((largura * 0.3563) * 1.27), Convert.ToInt32((altura * 0.1419) * 0.35));
            this.Controls.Add(lblMovimentos);

             
        }

    }
}
