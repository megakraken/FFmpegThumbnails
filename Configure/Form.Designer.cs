
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.clear_cache = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ext_webm = new System.Windows.Forms.CheckBox();
            this.ext_wmv = new System.Windows.Forms.CheckBox();
            this.ext_vob = new System.Windows.Forms.CheckBox();
            this.ext_mp2 = new System.Windows.Forms.CheckBox();
            this.ext_mpe = new System.Windows.Forms.CheckBox();
            this.ext_mpg = new System.Windows.Forms.CheckBox();
            this.ext_ogv = new System.Windows.Forms.CheckBox();
            this.ext_mpv = new System.Windows.Forms.CheckBox();
            this.ext_ts = new System.Windows.Forms.CheckBox();
            this.ext_m2v = new System.Windows.Forms.CheckBox();
            this.ext_mpeg = new System.Windows.Forms.CheckBox();
            this.ext_m4p = new System.Windows.Forms.CheckBox();
            this.ext_mkv = new System.Windows.Forms.CheckBox();
            this.ext_mp4 = new System.Windows.Forms.CheckBox();
            this.ext_mov = new System.Windows.Forms.CheckBox();
            this.ext_m4v = new System.Windows.Forms.CheckBox();
            this.ext_flv = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ext_3gp = new System.Windows.Forms.CheckBox();
            this.ext_avi = new System.Windows.Forms.CheckBox();
            this.ext_all = new System.Windows.Forms.CheckBox();
            this.ext_asf = new System.Windows.Forms.CheckBox();
            this.ext_3g2 = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.clear_cache.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.ts_custom);
            this.groupBox2.Controls.Add(this.ts_middle);
            this.groupBox2.Controls.Add(this.ts_beginning);
            this.groupBox2.Controls.Add(this.ts_first);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(253, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 151);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thumbnail Timestamp";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(176, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(266, 19);
            this.label13.TabIndex = 8;
            this.label13.Text = "seconds into the video";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(143, 112);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(30, 22);
            this.textBox1.TabIndex = 7;
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
            this.label9.Text = "Files don\'t exist under installation path. Please reinstall FFmpegThumbnailHandle" +
    "r.";
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
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(365, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(374, 37);
            this.label8.TabIndex = 31;
            this.label8.Text = "Files don\'t exist under installation path. Please reinstall FFmpegThumbnailHandle" +
    "r.";
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
            this.apply.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apply.Location = new System.Drawing.Point(643, 400);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(96, 31);
            this.apply.TabIndex = 29;
            this.apply.Text = "Apply";
            this.apply.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(246, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(483, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Files don\'t exist under installation path. Please reinstall FFmpegThumbnailHandle" +
    "r.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Coral;
            this.label6.Location = new System.Drawing.Point(246, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "Warning";
            // 
            // path
            // 
            this.path.AutoSize = true;
            this.path.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.path.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.path.Location = new System.Drawing.Point(365, 41);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(190, 17);
            this.path.TabIndex = 26;
            this.path.Text = "C:\\Windows\\system32\\igfm.exe";
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
            this.status.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.LimeGreen;
            this.status.Location = new System.Drawing.Point(469, 16);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(65, 17);
            this.status.TabIndex = 24;
            this.status.Text = "installed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "FFmpegThumbnailHandler is currently";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ext_webm);
            this.groupBox1.Controls.Add(this.ext_wmv);
            this.groupBox1.Controls.Add(this.ext_vob);
            this.groupBox1.Controls.Add(this.ext_mp2);
            this.groupBox1.Controls.Add(this.ext_mpe);
            this.groupBox1.Controls.Add(this.ext_mpg);
            this.groupBox1.Controls.Add(this.ext_ogv);
            this.groupBox1.Controls.Add(this.ext_mpv);
            this.groupBox1.Controls.Add(this.ext_ts);
            this.groupBox1.Controls.Add(this.ext_m2v);
            this.groupBox1.Controls.Add(this.ext_mpeg);
            this.groupBox1.Controls.Add(this.ext_m4p);
            this.groupBox1.Controls.Add(this.ext_mkv);
            this.groupBox1.Controls.Add(this.ext_mp4);
            this.groupBox1.Controls.Add(this.ext_mov);
            this.groupBox1.Controls.Add(this.ext_m4v);
            this.groupBox1.Controls.Add(this.ext_flv);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ext_3gp);
            this.groupBox1.Controls.Add(this.ext_avi);
            this.groupBox1.Controls.Add(this.ext_all);
            this.groupBox1.Controls.Add(this.ext_asf);
            this.groupBox1.Controls.Add(this.ext_3g2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 415);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Type Associations";
            // 
            // ext_webm
            // 
            this.ext_webm.AutoSize = true;
            this.ext_webm.Location = new System.Drawing.Point(89, 273);
            this.ext_webm.Name = "ext_webm";
            this.ext_webm.Size = new System.Drawing.Size(62, 21);
            this.ext_webm.TabIndex = 24;
            this.ext_webm.Text = "webm";
            this.ext_webm.UseVisualStyleBackColor = true;
            // 
            // ext_wmv
            // 
            this.ext_wmv.AutoSize = true;
            this.ext_wmv.Location = new System.Drawing.Point(89, 296);
            this.ext_wmv.Name = "ext_wmv";
            this.ext_wmv.Size = new System.Drawing.Size(53, 21);
            this.ext_wmv.TabIndex = 25;
            this.ext_wmv.Text = "wmv";
            this.ext_wmv.UseVisualStyleBackColor = true;
            // 
            // ext_vob
            // 
            this.ext_vob.AutoSize = true;
            this.ext_vob.Location = new System.Drawing.Point(89, 250);
            this.ext_vob.Name = "ext_vob";
            this.ext_vob.Size = new System.Drawing.Size(49, 21);
            this.ext_vob.TabIndex = 23;
            this.ext_vob.Text = "vob";
            this.ext_vob.UseVisualStyleBackColor = true;
            // 
            // ext_mp2
            // 
            this.ext_mp2.AutoSize = true;
            this.ext_mp2.Location = new System.Drawing.Point(10, 367);
            this.ext_mp2.Name = "ext_mp2";
            this.ext_mp2.Size = new System.Drawing.Size(53, 21);
            this.ext_mp2.TabIndex = 21;
            this.ext_mp2.Text = "mp2";
            this.ext_mp2.UseVisualStyleBackColor = true;
            // 
            // ext_mpe
            // 
            this.ext_mpe.AutoSize = true;
            this.ext_mpe.Location = new System.Drawing.Point(89, 108);
            this.ext_mpe.Name = "ext_mpe";
            this.ext_mpe.Size = new System.Drawing.Size(53, 21);
            this.ext_mpe.TabIndex = 22;
            this.ext_mpe.Text = "mpe";
            this.ext_mpe.UseVisualStyleBackColor = true;
            // 
            // ext_mpg
            // 
            this.ext_mpg.AutoSize = true;
            this.ext_mpg.Location = new System.Drawing.Point(10, 344);
            this.ext_mpg.Name = "ext_mpg";
            this.ext_mpg.Size = new System.Drawing.Size(54, 21);
            this.ext_mpg.TabIndex = 20;
            this.ext_mpg.Text = "mpg";
            this.ext_mpg.UseVisualStyleBackColor = true;
            // 
            // ext_ogv
            // 
            this.ext_ogv.AutoSize = true;
            this.ext_ogv.Location = new System.Drawing.Point(89, 225);
            this.ext_ogv.Name = "ext_ogv";
            this.ext_ogv.Size = new System.Drawing.Size(49, 21);
            this.ext_ogv.TabIndex = 19;
            this.ext_ogv.Text = "ogv";
            this.ext_ogv.UseVisualStyleBackColor = true;
            // 
            // ext_mpv
            // 
            this.ext_mpv.AutoSize = true;
            this.ext_mpv.Location = new System.Drawing.Point(89, 154);
            this.ext_mpv.Name = "ext_mpv";
            this.ext_mpv.Size = new System.Drawing.Size(52, 21);
            this.ext_mpv.TabIndex = 16;
            this.ext_mpv.Text = "mpv";
            this.ext_mpv.UseVisualStyleBackColor = true;
            // 
            // ext_ts
            // 
            this.ext_ts.AutoSize = true;
            this.ext_ts.Location = new System.Drawing.Point(89, 200);
            this.ext_ts.Name = "ext_ts";
            this.ext_ts.Size = new System.Drawing.Size(37, 21);
            this.ext_ts.TabIndex = 18;
            this.ext_ts.Text = "ts";
            this.ext_ts.UseVisualStyleBackColor = true;
            // 
            // ext_m2v
            // 
            this.ext_m2v.AutoSize = true;
            this.ext_m2v.Location = new System.Drawing.Point(89, 177);
            this.ext_m2v.Name = "ext_m2v";
            this.ext_m2v.Size = new System.Drawing.Size(51, 21);
            this.ext_m2v.TabIndex = 17;
            this.ext_m2v.Text = "m2v";
            this.ext_m2v.UseVisualStyleBackColor = true;
            // 
            // ext_mpeg
            // 
            this.ext_mpeg.AutoSize = true;
            this.ext_mpeg.Location = new System.Drawing.Point(89, 131);
            this.ext_mpeg.Name = "ext_mpeg";
            this.ext_mpeg.Size = new System.Drawing.Size(61, 21);
            this.ext_mpeg.TabIndex = 15;
            this.ext_mpeg.Text = "mpeg";
            this.ext_mpeg.UseVisualStyleBackColor = true;
            // 
            // ext_m4p
            // 
            this.ext_m4p.AutoSize = true;
            this.ext_m4p.Location = new System.Drawing.Point(10, 321);
            this.ext_m4p.Name = "ext_m4p";
            this.ext_m4p.Size = new System.Drawing.Size(53, 21);
            this.ext_m4p.TabIndex = 14;
            this.ext_m4p.Text = "m4p";
            this.ext_m4p.UseVisualStyleBackColor = true;
            // 
            // ext_mkv
            // 
            this.ext_mkv.AutoSize = true;
            this.ext_mkv.Location = new System.Drawing.Point(10, 250);
            this.ext_mkv.Name = "ext_mkv";
            this.ext_mkv.Size = new System.Drawing.Size(50, 21);
            this.ext_mkv.TabIndex = 11;
            this.ext_mkv.Text = "mkv";
            this.ext_mkv.UseVisualStyleBackColor = true;
            // 
            // ext_mp4
            // 
            this.ext_mp4.AutoSize = true;
            this.ext_mp4.Location = new System.Drawing.Point(10, 296);
            this.ext_mp4.Name = "ext_mp4";
            this.ext_mp4.Size = new System.Drawing.Size(53, 21);
            this.ext_mp4.TabIndex = 13;
            this.ext_mp4.Text = "mp4";
            this.ext_mp4.UseVisualStyleBackColor = true;
            // 
            // ext_mov
            // 
            this.ext_mov.AutoSize = true;
            this.ext_mov.Location = new System.Drawing.Point(10, 273);
            this.ext_mov.Name = "ext_mov";
            this.ext_mov.Size = new System.Drawing.Size(52, 21);
            this.ext_mov.TabIndex = 12;
            this.ext_mov.Text = "mov";
            this.ext_mov.UseVisualStyleBackColor = true;
            // 
            // ext_m4v
            // 
            this.ext_m4v.AutoSize = true;
            this.ext_m4v.Location = new System.Drawing.Point(10, 227);
            this.ext_m4v.Name = "ext_m4v";
            this.ext_m4v.Size = new System.Drawing.Size(51, 21);
            this.ext_m4v.TabIndex = 10;
            this.ext_m4v.Text = "m4v";
            this.ext_m4v.UseVisualStyleBackColor = true;
            // 
            // ext_flv
            // 
            this.ext_flv.AutoSize = true;
            this.ext_flv.Location = new System.Drawing.Point(10, 202);
            this.ext_flv.Name = "ext_flv";
            this.ext_flv.Size = new System.Drawing.Size(40, 21);
            this.ext_flv.TabIndex = 9;
            this.ext_flv.Text = "flv";
            this.ext_flv.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 38);
            this.label1.TabIndex = 8;
            this.label1.Text = "Use FFmpegThumbnailHandler for following types";
            // 
            // ext_3gp
            // 
            this.ext_3gp.AutoSize = true;
            this.ext_3gp.Location = new System.Drawing.Point(10, 131);
            this.ext_3gp.Name = "ext_3gp";
            this.ext_3gp.Size = new System.Drawing.Size(50, 21);
            this.ext_3gp.TabIndex = 5;
            this.ext_3gp.Text = "3gp";
            this.ext_3gp.UseVisualStyleBackColor = true;
            // 
            // ext_avi
            // 
            this.ext_avi.AutoSize = true;
            this.ext_avi.Location = new System.Drawing.Point(10, 177);
            this.ext_avi.Name = "ext_avi";
            this.ext_avi.Size = new System.Drawing.Size(43, 21);
            this.ext_avi.TabIndex = 7;
            this.ext_avi.Text = "avi";
            this.ext_avi.UseVisualStyleBackColor = true;
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
            // 
            // ext_asf
            // 
            this.ext_asf.AutoSize = true;
            this.ext_asf.Location = new System.Drawing.Point(10, 154);
            this.ext_asf.Name = "ext_asf";
            this.ext_asf.Size = new System.Drawing.Size(44, 21);
            this.ext_asf.TabIndex = 6;
            this.ext_asf.Text = "asf";
            this.ext_asf.UseVisualStyleBackColor = true;
            // 
            // ext_3g2
            // 
            this.ext_3g2.AutoSize = true;
            this.ext_3g2.Location = new System.Drawing.Point(10, 108);
            this.ext_3g2.Name = "ext_3g2";
            this.ext_3g2.Size = new System.Drawing.Size(49, 21);
            this.ext_3g2.TabIndex = 4;
            this.ext_3g2.Text = "3g2";
            this.ext_3g2.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 450);
            this.Controls.Add(this.clear_cache);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.uninstall);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.install);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.Text = "Configuration";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clear_cache;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox1;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label path;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ext_webm;
        private System.Windows.Forms.CheckBox ext_wmv;
        private System.Windows.Forms.CheckBox ext_vob;
        private System.Windows.Forms.CheckBox ext_mp2;
        private System.Windows.Forms.CheckBox ext_mpe;
        private System.Windows.Forms.CheckBox ext_mpg;
        private System.Windows.Forms.CheckBox ext_ogv;
        private System.Windows.Forms.CheckBox ext_mpv;
        private System.Windows.Forms.CheckBox ext_ts;
        private System.Windows.Forms.CheckBox ext_m2v;
        private System.Windows.Forms.CheckBox ext_mpeg;
        private System.Windows.Forms.CheckBox ext_m4p;
        private System.Windows.Forms.CheckBox ext_mkv;
        private System.Windows.Forms.CheckBox ext_mp4;
        private System.Windows.Forms.CheckBox ext_mov;
        private System.Windows.Forms.CheckBox ext_m4v;
        private System.Windows.Forms.CheckBox ext_flv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ext_3gp;
        private System.Windows.Forms.CheckBox ext_avi;
        private System.Windows.Forms.CheckBox ext_all;
        private System.Windows.Forms.CheckBox ext_asf;
        private System.Windows.Forms.CheckBox ext_3g2;
    }
}

