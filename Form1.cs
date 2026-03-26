using System;
using System.Collections.Generic;
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
                ofd.Filter = "PNG|*.png|ALL|*.*";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = false;
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
            GbxConvert.Enabled = true;
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

    private async void ExportIcoFile(object sender, EventArgs e)
    {
        try
        {
            BtnConvert.Enabled = false;

            uint[] sizes = GetOutputSizes();
            if (sizes.Length == 0)
            {
                MessageBox.Show("請選擇icon尺寸。", Text);
                return;
            }

            string outputPath;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.CheckPathExists = true;
                ofd.CheckFileExists = false;
                ofd.AddExtension = false;
                ofd.DefaultExt = ".ico";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return;

                outputPath = ofd.FileName;
            }

            using (MagickImageCollection collection = new MagickImageCollection())
            {
                foreach (uint size in sizes)
                {
                    var copy = image.Clone();
                    copy.Resize(size, size);
                    collection.Add(copy);
                }

                await collection.WriteAsync(outputPath, MagickFormat.Ico, cts.Token);
            }
        }
        catch (Exception excp)
        {
            MessageBox.Show($"輸出ico檔時發生例外狀況。 ({excp.Message})\n{excp.StackTrace}", "輸出ico檔");
        }
        finally
        {
            BtnConvert.Enabled = true;
        }
    }

    private uint[] GetOutputSizes()
    {
        List<uint> sizes = new List<uint>();
        if (ChbPx16.Checked)
            sizes.Add(16);
        if (ChbPx32.Checked)
            sizes.Add(32);
        if (ChbPx48.Checked)
            sizes.Add(48);
        if (ChbPx64.Checked)
            sizes.Add(64);
        if (ChbPx128.Checked)
            sizes.Add(128);
        return sizes.ToArray();
    }
}
