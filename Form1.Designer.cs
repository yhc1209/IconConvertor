using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IconConvertor;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private IContainer components = null;

    private TableLayoutPanel TlpMain = new TableLayoutPanel();

    private GroupBox GbxSource = new GroupBox();
    private TableLayoutPanel TlpSource = new TableLayoutPanel();
    private PictureBox PbxPreview = new PictureBox();
    private Label LblSourcePath = new Label();
    private TextBox TbxSourcePath = new TextBox();
    private Button BtnSelectSource = new Button();

    private GroupBox GbxConvert = new GroupBox();
    private TableLayoutPanel TlpConvert = new TableLayoutPanel();
    private Label LblSize = new Label();
    private CheckBox ChbPx16 = new CheckBox();
    private CheckBox ChbPx32 = new CheckBox();
    private CheckBox ChbPx48 = new CheckBox();
    private CheckBox ChbPx64 = new CheckBox();
    private CheckBox ChbPx128 = new CheckBox();
    private Button BtnConvert = new Button();

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        SuspendLayout();

        #region source group
        // PbxPreview
        PbxPreview.Name = "PbxPreview";
        PbxPreview.Dock = DockStyle.Fill;

        // LblSourcePath
        LblSourcePath.Name = "LblSourcePath";
        LblSourcePath.Text = "原圖路徑：";
        LblSourcePath.AutoSize = true;
        LblSourcePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;

        // TbxSourcePath
        TbxSourcePath.Name = "TbxSourcePath";
        TbxSourcePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        TbxSourcePath.ReadOnly = true;
        TbxSourcePath.PlaceholderText = "請選擇一個要轉換成ico的圖片";
        TbxSourcePath.TabStop = false;

        // BtnSelectSource
        BtnSelectSource.Name = "BtnSelectSource";
        BtnSelectSource.Text = "選擇原圖";
        BtnSelectSource.AutoSize = true;
        BtnSelectSource.TextAlign = ContentAlignment.MiddleCenter;
        BtnSelectSource.TabIndex = 0;
        BtnSelectSource.Click += SelectPicture;

        // TlpSource
        TlpSource.SuspendLayout();
        TlpSource.Name = "TlpSource";
        TlpSource.Dock = DockStyle.Fill;
        TlpSource.ColumnCount = 3;
        TlpSource.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        TlpSource.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        TlpSource.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        TlpSource.RowCount = 2;
        TlpSource.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        TlpSource.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpSource.Controls.Add(PbxPreview, 0, 0);
        TlpSource.Controls.Add(LblSourcePath, 0, 1);
        TlpSource.Controls.Add(TbxSourcePath, 1, 1);
        TlpSource.Controls.Add(BtnSelectSource, 2, 1);
        TlpSource.SetColumnSpan(PbxPreview, 3);
        TlpSource.ResumeLayout(false);

        // GbxSource
        GbxSource.SuspendLayout();
        GbxSource.Name = "GbxSource";
        GbxSource.Text = "原始圖片";
        GbxSource.Dock = DockStyle.Fill;
        GbxSource.Controls.Add(TlpSource);
        GbxSource.ResumeLayout(false);
        #endregion

        #region convert group
        // LblSize
        LblSize.Name = "LblSize";
        LblSize.Text = "包含尺寸：";
        LblSize.TextAlign = ContentAlignment.MiddleCenter;
        LblSize.AutoSize = true;
        LblSize.Anchor = AnchorStyles.Left;

        // ChbPx16
        ChbPx16.Name = "ChbPx16";
        ChbPx16.Text = "16 x 16";
        ChbPx16.AutoSize = true;
        ChbPx16.TextAlign = ContentAlignment.MiddleLeft;
        ChbPx16.Anchor = AnchorStyles.Left;
        ChbPx16.TabIndex = 1;

        // ChbPx32
        ChbPx32.Name = "ChbPx32";
        ChbPx32.Text = "32 x 32";
        ChbPx32.AutoSize = true;
        ChbPx32.TextAlign = ContentAlignment.MiddleLeft;
        ChbPx32.Anchor = AnchorStyles.Left;
        ChbPx32.TabIndex = 2;

        // ChbPx48
        ChbPx48.Name = "ChbPx48";
        ChbPx48.Text = "48 x 48";
        ChbPx48.AutoSize = true;
        ChbPx48.TextAlign = ContentAlignment.MiddleLeft;
        ChbPx48.Anchor = AnchorStyles.Left;
        ChbPx48.TabIndex = 3;

        // ChbPx64
        ChbPx64.Name = "ChbPx64";
        ChbPx64.Text = "64 x 64";
        ChbPx64.AutoSize = true;
        ChbPx64.TextAlign = ContentAlignment.MiddleLeft;
        ChbPx64.Anchor = AnchorStyles.Left;
        ChbPx64.TabIndex = 4;

        // ChbPx128
        ChbPx128.Name = "ChbPx128";
        ChbPx128.Text = "128 x 128";
        ChbPx128.AutoSize = true;
        ChbPx128.TextAlign = ContentAlignment.MiddleLeft;
        ChbPx128.Anchor = AnchorStyles.Left;
        ChbPx128.TabIndex = 5;

        // BtnConvert
        BtnConvert.Name = "BtnConvert";
        BtnConvert.Text = "轉換";
        BtnConvert.AutoSize = true;
        BtnConvert.TextAlign = ContentAlignment.MiddleCenter;
        BtnConvert.Anchor = AnchorStyles.Right;
        BtnConvert.TabIndex = 6;
        BtnConvert.Click += ExportIcoFile;

        // TlpConvert
        TlpConvert.SuspendLayout();
        TlpConvert.Name = "TlpConvert";
        TlpConvert.Dock = DockStyle.Fill;
        TlpConvert.ColumnCount = 1;
        TlpConvert.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        TlpConvert.RowCount = 8;
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        TlpConvert.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        TlpConvert.Controls.Add(LblSize, 0, 0);
        TlpConvert.Controls.Add(ChbPx16, 0, 1);
        TlpConvert.Controls.Add(ChbPx32, 0, 2);
        TlpConvert.Controls.Add(ChbPx48, 0, 3);
        TlpConvert.Controls.Add(ChbPx64, 0, 4);
        TlpConvert.Controls.Add(ChbPx128, 0, 5);
        TlpConvert.Controls.Add(BtnConvert, 0, 7);
        TlpConvert.ResumeLayout(false);

        // GbxConvert
        GbxConvert.SuspendLayout();
        GbxConvert.Name = "GbxConvert";
        GbxConvert.Text = "轉換";
        GbxConvert.Dock = DockStyle.Fill;
        GbxConvert.Controls.Add(TlpConvert);
        GbxConvert.Enabled = false;
        GbxConvert.ResumeLayout(false);
        #endregion

        // TlpMain
        TlpMain.SuspendLayout();
        TlpMain.Name = "TlpMain";
        TlpMain.Dock = DockStyle.Fill;
        TlpMain.ColumnCount = 2;
        TlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        TlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        TlpMain.RowCount = 1;
        TlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        TlpMain.Controls.Add(GbxSource, 0, 0);
        TlpMain.Controls.Add(GbxConvert, 1, 0);
        TlpMain.ResumeLayout(false);

        // Main form
        Text = "Icon Convertor";
        Padding = new Padding(10);
        components = new Container();
        AutoScaleMode = AutoScaleMode.Font;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(800, 450);
        MaximizeBox = false;
        MinimizeBox = false;
        Controls.Add(TlpMain);
        FormClosing += ClosingCallback;
        FormClosed += ClosedCallback;

        ResumeLayout(false);
    }
}
