using System.Drawing;
using System.Windows.Forms;

namespace EWalletApp.UI
{
    public static class UiTheme
    {
        public static readonly Color FormBackground = Color.FromArgb(241, 245, 249);
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);
        public static readonly Color Secondary = Color.FromArgb(71, 85, 105);
        public static readonly Color Success = Color.FromArgb(5, 150, 105);
        public static readonly Color Accent = Color.FromArgb(124, 58, 237);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Neutral = Color.FromArgb(148, 163, 184);
        public static readonly Color TitleColor = Color.FromArgb(15, 23, 42);
        public static readonly Color BalanceColor = Color.FromArgb(13, 148, 136);
        public static readonly Color LabelColor = Color.FromArgb(51, 65, 85);

        public static readonly Font TitleFont = new("Segoe UI", 16, FontStyle.Bold);
        public static readonly Font SubtitleFont = new("Segoe UI", 12, FontStyle.Bold);
        public static readonly Font BodyFont = new("Segoe UI", 9.75f);

        public static void ApplyForm(Form form)
        {
            form.BackColor = FormBackground;
            form.Font = BodyFont;
        }

        public static void StyleTitle(Label label)
        {
            label.Font = TitleFont;
            label.ForeColor = TitleColor;
        }

        public static void StyleLabel(Label label)
        {
            label.Font = BodyFont;
            label.ForeColor = LabelColor;
        }

        public static void StyleTextBox(TextBox textBox)
        {
            textBox.Font = BodyFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
        }

        public static void StyleButton(Button button, Color backColor, Color? foreColor = null)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = foreColor ?? Color.White;
            button.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void StylePrimaryButton(Button button) => StyleButton(button, Primary);
        public static void StyleSecondaryButton(Button button) => StyleButton(button, Secondary);
        public static void StyleSuccessButton(Button button) => StyleButton(button, Success);
        public static void StyleAccentButton(Button button) => StyleButton(button, Accent);
        public static void StyleDangerButton(Button button) => StyleButton(button, Danger);
        public static void StyleNeutralButton(Button button) => StyleButton(button, Neutral);

        public static void StyleDataGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Primary;
            grid.ColumnHeadersHeight = 36;
            grid.DefaultCellStyle.Font = BodyFont;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = TitleColor;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.GridColor = Color.FromArgb(226, 232, 240);
        }
    }
}
