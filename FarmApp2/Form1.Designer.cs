using System.Windows.Forms;

namespace FarmApp2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxWeight = new System.Windows.Forms.TextBox();
            this.dateTimePickerDateSold = new System.Windows.Forms.DateTimePicker();
            this.buttonAddCow = new System.Windows.Forms.Button();
            this.listViewCowsInput = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddReport = new System.Windows.Forms.Button();
            this.treeViewReports = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelDateSold = new System.Windows.Forms.Label();
            this.labelCowID = new System.Windows.Forms.Label();
            this.labelCowWeight = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.buttonDeleteCow = new System.Windows.Forms.Button();
            this.contextMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxID
            // 
            this.textBoxID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxID.Location = new System.Drawing.Point(78, 3);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(135, 20);
            this.textBoxID.TabIndex = 1;
            // 
            // textBoxWeight
            // 
            this.textBoxWeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxWeight.Location = new System.Drawing.Point(78, 28);
            this.textBoxWeight.Name = "textBoxWeight";
            this.textBoxWeight.Size = new System.Drawing.Size(57, 20);
            this.textBoxWeight.TabIndex = 2;
            this.textBoxWeight.TextChanged += new System.EventHandler(this.textBoxWeight_TextChanged);
            // 
            // dateTimePickerDateSold
            // 
            this.dateTimePickerDateSold.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePickerDateSold.Location = new System.Drawing.Point(78, 78);
            this.dateTimePickerDateSold.Name = "dateTimePickerDateSold";
            this.dateTimePickerDateSold.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDateSold.TabIndex = 3;
            // 
            // buttonAddCow
            // 
            this.buttonAddCow.Location = new System.Drawing.Point(12, 148);
            this.buttonAddCow.Name = "buttonAddCow";
            this.buttonAddCow.Size = new System.Drawing.Size(100, 23);
            this.buttonAddCow.TabIndex = 4;
            this.buttonAddCow.Text = "Add Cow";
            this.buttonAddCow.UseVisualStyleBackColor = true;
            this.buttonAddCow.Click += new System.EventHandler(this.buttonAddCow_Click_1);
            // 
            // listViewCowsInput
            // 
            this.listViewCowsInput.ContextMenuStrip = this.contextMenu;
            this.listViewCowsInput.HideSelection = false;
            this.listViewCowsInput.Location = new System.Drawing.Point(235, 42);
            this.listViewCowsInput.Name = "listViewCowsInput";
            this.listViewCowsInput.Size = new System.Drawing.Size(370, 229);
            this.listViewCowsInput.TabIndex = 5;
            this.listViewCowsInput.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            this.deleteMenuItem.Click += DeleteMenuItem_Click;
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.Size = new System.Drawing.Size(32, 19);
            this.deleteMenuItem.Text = "Delete";
            // 
            // buttonAddReport
            // 
            this.buttonAddReport.Location = new System.Drawing.Point(12, 177);
            this.buttonAddReport.Name = "buttonAddReport";
            this.buttonAddReport.Size = new System.Drawing.Size(100, 23);
            this.buttonAddReport.TabIndex = 6;
            this.buttonAddReport.Text = "Add Report";
            this.buttonAddReport.UseVisualStyleBackColor = true;
            // 
            // treeViewReports
            // 
            this.treeViewReports.Location = new System.Drawing.Point(611, 42);
            this.treeViewReports.Name = "treeViewReports";
            this.treeViewReports.Size = new System.Drawing.Size(554, 229);
            this.treeViewReports.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dateTimePickerDateSold, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelDateSold, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelCowID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelCowWeight, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxWeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelPrice, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPrice, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 100);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // labelDateSold
            // 
            this.labelDateSold.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDateSold.AutoSize = true;
            this.labelDateSold.Location = new System.Drawing.Point(15, 81);
            this.labelDateSold.Name = "labelDateSold";
            this.labelDateSold.Size = new System.Drawing.Size(57, 13);
            this.labelDateSold.TabIndex = 6;
            this.labelDateSold.Text = "Date Sold:";
            // 
            // labelCowID
            // 
            this.labelCowID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCowID.AutoSize = true;
            this.labelCowID.Location = new System.Drawing.Point(27, 6);
            this.labelCowID.Name = "labelCowID";
            this.labelCowID.Size = new System.Drawing.Size(45, 13);
            this.labelCowID.TabIndex = 4;
            this.labelCowID.Text = "Cow ID:";
            // 
            // labelCowWeight
            // 
            this.labelCowWeight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCowWeight.AutoSize = true;
            this.labelCowWeight.Location = new System.Drawing.Point(7, 31);
            this.labelCowWeight.Name = "labelCowWeight";
            this.labelCowWeight.Size = new System.Drawing.Size(65, 13);
            this.labelCowWeight.TabIndex = 5;
            this.labelCowWeight.Text = "Weight (kg):";
            // 
            // labelPrice
            // 
            this.labelPrice.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(23, 56);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(49, 13);
            this.labelPrice.TabIndex = 7;
            this.labelPrice.Text = "Price (£):";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxPrice.Location = new System.Drawing.Point(78, 53);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(57, 20);
            this.textBoxPrice.TabIndex = 8;
            // 
            // buttonDeleteCow
            // 
            this.buttonDeleteCow.Location = new System.Drawing.Point(119, 148);
            this.buttonDeleteCow.Name = "buttonDeleteCow";
            this.buttonDeleteCow.Size = new System.Drawing.Size(93, 23);
            this.buttonDeleteCow.TabIndex = 9;
            this.buttonDeleteCow.Text = "Delete";
            this.buttonDeleteCow.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 359);
            this.Controls.Add(this.buttonDeleteCow);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.treeViewReports);
            this.Controls.Add(this.buttonAddReport);
            this.Controls.Add(this.listViewCowsInput);
            this.Controls.Add(this.buttonAddCow);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.TextBox textBoxWeight;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateSold;
        private System.Windows.Forms.Button buttonAddCow;
        private System.Windows.Forms.ListView listViewCowsInput;
        private System.Windows.Forms.Button buttonAddReport;
        private System.Windows.Forms.TreeView treeViewReports;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelCowID;
        private System.Windows.Forms.Label labelCowWeight;
        private System.Windows.Forms.Label labelDateSold;
        private System.Windows.Forms.Button buttonDeleteCow;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private ToolStripMenuItem deleteMenuItem;
    }
}

