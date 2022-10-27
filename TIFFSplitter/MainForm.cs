using System.Drawing.Imaging;

namespace TIFFSplitter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(TbPath_DragEnter);
            this.DragDrop += new DragEventHandler(TbPath_DragDrop);
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

                ImageCodecInfo myImageCodecInfo;
                myImageCodecInfo = GetEncoderInfo("image/tiff");

                Encoder myEncoder = Encoder.Compression;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, (long)EncoderValue.CompressionCCITT4);
                myEncoderParameters.Param[0] = myEncoderParameter;

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
                                image.Save(memory, myImageCodecInfo, myEncoderParameters);
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
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        void TbPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void TbPath_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if(files.Count() == 1)
            {
                string extension = Path.GetExtension(files[0]).ToLower();
                if (extension == ".tif" || extension == ".tiff")
                {
                    tbPath.Text = files[0];
                }
                else
                {
                    MessageBox.Show("Soubor musí být typu TIF !", "ERROR", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Lze pøetáhnout pouze 1 soubor !", "ERROR", MessageBoxButtons.OK);
            }
        }
    }
}