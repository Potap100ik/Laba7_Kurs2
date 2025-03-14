﻿namespace LR7_LastOne
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbShop_OtBaldy = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbManufact_OtBaldy = new System.Windows.Forms.TextBox();
            this.nudPrice_OtBaldy = new System.Windows.Forms.NumericUpDown();
            this.rbMFP = new System.Windows.Forms.RadioButton();
            this.rbScanner = new System.Windows.Forms.RadioButton();
            this.rbPrinter = new System.Windows.Forms.RadioButton();
            this.rbDevice = new System.Windows.Forms.RadioButton();
            this.gbInterface = new System.Windows.Forms.GroupBox();
            this.cbDNS = new System.Windows.Forms.CheckBox();
            this.bEnterDNS = new System.Windows.Forms.Button();
            this.bBuy_OtBaldy = new System.Windows.Forms.Button();
            this.bInventoryShow = new System.Windows.Forms.Button();
            this.bMainDeviceSwitch = new System.Windows.Forms.Button();
            this.gbListOfDevices = new System.Windows.Forms.GroupBox();
            this.dgvListOfDevices = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlugStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssembleStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbMainDevice = new System.Windows.Forms.GroupBox();
            this.tbMainType = new System.Windows.Forms.Label();
            this.panelAssembleStat = new System.Windows.Forms.Panel();
            this.panelPlugStat = new System.Windows.Forms.Panel();
            this.tbMainManuf = new System.Windows.Forms.TextBox();
            this.tbPaperCount = new System.Windows.Forms.TextBox();
            this.tbMainPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbManagerBut = new System.Windows.Forms.GroupBox();
            this.bThrowDev = new System.Windows.Forms.Button();
            this.bDisassShop = new System.Windows.Forms.Button();
            this.bDisassSam = new System.Windows.Forms.Button();
            this.bAssem = new System.Windows.Forms.Button();
            this.bPlugSwitch = new System.Windows.Forms.Button();
            this.gbPrintScanCopy = new System.Windows.Forms.GroupBox();
            this.bCopy = new System.Windows.Forms.Button();
            this.bPaperCash = new System.Windows.Forms.Button();
            this.bScan = new System.Windows.Forms.Button();
            this.bPrint = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbMassages = new System.Windows.Forms.GroupBox();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.gbBuyLeave = new System.Windows.Forms.GroupBox();
            this.bLeaveDNS = new System.Windows.Forms.Button();
            this.bBuyForFree = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gbShop_OtBaldy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice_OtBaldy)).BeginInit();
            this.gbInterface.SuspendLayout();
            this.gbListOfDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListOfDevices)).BeginInit();
            this.gbMainDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbManagerBut.SuspendLayout();
            this.gbPrintScanCopy.SuspendLayout();
            this.gbMassages.SuspendLayout();
            this.gbBuyLeave.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbShop_OtBaldy
            // 
            this.gbShop_OtBaldy.Controls.Add(this.label2);
            this.gbShop_OtBaldy.Controls.Add(this.label1);
            this.gbShop_OtBaldy.Controls.Add(this.tbManufact_OtBaldy);
            this.gbShop_OtBaldy.Controls.Add(this.nudPrice_OtBaldy);
            this.gbShop_OtBaldy.Controls.Add(this.rbMFP);
            this.gbShop_OtBaldy.Controls.Add(this.rbScanner);
            this.gbShop_OtBaldy.Controls.Add(this.rbPrinter);
            this.gbShop_OtBaldy.Controls.Add(this.rbDevice);
            this.gbShop_OtBaldy.Location = new System.Drawing.Point(1352, 141);
            this.gbShop_OtBaldy.Name = "gbShop_OtBaldy";
            this.gbShop_OtBaldy.Size = new System.Drawing.Size(334, 292);
            this.gbShop_OtBaldy.TabIndex = 0;
            this.gbShop_OtBaldy.TabStop = false;
            this.gbShop_OtBaldy.Text = "Индивидуальный заказ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Производитель";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Цена";
            // 
            // tbManufact_OtBaldy
            // 
            this.tbManufact_OtBaldy.Location = new System.Drawing.Point(7, 265);
            this.tbManufact_OtBaldy.Name = "tbManufact_OtBaldy";
            this.tbManufact_OtBaldy.Size = new System.Drawing.Size(196, 26);
            this.tbManufact_OtBaldy.TabIndex = 2;
            this.tbManufact_OtBaldy.Text = "Soni";
            // 
            // nudPrice_OtBaldy
            // 
            this.nudPrice_OtBaldy.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPrice_OtBaldy.InterceptArrowKeys = false;
            this.nudPrice_OtBaldy.Location = new System.Drawing.Point(7, 199);
            this.nudPrice_OtBaldy.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPrice_OtBaldy.Name = "nudPrice_OtBaldy";
            this.nudPrice_OtBaldy.Size = new System.Drawing.Size(196, 26);
            this.nudPrice_OtBaldy.TabIndex = 1;
            this.nudPrice_OtBaldy.ThousandsSeparator = true;
            this.nudPrice_OtBaldy.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // rbMFP
            // 
            this.rbMFP.AutoSize = true;
            this.rbMFP.Location = new System.Drawing.Point(6, 136);
            this.rbMFP.Name = "rbMFP";
            this.rbMFP.Size = new System.Drawing.Size(72, 24);
            this.rbMFP.TabIndex = 0;
            this.rbMFP.TabStop = true;
            this.rbMFP.Text = "МФУ";
            this.rbMFP.UseVisualStyleBackColor = true;
            // 
            // rbScanner
            // 
            this.rbScanner.AutoSize = true;
            this.rbScanner.Location = new System.Drawing.Point(6, 106);
            this.rbScanner.Name = "rbScanner";
            this.rbScanner.Size = new System.Drawing.Size(98, 24);
            this.rbScanner.TabIndex = 0;
            this.rbScanner.TabStop = true;
            this.rbScanner.Text = "Сканнер";
            this.rbScanner.UseVisualStyleBackColor = true;
            // 
            // rbPrinter
            // 
            this.rbPrinter.AutoSize = true;
            this.rbPrinter.Location = new System.Drawing.Point(7, 76);
            this.rbPrinter.Name = "rbPrinter";
            this.rbPrinter.Size = new System.Drawing.Size(100, 24);
            this.rbPrinter.TabIndex = 0;
            this.rbPrinter.TabStop = true;
            this.rbPrinter.Text = "Принтер";
            this.rbPrinter.UseVisualStyleBackColor = true;
            // 
            // rbDevice
            // 
            this.rbDevice.AutoSize = true;
            this.rbDevice.Checked = true;
            this.rbDevice.Location = new System.Drawing.Point(7, 46);
            this.rbDevice.Name = "rbDevice";
            this.rbDevice.Size = new System.Drawing.Size(123, 24);
            this.rbDevice.TabIndex = 0;
            this.rbDevice.TabStop = true;
            this.rbDevice.Text = "Устройство";
            this.rbDevice.UseVisualStyleBackColor = true;
            // 
            // gbInterface
            // 
            this.gbInterface.Controls.Add(this.cbDNS);
            this.gbInterface.Controls.Add(this.bEnterDNS);
            this.gbInterface.Controls.Add(this.bBuy_OtBaldy);
            this.gbInterface.Controls.Add(this.bInventoryShow);
            this.gbInterface.Location = new System.Drawing.Point(1186, 13);
            this.gbInterface.Name = "gbInterface";
            this.gbInterface.Size = new System.Drawing.Size(500, 111);
            this.gbInterface.TabIndex = 2;
            this.gbInterface.TabStop = false;
            this.gbInterface.Text = "Кнопочки";
            // 
            // cbDNS
            // 
            this.cbDNS.AutoSize = true;
            this.cbDNS.Location = new System.Drawing.Point(276, 83);
            this.cbDNS.Name = "cbDNS";
            this.cbDNS.Size = new System.Drawing.Size(219, 24);
            this.cbDNS.TabIndex = 4;
            this.cbDNS.Text = "Индивидуальный выбор";
            this.cbDNS.UseVisualStyleBackColor = true;
            // 
            // bEnterDNS
            // 
            this.bEnterDNS.BackColor = System.Drawing.Color.Black;
            this.bEnterDNS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bEnterDNS.BackgroundImage")));
            this.bEnterDNS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bEnterDNS.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.bEnterDNS.FlatAppearance.BorderSize = 3;
            this.bEnterDNS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bEnterDNS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.bEnterDNS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnterDNS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bEnterDNS.Location = new System.Drawing.Point(274, 22);
            this.bEnterDNS.Name = "bEnterDNS";
            this.bEnterDNS.Size = new System.Drawing.Size(218, 55);
            this.bEnterDNS.TabIndex = 3;
            this.bEnterDNS.UseVisualStyleBackColor = false;
            this.bEnterDNS.Click += new System.EventHandler(this.bEnterDNS_Click);
            // 
            // bBuy_OtBaldy
            // 
            this.bBuy_OtBaldy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBuy_OtBaldy.Location = new System.Drawing.Point(116, 22);
            this.bBuy_OtBaldy.Name = "bBuy_OtBaldy";
            this.bBuy_OtBaldy.Size = new System.Drawing.Size(155, 86);
            this.bBuy_OtBaldy.TabIndex = 2;
            this.bBuy_OtBaldy.Text = "Личный заказ";
            this.bBuy_OtBaldy.UseVisualStyleBackColor = true;
            this.bBuy_OtBaldy.Click += new System.EventHandler(this.bBuy_OtBaldy_Click);
            // 
            // bInventoryShow
            // 
            this.bInventoryShow.Dock = System.Windows.Forms.DockStyle.Left;
            this.bInventoryShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bInventoryShow.Location = new System.Drawing.Point(3, 22);
            this.bInventoryShow.Name = "bInventoryShow";
            this.bInventoryShow.Size = new System.Drawing.Size(110, 86);
            this.bInventoryShow.TabIndex = 1;
            this.bInventoryShow.Text = "Инвентарь";
            this.bInventoryShow.UseVisualStyleBackColor = true;
            this.bInventoryShow.Click += new System.EventHandler(this.bShowInventory_Click);
            // 
            // bMainDeviceSwitch
            // 
            this.bMainDeviceSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMainDeviceSwitch.Location = new System.Drawing.Point(1032, 35);
            this.bMainDeviceSwitch.Name = "bMainDeviceSwitch";
            this.bMainDeviceSwitch.Size = new System.Drawing.Size(133, 86);
            this.bMainDeviceSwitch.TabIndex = 0;
            this.bMainDeviceSwitch.Text = "Сменить оружие";
            this.bMainDeviceSwitch.UseVisualStyleBackColor = true;
            this.bMainDeviceSwitch.Click += new System.EventHandler(this.bSwitchMainDevice_Click);
            // 
            // gbListOfDevices
            // 
            this.gbListOfDevices.Controls.Add(this.dgvListOfDevices);
            this.gbListOfDevices.Location = new System.Drawing.Point(369, 141);
            this.gbListOfDevices.Name = "gbListOfDevices";
            this.gbListOfDevices.Size = new System.Drawing.Size(968, 467);
            this.gbListOfDevices.TabIndex = 3;
            this.gbListOfDevices.TabStop = false;
            this.gbListOfDevices.Text = "ДНС";
            // 
            // dgvListOfDevices
            // 
            this.dgvListOfDevices.AllowUserToAddRows = false;
            this.dgvListOfDevices.AllowUserToDeleteRows = false;
            this.dgvListOfDevices.AllowUserToResizeColumns = false;
            this.dgvListOfDevices.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvListOfDevices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListOfDevices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListOfDevices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvListOfDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListOfDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Type,
            this.Manufacturer,
            this.Price,
            this.PlugStatus,
            this.AssembleStatus});
            this.dgvListOfDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListOfDevices.Location = new System.Drawing.Point(3, 22);
            this.dgvListOfDevices.Name = "dgvListOfDevices";
            this.dgvListOfDevices.ReadOnly = true;
            this.dgvListOfDevices.RowHeadersVisible = false;
            this.dgvListOfDevices.RowHeadersWidth = 62;
            this.dgvListOfDevices.RowTemplate.Height = 28;
            this.dgvListOfDevices.Size = new System.Drawing.Size(962, 442);
            this.dgvListOfDevices.TabIndex = 0;
            this.dgvListOfDevices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListOfDevices_CellDoubleClick);
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.FillWeight = 44.31819F;
            this.Index.HeaderText = "Index";
            this.Index.MinimumWidth = 8;
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.FillWeight = 136.6761F;
            this.Type.HeaderText = "Тип устройства";
            this.Type.MinimumWidth = 8;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Manufacturer
            // 
            this.Manufacturer.DataPropertyName = "Manufacturer";
            this.Manufacturer.FillWeight = 136.6761F;
            this.Manufacturer.HeaderText = "Производитель";
            this.Manufacturer.MinimumWidth = 8;
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.FillWeight = 136.6761F;
            this.Price.HeaderText = "Цена";
            this.Price.MinimumWidth = 8;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // PlugStatus
            // 
            this.PlugStatus.DataPropertyName = "PlugStatus";
            this.PlugStatus.FillWeight = 50.93518F;
            this.PlugStatus.HeaderText = "Сеть";
            this.PlugStatus.MinimumWidth = 8;
            this.PlugStatus.Name = "PlugStatus";
            this.PlugStatus.ReadOnly = true;
            this.PlugStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AssembleStatus
            // 
            this.AssembleStatus.DataPropertyName = "Assamble";
            this.AssembleStatus.FillWeight = 94.71848F;
            this.AssembleStatus.HeaderText = "Собранность";
            this.AssembleStatus.MinimumWidth = 8;
            this.AssembleStatus.Name = "AssembleStatus";
            this.AssembleStatus.ReadOnly = true;
            this.AssembleStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // gbMainDevice
            // 
            this.gbMainDevice.Controls.Add(this.tbMainType);
            this.gbMainDevice.Controls.Add(this.panelAssembleStat);
            this.gbMainDevice.Controls.Add(this.panelPlugStat);
            this.gbMainDevice.Controls.Add(this.tbMainManuf);
            this.gbMainDevice.Controls.Add(this.tbPaperCount);
            this.gbMainDevice.Controls.Add(this.tbMainPrice);
            this.gbMainDevice.Controls.Add(this.label4);
            this.gbMainDevice.Controls.Add(this.label7);
            this.gbMainDevice.Controls.Add(this.label8);
            this.gbMainDevice.Controls.Add(this.label13);
            this.gbMainDevice.Controls.Add(this.label6);
            this.gbMainDevice.Controls.Add(this.label3);
            this.gbMainDevice.Controls.Add(this.pictureBox1);
            this.gbMainDevice.Location = new System.Drawing.Point(13, 5);
            this.gbMainDevice.Name = "gbMainDevice";
            this.gbMainDevice.Size = new System.Drawing.Size(350, 233);
            this.gbMainDevice.TabIndex = 4;
            this.gbMainDevice.TabStop = false;
            this.gbMainDevice.Text = "Статус устройства";
            // 
            // tbMainType
            // 
            this.tbMainType.AutoSize = true;
            this.tbMainType.Font = new System.Drawing.Font("Alien Encounters", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMainType.Location = new System.Drawing.Point(7, 162);
            this.tbMainType.Name = "tbMainType";
            this.tbMainType.Size = new System.Drawing.Size(71, 23);
            this.tbMainType.TabIndex = 6;
            this.tbMainType.Text = "пусто";
            // 
            // panelAssembleStat
            // 
            this.panelAssembleStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAssembleStat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelAssembleStat.Location = new System.Drawing.Point(293, 62);
            this.panelAssembleStat.Name = "panelAssembleStat";
            this.panelAssembleStat.Size = new System.Drawing.Size(51, 31);
            this.panelAssembleStat.TabIndex = 5;
            // 
            // panelPlugStat
            // 
            this.panelPlugStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPlugStat.Location = new System.Drawing.Point(293, 22);
            this.panelPlugStat.Name = "panelPlugStat";
            this.panelPlugStat.Size = new System.Drawing.Size(51, 31);
            this.panelPlugStat.TabIndex = 5;
            // 
            // tbMainManuf
            // 
            this.tbMainManuf.Location = new System.Drawing.Point(144, 191);
            this.tbMainManuf.Name = "tbMainManuf";
            this.tbMainManuf.ReadOnly = true;
            this.tbMainManuf.Size = new System.Drawing.Size(200, 26);
            this.tbMainManuf.TabIndex = 4;
            // 
            // tbPaperCount
            // 
            this.tbPaperCount.Location = new System.Drawing.Point(292, 114);
            this.tbPaperCount.Name = "tbPaperCount";
            this.tbPaperCount.ReadOnly = true;
            this.tbPaperCount.Size = new System.Drawing.Size(52, 26);
            this.tbPaperCount.TabIndex = 4;
            // 
            // tbMainPrice
            // 
            this.tbMainPrice.Location = new System.Drawing.Point(228, 159);
            this.tbMainPrice.Name = "tbMainPrice";
            this.tbMainPrice.ReadOnly = true;
            this.tbMainPrice.Size = new System.Drawing.Size(116, 26);
            this.tbMainPrice.TabIndex = 4;
            this.tbMainPrice.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Производитель";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(442, -73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Цена";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Бумага";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(223, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 20);
            this.label13.TabIndex = 3;
            this.label13.Text = "Сеть";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "Сборка";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Цена";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // gbManagerBut
            // 
            this.gbManagerBut.Controls.Add(this.bThrowDev);
            this.gbManagerBut.Controls.Add(this.bDisassShop);
            this.gbManagerBut.Controls.Add(this.bDisassSam);
            this.gbManagerBut.Controls.Add(this.bAssem);
            this.gbManagerBut.Controls.Add(this.bPlugSwitch);
            this.gbManagerBut.Controls.Add(this.gbPrintScanCopy);
            this.gbManagerBut.Controls.Add(this.label12);
            this.gbManagerBut.Controls.Add(this.label11);
            this.gbManagerBut.Controls.Add(this.label10);
            this.gbManagerBut.Controls.Add(this.label9);
            this.gbManagerBut.Controls.Add(this.label5);
            this.gbManagerBut.Location = new System.Drawing.Point(13, 245);
            this.gbManagerBut.Name = "gbManagerBut";
            this.gbManagerBut.Size = new System.Drawing.Size(350, 363);
            this.gbManagerBut.TabIndex = 5;
            this.gbManagerBut.TabStop = false;
            this.gbManagerBut.Text = "Управление";
            // 
            // bThrowDev
            // 
            this.bThrowDev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bThrowDev.Location = new System.Drawing.Point(10, 162);
            this.bThrowDev.Name = "bThrowDev";
            this.bThrowDev.Size = new System.Drawing.Size(98, 26);
            this.bThrowDev.TabIndex = 1;
            this.bThrowDev.UseVisualStyleBackColor = true;
            this.bThrowDev.Click += new System.EventHandler(this.bThrow_Click);
            // 
            // bDisassShop
            // 
            this.bDisassShop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDisassShop.Location = new System.Drawing.Point(10, 130);
            this.bDisassShop.Name = "bDisassShop";
            this.bDisassShop.Size = new System.Drawing.Size(98, 26);
            this.bDisassShop.TabIndex = 1;
            this.bDisassShop.UseVisualStyleBackColor = true;
            this.bDisassShop.Click += new System.EventHandler(this.bDisassShop_Click);
            // 
            // bDisassSam
            // 
            this.bDisassSam.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDisassSam.Location = new System.Drawing.Point(10, 98);
            this.bDisassSam.Name = "bDisassSam";
            this.bDisassSam.Size = new System.Drawing.Size(98, 26);
            this.bDisassSam.TabIndex = 1;
            this.bDisassSam.UseVisualStyleBackColor = true;
            this.bDisassSam.Click += new System.EventHandler(this.bDisassSam_Click);
            // 
            // bAssem
            // 
            this.bAssem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bAssem.Location = new System.Drawing.Point(10, 66);
            this.bAssem.Name = "bAssem";
            this.bAssem.Size = new System.Drawing.Size(98, 26);
            this.bAssem.TabIndex = 1;
            this.bAssem.UseVisualStyleBackColor = true;
            this.bAssem.Click += new System.EventHandler(this.bAssem_Click);
            // 
            // bPlugSwitch
            // 
            this.bPlugSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bPlugSwitch.Location = new System.Drawing.Point(10, 34);
            this.bPlugSwitch.Name = "bPlugSwitch";
            this.bPlugSwitch.Size = new System.Drawing.Size(98, 26);
            this.bPlugSwitch.TabIndex = 1;
            this.bPlugSwitch.UseVisualStyleBackColor = true;
            this.bPlugSwitch.Click += new System.EventHandler(this.bPlugSwitch_Click);
            // 
            // gbPrintScanCopy
            // 
            this.gbPrintScanCopy.Controls.Add(this.bCopy);
            this.gbPrintScanCopy.Controls.Add(this.bPaperCash);
            this.gbPrintScanCopy.Controls.Add(this.bScan);
            this.gbPrintScanCopy.Controls.Add(this.bPrint);
            this.gbPrintScanCopy.Location = new System.Drawing.Point(7, 194);
            this.gbPrintScanCopy.Name = "gbPrintScanCopy";
            this.gbPrintScanCopy.Size = new System.Drawing.Size(337, 163);
            this.gbPrintScanCopy.TabIndex = 0;
            this.gbPrintScanCopy.TabStop = false;
            // 
            // bCopy
            // 
            this.bCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCopy.Location = new System.Drawing.Point(165, 25);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(115, 80);
            this.bCopy.TabIndex = 1;
            this.bCopy.Text = "Копировать";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // bPaperCash
            // 
            this.bPaperCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPaperCash.Location = new System.Drawing.Point(22, 111);
            this.bPaperCash.Name = "bPaperCash";
            this.bPaperCash.Size = new System.Drawing.Size(258, 39);
            this.bPaperCash.TabIndex = 1;
            this.bPaperCash.Text = "Бумаги надыбать";
            this.bPaperCash.UseVisualStyleBackColor = true;
            this.bPaperCash.Click += new System.EventHandler(this.bPaperCash_Click);
            // 
            // bScan
            // 
            this.bScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bScan.Location = new System.Drawing.Point(22, 73);
            this.bScan.Name = "bScan";
            this.bScan.Size = new System.Drawing.Size(98, 32);
            this.bScan.TabIndex = 1;
            this.bScan.Text = "Скан";
            this.bScan.UseVisualStyleBackColor = true;
            this.bScan.Click += new System.EventHandler(this.bScan_Click);
            // 
            // bPrint
            // 
            this.bPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPrint.Location = new System.Drawing.Point(22, 25);
            this.bPrint.Name = "bPrint";
            this.bPrint.Size = new System.Drawing.Size(98, 32);
            this.bPrint.TabIndex = 1;
            this.bPrint.Text = "Печать";
            this.bPrint.UseVisualStyleBackColor = true;
            this.bPrint.Click += new System.EventHandler(this.bPrint_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.LightCoral;
            this.label12.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(140, 165);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 23);
            this.label12.TabIndex = 3;
            this.label12.Text = "на помойку";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(140, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 20);
            this.label11.TabIndex = 3;
            this.label11.Text = "отнести в сервис";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "собрать (умелые ручки)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(140, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "разобрать";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Сеть вкл/выкл";
            // 
            // gbMassages
            // 
            this.gbMassages.Controls.Add(this.rtbMessage);
            this.gbMassages.Location = new System.Drawing.Point(372, 5);
            this.gbMassages.Name = "gbMassages";
            this.gbMassages.Size = new System.Drawing.Size(637, 137);
            this.gbMassages.TabIndex = 6;
            this.gbMassages.TabStop = false;
            this.gbMassages.Text = "СМС-ки";
            // 
            // rtbMessage
            // 
            this.rtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessage.Location = new System.Drawing.Point(3, 22);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.ReadOnly = true;
            this.rtbMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMessage.ShowSelectionMargin = true;
            this.rtbMessage.Size = new System.Drawing.Size(631, 112);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            // 
            // gbBuyLeave
            // 
            this.gbBuyLeave.Controls.Add(this.bLeaveDNS);
            this.gbBuyLeave.Controls.Add(this.bBuyForFree);
            this.gbBuyLeave.Location = new System.Drawing.Point(1352, 439);
            this.gbBuyLeave.Name = "gbBuyLeave";
            this.gbBuyLeave.Size = new System.Drawing.Size(334, 169);
            this.gbBuyLeave.TabIndex = 7;
            this.gbBuyLeave.TabStop = false;
            // 
            // bLeaveDNS
            // 
            this.bLeaveDNS.Dock = System.Windows.Forms.DockStyle.Right;
            this.bLeaveDNS.Location = new System.Drawing.Point(181, 22);
            this.bLeaveDNS.Name = "bLeaveDNS";
            this.bLeaveDNS.Size = new System.Drawing.Size(150, 144);
            this.bLeaveDNS.TabIndex = 0;
            this.bLeaveDNS.Text = "Покинуть ДНС";
            this.bLeaveDNS.UseVisualStyleBackColor = true;
            this.bLeaveDNS.Click += new System.EventHandler(this.bLeaveDNS_Click);
            // 
            // bBuyForFree
            // 
            this.bBuyForFree.Dock = System.Windows.Forms.DockStyle.Left;
            this.bBuyForFree.Location = new System.Drawing.Point(3, 22);
            this.bBuyForFree.Name = "bBuyForFree";
            this.bBuyForFree.Size = new System.Drawing.Size(150, 144);
            this.bBuyForFree.TabIndex = 0;
            this.bBuyForFree.Text = "Купить за бесплатно";
            this.bBuyForFree.UseVisualStyleBackColor = true;
            this.bBuyForFree.Click += new System.EventHandler(this.bBuyForFree_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1706, 620);
            this.Controls.Add(this.gbBuyLeave);
            this.Controls.Add(this.gbMassages);
            this.Controls.Add(this.gbManagerBut);
            this.Controls.Add(this.gbMainDevice);
            this.Controls.Add(this.bMainDeviceSwitch);
            this.Controls.Add(this.gbListOfDevices);
            this.Controls.Add(this.gbInterface);
            this.Controls.Add(this.gbShop_OtBaldy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbShop_OtBaldy.ResumeLayout(false);
            this.gbShop_OtBaldy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice_OtBaldy)).EndInit();
            this.gbInterface.ResumeLayout(false);
            this.gbInterface.PerformLayout();
            this.gbListOfDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListOfDevices)).EndInit();
            this.gbMainDevice.ResumeLayout(false);
            this.gbMainDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbManagerBut.ResumeLayout(false);
            this.gbManagerBut.PerformLayout();
            this.gbPrintScanCopy.ResumeLayout(false);
            this.gbMassages.ResumeLayout(false);
            this.gbBuyLeave.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbShop_OtBaldy;
        private System.Windows.Forms.GroupBox gbInterface;
        private System.Windows.Forms.GroupBox gbListOfDevices;
        private System.Windows.Forms.GroupBox gbMainDevice;
        private System.Windows.Forms.GroupBox gbManagerBut;
        private System.Windows.Forms.GroupBox gbPrintScanCopy;
        private System.Windows.Forms.RadioButton rbDevice;
        private System.Windows.Forms.RadioButton rbScanner;
        private System.Windows.Forms.RadioButton rbPrinter;
        private System.Windows.Forms.NumericUpDown nudPrice_OtBaldy;
        private System.Windows.Forms.RadioButton rbMFP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbManufact_OtBaldy;
        private System.Windows.Forms.Button bMainDeviceSwitch;
        private System.Windows.Forms.TextBox tbMainManuf;
        private System.Windows.Forms.TextBox tbMainPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbPaperCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bThrowDev;
        private System.Windows.Forms.Button bDisassShop;
        private System.Windows.Forms.Button bDisassSam;
        private System.Windows.Forms.Button bAssem;
        private System.Windows.Forms.Button bPlugSwitch;
        private System.Windows.Forms.Button bPrint;
        private System.Windows.Forms.DataGridView dgvListOfDevices;
        private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.Button bScan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbMassages;
        private System.Windows.Forms.Panel panelAssembleStat;
        private System.Windows.Forms.Panel panelPlugStat;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbBuyLeave;
        private System.Windows.Forms.Button bLeaveDNS;
        private System.Windows.Forms.Button bBuyForFree;
        private System.Windows.Forms.Button bEnterDNS;
        private System.Windows.Forms.Button bBuy_OtBaldy;
        private System.Windows.Forms.Button bInventoryShow;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox cbDNS;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.Button bPaperCash;
        private System.Windows.Forms.Label tbMainType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlugStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssembleStatus;
    }
}