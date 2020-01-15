using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace memory_game
{
    class Criar:PictureBox
    {

        int w = Screen.PrimaryScreen.Bounds.Width;

        int h = Screen.PrimaryScreen.Bounds.Height;

        int controle = -1;

        int nivel;

        List<string> nomes = new List<string>(0);
        List<int> nu = new List<int>(0);


        DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory()+ @"\Cartas");
        FileInfo fileInfo = new FileInfo(Directory.GetCurrentDirectory() + @"\Cartas");


        public PictureBox criar(PictureBox pictureBox1, int tag, int niv)
        {
            int nivel = niv;

            if(controle == -1)
            {
                controle+=1;
                Aleatorio();
            }

            int largura = Convert.ToInt32((w * 0.07)*0.85);
            int altura = Convert.ToInt32((h * 0.187)*0.85);

            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            if (nivel == 1)
            {
                pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Cartas\" + nomes[nu[Convert.ToInt32(controle / 2)]] + ".png");
                //MessageBox.Show("2 tamanho nomes " + nomes.Count + "tamanho aleatorios " +nu.Count+"controle "+ controle);
            }
            else if (nivel == 2)
            {

                if (tag % 2 != 0)
                {
                    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Cards\Som.png");
                    //MessageBox.Show("2 tamanho nomes " + nomes.Count + "tamanho aleatorios " + nu.Count + "controle " + controle);
                }
                else if(tag % 2 == 0)
                {

                    //MessageBox.Show("tamanho nomes " + nomes.Count + "tamanho aleatorios " + nu.Count + "controle " + controle);
                    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Cartas\" + nomes[nu[Convert.ToInt32(controle / 2)]] + ".png");
                }
            }
            //MessageBox.Show("" + controle);
            pictureBox1.Name = nomes[nu[Convert.ToInt32(controle/2)]];
            pictureBox1.Size = new System.Drawing.Size(largura,altura);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.BackColor = Color.Transparent;
            //pictureBox1.TabIndex = 0;
            pictureBox1.Tag = tag;

            controle++;
            return pictureBox1;            
        }


        private void BuscaArquivos(DirectoryInfo dir)
        {
            // lista arquivos do diretorio corrente
            foreach (FileInfo file in dir.GetFiles())
            {
                // aqui no caso estou guardando o nome completo do arquivo em em controle ListBox
                // voce faz como quiser
                //caminho.Add(file.FullName);
                //pega todo o caminho
                string nome = file.Name;
                nome = nome.Remove(nome.Length - 4);
                nomes.Add(nome);
                //pega apenas o nome do arquivo
            }

            // busca arquivos do proximo sub-diretorio
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                BuscaArquivos(subDir);
            }

        }

        private void Aleatorio()
        {
            BuscaArquivos(dirInfo);


            Random Sorte = new Random();
            for (int z = 0; z < nomes.Count;)
            {
                bool controle = false;
                int num = Sorte.Next(0, nomes.Count);
                for (int i = 0; i < nu.Count; i++)
                {
                    if (num == nu[i])
                    {
                        controle = true;
                        break;
                    }
                }
                if (controle != true)
                {
                    nu.Add(num);
                    z++;
                }
            }

           /* for (int i = 0; i < nu.Count; i++)
            {
                Console.WriteLine("" + nu[i]);
            }
            Console.ReadLine();
            */
        }

        public void limpa()
        {
            nomes.Clear();
            nu.Clear();
        }
    }
}
