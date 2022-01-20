using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;    

namespace EditorTXT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        #region Menu Arquivo
        private void mArquivoNovo_Click(object sender, EventArgs e)
        {
            txtConteudo.Clear();
            
            mArquivoSalvar.Enabled = true;
            Text = Application.ProductName;
        }
        private void mArquivoNovaJanela_Click(object sender, EventArgs e)
        {
            //Form1 f = new Form1();
            //f.Show();

            Thread t = new Thread(() => Application.Run(new Form1()));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

        }
        private void mArquivoAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Abrir...";
            dialog.Filter = "rich text file | *.rtf|texto|*.txt|todos|*.*";

            DialogResult result = dialog.ShowDialog();
            
            if(result != DialogResult.Cancel && result != DialogResult.Abort)
            {
                if (File.Exists(dialog.FileName))
                {
                    FileInfo file = new FileInfo(dialog.FileName);
                    Text = Application.ProductName + " - " + file.Name;

                    Gerenciador.FolderPath = file.DirectoryName + "\\";
                    Gerenciador.FileName = file.Name.Remove(file.Name.LastIndexOf("."));
                    Gerenciador.FileExt = file.Extension;

                    StreamReader stream = null;
                    try
                    {
                        stream = new StreamReader(file.FullName, true);

                        txtConteudo.Text += stream.ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Formato de arquivo não suportado.\n{ex.Message}");
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }

        private void mArquivoSalvar_Click(object sender, EventArgs e)
        {
            if (File.Exists(Gerenciador.FilePath))
            {
                SalvarArquivo(Gerenciador.FilePath);
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Salvar...";
                dialog.Filter = "rich text file | *.rtf|texto|*.txt|todos|*.*";
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;

                var result = dialog.ShowDialog();
                if (result != DialogResult.Cancel && result != DialogResult.Abort)
                {
                    SalvarArquivo(dialog.FileName);
                }
            }
        }

        private void mArquivoSalvarComo_Click(object sender, EventArgs e)
        {
            if (File.Exists(Gerenciador.FilePath))
            {
                SalvarArquivo(Gerenciador.FilePath);
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Salvar Como...";
                dialog.Filter = "rich text file | *.rtf|texto|*.txt|todos|*.*";
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;

                var result = dialog.ShowDialog();
                if (result != DialogResult.Cancel && result != DialogResult.Abort)
                {
                    SalvarArquivo(dialog.FileName);
                }
            }
        }

        private void mArquivoSair_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja realmente sair?", "SAIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                Application.Exit();
            };
        }

        private void SalvarArquivo(string path)
        {
            //Objeto responsável por escrever o arquivo
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(path, false);
                writer.Write(txtConteudo.Text);

                FileInfo file = new FileInfo(path);
                Gerenciador.FolderPath = file.DirectoryName + "\\";
                Gerenciador.FileName = file.Name.Remove(file.Name.LastIndexOf("."));
                Gerenciador.FileExt = file.Extension;

                Text = Application.ProductName + " - " + file.Name;

                mArquivoSalvar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Salvar Arquivo: \n" + ex.Message);
                throw;
            }
            finally
            {
                writer.Close();
            }
        }

        private void txtConteudo_TextChanged(object sender, EventArgs e)
        {
            mArquivoSalvar.Enabled = true;
        }

        #endregion

        #region Menu editar
        private void mEditarDesfazer_Click(object sender, EventArgs e)
        {
            txtConteudo.Undo();
        }
        private void mEditarRefazer_Click(object sender, EventArgs e)
        {
            txtConteudo.Redo();
        }

        private void mEditarRecortar_Click(object sender, EventArgs e)
        {
            txtConteudo.Cut();
        }
        private void mEditarCopiar_Click(object sender, EventArgs e)
        {
            txtConteudo.Copy();
        }

        private void mEditarColar_Click(object sender, EventArgs e)
        {
            txtConteudo.Paste();
        }

        private void mEditarExcluir_Click(object sender, EventArgs e)
        {
            txtConteudo.Text = txtConteudo.Text.Remove(txtConteudo.SelectionStart,txtConteudo.SelectedText.Length);
        }

        private void mEditarDH_Click(object sender, EventArgs e)
        {
            int index = txtConteudo.SelectionStart;
            string DataHora = DateTime.Now.ToString();

            if (index == txtConteudo.Text.Length)
            {
                txtConteudo.Text += DataHora;
                return;
            }
            string temp = "";
            for (int i = 0; i < txtConteudo.Text.Length; i++)
            {
                if (i==txtConteudo.SelectionStart)
                {
                    temp += DataHora;
                    temp += txtConteudo.Text[i];
                }
                else
                {
                    temp += txtConteudo.Text[i];
                }


            }

            txtConteudo.Text = temp;
            txtConteudo.SelectionStart = index + DataHora.Length;
        }



        #endregion

        #region Menu Formatar
        private void mFormatarQuebraLinha_Click(object sender, EventArgs e) => txtConteudo.WordWrap = mFormatarQuebraLinha.Checked;

        private void mFormatarFonte_Click(object sender, EventArgs e)
        {
            FontDialog Fonte = new FontDialog();
            Fonte.ShowColor = true;
            Fonte.ShowEffects = true;

            Fonte.Font = txtConteudo.Font;
            Fonte.Color = txtConteudo.ForeColor;

            DialogResult result = Fonte.ShowDialog();

            if(result == DialogResult.OK)
            {
                txtConteudo.Font = Fonte.Font;
                txtConteudo.ForeColor = Fonte.Color;
            }
        }

        #endregion

        #region Menu Exibir

        #region Submenu Zoom

        private void mExibirZoomAmpliar_Click(object sender, EventArgs e)
        {
            txtConteudo.ZoomFactor += 0.1f;
            AtualizarZoomLabel(txtConteudo.ZoomFactor);
        }

        private void mExibirZoomReduzir_Click(object sender, EventArgs e)
        {
            txtConteudo.ZoomFactor -= 0.1f;
            AtualizarZoomLabel(txtConteudo.ZoomFactor);
        }

        private void mExibirZoomRestaurar_Click(object sender, EventArgs e)
        {
            txtConteudo.ZoomFactor = 1;
            AtualizarZoomLabel(txtConteudo.ZoomFactor);
        }

        private void AtualizarZoomLabel(float Zoom)
        {
            statusBarLabel.Text = $"{(Zoom * 100)}%";
        }

        #endregion

        private void mExibirBarraStatus_Click(object sender, EventArgs e)
        {
            statusBar.Visible = mExibirBarraStatus.Checked;
        }

        #endregion

        #region Menu Ajuda
        private void mAjudaExibir_Click(object sender, EventArgs e)
        {
            FormAjuda form = new FormAjuda();
            form.Show();
        }

        private void mAjudaSobre_Click(object sender, EventArgs e)
        {
            FormSobre form = new FormSobre();
            form.Show();
        }

        #endregion

    }
}
