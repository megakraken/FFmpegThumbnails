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
            }
        }

        private void uninstall_Click(object sender, EventArgs e) {
            try {
                Config.Uninstall();
                MessageBox.Show(this, "FFmpegThumbnailHandler has been installed.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error installing FFmpegThumbnailHandler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
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
    }
}
