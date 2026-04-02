using lab5;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace lab5
    
{
    public partial class Form1 : Form
    {
        private Building building;

        public Form1()
        {
            InitializeComponent();
 
            building = new Building("Ейфелева вежа", 1889, 330, new List<string> { "Туристична", "Оглядова" });

        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            treeViewProperties.Nodes.Clear();

            DisplayProperties(building);
        }

        // Завдання 4
        private void DisplayProperties(object obj)
        {
            Type type = obj.GetType();

            // Кореневий вузол
            TreeNode root = new TreeNode($"Клас: {type.Name}");
            treeViewProperties.Nodes.Add(root);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                object value = prop.GetValue(obj);

                if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    TreeNode collectionNode = new TreeNode($"{prop.Name} ({prop.PropertyType.Name})");

                    foreach (var item in enumerable)
                    {
                        collectionNode.Nodes.Add($"({item.GetType().Name}) {item}");
                    }

                    root.Nodes.Add(collectionNode);
                }
                else
                {
                    root.Nodes.Add($"{prop.Name} ({prop.PropertyType.Name}) = {value}");
                }
            }

            treeViewProperties.ExpandAll();
        }
    }
}