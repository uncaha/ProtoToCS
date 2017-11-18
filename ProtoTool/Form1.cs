using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System.IO;
using ProtoBuf.Reflection;
namespace ProtoTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int Export()
        {
            int exitCode = 0;
            string tpath = textBox1.Text;
            string tdirpath = textBox2.Text;
            CodeGenerator codegen = CSharpCodeGenerator.Default;
            var set = new FileDescriptorSet();
            set.AddImportPath(tpath);

            DirectoryInfo tdirfolder = new DirectoryInfo(tpath);
            FileInfo[] tfileinfos = tdirfolder.GetFiles("*.proto", System.IO.SearchOption.AllDirectories);
            foreach (var input in tfileinfos)
            {
                if (!set.Add(input.Name, true))
                {
                    Console.Error.WriteLine($"File not found: {input}");
                    exitCode = 1;
                }
            }


            set.Process();
            var errors = set.GetErrors();
            foreach (var err in errors)
            {
                if (err.IsError) exitCode++;
                Console.Error.WriteLine(err.ToString());
            }
            if (exitCode != 0) return exitCode;

            var files = codegen.Generate(set);
            foreach (var file in files)
            {
                var path = Path.Combine(tdirpath, file.Name);
                File.WriteAllText(path, file.Text);
            }


            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Export();
        }

    }
}
