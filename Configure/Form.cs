using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configure {
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();
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
            foreach (var c in fileTypes.Controls) {
                if (c is CheckBox cb)
                    cb.CheckedChanged += (s, _) => apply.Enabled = true;
            }
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
            if (string.IsNullOrEmpty(path)) {
                status.Text = "not installed";
                status.ForeColor = Color.Red;
            } else {
                status.Text = "installed";
                status.ForeColor = Color.Green;
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
