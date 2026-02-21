
namespace Configure {
    partial class Form {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.clear_cache = new System.Windows.Forms.Button();
            this.thumbnails = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ts_custom = new System.Windows.Forms.RadioButton();
            this.ts_middle = new System.Windows.Forms.RadioButton();
            this.ts_beginning = new System.Windows.Forms.RadioButton();
            this.ts_first = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.uninstall = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.install = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.info_text = new System.Windows.Forms.Label();
            this.info_label = new System.Windows.Forms.Label();
            this.installationPath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fileTypes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ext_all = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cover = new System.Windows.Forms.CheckBox();
            this.thumbnails.SuspendLayout();
            this.fileTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // clear_cache
            // 
            this.clear_cache.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_cache.Location = new System.Drawing.Point(541, 400);
            this.clear_cache.Name = "clear_cache";
            this.clear_cache.Size = new System.Drawing.Size(96, 31);
            this.clear_cache.TabIndex = 35;
            this.clear_cache.Text = "Clear Cache";
            this.toolTip.SetToolTip(this.clear_cache, resources.GetString("clear_cache.ToolTip"));
            this.clear_cache.UseVisualStyleBackColor = true;
            this.clear_cache.Click += new System.EventHandler(this.clear_cache_Click);
            // 
            // thumbnails
            // 
            this.thumbnails.Controls.Add(this.label13);
            this.thumbnails.Controls.Add(this.seconds);
            this.thumbnails.Controls.Add(this.label12);
            this.thumbnails.Controls.Add(this.label11);
            this.thumbnails.Controls.Add(this.label10);
            this.thumbnails.Controls.Add(this.ts_custom);
            this.thumbnails.Controls.Add(this.ts_middle);
            this.thumbnails.Controls.Add(this.ts_beginning);
            this.thumbnails.Controls.Add(this.ts_first);
            this.thumbnails.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thumbnails.Location = new System.Drawing.Point(253, 243);
            this.thumbnails.Name = "thumbnails";
            this.thumbnails.Size = new System.Drawing.Size(486, 151);
            this.thumbnails.TabIndex = 34;
            this.thumbnails.TabStop = false;
            this.thumbnails.Text = "Thumbnail Timestamp";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(176, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(266, 19);
            this.label13.TabIndex = 8;
            this.label13.Text = "seconds into the video";
            // 
            // seconds
            // 
            this.seconds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seconds.Location = new System.Drawing.Point(143, 112);
            this.seconds.Margin = new System.Windows.Forms.Padding(0);
            this.seconds.Multiline = true;
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(30, 22);
            this.seconds.TabIndex = 7;
            this.seconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seconds_KeyPress);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(140, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(336, 19);
            this.label12.TabIndex = 6;
            this.label12.Text = "Generate thumbnail from the midpoint of the video";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(140, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(336, 19);
            this.label11.TabIndex = 5;
            this.label11.Text = "Use the first frame of the video as thumbnail";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(140, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(336, 19);
            this.label10.TabIndex = 4;
            this.label10.Text = "Generate thumbnail about 5 seconds into the video";
            // 
            // ts_custom
            // 
            this.ts_custom.AutoSize = true;
            this.ts_custom.Location = new System.Drawing.Point(13, 112);
            this.ts_custom.Name = "ts_custom";
            this.ts_custom.Size = new System.Drawing.Size(70, 21);
            this.ts_custom.TabIndex = 3;
            this.ts_custom.TabStop = true;
            this.ts_custom.Text = "Custom";
            this.ts_custom.UseVisualStyleBackColor = true;
            // 
            // ts_middle
            // 
            this.ts_middle.AutoSize = true;
            this.ts_middle.Location = new System.Drawing.Point(13, 85);
            this.ts_middle.Name = "ts_middle";
            this.ts_middle.Size = new System.Drawing.Size(67, 21);
            this.ts_middle.TabIndex = 2;
            this.ts_middle.TabStop = true;
            this.ts_middle.Tag = "80000003";
            this.ts_middle.Text = "Middle";
            this.ts_middle.UseVisualStyleBackColor = true;
            // 
            // ts_beginning
            // 
            this.ts_beginning.AutoSize = true;
            this.ts_beginning.Location = new System.Drawing.Point(13, 58);
            this.ts_beginning.Name = "ts_beginning";
            this.ts_beginning.Size = new System.Drawing.Size(83, 21);
            this.ts_beginning.TabIndex = 1;
            this.ts_beginning.TabStop = true;
            this.ts_beginning.Tag = "80000002";
            this.ts_beginning.Text = "Beginning";
            this.ts_beginning.UseVisualStyleBackColor = true;
            // 
            // ts_first
            // 
            this.ts_first.AutoSize = true;
            this.ts_first.Location = new System.Drawing.Point(13, 31);
            this.ts_first.Name = "ts_first";
            this.ts_first.Size = new System.Drawing.Size(90, 21);
            this.ts_first.TabIndex = 0;
            this.ts_first.TabStop = true;
            this.ts_first.Tag = "80000001";
            this.ts_first.Text = "First Frame";
            this.ts_first.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(365, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(374, 37);
            this.label9.TabIndex = 33;
            this.label9.Text = "Unregisters the Thumbnail Provider, removes FFmpeg Binaries and restores file-typ" +
    "e associations to their original values.";
            // 
            // uninstall
            // 
            this.uninstall.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uninstall.Location = new System.Drawing.Point(253, 187);
            this.uninstall.Name = "uninstall";
            this.uninstall.Size = new System.Drawing.Size(96, 31);
            this.uninstall.TabIndex = 32;
            this.uninstall.Text = "Uninstall";
            this.uninstall.UseVisualStyleBackColor = true;
            this.uninstall.Click += new System.EventHandler(this.uninstall_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(365, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(374, 37);
            this.label8.TabIndex = 31;
            this.label8.Text = "Extracts FFmpeg Binaries and registers the Thumbnail Provider with Windows Explor" +
    "er.";
            // 
            // install
            // 
            this.install.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.install.Location = new System.Drawing.Point(253, 131);
            this.install.Name = "install";
            this.install.Size = new System.Drawing.Size(96, 31);
            this.install.TabIndex = 30;
            this.install.Text = "Install";
            this.install.UseVisualStyleBackColor = true;
            this.install.Click += new System.EventHandler(this.install_Click);
            // 
            // apply
            // 
            this.apply.Enabled = false;
            this.apply.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apply.Location = new System.Drawing.Point(643, 400);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(96, 31);
            this.apply.TabIndex = 29;
            this.apply.Text = "Apply";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // info_text
            // 
            this.info_text.AutoSize = true;
            this.info_text.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_text.Location = new System.Drawing.Point(246, 92);
            this.info_text.Name = "info_text";
            this.info_text.Size = new System.Drawing.Size(483, 17);
            this.info_text.TabIndex = 28;
            this.info_text.Text = "Files don\'t exist under installation path. Please reinstall FFmpegThumbnailHandle" +
    "r.";
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_label.ForeColor = System.Drawing.Color.Coral;
            this.info_label.Location = new System.Drawing.Point(246, 70);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(57, 17);
            this.info_label.TabIndex = 27;
            this.info_label.Text = "Warning";
            // 
            // installationPath
            // 
            this.installationPath.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installationPath.Location = new System.Drawing.Point(347, 41);
            this.installationPath.Name = "installationPath";
            this.installationPath.Size = new System.Drawing.Size(364, 38);
            this.installationPath.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(246, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "Installation path:";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.LimeGreen;
            this.status.Location = new System.Drawing.Point(473, 16);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(56, 17);
            this.status.TabIndex = 24;
            this.status.Text = "installed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "FFmpegThumbnailProvider is currently";
            // 
            // fileTypes
            // 
            this.fileTypes.Controls.Add(this.label1);
            this.fileTypes.Controls.Add(this.ext_all);
            this.fileTypes.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileTypes.Location = new System.Drawing.Point(12, 16);
            this.fileTypes.Name = "fileTypes";
            this.fileTypes.Size = new System.Drawing.Size(210, 415);
            this.fileTypes.TabIndex = 22;
            this.fileTypes.TabStop = false;
            this.fileTypes.Text = "File Type Associations";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 38);
            this.label1.TabIndex = 8;
            this.label1.Text = "Use FFmpegThumbnails for following types";
            // 
            // ext_all
            // 
            this.ext_all.AutoSize = true;
            this.ext_all.Location = new System.Drawing.Point(10, 72);
            this.ext_all.Name = "ext_all";
            this.ext_all.Size = new System.Drawing.Size(78, 21);
            this.ext_all.TabIndex = 3;
            this.ext_all.Text = "Select all";
            this.ext_all.UseVisualStyleBackColor = true;
            this.ext_all.CheckedChanged += new System.EventHandler(this.ext_all_CheckedChanged);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.AutoPopDelay = 20000;
            this.toolTip.InitialDelay = 10;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;
            // 
            // cover
            // 
            this.cover.AutoSize = true;
            this.cover.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.cover.Location = new System.Drawing.Point(253, 406);
            this.cover.Name = "cover";
            this.cover.Size = new System.Drawing.Size(229, 21);
            this.cover.TabIndex = 36;
            this.cover.Text = "Use cover as thumbnail if available";
            this.cover.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 450);
            this.Controls.Add(this.cover);
            this.Controls.Add(this.clear_cache);
            this.Controls.Add(this.thumbnails);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.uninstall);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.install);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.info_text);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.installationPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fileTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.Text = "FFmpegThumbnails Configuration";
            this.Load += new System.EventHandler(this.Form_Load);
            this.thumbnails.ResumeLayout(false);
            this.thumbnails.PerformLayout();
            this.fileTypes.ResumeLayout(false);
            this.fileTypes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clear_cache;
        private System.Windows.Forms.GroupBox thumbnails;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox seconds;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton ts_custom;
        private System.Windows.Forms.RadioButton ts_middle;
        private System.Windows.Forms.RadioButton ts_beginning;
        private System.Windows.Forms.RadioButton ts_first;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button uninstall;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button install;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.Label info_text;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Label installationPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox fileTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ext_all;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox cover;
    }
}

