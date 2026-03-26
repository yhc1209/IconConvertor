using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using ImageMagick;

namespace IconConvertor;

public partial class Form1 : Form
{
    private MagickImage image;
    private readonly CancellationTokenSource cts;
    public Form1()
    {
        cts = new CancellationTokenSource();
        InitializeComponent();
    }

    private void ClosingCallback(object sender, FormClosingEventArgs e)
    {
        if (!BtnSelectSource.Enabled || (GbxConvert.Enabled && !BtnConvert.Enabled))
        {
            DialogResult result = MessageBox.Show("還有動作正在進行中，你要取消嗎？", Text, MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            
            cts.Cancel();
        }
    }

    private void ClosedCallback(object sender, FormClosedEventArgs e)
    {
        image?.Dispose();
        PbxPreview.Image?.Dispose();
    }

    private async void SelectPicture(object sender, EventArgs e)
    {
        try
        {
            BtnSelectSource.Enabled = false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.png|PNG";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Title = "選擇原始圖片";
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return;
                TbxSourcePath.Text = ofd.FileName;
            }

            image?.Dispose();
            image = new MagickImage(TbxSourcePath.Text);
            using (MemoryStream ms = new MemoryStream())
            {
                await image.WriteAsync(ms, MagickFormat.Bmp, cts.Token);
                ms.Seek(0, SeekOrigin.Begin);
                PbxPreview.Image?.Dispose();
                PbxPreview.Image = Image.FromStream(ms);
                PbxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            }
            // GbxConvert.Enabled = true;
        }
        catch (Exception excp)
        {
            MessageBox.Show($"選擇原始圖片時發生例外狀況。 ({excp.Message})\n{excp.StackTrace}", "選擇原始圖片");
            TbxSourcePath.Clear();
        }
        finally
        {
            BtnSelectSource.Enabled = true;
        }
    }
}
