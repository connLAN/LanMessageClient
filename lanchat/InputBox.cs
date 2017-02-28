using System;
using System.Drawing;
using System.Windows.Forms;

namespace LANChat
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button, 
        /// and then returns a string containing the contents of the text box.
        /// </summary>
        /// <param name="prompt">The text to display as message in the input box.</param>
        /// <returns></returns>
        public static string Show(string prompt)
        {
            return Show(prompt, "Input", string.Empty, FormStartPosition.CenterScreen, new Point(0, 0), false);
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button, 
        /// and then returns a string containing the contents of the text box.
        /// </summary>
        /// <param name="prompt">The text to display as message in the input box.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <returns></returns>
        public static string Show(string prompt, string caption)
        {
            return Show(prompt, caption, string.Empty, FormStartPosition.CenterScreen, new Point(0, 0), false);
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button, 
        /// and then returns a string containing the contents of the text box.
        /// </summary>
        /// <param name="prompt">The text to display as message in the input box.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="position">A point that represents the upper-left corner of the input box in screen cooridnates.</param>
        /// <param name="defaultResponse">The text displayed in the text box as the default response.</param>
        /// <returns></returns>
        public static string Show(string prompt, string caption, Point position)
        {
            return Show(prompt, caption, string.Empty, FormStartPosition.Manual, position, false);
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button, 
        /// and then returns a string containing the contents of the text box.
        /// </summary>
        /// <param name="prompt">The text to display as message in the input box.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">The text displayed in the text box as the default response.</param>
        /// <param name="position">A point that represents the upper-left corner of the input box in screen cooridnates.</param>
        /// <returns></returns>
        public static string Show(string prompt, string caption, string defaultResponse, Point position)
        {
            return Show(prompt, caption, defaultResponse, FormStartPosition.Manual, position, false);
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button, 
        /// and then returns a string containing the contents of the text box.
        /// </summary>
        /// <param name="prompt">The text to display as message in the input box.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">The text displayed in the text box as the default response.</param>
        /// <param name="position">A point that represents the upper-left corner of the input box in screen cooridnates.</param>
        /// <param name="multiLine">Indicates whether the text box should be multiline.</param>
        /// <returns></returns>
        public static string Show(string prompt, string caption, string defaultResponse, Point position, bool multiLine)
        {
            return Show(prompt, caption, defaultResponse, FormStartPosition.Manual, position, multiLine);
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button, 
        /// and then returns a string containing the contents of the text box.
        /// </summary>
        /// <param name="prompt">The text to display as message in the input box.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">The text displayed in the text box as the default response.</param>
        /// <param name="formStartPosition">Starting position of the input box.</param>
        /// <param name="position">A point that represents the upper-left corner of the input box in screen cooridnates.</param>
        /// <returns></returns>
        private static string Show(string prompt, string caption, string defaultResponse,
            FormStartPosition formStartPosition, Point position, bool multiLine)
        {
            string value = string.Empty;
            using (InputBox inputBox = new InputBox()) {
                if (multiLine) {
                    inputBox.ClientSize = new Size(300, 205);
                    inputBox.txtInput.Multiline = true;
                    inputBox.txtInput.ScrollBars = ScrollBars.Vertical;
                    using (FontFamily fontFamily = new FontFamily("Tahoma"))
                        inputBox.txtInput.Font = new Font(fontFamily, 9);
                    inputBox.tableLayoutPanel.RowStyles[1].Height += 100;
                }
                else {
                    inputBox.ClientSize = new Size(300, 105);
                }
                inputBox.StartPosition = formStartPosition;
                if (formStartPosition == FormStartPosition.Manual) {
                    position.X = position.X < 0 ? 0 : position.X;
                    position.Y = position.Y < 0 ? 0 : position.Y;
                    Rectangle screenRect = SystemInformation.WorkingArea;
                    Rectangle windowRect = new Rectangle(position, inputBox.Size);
                    position.X = windowRect.Right > screenRect.Right ? screenRect.Right - inputBox.Width : position.X;
                    position.Y = windowRect.Bottom > screenRect.Bottom ? screenRect.Bottom - inputBox.Height : position.Y;
                }
                inputBox.Location = position;
                inputBox.txtInput.Text = defaultResponse;
                inputBox.Text = caption;
                inputBox.lblPrompt.Text = prompt;
                if (inputBox.ShowDialog() == DialogResult.OK)
                    value = inputBox.txtInput.Text;
            }
            return value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtInput.Text = string.Empty;
        }
    }
}
