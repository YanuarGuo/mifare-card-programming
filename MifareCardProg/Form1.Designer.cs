namespace MifareCardProg
{
    partial class MainMifareProg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMifareProg));
            this.bBinUpd = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.bBinRead = new System.Windows.Forms.Button();
            this.gbValBlk = new System.Windows.Forms.GroupBox();
            this.tValAmt = new System.Windows.Forms.TextBox();
            this.bValRes = new System.Windows.Forms.Button();
            this.bValRead = new System.Windows.Forms.Button();
            this.bValDec = new System.Windows.Forms.Button();
            this.bValInc = new System.Windows.Forms.Button();
            this.bValStor = new System.Windows.Forms.Button();
            this.tValTar = new System.Windows.Forms.TextBox();
            this.tValSrc = new System.Windows.Forms.TextBox();
            this.tValBlk = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.gbBinOps = new System.Windows.Forms.GroupBox();
            this.tBinData = new System.Windows.Forms.TextBox();
            this.tBinLen = new System.Windows.Forms.TextBox();
            this.tBinBlk = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.rbSource3 = new System.Windows.Forms.RadioButton();
            this.rbSource2 = new System.Windows.Forms.RadioButton();
            this.rbSource1 = new System.Windows.Forms.RadioButton();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.mMsg = new System.Windows.Forms.ListBox();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.rbKType2 = new System.Windows.Forms.RadioButton();
            this.bLoadKey = new System.Windows.Forms.Button();
            this.tKey6 = new System.Windows.Forms.TextBox();
            this.tKey5 = new System.Windows.Forms.TextBox();
            this.tKey4 = new System.Windows.Forms.TextBox();
            this.tKey3 = new System.Windows.Forms.TextBox();
            this.tKey2 = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.tKey1 = new System.Windows.Forms.TextBox();
            this.rbKType1 = new System.Windows.Forms.RadioButton();
            this.tMemAdd = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.rbVolMem = new System.Windows.Forms.RadioButton();
            this.gbLoadKeys = new System.Windows.Forms.GroupBox();
            this.rbNonVolMem = new System.Windows.Forms.RadioButton();
            this.bConnect = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.bAuth = new System.Windows.Forms.Button();
            this.gbAuth = new System.Windows.Forms.GroupBox();
            this.tKeyIn5 = new System.Windows.Forms.TextBox();
            this.tKeyIn6 = new System.Windows.Forms.TextBox();
            this.tKeyIn4 = new System.Windows.Forms.TextBox();
            this.tKeyIn3 = new System.Windows.Forms.TextBox();
            this.tKeyIn2 = new System.Windows.Forms.TextBox();
            this.tKeyIn1 = new System.Windows.Forms.TextBox();
            this.tKeyAdd = new System.Windows.Forms.TextBox();
            this.tBlkNo = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.gbKType = new System.Windows.Forms.GroupBox();
            this.bQuit = new System.Windows.Forms.Button();
            this.gbValBlk.SuspendLayout();
            this.gbBinOps.SuspendLayout();
            this.gbSource.SuspendLayout();
            this.gbLoadKeys.SuspendLayout();
            this.gbAuth.SuspendLayout();
            this.gbKType.SuspendLayout();
            this.SuspendLayout();
            // 
            // bBinUpd
            // 
            this.bBinUpd.Location = new System.Drawing.Point(140, 106);
            this.bBinUpd.Name = "bBinUpd";
            this.bBinUpd.Size = new System.Drawing.Size(116, 23);
            this.bBinUpd.TabIndex = 19;
            this.bBinUpd.Text = "Update Block";
            this.bBinUpd.UseVisualStyleBackColor = true;
            this.bBinUpd.Click += new System.EventHandler(this.bBinUpd_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "Select Reader";
            // 
            // bBinRead
            // 
            this.bBinRead.Location = new System.Drawing.Point(27, 106);
            this.bBinRead.Name = "bBinRead";
            this.bBinRead.Size = new System.Drawing.Size(107, 23);
            this.bBinRead.TabIndex = 18;
            this.bBinRead.Text = "Read Block";
            this.bBinRead.UseVisualStyleBackColor = true;
            this.bBinRead.Click += new System.EventHandler(this.bBinRead_Click);
            // 
            // gbValBlk
            // 
            this.gbValBlk.Controls.Add(this.tValAmt);
            this.gbValBlk.Controls.Add(this.bValRes);
            this.gbValBlk.Controls.Add(this.bValRead);
            this.gbValBlk.Controls.Add(this.bValDec);
            this.gbValBlk.Controls.Add(this.bValInc);
            this.gbValBlk.Controls.Add(this.bValStor);
            this.gbValBlk.Controls.Add(this.tValTar);
            this.gbValBlk.Controls.Add(this.tValSrc);
            this.gbValBlk.Controls.Add(this.tValBlk);
            this.gbValBlk.Controls.Add(this.Label13);
            this.gbValBlk.Controls.Add(this.Label12);
            this.gbValBlk.Controls.Add(this.Label11);
            this.gbValBlk.Controls.Add(this.Label10);
            this.gbValBlk.Location = new System.Drawing.Point(315, 14);
            this.gbValBlk.Name = "gbValBlk";
            this.gbValBlk.Size = new System.Drawing.Size(344, 168);
            this.gbValBlk.TabIndex = 33;
            this.gbValBlk.TabStop = false;
            this.gbValBlk.Text = "Value Block Functions";
            // 
            // tValAmt
            // 
            this.tValAmt.Location = new System.Drawing.Point(96, 27);
            this.tValAmt.Name = "tValAmt";
            this.tValAmt.Size = new System.Drawing.Size(111, 20);
            this.tValAmt.TabIndex = 28;
            // 
            // bValRes
            // 
            this.bValRes.Location = new System.Drawing.Point(237, 133);
            this.bValRes.Name = "bValRes";
            this.bValRes.Size = new System.Drawing.Size(101, 23);
            this.bValRes.TabIndex = 27;
            this.bValRes.Text = "Restore Value";
            this.bValRes.UseVisualStyleBackColor = true;
            this.bValRes.Click += new System.EventHandler(this.bValRes_Click);
            // 
            // bValRead
            // 
            this.bValRead.Location = new System.Drawing.Point(237, 104);
            this.bValRead.Name = "bValRead";
            this.bValRead.Size = new System.Drawing.Size(101, 23);
            this.bValRead.TabIndex = 26;
            this.bValRead.Text = "Read Value";
            this.bValRead.UseVisualStyleBackColor = true;
            this.bValRead.Click += new System.EventHandler(this.bValRead_Click);
            // 
            // bValDec
            // 
            this.bValDec.Location = new System.Drawing.Point(237, 76);
            this.bValDec.Name = "bValDec";
            this.bValDec.Size = new System.Drawing.Size(101, 23);
            this.bValDec.TabIndex = 25;
            this.bValDec.Text = "Decrement";
            this.bValDec.UseVisualStyleBackColor = true;
            this.bValDec.Click += new System.EventHandler(this.bValDec_Click);
            // 
            // bValInc
            // 
            this.bValInc.Location = new System.Drawing.Point(237, 50);
            this.bValInc.Name = "bValInc";
            this.bValInc.Size = new System.Drawing.Size(101, 23);
            this.bValInc.TabIndex = 24;
            this.bValInc.Text = "Increment";
            this.bValInc.UseVisualStyleBackColor = true;
            this.bValInc.Click += new System.EventHandler(this.bValInc_Click);
            // 
            // bValStor
            // 
            this.bValStor.Location = new System.Drawing.Point(237, 22);
            this.bValStor.Name = "bValStor";
            this.bValStor.Size = new System.Drawing.Size(101, 23);
            this.bValStor.TabIndex = 23;
            this.bValStor.Text = "Store Value";
            this.bValStor.UseVisualStyleBackColor = true;
            this.bValStor.Click += new System.EventHandler(this.bValStor_Click);
            // 
            // tValTar
            // 
            this.tValTar.Location = new System.Drawing.Point(96, 104);
            this.tValTar.MaxLength = 2;
            this.tValTar.Name = "tValTar";
            this.tValTar.Size = new System.Drawing.Size(33, 20);
            this.tValTar.TabIndex = 22;
            // 
            // tValSrc
            // 
            this.tValSrc.Location = new System.Drawing.Point(96, 79);
            this.tValSrc.MaxLength = 2;
            this.tValSrc.Name = "tValSrc";
            this.tValSrc.Size = new System.Drawing.Size(33, 20);
            this.tValSrc.TabIndex = 21;
            // 
            // tValBlk
            // 
            this.tValBlk.Location = new System.Drawing.Point(96, 53);
            this.tValBlk.MaxLength = 2;
            this.tValBlk.Name = "tValBlk";
            this.tValBlk.Size = new System.Drawing.Size(33, 20);
            this.tValBlk.TabIndex = 20;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(17, 111);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(68, 13);
            this.Label13.TabIndex = 3;
            this.Label13.Text = "Target Block";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(17, 82);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(71, 13);
            this.Label12.TabIndex = 2;
            this.Label12.Text = "Source Block";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(17, 53);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(54, 13);
            this.Label11.TabIndex = 1;
            this.Label11.Text = "Block No.";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(17, 27);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(73, 13);
            this.Label10.TabIndex = 0;
            this.Label10.Text = "Value Amount";
            // 
            // gbBinOps
            // 
            this.gbBinOps.Controls.Add(this.tBinData);
            this.gbBinOps.Controls.Add(this.tBinLen);
            this.gbBinOps.Controls.Add(this.tBinBlk);
            this.gbBinOps.Controls.Add(this.bBinUpd);
            this.gbBinOps.Controls.Add(this.bBinRead);
            this.gbBinOps.Controls.Add(this.Label9);
            this.gbBinOps.Controls.Add(this.Label8);
            this.gbBinOps.Controls.Add(this.Label7);
            this.gbBinOps.Location = new System.Drawing.Point(12, 492);
            this.gbBinOps.Name = "gbBinOps";
            this.gbBinOps.Size = new System.Drawing.Size(297, 135);
            this.gbBinOps.TabIndex = 32;
            this.gbBinOps.TabStop = false;
            this.gbBinOps.Text = "Binary Block Functions";
            // 
            // tBinData
            // 
            this.tBinData.Location = new System.Drawing.Point(12, 80);
            this.tBinData.Name = "tBinData";
            this.tBinData.Size = new System.Drawing.Size(252, 20);
            this.tBinData.TabIndex = 22;
            // 
            // tBinLen
            // 
            this.tBinLen.Location = new System.Drawing.Point(231, 25);
            this.tBinLen.MaxLength = 2;
            this.tBinLen.Name = "tBinLen";
            this.tBinLen.Size = new System.Drawing.Size(33, 20);
            this.tBinLen.TabIndex = 21;
            this.tBinLen.TextChanged += new System.EventHandler(this.tBinLen_TextChanged);
            // 
            // tBinBlk
            // 
            this.tBinBlk.Location = new System.Drawing.Point(99, 28);
            this.tBinBlk.MaxLength = 2;
            this.tBinBlk.Name = "tBinBlk";
            this.tBinBlk.Size = new System.Drawing.Size(33, 20);
            this.tBinBlk.TabIndex = 20;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(9, 62);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(60, 13);
            this.Label9.TabIndex = 16;
            this.Label9.Text = "Data (Text)";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(156, 28);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(69, 13);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "Length (Dec)";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(7, 28);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(88, 13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "Start Block (Dec)";
            // 
            // rbSource3
            // 
            this.rbSource3.AutoSize = true;
            this.rbSource3.Location = new System.Drawing.Point(11, 63);
            this.rbSource3.Name = "rbSource3";
            this.rbSource3.Size = new System.Drawing.Size(122, 17);
            this.rbSource3.TabIndex = 2;
            this.rbSource3.TabStop = true;
            this.rbSource3.Text = "Non-Volatile Memory";
            this.rbSource3.UseVisualStyleBackColor = true;
            this.rbSource3.CheckedChanged += new System.EventHandler(this.rbSource3_CheckedChanged);
            // 
            // rbSource2
            // 
            this.rbSource2.AutoSize = true;
            this.rbSource2.Location = new System.Drawing.Point(11, 42);
            this.rbSource2.Name = "rbSource2";
            this.rbSource2.Size = new System.Drawing.Size(99, 17);
            this.rbSource2.TabIndex = 1;
            this.rbSource2.TabStop = true;
            this.rbSource2.Text = "Volatile Memory";
            this.rbSource2.UseVisualStyleBackColor = true;
            this.rbSource2.CheckedChanged += new System.EventHandler(this.rbSource2_CheckedChanged);
            // 
            // rbSource1
            // 
            this.rbSource1.AutoSize = true;
            this.rbSource1.Location = new System.Drawing.Point(11, 19);
            this.rbSource1.Name = "rbSource1";
            this.rbSource1.Size = new System.Drawing.Size(87, 17);
            this.rbSource1.TabIndex = 0;
            this.rbSource1.TabStop = true;
            this.rbSource1.Text = "Manual Input";
            this.rbSource1.UseVisualStyleBackColor = true;
            this.rbSource1.CheckedChanged += new System.EventHandler(this.rbSource1_CheckedChanged);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(433, 604);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(113, 23);
            this.bReset.TabIndex = 36;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(325, 604);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(102, 23);
            this.bClear.TabIndex = 35;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // mMsg
            // 
            this.mMsg.FormattingEnabled = true;
            this.mMsg.Location = new System.Drawing.Point(315, 192);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(344, 394);
            this.mMsg.TabIndex = 34;
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.rbSource3);
            this.gbSource.Controls.Add(this.rbSource2);
            this.gbSource.Controls.Add(this.rbSource1);
            this.gbSource.Location = new System.Drawing.Point(10, 19);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(140, 86);
            this.gbSource.TabIndex = 0;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Key Source";
            // 
            // rbKType2
            // 
            this.rbKType2.AutoSize = true;
            this.rbKType2.Location = new System.Drawing.Point(16, 53);
            this.rbKType2.Name = "rbKType2";
            this.rbKType2.Size = new System.Drawing.Size(53, 17);
            this.rbKType2.TabIndex = 2;
            this.rbKType2.TabStop = true;
            this.rbKType2.Text = "Key B";
            this.rbKType2.UseVisualStyleBackColor = true;
            // 
            // bLoadKey
            // 
            this.bLoadKey.Location = new System.Drawing.Point(169, 100);
            this.bLoadKey.Name = "bLoadKey";
            this.bLoadKey.Size = new System.Drawing.Size(118, 23);
            this.bLoadKey.TabIndex = 11;
            this.bLoadKey.Text = "Load Key";
            this.bLoadKey.UseVisualStyleBackColor = true;
            this.bLoadKey.Click += new System.EventHandler(this.bLoadKey_Click);
            // 
            // tKey6
            // 
            this.tKey6.Location = new System.Drawing.Point(262, 74);
            this.tKey6.MaxLength = 2;
            this.tKey6.Name = "tKey6";
            this.tKey6.Size = new System.Drawing.Size(25, 20);
            this.tKey6.TabIndex = 10;
            // 
            // tKey5
            // 
            this.tKey5.Location = new System.Drawing.Point(231, 74);
            this.tKey5.MaxLength = 2;
            this.tKey5.Name = "tKey5";
            this.tKey5.Size = new System.Drawing.Size(25, 20);
            this.tKey5.TabIndex = 9;
            // 
            // tKey4
            // 
            this.tKey4.Location = new System.Drawing.Point(200, 74);
            this.tKey4.MaxLength = 2;
            this.tKey4.Name = "tKey4";
            this.tKey4.Size = new System.Drawing.Size(25, 20);
            this.tKey4.TabIndex = 8;
            // 
            // tKey3
            // 
            this.tKey3.Location = new System.Drawing.Point(169, 74);
            this.tKey3.MaxLength = 2;
            this.tKey3.Name = "tKey3";
            this.tKey3.Size = new System.Drawing.Size(25, 20);
            this.tKey3.TabIndex = 7;
            // 
            // tKey2
            // 
            this.tKey2.Location = new System.Drawing.Point(138, 74);
            this.tKey2.MaxLength = 2;
            this.tKey2.Name = "tKey2";
            this.tKey2.Size = new System.Drawing.Size(25, 20);
            this.tKey2.TabIndex = 5;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 74);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(82, 13);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Key Value Input";
            // 
            // tKey1
            // 
            this.tKey1.Location = new System.Drawing.Point(107, 74);
            this.tKey1.MaxLength = 2;
            this.tKey1.Name = "tKey1";
            this.tKey1.Size = new System.Drawing.Size(25, 20);
            this.tKey1.TabIndex = 6;
            // 
            // rbKType1
            // 
            this.rbKType1.AutoSize = true;
            this.rbKType1.Location = new System.Drawing.Point(16, 19);
            this.rbKType1.Name = "rbKType1";
            this.rbKType1.Size = new System.Drawing.Size(53, 17);
            this.rbKType1.TabIndex = 1;
            this.rbKType1.TabStop = true;
            this.rbKType1.Text = "Key A";
            this.rbKType1.UseVisualStyleBackColor = true;
            // 
            // tMemAdd
            // 
            this.tMemAdd.Location = new System.Drawing.Point(107, 46);
            this.tMemAdd.MaxLength = 2;
            this.tMemAdd.Name = "tMemAdd";
            this.tMemAdd.Size = new System.Drawing.Size(25, 20);
            this.tMemAdd.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 49);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Key Store No.";
            // 
            // rbVolMem
            // 
            this.rbVolMem.AutoSize = true;
            this.rbVolMem.Location = new System.Drawing.Point(139, 21);
            this.rbVolMem.Name = "rbVolMem";
            this.rbVolMem.Size = new System.Drawing.Size(99, 17);
            this.rbVolMem.TabIndex = 1;
            this.rbVolMem.TabStop = true;
            this.rbVolMem.Text = "Volatile Memory";
            this.rbVolMem.UseVisualStyleBackColor = true;
            this.rbVolMem.CheckedChanged += new System.EventHandler(this.rbVolMem_CheckedChanged);
            // 
            // gbLoadKeys
            // 
            this.gbLoadKeys.Controls.Add(this.bLoadKey);
            this.gbLoadKeys.Controls.Add(this.tKey6);
            this.gbLoadKeys.Controls.Add(this.tKey5);
            this.gbLoadKeys.Controls.Add(this.tKey4);
            this.gbLoadKeys.Controls.Add(this.tKey3);
            this.gbLoadKeys.Controls.Add(this.tKey1);
            this.gbLoadKeys.Controls.Add(this.tKey2);
            this.gbLoadKeys.Controls.Add(this.tMemAdd);
            this.gbLoadKeys.Controls.Add(this.Label3);
            this.gbLoadKeys.Controls.Add(this.Label2);
            this.gbLoadKeys.Controls.Add(this.rbVolMem);
            this.gbLoadKeys.Controls.Add(this.rbNonVolMem);
            this.gbLoadKeys.Location = new System.Drawing.Point(12, 105);
            this.gbLoadKeys.Name = "gbLoadKeys";
            this.gbLoadKeys.Size = new System.Drawing.Size(296, 135);
            this.gbLoadKeys.TabIndex = 30;
            this.gbLoadKeys.TabStop = false;
            this.gbLoadKeys.Text = "Store Authentication Keys to Device";
            // 
            // rbNonVolMem
            // 
            this.rbNonVolMem.AutoSize = true;
            this.rbNonVolMem.Location = new System.Drawing.Point(10, 20);
            this.rbNonVolMem.Name = "rbNonVolMem";
            this.rbNonVolMem.Size = new System.Drawing.Size(122, 17);
            this.rbNonVolMem.TabIndex = 0;
            this.rbNonVolMem.TabStop = true;
            this.rbNonVolMem.Text = "Non-Volatile Memory";
            this.rbNonVolMem.UseVisualStyleBackColor = true;
            this.rbNonVolMem.CheckedChanged += new System.EventHandler(this.rbNonVolMem_CheckedChanged);
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(191, 76);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(117, 23);
            this.bConnect.TabIndex = 29;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(191, 47);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(117, 23);
            this.bInit.TabIndex = 28;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 20);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(216, 21);
            this.cbReader.TabIndex = 27;
            // 
            // bAuth
            // 
            this.bAuth.Location = new System.Drawing.Point(169, 207);
            this.bAuth.Name = "bAuth";
            this.bAuth.Size = new System.Drawing.Size(118, 23);
            this.bAuth.TabIndex = 13;
            this.bAuth.Text = "Authenticate";
            this.bAuth.UseVisualStyleBackColor = true;
            this.bAuth.Click += new System.EventHandler(this.bAuth_Click);
            // 
            // gbAuth
            // 
            this.gbAuth.Controls.Add(this.tKeyIn5);
            this.gbAuth.Controls.Add(this.tKeyIn6);
            this.gbAuth.Controls.Add(this.tKeyIn4);
            this.gbAuth.Controls.Add(this.tKeyIn3);
            this.gbAuth.Controls.Add(this.tKeyIn2);
            this.gbAuth.Controls.Add(this.tKeyIn1);
            this.gbAuth.Controls.Add(this.tKeyAdd);
            this.gbAuth.Controls.Add(this.tBlkNo);
            this.gbAuth.Controls.Add(this.bAuth);
            this.gbAuth.Controls.Add(this.Label6);
            this.gbAuth.Controls.Add(this.Label5);
            this.gbAuth.Controls.Add(this.Label4);
            this.gbAuth.Controls.Add(this.gbKType);
            this.gbAuth.Controls.Add(this.gbSource);
            this.gbAuth.Location = new System.Drawing.Point(12, 246);
            this.gbAuth.Name = "gbAuth";
            this.gbAuth.Size = new System.Drawing.Size(297, 240);
            this.gbAuth.TabIndex = 31;
            this.gbAuth.TabStop = false;
            this.gbAuth.Text = "Authentication Function";
            // 
            // tKeyIn5
            // 
            this.tKeyIn5.Location = new System.Drawing.Point(231, 181);
            this.tKeyIn5.MaxLength = 2;
            this.tKeyIn5.Name = "tKeyIn5";
            this.tKeyIn5.Size = new System.Drawing.Size(25, 20);
            this.tKeyIn5.TabIndex = 22;
            // 
            // tKeyIn6
            // 
            this.tKeyIn6.Location = new System.Drawing.Point(262, 181);
            this.tKeyIn6.MaxLength = 2;
            this.tKeyIn6.Name = "tKeyIn6";
            this.tKeyIn6.Size = new System.Drawing.Size(25, 20);
            this.tKeyIn6.TabIndex = 21;
            // 
            // tKeyIn4
            // 
            this.tKeyIn4.Location = new System.Drawing.Point(200, 181);
            this.tKeyIn4.MaxLength = 2;
            this.tKeyIn4.Name = "tKeyIn4";
            this.tKeyIn4.Size = new System.Drawing.Size(25, 20);
            this.tKeyIn4.TabIndex = 19;
            // 
            // tKeyIn3
            // 
            this.tKeyIn3.Location = new System.Drawing.Point(169, 181);
            this.tKeyIn3.MaxLength = 2;
            this.tKeyIn3.Name = "tKeyIn3";
            this.tKeyIn3.Size = new System.Drawing.Size(25, 20);
            this.tKeyIn3.TabIndex = 18;
            // 
            // tKeyIn2
            // 
            this.tKeyIn2.Location = new System.Drawing.Point(138, 181);
            this.tKeyIn2.MaxLength = 2;
            this.tKeyIn2.Name = "tKeyIn2";
            this.tKeyIn2.Size = new System.Drawing.Size(25, 20);
            this.tKeyIn2.TabIndex = 17;
            // 
            // tKeyIn1
            // 
            this.tKeyIn1.Location = new System.Drawing.Point(107, 181);
            this.tKeyIn1.MaxLength = 2;
            this.tKeyIn1.Name = "tKeyIn1";
            this.tKeyIn1.Size = new System.Drawing.Size(25, 20);
            this.tKeyIn1.TabIndex = 16;
            // 
            // tKeyAdd
            // 
            this.tKeyAdd.Location = new System.Drawing.Point(108, 155);
            this.tKeyAdd.MaxLength = 2;
            this.tKeyAdd.Name = "tKeyAdd";
            this.tKeyAdd.Size = new System.Drawing.Size(25, 20);
            this.tKeyAdd.TabIndex = 15;
            // 
            // tBlkNo
            // 
            this.tBlkNo.Location = new System.Drawing.Point(107, 129);
            this.tBlkNo.MaxLength = 3;
            this.tBlkNo.Name = "tBlkNo";
            this.tBlkNo.Size = new System.Drawing.Size(25, 20);
            this.tBlkNo.TabIndex = 14;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(7, 181);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(82, 13);
            this.Label6.TabIndex = 4;
            this.Label6.Text = "Key Value Input";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(9, 155);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(93, 13);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Key Store Number";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(9, 128);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(80, 13);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Block No (Dec)";
            // 
            // gbKType
            // 
            this.gbKType.Controls.Add(this.rbKType2);
            this.gbKType.Controls.Add(this.rbKType1);
            this.gbKType.Location = new System.Drawing.Point(169, 19);
            this.gbKType.Name = "gbKType";
            this.gbKType.Size = new System.Drawing.Size(109, 86);
            this.gbKType.TabIndex = 1;
            this.gbKType.TabStop = false;
            this.gbKType.Text = "Key Type";
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(552, 604);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(101, 23);
            this.bQuit.TabIndex = 37;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // MainMifareProg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 640);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.gbValBlk);
            this.Controls.Add(this.gbBinOps);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.gbLoadKeys);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.gbAuth);
            this.Controls.Add(this.bQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMifareProg";
            this.Text = "Mifare Card Programming";
            this.Load += new System.EventHandler(this.MainMifareProg_Load);
            this.gbValBlk.ResumeLayout(false);
            this.gbValBlk.PerformLayout();
            this.gbBinOps.ResumeLayout(false);
            this.gbBinOps.PerformLayout();
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.gbLoadKeys.ResumeLayout(false);
            this.gbLoadKeys.PerformLayout();
            this.gbAuth.ResumeLayout(false);
            this.gbAuth.PerformLayout();
            this.gbKType.ResumeLayout(false);
            this.gbKType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button bBinUpd;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button bBinRead;
        internal System.Windows.Forms.GroupBox gbValBlk;
        internal System.Windows.Forms.Button bValRes;
        internal System.Windows.Forms.Button bValRead;
        internal System.Windows.Forms.Button bValDec;
        internal System.Windows.Forms.Button bValInc;
        internal System.Windows.Forms.Button bValStor;
        internal System.Windows.Forms.TextBox tValTar;
        internal System.Windows.Forms.TextBox tValSrc;
        internal System.Windows.Forms.TextBox tValBlk;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.GroupBox gbBinOps;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.RadioButton rbSource3;
        internal System.Windows.Forms.RadioButton rbSource2;
        internal System.Windows.Forms.RadioButton rbSource1;
        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.ListBox mMsg;
        internal System.Windows.Forms.GroupBox gbSource;
        internal System.Windows.Forms.RadioButton rbKType2;
        internal System.Windows.Forms.Button bLoadKey;
        internal System.Windows.Forms.TextBox tKey6;
        internal System.Windows.Forms.TextBox tKey5;
        internal System.Windows.Forms.TextBox tKey4;
        internal System.Windows.Forms.TextBox tKey3;
        internal System.Windows.Forms.TextBox tKey2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox tKey1;
        internal System.Windows.Forms.RadioButton rbKType1;
        internal System.Windows.Forms.TextBox tMemAdd;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.RadioButton rbVolMem;
        internal System.Windows.Forms.GroupBox gbLoadKeys;
        internal System.Windows.Forms.RadioButton rbNonVolMem;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.Button bAuth;
        internal System.Windows.Forms.GroupBox gbAuth;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.GroupBox gbKType;
        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.TextBox tBlkNo;
        internal System.Windows.Forms.TextBox tKeyAdd;
        internal System.Windows.Forms.TextBox tKeyIn6;
        internal System.Windows.Forms.TextBox tKeyIn4;
        internal System.Windows.Forms.TextBox tKeyIn3;
        internal System.Windows.Forms.TextBox tKeyIn2;
        internal System.Windows.Forms.TextBox tKeyIn1;
        internal System.Windows.Forms.TextBox tKeyIn5;
        internal System.Windows.Forms.TextBox tBinData;
        internal System.Windows.Forms.TextBox tBinLen;
        internal System.Windows.Forms.TextBox tBinBlk;
        internal System.Windows.Forms.TextBox tValAmt;
    }
}

