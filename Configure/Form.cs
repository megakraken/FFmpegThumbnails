using System;
using System.Drawing;
using System.Windows.Forms;

namespace Configure {
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();
            AddExtensionCheckboxes();
        }

        void AddExtensionCheckboxes() {
            int l = 10, t = 108, h = 23, w = 80,
                numRow = 12, i = 0;
            foreach(var pair in Config.GetFileAssociations()) {
                var col = (int)(i / (double)numRow);
                var cb = new CheckBox {
                    Name = $"ext_{pair.Key}",
                    Text = pair.Key,
                    Checked = pair.Value,
                    UseVisualStyleBackColor = true,
                    Location = new Point(l + col * w, t + (i % numRow) * h),
                    Size = new Size(70, 21),
                };
                cb.CheckedChanged += (s, _) => apply.Enabled = true;
                fileTypes.Controls.Add(cb);
                i++;
            }
        }

        private void install_Click(object sender, EventArgs e) {
            try {
                Config.Install();
                MessageBox.Show(this, "FFmpegThumbnailHandler has been installed.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error installing FFmpegThumbnailHandler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            } finally {
                UpdateStatus();
            }
        }

        private void uninstall_Click(object sender, EventArgs e) {
            try {
                Config.Uninstall();
                MessageBox.Show(this, "FFmpegThumbnailHandler has been removed.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error uninstalling FFmpegThumbnailHandler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            } finally {
                UpdateStatus();
            }
        }

        private void clear_cache_Click(object sender, EventArgs e) {
            try {
                Config.ClearThumbnailCache();
                MessageBox.Show(this, "Thumbnail Cache has been cleared.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error clearing Thu,bnail Cache",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void apply_Click(object sender, EventArgs e) {
            apply.Enabled = false;
        }

        private void Form_Load(object sender, EventArgs e) {
            UpdateStatus();
            foreach (var c in thumbnails.Controls) {
                if (c is RadioButton rb)
                    rb.CheckedChanged += (s, _) => apply.Enabled = true;
                if (c is TextBox tb)
                    tb.TextChanged += (s, _) => apply.Enabled = true;
            }
        }

        private void ext_all_CheckedChanged(object sender, EventArgs e) {
            foreach (var c in fileTypes.Controls) {
                if (c is CheckBox cb)
                    cb.Checked = ext_all.Checked;
            }
        }

        void UpdateStatus() {
            var path = Config.GetInstallationPath();
            var installed = !string.IsNullOrEmpty(path);
            uninstall.Enabled = installed;
            if (installed == false) {
                status.Text = "not installed";
                status.ForeColor = Color.Red;
            } else {
                status.Text = "installed";
                status.ForeColor = Color.LimeGreen;

                const int ThumbnailFirstFrame = unchecked((int)0x80000001);
                const int ThumbnailBeginning  = unchecked((int)0x80000002);
                const int ThumbnailMiddle     = unchecked((int)0x80000003);
                int timestamp = Config.GetThumbnailTimestamp();

                switch (timestamp) {
                    case ThumbnailFirstFrame:
                        ts_first.Checked = true;
                        break;
                    case ThumbnailBeginning:
                        ts_beginning.Checked = true;
                        break;
                    case ThumbnailMiddle:
                        ts_middle.Checked = true;
                        break;
                    case -1:
                        break;
                    default:
                        if (timestamp > 0) {
                            ts_custom.Checked = true;
                            seconds.Text = timestamp.ToString();
                        }
                        break;
                }
            }
            installationPath.Text = path;
        }

        private void seconds_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }
    }
}
