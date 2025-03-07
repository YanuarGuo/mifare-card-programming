using System;

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
            this.Label1 = new System.Windows.Forms.Label();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.bQuit = new System.Windows.Forms.Button();
            this.btnGetUID = new System.Windows.Forms.Button();
            this.mMsg = new System.Windows.Forms.ListBox();
            this.rbNonVolMem = new System.Windows.Forms.RadioButton();
            this.rbVolMem = new System.Windows.Forms.RadioButton();
            this.gbLoadKeys = new System.Windows.Forms.GroupBox();
            this.bLoadKey = new System.Windows.Forms.Button();
            this.tKey6 = new System.Windows.Forms.TextBox();
            this.tKey5 = new System.Windows.Forms.TextBox();
            this.tKey4 = new System.Windows.Forms.TextBox();
            this.tKey3 = new System.Windows.Forms.TextBox();
            this.tKey2 = new System.Windows.Forms.TextBox();
            this.tKey1 = new System.Windows.Forms.TextBox();
            this.tMemAdd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbAuth = new System.Windows.Forms.GroupBox();
            this.bAuth = new System.Windows.Forms.Button();
            this.tKeyIn6 = new System.Windows.Forms.TextBox();
            this.tKeyIn5 = new System.Windows.Forms.TextBox();
            this.tKeyIn4 = new System.Windows.Forms.TextBox();
            this.tKeyIn3 = new System.Windows.Forms.TextBox();
            this.tKeyIn2 = new System.Windows.Forms.TextBox();
            this.tKeyIn1 = new System.Windows.Forms.TextBox();
            this.tKeyAdd = new System.Windows.Forms.TextBox();
            this.tBlkNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbKType = new System.Windows.Forms.GroupBox();
            this.rbKType2 = new System.Windows.Forms.RadioButton();
            this.rbKType1 = new System.Windows.Forms.RadioButton();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.rbSource3 = new System.Windows.Forms.RadioButton();
            this.rbSource2 = new System.Windows.Forms.RadioButton();
            this.rbSource1 = new System.Windows.Forms.RadioButton();
            this.gbBinOps = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.bHexUpd = new System.Windows.Forms.Button();
            this.bHexRead = new System.Windows.Forms.Button();
            this.tbHextoStr = new System.Windows.Forms.TextBox();
            this.tBinLen = new System.Windows.Forms.TextBox();
            this.tBinBlk = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbValBlk = new System.Windows.Forms.GroupBox();
            this.bValRes = new System.Windows.Forms.Button();
            this.bValRead = new System.Windows.Forms.Button();
            this.bValDec = new System.Windows.Forms.Button();
            this.bValInc = new System.Windows.Forms.Button();
            this.bValStor = new System.Windows.Forms.Button();
            this.tValTar = new System.Windows.Forms.TextBox();
            this.tValSrc = new System.Windows.Forms.TextBox();
            this.tValBlk = new System.Windows.Forms.TextBox();
            this.tValAmt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bReadAll = new System.Windows.Forms.Button();
            this.dReadAll = new System.Windows.Forms.DataGridView();
            this.ProfilePict = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.gbProfileCard = new System.Windows.Forms.GroupBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.lblAddPhoto = new System.Windows.Forms.Label();
            this.TxtNumber = new System.Windows.Forms.TextBox();
            this.TxtAddress = new System.Windows.Forms.TextBox();
            this.TxtGender = new System.Windows.Forms.TextBox();
            this.TxtBirthDate = new System.Windows.Forms.TextBox();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.gbLoadKeys.SuspendLayout();
            this.gbAuth.SuspendLayout();
            this.gbKType.SuspendLayout();
            this.gbSource.SuspendLayout();
            this.gbBinOps.SuspendLayout();
            this.gbValBlk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dReadAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePict)).BeginInit();
            this.gbProfileCard.SuspendLayout();
            this.SuspendLayout();
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
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(551, 620);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(81, 23);
            this.bReset.TabIndex = 36;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(464, 620);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(81, 23);
            this.bClear.TabIndex = 35;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(192, 78);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(117, 23);
            this.bConnect.TabIndex = 29;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(192, 49);
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
            this.cbReader.Location = new System.Drawing.Point(93, 23);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(216, 21);
            this.cbReader.TabIndex = 27;
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(638, 620);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(81, 23);
            this.bQuit.TabIndex = 37;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // btnGetUID
            // 
            this.btnGetUID.Location = new System.Drawing.Point(69, 78);
            this.btnGetUID.Name = "btnGetUID";
            this.btnGetUID.Size = new System.Drawing.Size(117, 23);
            this.btnGetUID.TabIndex = 38;
            this.btnGetUID.Text = "Get UID";
            this.btnGetUID.UseVisualStyleBackColor = true;
            this.btnGetUID.Click += new System.EventHandler(this.btnGetUID_Click);
            // 
            // mMsg
            // 
            this.mMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mMsg.FormattingEnabled = true;
            this.mMsg.Location = new System.Drawing.Point(334, 232);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(385, 143);
            this.mMsg.TabIndex = 34;
            // 
            // rbNonVolMem
            // 
            this.rbNonVolMem.AutoSize = true;
            this.rbNonVolMem.Location = new System.Drawing.Point(6, 28);
            this.rbNonVolMem.Name = "rbNonVolMem";
            this.rbNonVolMem.Size = new System.Drawing.Size(122, 17);
            this.rbNonVolMem.TabIndex = 39;
            this.rbNonVolMem.TabStop = true;
            this.rbNonVolMem.Text = "Non-Volatile Memory";
            this.rbNonVolMem.UseVisualStyleBackColor = true;
            this.rbNonVolMem.CheckedChanged += new System.EventHandler(this.rbNonVolMem_CheckedChanged);
            // 
            // rbVolMem
            // 
            this.rbVolMem.AutoSize = true;
            this.rbVolMem.Location = new System.Drawing.Point(144, 27);
            this.rbVolMem.Name = "rbVolMem";
            this.rbVolMem.Size = new System.Drawing.Size(99, 17);
            this.rbVolMem.TabIndex = 40;
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
            this.gbLoadKeys.Controls.Add(this.tKey2);
            this.gbLoadKeys.Controls.Add(this.tKey1);
            this.gbLoadKeys.Controls.Add(this.tMemAdd);
            this.gbLoadKeys.Controls.Add(this.label3);
            this.gbLoadKeys.Controls.Add(this.label2);
            this.gbLoadKeys.Controls.Add(this.rbNonVolMem);
            this.gbLoadKeys.Controls.Add(this.rbVolMem);
            this.gbLoadKeys.Location = new System.Drawing.Point(15, 114);
            this.gbLoadKeys.Name = "gbLoadKeys";
            this.gbLoadKeys.Size = new System.Drawing.Size(293, 153);
            this.gbLoadKeys.TabIndex = 30;
            this.gbLoadKeys.TabStop = false;
            this.gbLoadKeys.Text = "Store Authentication Keys to Device";
            // 
            // bLoadKey
            // 
            this.bLoadKey.Location = new System.Drawing.Point(197, 114);
            this.bLoadKey.Name = "bLoadKey";
            this.bLoadKey.Size = new System.Drawing.Size(75, 23);
            this.bLoadKey.TabIndex = 48;
            this.bLoadKey.Text = "Load key";
            this.bLoadKey.UseVisualStyleBackColor = true;
            this.bLoadKey.Click += new System.EventHandler(this.bLoadKey_Click);
            // 
            // tKey6
            // 
            this.tKey6.Location = new System.Drawing.Point(249, 85);
            this.tKey6.MaxLength = 2;
            this.tKey6.Name = "tKey6";
            this.tKey6.Size = new System.Drawing.Size(23, 20);
            this.tKey6.TabIndex = 47;
            // 
            // tKey5
            // 
            this.tKey5.Location = new System.Drawing.Point(220, 85);
            this.tKey5.MaxLength = 2;
            this.tKey5.Name = "tKey5";
            this.tKey5.Size = new System.Drawing.Size(23, 20);
            this.tKey5.TabIndex = 46;
            // 
            // tKey4
            // 
            this.tKey4.Location = new System.Drawing.Point(192, 85);
            this.tKey4.MaxLength = 2;
            this.tKey4.Name = "tKey4";
            this.tKey4.Size = new System.Drawing.Size(23, 20);
            this.tKey4.TabIndex = 45;
            // 
            // tKey3
            // 
            this.tKey3.Location = new System.Drawing.Point(163, 85);
            this.tKey3.MaxLength = 2;
            this.tKey3.Name = "tKey3";
            this.tKey3.Size = new System.Drawing.Size(23, 20);
            this.tKey3.TabIndex = 44;
            // 
            // tKey2
            // 
            this.tKey2.Location = new System.Drawing.Point(134, 85);
            this.tKey2.MaxLength = 2;
            this.tKey2.Name = "tKey2";
            this.tKey2.Size = new System.Drawing.Size(23, 20);
            this.tKey2.TabIndex = 43;
            // 
            // tKey1
            // 
            this.tKey1.Location = new System.Drawing.Point(105, 85);
            this.tKey1.MaxLength = 2;
            this.tKey1.Name = "tKey1";
            this.tKey1.Size = new System.Drawing.Size(23, 20);
            this.tKey1.TabIndex = 42;
            // 
            // tMemAdd
            // 
            this.tMemAdd.Location = new System.Drawing.Point(105, 56);
            this.tMemAdd.MaxLength = 2;
            this.tMemAdd.Name = "tMemAdd";
            this.tMemAdd.Size = new System.Drawing.Size(23, 20);
            this.tMemAdd.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Key Value Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Key Store No.";
            // 
            // gbAuth
            // 
            this.gbAuth.Controls.Add(this.bAuth);
            this.gbAuth.Controls.Add(this.tKeyIn6);
            this.gbAuth.Controls.Add(this.tKeyIn5);
            this.gbAuth.Controls.Add(this.tKeyIn4);
            this.gbAuth.Controls.Add(this.tKeyIn3);
            this.gbAuth.Controls.Add(this.tKeyIn2);
            this.gbAuth.Controls.Add(this.tKeyIn1);
            this.gbAuth.Controls.Add(this.tKeyAdd);
            this.gbAuth.Controls.Add(this.tBlkNo);
            this.gbAuth.Controls.Add(this.label6);
            this.gbAuth.Controls.Add(this.label5);
            this.gbAuth.Controls.Add(this.label4);
            this.gbAuth.Controls.Add(this.gbKType);
            this.gbAuth.Controls.Add(this.gbSource);
            this.gbAuth.Location = new System.Drawing.Point(15, 273);
            this.gbAuth.Name = "gbAuth";
            this.gbAuth.Size = new System.Drawing.Size(293, 233);
            this.gbAuth.TabIndex = 39;
            this.gbAuth.TabStop = false;
            this.gbAuth.Text = "Authentication Function";
            // 
            // bAuth
            // 
            this.bAuth.Location = new System.Drawing.Point(180, 198);
            this.bAuth.Name = "bAuth";
            this.bAuth.Size = new System.Drawing.Size(94, 23);
            this.bAuth.TabIndex = 51;
            this.bAuth.Text = "Authenticate";
            this.bAuth.UseVisualStyleBackColor = true;
            this.bAuth.Click += new System.EventHandler(this.bAuth_Click);
            // 
            // tKeyIn6
            // 
            this.tKeyIn6.Location = new System.Drawing.Point(250, 170);
            this.tKeyIn6.MaxLength = 2;
            this.tKeyIn6.Name = "tKeyIn6";
            this.tKeyIn6.Size = new System.Drawing.Size(23, 20);
            this.tKeyIn6.TabIndex = 50;
            // 
            // tKeyIn5
            // 
            this.tKeyIn5.Location = new System.Drawing.Point(221, 170);
            this.tKeyIn5.MaxLength = 2;
            this.tKeyIn5.Name = "tKeyIn5";
            this.tKeyIn5.Size = new System.Drawing.Size(23, 20);
            this.tKeyIn5.TabIndex = 49;
            // 
            // tKeyIn4
            // 
            this.tKeyIn4.Location = new System.Drawing.Point(192, 170);
            this.tKeyIn4.MaxLength = 2;
            this.tKeyIn4.Name = "tKeyIn4";
            this.tKeyIn4.Size = new System.Drawing.Size(23, 20);
            this.tKeyIn4.TabIndex = 48;
            // 
            // tKeyIn3
            // 
            this.tKeyIn3.Location = new System.Drawing.Point(163, 170);
            this.tKeyIn3.MaxLength = 2;
            this.tKeyIn3.Name = "tKeyIn3";
            this.tKeyIn3.Size = new System.Drawing.Size(23, 20);
            this.tKeyIn3.TabIndex = 47;
            // 
            // tKeyIn2
            // 
            this.tKeyIn2.Location = new System.Drawing.Point(134, 170);
            this.tKeyIn2.MaxLength = 2;
            this.tKeyIn2.Name = "tKeyIn2";
            this.tKeyIn2.Size = new System.Drawing.Size(23, 20);
            this.tKeyIn2.TabIndex = 46;
            // 
            // tKeyIn1
            // 
            this.tKeyIn1.Location = new System.Drawing.Point(105, 170);
            this.tKeyIn1.MaxLength = 2;
            this.tKeyIn1.Name = "tKeyIn1";
            this.tKeyIn1.Size = new System.Drawing.Size(23, 20);
            this.tKeyIn1.TabIndex = 45;
            // 
            // tKeyAdd
            // 
            this.tKeyAdd.Location = new System.Drawing.Point(105, 146);
            this.tKeyAdd.MaxLength = 2;
            this.tKeyAdd.Name = "tKeyAdd";
            this.tKeyAdd.Size = new System.Drawing.Size(23, 20);
            this.tKeyAdd.TabIndex = 44;
            // 
            // tBlkNo
            // 
            this.tBlkNo.Location = new System.Drawing.Point(105, 122);
            this.tBlkNo.MaxLength = 2;
            this.tBlkNo.Name = "tBlkNo";
            this.tBlkNo.Size = new System.Drawing.Size(23, 20);
            this.tBlkNo.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Key Value Input";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Key Store Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Block No. (Dec)";
            // 
            // gbKType
            // 
            this.gbKType.Controls.Add(this.rbKType2);
            this.gbKType.Controls.Add(this.rbKType1);
            this.gbKType.Location = new System.Drawing.Point(149, 19);
            this.gbKType.Name = "gbKType";
            this.gbKType.Size = new System.Drawing.Size(125, 93);
            this.gbKType.TabIndex = 1;
            this.gbKType.TabStop = false;
            this.gbKType.Text = "Key Type";
            // 
            // rbKType2
            // 
            this.rbKType2.AutoSize = true;
            this.rbKType2.Location = new System.Drawing.Point(12, 42);
            this.rbKType2.Name = "rbKType2";
            this.rbKType2.Size = new System.Drawing.Size(53, 17);
            this.rbKType2.TabIndex = 1;
            this.rbKType2.TabStop = true;
            this.rbKType2.Text = "Key B";
            this.rbKType2.UseVisualStyleBackColor = true;
            // 
            // rbKType1
            // 
            this.rbKType1.AutoSize = true;
            this.rbKType1.Location = new System.Drawing.Point(12, 19);
            this.rbKType1.Name = "rbKType1";
            this.rbKType1.Size = new System.Drawing.Size(53, 17);
            this.rbKType1.TabIndex = 0;
            this.rbKType1.TabStop = true;
            this.rbKType1.Text = "Key A";
            this.rbKType1.UseVisualStyleBackColor = true;
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.rbSource3);
            this.gbSource.Controls.Add(this.rbSource2);
            this.gbSource.Controls.Add(this.rbSource1);
            this.gbSource.Location = new System.Drawing.Point(6, 19);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(137, 93);
            this.gbSource.TabIndex = 0;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Key Source";
            // 
            // rbSource3
            // 
            this.rbSource3.AutoSize = true;
            this.rbSource3.Location = new System.Drawing.Point(12, 65);
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
            this.rbSource2.Location = new System.Drawing.Point(12, 42);
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
            this.rbSource1.Location = new System.Drawing.Point(12, 19);
            this.rbSource1.Name = "rbSource1";
            this.rbSource1.Size = new System.Drawing.Size(87, 17);
            this.rbSource1.TabIndex = 0;
            this.rbSource1.TabStop = true;
            this.rbSource1.Text = "Manual Input";
            this.rbSource1.UseVisualStyleBackColor = true;
            this.rbSource1.CheckedChanged += new System.EventHandler(this.rbSource1_CheckedChanged);
            // 
            // gbBinOps
            // 
            this.gbBinOps.Controls.Add(this.label14);
            this.gbBinOps.Controls.Add(this.bHexUpd);
            this.gbBinOps.Controls.Add(this.bHexRead);
            this.gbBinOps.Controls.Add(this.tbHextoStr);
            this.gbBinOps.Controls.Add(this.tBinLen);
            this.gbBinOps.Controls.Add(this.tBinBlk);
            this.gbBinOps.Controls.Add(this.label8);
            this.gbBinOps.Controls.Add(this.label7);
            this.gbBinOps.Location = new System.Drawing.Point(15, 512);
            this.gbBinOps.Name = "gbBinOps";
            this.gbBinOps.Size = new System.Drawing.Size(293, 147);
            this.gbBinOps.TabIndex = 49;
            this.gbBinOps.TabStop = false;
            this.gbBinOps.Text = "Binary Block Functions";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 57;
            this.label14.Text = "In Hexa String";
            // 
            // bHexUpd
            // 
            this.bHexUpd.Location = new System.Drawing.Point(78, 108);
            this.bHexUpd.Name = "bHexUpd";
            this.bHexUpd.Size = new System.Drawing.Size(94, 23);
            this.bHexUpd.TabIndex = 56;
            this.bHexUpd.Text = "Update Block";
            this.bHexUpd.UseVisualStyleBackColor = true;
            this.bHexUpd.Click += new System.EventHandler(this.bHexUpd_Click);
            // 
            // bHexRead
            // 
            this.bHexRead.Location = new System.Drawing.Point(174, 108);
            this.bHexRead.Name = "bHexRead";
            this.bHexRead.Size = new System.Drawing.Size(94, 23);
            this.bHexRead.TabIndex = 55;
            this.bHexRead.Text = "Read Block";
            this.bHexRead.UseVisualStyleBackColor = true;
            this.bHexRead.Click += new System.EventHandler(this.bHexRead_Click);
            // 
            // tbHextoStr
            // 
            this.tbHextoStr.Location = new System.Drawing.Point(9, 82);
            this.tbHextoStr.MaxLength = 32;
            this.tbHextoStr.Name = "tbHextoStr";
            this.tbHextoStr.Size = new System.Drawing.Size(259, 20);
            this.tbHextoStr.TabIndex = 54;
            // 
            // tBinLen
            // 
            this.tBinLen.Location = new System.Drawing.Point(233, 19);
            this.tBinLen.MaxLength = 2;
            this.tBinLen.Name = "tBinLen";
            this.tBinLen.Size = new System.Drawing.Size(35, 20);
            this.tBinLen.TabIndex = 9;
            // 
            // tBinBlk
            // 
            this.tBinBlk.Location = new System.Drawing.Point(105, 19);
            this.tBinBlk.MaxLength = 2;
            this.tBinBlk.Name = "tBinBlk";
            this.tBinBlk.Size = new System.Drawing.Size(35, 20);
            this.tBinBlk.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(158, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Length (Dec)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Start Block (Dec)";
            // 
            // gbValBlk
            // 
            this.gbValBlk.Controls.Add(this.bValRes);
            this.gbValBlk.Controls.Add(this.bValRead);
            this.gbValBlk.Controls.Add(this.bValDec);
            this.gbValBlk.Controls.Add(this.bValInc);
            this.gbValBlk.Controls.Add(this.bValStor);
            this.gbValBlk.Controls.Add(this.tValTar);
            this.gbValBlk.Controls.Add(this.tValSrc);
            this.gbValBlk.Controls.Add(this.tValBlk);
            this.gbValBlk.Controls.Add(this.tValAmt);
            this.gbValBlk.Controls.Add(this.label13);
            this.gbValBlk.Controls.Add(this.label12);
            this.gbValBlk.Controls.Add(this.label11);
            this.gbValBlk.Controls.Add(this.label10);
            this.gbValBlk.Location = new System.Drawing.Point(334, 23);
            this.gbValBlk.Name = "gbValBlk";
            this.gbValBlk.Size = new System.Drawing.Size(385, 192);
            this.gbValBlk.TabIndex = 50;
            this.gbValBlk.TabStop = false;
            this.gbValBlk.Text = "Value Block Function";
            // 
            // bValRes
            // 
            this.bValRes.Location = new System.Drawing.Point(252, 147);
            this.bValRes.Name = "bValRes";
            this.bValRes.Size = new System.Drawing.Size(117, 23);
            this.bValRes.TabIndex = 33;
            this.bValRes.Text = "Restore Value";
            this.bValRes.UseVisualStyleBackColor = true;
            this.bValRes.Click += new System.EventHandler(this.bValRes_Click);
            // 
            // bValRead
            // 
            this.bValRead.Location = new System.Drawing.Point(252, 118);
            this.bValRead.Name = "bValRead";
            this.bValRead.Size = new System.Drawing.Size(117, 23);
            this.bValRead.TabIndex = 32;
            this.bValRead.Text = "Read Value";
            this.bValRead.UseVisualStyleBackColor = true;
            this.bValRead.Click += new System.EventHandler(this.bValRead_Click);
            // 
            // bValDec
            // 
            this.bValDec.Location = new System.Drawing.Point(252, 88);
            this.bValDec.Name = "bValDec";
            this.bValDec.Size = new System.Drawing.Size(117, 23);
            this.bValDec.TabIndex = 31;
            this.bValDec.Text = "Decrement";
            this.bValDec.UseVisualStyleBackColor = true;
            this.bValDec.Click += new System.EventHandler(this.bValDec_Click);
            // 
            // bValInc
            // 
            this.bValInc.Location = new System.Drawing.Point(252, 59);
            this.bValInc.Name = "bValInc";
            this.bValInc.Size = new System.Drawing.Size(117, 23);
            this.bValInc.TabIndex = 30;
            this.bValInc.Text = "Increment";
            this.bValInc.UseVisualStyleBackColor = true;
            this.bValInc.Click += new System.EventHandler(this.bValInc_Click);
            // 
            // bValStor
            // 
            this.bValStor.Location = new System.Drawing.Point(252, 31);
            this.bValStor.Name = "bValStor";
            this.bValStor.Size = new System.Drawing.Size(117, 23);
            this.bValStor.TabIndex = 29;
            this.bValStor.Text = "Store Value";
            this.bValStor.UseVisualStyleBackColor = true;
            this.bValStor.Click += new System.EventHandler(this.bValStor_Click);
            // 
            // tValTar
            // 
            this.tValTar.Location = new System.Drawing.Point(89, 120);
            this.tValTar.MaxLength = 2;
            this.tValTar.Name = "tValTar";
            this.tValTar.Size = new System.Drawing.Size(50, 20);
            this.tValTar.TabIndex = 14;
            // 
            // tValSrc
            // 
            this.tValSrc.Location = new System.Drawing.Point(89, 91);
            this.tValSrc.MaxLength = 2;
            this.tValSrc.Name = "tValSrc";
            this.tValSrc.Size = new System.Drawing.Size(50, 20);
            this.tValSrc.TabIndex = 13;
            // 
            // tValBlk
            // 
            this.tValBlk.Location = new System.Drawing.Point(89, 62);
            this.tValBlk.MaxLength = 2;
            this.tValBlk.Name = "tValBlk";
            this.tValBlk.Size = new System.Drawing.Size(50, 20);
            this.tValBlk.TabIndex = 12;
            // 
            // tValAmt
            // 
            this.tValAmt.Location = new System.Drawing.Point(89, 33);
            this.tValAmt.MaxLength = 2;
            this.tValAmt.Name = "tValAmt";
            this.tValAmt.Size = new System.Drawing.Size(131, 20);
            this.tValAmt.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Target Block";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Source Block";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Block No.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Value Amount";
            // 
            // bReadAll
            // 
            this.bReadAll.Location = new System.Drawing.Point(377, 620);
            this.bReadAll.Name = "bReadAll";
            this.bReadAll.Size = new System.Drawing.Size(81, 23);
            this.bReadAll.TabIndex = 51;
            this.bReadAll.Text = "Read All";
            this.bReadAll.UseVisualStyleBackColor = true;
            this.bReadAll.Click += new System.EventHandler(this.bReadAll_Click);
            // 
            // dReadAll
            // 
            this.dReadAll.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dReadAll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dReadAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dReadAll.GridColor = System.Drawing.SystemColors.InfoText;
            this.dReadAll.Location = new System.Drawing.Point(334, 377);
            this.dReadAll.Name = "dReadAll";
            this.dReadAll.Size = new System.Drawing.Size(385, 222);
            this.dReadAll.TabIndex = 52;
            // 
            // ProfilePict
            // 
            this.ProfilePict.Location = new System.Drawing.Point(116, 19);
            this.ProfilePict.Name = "ProfilePict";
            this.ProfilePict.Size = new System.Drawing.Size(100, 120);
            this.ProfilePict.TabIndex = 53;
            this.ProfilePict.TabStop = false;
            this.ProfilePict.Click += new System.EventHandler(this.ProfilePict_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 239);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Address";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 215);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 56;
            this.label15.Text = "Gender";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 187);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 13);
            this.label16.TabIndex = 55;
            this.label16.Text = "Date of Birth";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 162);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 54;
            this.label17.Text = "Full Name";
            // 
            // gbProfileCard
            // 
            this.gbProfileCard.Controls.Add(this.BtnReset);
            this.gbProfileCard.Controls.Add(this.BtnConfirm);
            this.gbProfileCard.Controls.Add(this.lblAddPhoto);
            this.gbProfileCard.Controls.Add(this.TxtNumber);
            this.gbProfileCard.Controls.Add(this.TxtAddress);
            this.gbProfileCard.Controls.Add(this.TxtGender);
            this.gbProfileCard.Controls.Add(this.TxtBirthDate);
            this.gbProfileCard.Controls.Add(this.TxtName);
            this.gbProfileCard.Controls.Add(this.label18);
            this.gbProfileCard.Controls.Add(this.label15);
            this.gbProfileCard.Controls.Add(this.ProfilePict);
            this.gbProfileCard.Controls.Add(this.label9);
            this.gbProfileCard.Controls.Add(this.label17);
            this.gbProfileCard.Controls.Add(this.label16);
            this.gbProfileCard.Location = new System.Drawing.Point(746, 23);
            this.gbProfileCard.Name = "gbProfileCard";
            this.gbProfileCard.Size = new System.Drawing.Size(316, 412);
            this.gbProfileCard.TabIndex = 34;
            this.gbProfileCard.TabStop = false;
            this.gbProfileCard.Text = "Profile Card";
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(142, 369);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(75, 23);
            this.BtnReset.TabIndex = 65;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Location = new System.Drawing.Point(223, 369);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(75, 23);
            this.BtnConfirm.TabIndex = 64;
            this.BtnConfirm.Text = "Confirm";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // lblAddPhoto
            // 
            this.lblAddPhoto.AutoSize = true;
            this.lblAddPhoto.Location = new System.Drawing.Point(137, 71);
            this.lblAddPhoto.Name = "lblAddPhoto";
            this.lblAddPhoto.Size = new System.Drawing.Size(57, 13);
            this.lblAddPhoto.TabIndex = 63;
            this.lblAddPhoto.Text = "Add Photo";
            this.lblAddPhoto.Click += new System.EventHandler(this.lblAddPhoto_Click);
            // 
            // TxtNumber
            // 
            this.TxtNumber.Location = new System.Drawing.Point(106, 310);
            this.TxtNumber.MaxLength = 15;
            this.TxtNumber.Name = "TxtNumber";
            this.TxtNumber.Size = new System.Drawing.Size(192, 20);
            this.TxtNumber.TabIndex = 62;
            // 
            // TxtAddress
            // 
            this.TxtAddress.Location = new System.Drawing.Point(106, 236);
            this.TxtAddress.MaxLength = 150;
            this.TxtAddress.Multiline = true;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(192, 68);
            this.TxtAddress.TabIndex = 61;
            // 
            // TxtGender
            // 
            this.TxtGender.Location = new System.Drawing.Point(106, 210);
            this.TxtGender.MaxLength = 10;
            this.TxtGender.Name = "TxtGender";
            this.TxtGender.Size = new System.Drawing.Size(192, 20);
            this.TxtGender.TabIndex = 60;
            // 
            // TxtBirthDate
            // 
            this.TxtBirthDate.Location = new System.Drawing.Point(106, 184);
            this.TxtBirthDate.MaxLength = 30;
            this.TxtBirthDate.Name = "TxtBirthDate";
            this.TxtBirthDate.Size = new System.Drawing.Size(192, 20);
            this.TxtBirthDate.TabIndex = 59;
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(106, 159);
            this.TxtName.MaxLength = 100;
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(192, 20);
            this.TxtName.TabIndex = 34;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 313);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 13);
            this.label18.TabIndex = 58;
            this.label18.Text = "Contact Number";
            // 
            // MainMifareProg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 671);
            this.Controls.Add(this.gbProfileCard);
            this.Controls.Add(this.dReadAll);
            this.Controls.Add(this.bReadAll);
            this.Controls.Add(this.gbValBlk);
            this.Controls.Add(this.gbBinOps);
            this.Controls.Add(this.gbAuth);
            this.Controls.Add(this.gbLoadKeys);
            this.Controls.Add(this.btnGetUID);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.bQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMifareProg";
            this.Text = "Mifare Card Programming - Yanuar Rev";
            this.Load += new System.EventHandler(this.MainMifareProg_Load);
            this.gbLoadKeys.ResumeLayout(false);
            this.gbLoadKeys.PerformLayout();
            this.gbAuth.ResumeLayout(false);
            this.gbAuth.PerformLayout();
            this.gbKType.ResumeLayout(false);
            this.gbKType.PerformLayout();
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.gbBinOps.ResumeLayout(false);
            this.gbBinOps.PerformLayout();
            this.gbValBlk.ResumeLayout(false);
            this.gbValBlk.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dReadAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePict)).EndInit();
            this.gbProfileCard.ResumeLayout(false);
            this.gbProfileCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.Button btnGetUID;
        internal System.Windows.Forms.ListBox mMsg;
        private System.Windows.Forms.RadioButton rbNonVolMem;
        private System.Windows.Forms.RadioButton rbVolMem;
        internal System.Windows.Forms.GroupBox gbLoadKeys;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox tMemAdd;
        internal System.Windows.Forms.TextBox tKey6;
        internal System.Windows.Forms.TextBox tKey5;
        internal System.Windows.Forms.TextBox tKey4;
        internal System.Windows.Forms.TextBox tKey3;
        internal System.Windows.Forms.TextBox tKey2;
        internal System.Windows.Forms.TextBox tKey1;
        internal System.Windows.Forms.Button bLoadKey;
        private System.Windows.Forms.GroupBox gbAuth;
        private System.Windows.Forms.GroupBox gbKType;
        private System.Windows.Forms.GroupBox gbSource;
        internal System.Windows.Forms.RadioButton rbSource3;
        internal System.Windows.Forms.RadioButton rbSource2;
        internal System.Windows.Forms.RadioButton rbSource1;
        internal System.Windows.Forms.RadioButton rbKType2;
        internal System.Windows.Forms.RadioButton rbKType1;
        internal System.Windows.Forms.Button bAuth;
        internal System.Windows.Forms.TextBox tKeyIn6;
        internal System.Windows.Forms.TextBox tKeyIn5;
        internal System.Windows.Forms.TextBox tKeyIn4;
        internal System.Windows.Forms.TextBox tKeyIn3;
        internal System.Windows.Forms.TextBox tKeyIn2;
        internal System.Windows.Forms.TextBox tKeyIn1;
        internal System.Windows.Forms.TextBox tKeyAdd;
        internal System.Windows.Forms.TextBox tBlkNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbBinOps;
        internal System.Windows.Forms.TextBox tBinLen;
        internal System.Windows.Forms.TextBox tBinBlk;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbValBlk;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button bValRes;
        internal System.Windows.Forms.Button bValRead;
        internal System.Windows.Forms.Button bValDec;
        internal System.Windows.Forms.Button bValInc;
        internal System.Windows.Forms.Button bValStor;
        internal System.Windows.Forms.TextBox tValTar;
        internal System.Windows.Forms.TextBox tValSrc;
        internal System.Windows.Forms.TextBox tValBlk;
        internal System.Windows.Forms.TextBox tValAmt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Button bHexUpd;
        internal System.Windows.Forms.Button bHexRead;
        internal System.Windows.Forms.TextBox tbHextoStr;
        internal System.Windows.Forms.Button bReadAll;
        private System.Windows.Forms.DataGridView dReadAll;
        private System.Windows.Forms.PictureBox ProfilePict;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox gbProfileCard;
        internal System.Windows.Forms.TextBox TxtNumber;
        internal System.Windows.Forms.TextBox TxtAddress;
        internal System.Windows.Forms.TextBox TxtGender;
        internal System.Windows.Forms.TextBox TxtBirthDate;
        internal System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblAddPhoto;
        internal System.Windows.Forms.Button BtnReset;
        internal System.Windows.Forms.Button BtnConfirm;
    }
}

