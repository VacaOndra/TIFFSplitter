using System.Drawing.Imaging;

namespace TIFFSplitter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnSplit_Click(object sender, EventArgs e)
        {
            string path = tbPath.Text;
            if(File.Exists(path))
            {
                string saveTo = Path.GetDirectoryName(path);

                if (!saveTo.EndsWith(@"\"))
                    saveTo += @"\";

                FileStream fs = new FileStream(path, FileMode.Open);
                Image image = Image.FromStream(fs);

                int pagesCount = image.GetFrameCount(FrameDimension.Page);
                if (pagesCount > 0)
                {
                    for (int i = 0; i < pagesCount; i++)
                    {
                        string outputFileName = $"{saveTo}{Path.GetFileNameWithoutExtension(path)}_L{i + 1}{Path.GetExtension(path)}";
                        image.SelectActiveFrame(FrameDimension.Page, i);
                        using (MemoryStream memory = new MemoryStream())
                        {
                            using (FileStream fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                            {
                                image.Save(memory, ImageFormat.Tiff);
                                byte[] bytes = memory.ToArray();
                                fileStream.Write(bytes, 0, bytes.Length);
                            }
                        }
                    }

                    MessageBox.Show("Rozdìlení dokonèeno !", "INFO", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Soubor obsahuje pouze 1 stránku !", "INFO", MessageBoxButtons.OK);
                }
                fs.Close();
            }
            else
            {
                MessageBox.Show("Soubor neexistuje !", "ERROR", MessageBoxButtons.OK);
            }
            
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Obrázek TIF | *.tif;*.tiff";
            if (od.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = od.FileName;
            }
        }
    }
}