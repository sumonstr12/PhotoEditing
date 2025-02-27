using System;

namespace PhotoEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeImgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessConstractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saturationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorAdjustmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shepiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackWhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vintageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oilPaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sketchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.glowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartoonEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Mainpanel = new System.Windows.Forms.Panel();
            this.buttonCrop = new System.Windows.Forms.Button();
            this.buttonText = new System.Windows.Forms.Button();
            this.buttonColor = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonImageInfo = new System.Windows.Forms.Button();
            this.buttonZoomIn2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.Mainpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1017, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MainmenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.saveAsToolStripMenuItem.Text = "&Save as";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.closeToolStripMenuItem.Text = "&Close";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.resizeImgeToolStripMenuItem,
            this.rotateToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            this.cutToolStripMenuItem.Text = "&Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // resizeImgeToolStripMenuItem
            // 
            this.resizeImgeToolStripMenuItem.Name = "resizeImgeToolStripMenuItem";
            this.resizeImgeToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            this.resizeImgeToolStripMenuItem.Text = "&Resize Imge";
            this.resizeImgeToolStripMenuItem.Click += new System.EventHandler(this.resizeImgeToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            this.rotateToolStripMenuItem.Text = "&Rotate";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.rotateToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brightnessConstractToolStripMenuItem,
            this.saturationToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.colorAdjustmentToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // brightnessConstractToolStripMenuItem
            // 
            this.brightnessConstractToolStripMenuItem.Name = "brightnessConstractToolStripMenuItem";
            this.brightnessConstractToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.brightnessConstractToolStripMenuItem.Text = "Brightness & constract";
            // 
            // saturationToolStripMenuItem
            // 
            this.saturationToolStripMenuItem.Name = "saturationToolStripMenuItem";
            this.saturationToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.saturationToolStripMenuItem.Text = "&Saturation";
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.sharpenToolStripMenuItem.Text = "&Sharpen";
            // 
            // colorAdjustmentToolStripMenuItem
            // 
            this.colorAdjustmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayscaleToolStripMenuItem,
            this.shepiaToolStripMenuItem,
            this.invertColorToolStripMenuItem});
            this.colorAdjustmentToolStripMenuItem.Name = "colorAdjustmentToolStripMenuItem";
            this.colorAdjustmentToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.colorAdjustmentToolStripMenuItem.Text = "&Color Adjustment";
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.grayscaleToolStripMenuItem.Text = "&Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // shepiaToolStripMenuItem
            // 
            this.shepiaToolStripMenuItem.Name = "shepiaToolStripMenuItem";
            this.shepiaToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.shepiaToolStripMenuItem.Text = "&shepia";
            this.shepiaToolStripMenuItem.Click += new System.EventHandler(this.shepiaToolStripMenuItem_Click);
            // 
            // invertColorToolStripMenuItem
            // 
            this.invertColorToolStripMenuItem.Name = "invertColorToolStripMenuItem";
            this.invertColorToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.invertColorToolStripMenuItem.Text = "&Invert Color";
            this.invertColorToolStripMenuItem.Click += new System.EventHandler(this.invertColorToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blackWhiteToolStripMenuItem,
            this.vintageToolStripMenuItem,
            this.oilPaintToolStripMenuItem,
            this.sketchToolStripMenuItem,
            this.glowToolStripMenuItem,
            this.cartoonEffectToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(66, 29);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // blackWhiteToolStripMenuItem
            // 
            this.blackWhiteToolStripMenuItem.Name = "blackWhiteToolStripMenuItem";
            this.blackWhiteToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.blackWhiteToolStripMenuItem.Text = "Black & White";
            this.blackWhiteToolStripMenuItem.Click += new System.EventHandler(this.blackWhiteToolStripMenuItem_Click);
            // 
            // vintageToolStripMenuItem
            // 
            this.vintageToolStripMenuItem.Name = "vintageToolStripMenuItem";
            this.vintageToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.vintageToolStripMenuItem.Text = "Vintage";
            this.vintageToolStripMenuItem.Click += new System.EventHandler(this.vintageToolStripMenuItem_Click);
            // 
            // oilPaintToolStripMenuItem
            // 
            this.oilPaintToolStripMenuItem.Name = "oilPaintToolStripMenuItem";
            this.oilPaintToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.oilPaintToolStripMenuItem.Text = "Oil Paint";
            this.oilPaintToolStripMenuItem.Click += new System.EventHandler(this.oilPaintToolStripMenuItem_Click);
            // 
            // sketchToolStripMenuItem
            // 
            this.sketchToolStripMenuItem.Name = "sketchToolStripMenuItem";
            this.sketchToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.sketchToolStripMenuItem.Text = "Sketch";
            this.sketchToolStripMenuItem.Click += new System.EventHandler(this.sketchToolStripMenuItem_Click);
            // 
            // glowToolStripMenuItem
            // 
            this.glowToolStripMenuItem.Name = "glowToolStripMenuItem";
            this.glowToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.glowToolStripMenuItem.Text = "Glow";
            this.glowToolStripMenuItem.Click += new System.EventHandler(this.glowToolStripMenuItem_Click);
            // 
            // cartoonEffectToolStripMenuItem
            // 
            this.cartoonEffectToolStripMenuItem.Name = "cartoonEffectToolStripMenuItem";
            this.cartoonEffectToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.cartoonEffectToolStripMenuItem.Text = "Cartoon Effect";
            this.cartoonEffectToolStripMenuItem.Click += new System.EventHandler(this.cartoonEffectToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.userGuideToolStripMenuItem.Text = "User Guide";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Mainpanel
            // 
            this.Mainpanel.Controls.Add(this.buttonCrop);
            this.Mainpanel.Controls.Add(this.buttonText);
            this.Mainpanel.Controls.Add(this.buttonColor);
            this.Mainpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Mainpanel.Location = new System.Drawing.Point(0, 33);
            this.Mainpanel.Name = "Mainpanel";
            this.Mainpanel.Size = new System.Drawing.Size(101, 453);
            this.Mainpanel.TabIndex = 1;
            // 
            // buttonCrop
            // 
            this.buttonCrop.Location = new System.Drawing.Point(12, 85);
            this.buttonCrop.Name = "buttonCrop";
            this.buttonCrop.Size = new System.Drawing.Size(75, 36);
            this.buttonCrop.TabIndex = 2;
            this.buttonCrop.Text = "Crop";
            this.buttonCrop.UseVisualStyleBackColor = true;
            this.buttonCrop.Click += new System.EventHandler(this.buttonCrop_Click);
            // 
            // buttonText
            // 
            this.buttonText.Location = new System.Drawing.Point(12, 46);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(75, 33);
            this.buttonText.TabIndex = 1;
            this.buttonText.Text = "Text";
            this.buttonText.UseVisualStyleBackColor = true;
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(12, 3);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(76, 37);
            this.buttonColor.TabIndex = 0;
            this.buttonColor.Text = "Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(101, 33);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(916, 453);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZoomOut.Location = new System.Drawing.Point(885, 36);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonZoomOut.Size = new System.Drawing.Size(36, 31);
            this.buttonZoomOut.TabIndex = 4;
            this.buttonZoomOut.Text = "-";
            this.buttonZoomOut.UseVisualStyleBackColor = true;
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonImageInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(927, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, 453);
            this.panel1.TabIndex = 5;
            // 
            // buttonImageInfo
            // 
            this.buttonImageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImageInfo.Location = new System.Drawing.Point(6, 408);
            this.buttonImageInfo.Name = "buttonImageInfo";
            this.buttonImageInfo.Size = new System.Drawing.Size(75, 34);
            this.buttonImageInfo.TabIndex = 0;
            this.buttonImageInfo.Text = "Info";
            this.buttonImageInfo.UseVisualStyleBackColor = true;
            this.buttonImageInfo.Click += new System.EventHandler(this.buttonImageInfo_Click);
            // 
            // buttonZoomIn2
            // 
            this.buttonZoomIn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZoomIn2.Location = new System.Drawing.Point(842, 37);
            this.buttonZoomIn2.Name = "buttonZoomIn2";
            this.buttonZoomIn2.Size = new System.Drawing.Size(37, 30);
            this.buttonZoomIn2.TabIndex = 7;
            this.buttonZoomIn2.Text = "+";
            this.buttonZoomIn2.UseVisualStyleBackColor = true;
            this.buttonZoomIn2.Click += new System.EventHandler(this.buttonZoomIn2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 486);
            this.Controls.Add(this.buttonZoomIn2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonZoomOut);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.Mainpanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Mainpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        






        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeImgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessConstractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saturationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorAdjustmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shepiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackWhiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vintageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oilPaintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sketchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem glowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartoonEffectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel Mainpanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.Button buttonZoomOut;
        private System.Windows.Forms.Button buttonText;
        private System.Windows.Forms.Button buttonCrop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonImageInfo;
        private System.Windows.Forms.Button buttonZoomIn2;
    }
}

