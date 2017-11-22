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
        string mSPath = "";
        string mDPaht = "";
        string mPathsFile = "/Paths.txt";
        public Form1()
        {
            InitializeComponent();

            string tpathfile = Directory.GetCurrentDirectory() + mPathsFile;
            if(File.Exists(tpathfile))
            {
               string[] tlines =   File.ReadAllLines(tpathfile);
                textBox1.Text = mSPath = tlines[0];
                textBox2.Text = mDPaht = tlines[1];

            }

          //  string tpath = textBox1.Text;
           // string tdirpath = textBox2.Text;
        }

        private int Export()
        {
            textBox3.Text = "";
            System.Text.StringBuilder tnewmsg = new StringBuilder();
            int exitCode = 0;
            mSPath = textBox1.Text;
            mDPaht = textBox2.Text;

            if (string.IsNullOrEmpty(mSPath) || string.IsNullOrEmpty(mDPaht)) return 1;
            string tpathfile =  Directory.GetCurrentDirectory() + mPathsFile;
            StringBuilder tstrb = new StringBuilder();
            tstrb.AppendLine(mSPath);
            tstrb.AppendLine(mDPaht);
            File.WriteAllText(tpathfile, tstrb.ToString());


            tnewmsg.AppendLine(System.DateTime.Now.ToString() + ":开始导出.");
            textBox3.Text = tnewmsg.ToString();
            CodeGenerator codegen = CSharpCodeGenerator.Default;
            var set = new FileDescriptorSet();
            set.AddImportPath(mSPath);

            string[] tpaths = Directory.GetDirectories(mSPath);

            foreach(string tp in tpaths)
            {
                set.AddImportPath(tp);
            }

            DirectoryInfo tdirfolder = new DirectoryInfo(mSPath);
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
                tnewmsg.AppendLine(err.ToString());              
                Console.Error.WriteLine(err.ToString());
            }
            textBox3.Text = tnewmsg.ToString();
            if (exitCode != 0) return exitCode;

            var files = codegen.Generate(set);
            foreach (var file in files)
            {
                var path = Path.Combine(mDPaht, file.Name);
                File.WriteAllText(path, file.Text);
            }

            tnewmsg.AppendLine(System.DateTime.Now.ToString() + ":导出结束.");
            textBox3.Text = tnewmsg.ToString();
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Export();
            }
            catch (Exception _error)
            {
                textBox3.Text += _error.ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog topen = new FolderBrowserDialog();
            topen.RootFolder = Environment.SpecialFolder.MyComputer;
            if (topen.ShowDialog() == DialogResult.OK)
            {
                mSPath = topen.SelectedPath;
                textBox1.Text = mSPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog topen = new FolderBrowserDialog();
            topen.RootFolder = Environment.SpecialFolder.MyComputer;
            if (topen.ShowDialog() == DialogResult.OK)
            {
                mDPaht = topen.SelectedPath;
                textBox2.Text = mDPaht;
            }
        }
    }
}
