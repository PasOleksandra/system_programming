using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace lab6._3
{
    public partial class Form1 : Form
    {
        string currentPath = "";

        public Form1()
        {
            InitializeComponent();
        }

        // Кнопка вибору папки
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                currentPath = folderDialog.SelectedPath;
                ShowFolderContent(currentPath);
            }
        }

        // Виведення вмісту папки
        private void ShowFolderContent(string path)
        {
            try
            {
                listBoxFiles.Items.Clear();
                txtPath.Text = path;
                currentPath = path;

                // Додавання папок
                string[] folders = Directory.GetDirectories(path);
                foreach (string folder in folders)
                {
                    listBoxFiles.Items.Add("[Папка] " + Path.GetFileName(folder));
                }

                // Додавання файлів
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    listBoxFiles.Items.Add(Path.GetFileName(file));
                }
            }
            catch
            {
                MessageBox.Show("Не вдалося відкрити папку.");
            }
        }

        // Перехід назад
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(currentPath);

                if (dir.Parent != null)
                {
                    ShowFolderContent(dir.Parent.FullName);
                }
            }
            catch
            {
                MessageBox.Show("Неможливо повернутися назад.");
            }
        }

        // Подвійний клік по елементу
        private void listBoxFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem == null)
                return;

            string selectedItem = listBoxFiles.SelectedItem.ToString();

            // Якщо вибрана папка
            if (selectedItem.StartsWith("[Папка] "))
            {
                string folderName = selectedItem.Replace("[Папка] ", "");
                string newPath = Path.Combine(currentPath, folderName);

                ShowFolderContent(newPath);
            }
            else
            {
                string filePath = Path.Combine(currentPath, selectedItem);

                try
                {
                    Process.Start(new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Не вдалося відкрити файл.");
                }
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}