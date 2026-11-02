using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace IDCard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                TextRenderer.DrawText(e.Graphics, "FOTO", pictureBox1.Font,
                    pictureBox1.ClientRectangle, Color.FromArgb(54, 85, 129),
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Title = "Şəkil seçin";
                op.Filter = "Şəkillər (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (op.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    // Copy the image so its source file is not left locked.
                    using (Image source = Image.FromFile(op.FileName))
                    {
                        Image previous = pictureBox1.Image;
                        pictureBox1.Image = new Bitmap(source);
                        if (previous != null) previous.Dispose();
                    }
                    pictureBox1.Invalidate();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Şəkil açıla bilmədi. Başqa şəkil seçin.", "Xəta",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { label22.Text = textBox1.Text; }
        private void textBox2_TextChanged(object sender, EventArgs e) { label21.Text = textBox2.Text; }
        private void textBox4_TextChanged(object sender, EventArgs e) { label5.Text = textBox4.Text; }
        private void textBox3_TextChanged(object sender, EventArgs e) { label6.Text = textBox3.Text; }
        private void textBox6_TextChanged(object sender, EventArgs e) { label8.Text = textBox6.Text; }
        private void textBox5_TextChanged(object sender, EventArgs e) { label11.Text = textBox5.Text; }
        private void textBox8_TextChanged(object sender, EventArgs e) { label7.Text = textBox8.Text; }
        private void textBox7_TextChanged(object sender, EventArgs e) { label12.Text = textBox7.Text; }
        private void textBox10_TextChanged(object sender, EventArgs e) { label18.Text = textBox10.Text; }
        private void textBox12_TextChanged(object sender, EventArgs e) { label15.Text = textBox12.Text; }
        private void textBox11_TextChanged(object sender, EventArgs e) { label17.Text = textBox11.Text; }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label10.Text = dateTimePicker1.Checked ? dateTimePicker1.Value.ToString("dd.MM.yyyy") : "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear();
            textBox10.Clear(); textBox11.Clear(); textBox12.Clear();
            dateTimePicker1.Checked = false;
            label10.Text = "";
            Image previous = pictureBox1.Image;
            pictureBox1.Image = null;
            if (previous != null) previous.Dispose();
            pictureBox1.Invalidate();
            textBox1.Focus();
        }

        private bool ValidateInputs()
        {
            TextBox[] inputs = { textBox1, textBox2, textBox4, textBox3, textBox6,
                textBox5, textBox8, textBox7, textBox10, textBox12, textBox11 };
            foreach (TextBox input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input.Text))
                {
                    MessageBox.Show(this, "Məlumatlar tam deyil. Bütün xanaları doldurun.", "Xəta",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    input.Focus();
                    return false;
                }
            }
            if (!dateTimePicker1.Checked)
            {
                MessageBox.Show(this, "Doğum tarixini seçin (tarixin yanındakı işarəni aktiv edin).", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker1.Focus();
                return false;
            }
            return true;
        }

        private string DocumentText()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("AZƏRBAYCAN RESPUBLİKASI");
            result.AppendLine("Şəxsiyyət vəsiqəsi");
            result.AppendLine();
            result.AppendLine("Seriya və nömrə: " + textBox1.Text);
            result.AppendLine("FİN kod: " + textBox2.Text);
            result.AppendLine("Soyad: " + textBox4.Text);
            result.AppendLine("Ad: " + textBox3.Text);
            result.AppendLine("Ata adı: " + textBox6.Text);
            result.AppendLine("Cins: " + textBox5.Text);
            result.AppendLine("Doğum tarixi: " + dateTimePicker1.Value.ToString("dd.MM.yyyy"));
            result.AppendLine("Doğum yeri: " + textBox8.Text);
            result.AppendLine("Ailə vəziyyəti: " + textBox7.Text);
            result.AppendLine("Verilmə tarixi: " + textBox10.Text);
            result.AppendLine("Etibarlıdır: " + textBox12.Text);
            result.AppendLine("Verən orqan: " + textBox11.Text);
            return result.ToString();
        }

        private Bitmap RenderCard()
        {
            Bitmap image = new Bitmap(cardPanel.Width, cardPanel.Height);
            try
            {
                cardPanel.DrawToBitmap(image, cardPanel.ClientRectangle);
                // Render children explicitly: this also supports runtimes whose
                // DrawToBitmap implementation only renders the parent control.
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    for (int i = cardPanel.Controls.Count - 1; i >= 0; i--)
                    {
                        Control child = cardPanel.Controls[i];
                        if (!child.Visible || child.Width == 0 || child.Height == 0) continue;
                        using (Bitmap part = new Bitmap(child.Width, child.Height))
                        {
                            child.DrawToBitmap(part, child.ClientRectangle);
                            graphics.DrawImageUnscaled(part, child.Location);
                        }
                    }
                }
                return image;
            }
            catch
            {
                image.Dispose();
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            using (SaveFileDialog sv = new SaveFileDialog())
            {
                sv.Title = "Vəsiqəni yadda saxla";
                sv.Filter = "Vəsiqə şəkli (*.png)|*.png|Mətn faylı (*.txt)|*.txt";
                sv.DefaultExt = "png";
                sv.AddExtension = true;
                sv.FileName = "Vesiqe";
                if (sv.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    if (sv.FilterIndex == 2)
                        File.WriteAllText(sv.FileName, DocumentText(), new UTF8Encoding(true));
                    else
                    {
                        using (Bitmap image = RenderCard())
                        {
                            image.Save(sv.FileName, ImageFormat.Png);
                        }
                    }
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    timer1.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Fayl saxlanmadı: " + ex.Message, "Xəta",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = Math.Min(progressBar1.Maximum, progressBar1.Value + 10);
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                progressBar1.Visible = false;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer1.Stop();
            Image previous = pictureBox1.Image;
            pictureBox1.Image = null;
            if (previous != null) previous.Dispose();
            base.OnFormClosed(e);
        }
    }
}
