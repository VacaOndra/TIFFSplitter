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
            CompressionCheckBox.SetItemChecked(3, true);
        }
        

        private void BtnSplit_Click(object sender, EventArgs e)
        {
            string path = tbPath.Text;
            if(File.Exists(path) && Path.GetDirectoryName(path) is string saveTo)
            {
                saveTo += !saveTo.EndsWith(@"\") ? @"\" : "";
                FileStream fs = new(path, FileMode.Open);
                Image image = Image.FromStream(fs);
                ImageCodecInfo? myImageCodecInfo = GetEncoderInfo("image/tiff");
                Encoder myEncoder = Encoder.Compression;
                EncoderParameters myEncoderParameters = new(1);
                EncoderParameter myEncoderParameter;
                switch(CompressionCheckBox.SelectedIndex)
                {
                    case 0:
                        myEncoderParameter = new(myEncoder, (long)EncoderValue.CompressionNone);
                        break;
                    case 1:
                        myEncoderParameter = new(myEncoder, (long)EncoderValue.CompressionLZW);
                        break;
                    case 2:
                        myEncoderParameter = new(myEncoder, (long)EncoderValue.CompressionCCITT3);
                        break;
                    case 3:
                        myEncoderParameter = new(myEncoder, (long)EncoderValue.CompressionCCITT4);
                        break;
                    default:
                        myEncoderParameter = new(myEncoder, (long)EncoderValue.CompressionCCITT4);
                        break;
                }
                myEncoderParameters.Param[0] = myEncoderParameter;

                int pagesCount = image.GetFrameCount(FrameDimension.Page);
                if (pagesCount > 0)
                {
                    for (int i = 0; i < pagesCount; i++)
                    {
                        string outputFileName = $"{saveTo}{Path.GetFileNameWithoutExtension(path)}_L{i + 1}{Path.GetExtension(path)}";
                        image.SelectActiveFrame(FrameDimension.Page, i);
                        using MemoryStream memory = new();
                        using FileStream fileStream = new(outputFileName, FileMode.Create, FileAccess.ReadWrite);
                        if(myImageCodecInfo is ImageCodecInfo codecInfo)
                        {
                            image.Save(memory, codecInfo, myEncoderParameters);
                            byte[] bytes = memory.ToArray();
                            fileStream.Write(bytes, 0, bytes.Length);
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
            OpenFileDialog od = new()
            {
                Filter = "Obrázek TIF | *.tif;*.tiff"
            };
            if (od.ShowDialog() == DialogResult.OK)
                tbPath.Text = od.FileName;
        }
        private static ImageCodecInfo? GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < encoders.Length; ++i)
                if (encoders[i].MimeType == mimeType)
                    return encoders[i];
            return null;
        }
        private void TbPath_DragEnter(object? sender, DragEventArgs e)
        {
            if(e.Data is IDataObject dataObject)
                if (dataObject.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
        }
        private void TbPath_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data is IDataObject dataObject)
            {
                string[] files = (string[])dataObject.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    string extension = Path.GetExtension(files[0]).ToLower();
                    if (extension == ".tif" || extension == ".tiff")
                        tbPath.Text = files[0];
                    else
                        MessageBox.Show("Soubor musí být typu TIF !", "ERROR", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Lze pøetáhnout pouze 1 soubor !", "ERROR", MessageBoxButtons.OK);
                }
            }
        }

        private void CompressionCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < CompressionCheckBox.Items.Count; i++)
            {
                if (e.Index != i)
                    CompressionCheckBox.SetItemChecked(i, false);
            }
        }
    }
}