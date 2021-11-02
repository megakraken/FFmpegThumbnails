using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
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
                MessageBox.Show(this, "FFmpegThumbnailProvider has been installed.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error installing FFmpegThumbnailProvider",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            } finally {
                UpdateControls();
            }
        }

        private void uninstall_Click(object sender, EventArgs e) {
            try {
                Config.Uninstall();
                MessageBox.Show(this, "FFmpegThumbnailProvider has been removed.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error uninstalling FFmpegThumbnailProvider",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            } finally {
                UpdateControls();
            }
        }

        private void clear_cache_Click(object sender, EventArgs e) {
            try {
                Config.ClearThumbnailCache();
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error clearing Thumbnail Cache",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void apply_Click(object sender, EventArgs e) {
            int timestamp = -1;
            foreach (var c in thumbnails.Controls) {
                if (c is RadioButton rb) {
                    if (rb.Checked) {
                        if (rb.Tag != null) {
                            timestamp = int.Parse(rb.Tag.ToString(), NumberStyles.HexNumber);
                        } else {
                            int.TryParse(seconds.Text, out timestamp);
                        }
                    }
                }
            }
            if (timestamp != -1)
                Config.SetThumbnailTimestamp(timestamp);
            foreach (var c in fileTypes.Controls) {
                if (c is CheckBox cb) {
                    if (cb == ext_all)
                        continue;
                    Config.SetFileAssociation(cb.Text, cb.Checked);
                }
            }
            apply.Enabled = false;
        }

        private void Form_Load(object sender, EventArgs e) {
            UpdateControls();
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

        void UpdateControls() {
            var path = Config.GetInstallationPath();
            var installed = !string.IsNullOrEmpty(path);
            uninstall.Enabled = installed;
            if (installed == false) {
                status.Text = "not installed";
                status.ForeColor = Color.Red;
            } else {
                status.Text = "installed";
                status.ForeColor = Color.LimeGreen;
            }
            installationPath.Text = path;

            var timestamp = Config.GetThumbnailTimestamp();
            if (timestamp > 0) {
                ts_custom.Checked = true;
                seconds.Text = timestamp.ToString();
            } else {
                var rb = new[] { ts_first, ts_beginning, ts_middle };
                foreach (var r in rb)
                    r.Checked = int.Parse(r.Tag.ToString(), NumberStyles.HexNumber) == timestamp;
            }
            foreach (var c in fileTypes.Controls) {
                if (c is CheckBox cb)
                    cb.Enabled = installed;
            }
            foreach (Control c in thumbnails.Controls) {
                if (c is RadioButton || c is TextBox)
                    c.Enabled = installed;
            }
            if (installed) {
                var provider = Path.Combine(path, "FFmpegThumbnailProvider.dll");
                if (!File.Exists(provider)) {
                    info_label.Text = "Warning";
                    info_label.ForeColor = Color.Coral;
                    info_text.Text =
                        "Files don't exist under installation path. Please reinstall FFmpegThumbnailHandler.";
                } else {
                    var info = FileVersionInfo.GetVersionInfo(provider);
                    info_label.Text = "Version Information (installed)";
                    info_label.ForeColor = Color.LimeGreen;
                    info_text.Text = info.ProductName;
                }
            } else {
                var info = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                info_label.Text = "Version Information (contained)";
                info_label.ForeColor = Color.Coral;
                info_text.Text = info.ProductName;
            }
            apply.Enabled = false;
        }

        private void seconds_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }
    }
}
