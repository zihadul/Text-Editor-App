using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace TextEditorApp
{
    public partial class TextEditorMainMenuUI : Form
    {
        string path="";
        public TextEditorMainMenuUI()
        {
            InitializeComponent();
        }
        private void Clear()
        {
            path = "";
            this.TextEditor.Clear();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Save(bool SaveAs)
        {
            try
            {
                if (SaveAs == true)
                {
                    string file = SetFilePath();

                    if (file != "")
                    {
                        this.TextEditor.SaveFile(file, RichTextBoxStreamType.RichText);
                        path = file;
                        file = null;
                    }
                    if (exit) {
                        this.Close();
                    }
                }
                else
                {
                    if (path == "")
                    {
                        string file = SetFilePath();

                        if (file != "")
                        {
                            this.TextEditor.SaveFile(file, RichTextBoxStreamType.RichText);
                            path = file;
                            file = null;
                        }
                    }
                    else
                    {
                        this.TextEditor.SaveFile(path, RichTextBoxStreamType.RichText);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private string SetFilePath()
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "RTF (*.rtf)|*.rtf|TXT (*.txt)|*.txt";
            if (s.ShowDialog(this) == DialogResult.OK)
            {
                return s.FileName;
            }
            else
            {
                return "";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void Open()
        {
            try
            {
                string file = GetFilePath();

                if (file != "")
                {
                    Clear();
                    try
                    {
                        this.TextEditor.Rtf = System.IO.File.ReadAllText(file, System.Text.Encoding.Default);
                    }
                    catch (Exception) //error occured, that means we loaded invalid RTF, so load as plain text
                    {
                        this.TextEditor.Text = System.IO.File.ReadAllText(file, System.Text.Encoding.Default);
                    }
                    path = file;
                }
                file = null;
            }
            catch (Exception)
            {
                Clear();
            }
        }
        
        private string GetFilePath()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.RestoreDirectory = true;
            openFile.ShowReadOnly = false;
            openFile.ReadOnlyChecked = false;
            openFile.Filter = "RTF (*.rtf)|*.rtf|TXT (*.txt)|*.txt";
            if (openFile.ShowDialog(this) == DialogResult.OK)
            {
                return openFile.FileName;
            }
            else
            {
                return "";
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            TextEditor.Paste();
        }
        private bool exit = false;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.Text != "")
            {
                DialogResult result = MessageBox.Show(@"Do you want to save changes?", @"WrEnSoft Text Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Save(true);
                    exit = true;
                }
                if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            else {
                this.Close();

            }
            
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            TextEditor.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            TextEditor.Copy();
        }

        private void toolStripMainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void undoStripButton_Click(object sender, EventArgs e)
        {
            TextEditor.Undo();
        }

        private void redoStripButton_Click(object sender, EventArgs e)
        {
            TextEditor.Redo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.SelectAll();
        }

        private void selectAllToolStripButton_Click(object sender, EventArgs e)
        {
            TextEditor.SelectAll();
            
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Copy();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Redo();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
            {

                TextEditor.WordWrap = true;
            }
            else {

                TextEditor.WordWrap = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult fontResult = fontDialog.ShowDialog();
            if (fontResult == DialogResult.OK) {

                TextEditor.Font = fontDialog.Font;
            }
        }

        private void mainToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainToolBarToolStripMenuItem.Checked)
            {

                toolStripMainMenu.Visible = true;
            }
            else
            {
                toolStripMainMenu.Visible = false;
            }
        }

        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void fontToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult fontResult = fontDialog.ShowDialog();
            if (fontResult == DialogResult.OK)
            {

                TextEditor.Font = fontDialog.Font;
            }
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult colorResult = colorDialog.ShowDialog();
            if (colorResult == DialogResult.OK) {

                TextEditor.ForeColor = colorDialog.Color;
            }
        }

        private void colorToolStripButton_Click(object sender, EventArgs e)
        {

            DialogResult colorResult = colorDialog.ShowDialog();
            if (colorResult == DialogResult.OK)
            {

                TextEditor.ForeColor = colorDialog.Color;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (TextEditor.Text != "")
            {
                DialogResult result = MessageBox.Show(@"Do you want to save changes?", @"WrEnSoft Text Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Save(true);
                    exit = true;
                }
                if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();

            }
        }

        private void TextEditorMainMenuUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "N") {
                Clear();
            }
            if (e.Control && e.KeyCode.ToString() == "O")
            {
                Open();
            }
            if (e.Control && e.KeyCode.ToString() == "S")
            {
                Save(false);
            }
          


        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

       

      


       

       

       

       
    }
}
