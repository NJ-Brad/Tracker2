namespace Tracker
{
    partial class NoteDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label3 = new Label();
            textBox4 = new TextBox();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(84, 22);
            label1.Name = "label1";
            label1.Size = new Size(39, 20);
            label1.TabIndex = 0;
            label1.Text = "Text:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(129, 19);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(584, 27);
            textBox1.TabIndex = 1;
            textBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(85, 55);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 2;
            label2.Text = "Tags";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(129, 52);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(584, 27);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(129, 131);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(584, 27);
            textBox3.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 134);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 4;
            label3.Text = "Follow Up";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(129, 164);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(131, 27);
            textBox4.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(40, 167);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 6;
            label4.Text = "Completed";
            // 
            // button1
            // 
            button1.Location = new Point(266, 164);
            button1.Name = "button1";
            button1.Size = new Size(126, 29);
            button1.TabIndex = 8;
            button1.Text = "Mark Complete";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(517, 201);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 9;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(617, 201);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 10;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(136, 85);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(38, 20);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Idea";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(180, 85);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(47, 20);
            linkLabel2.TabIndex = 12;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "I Owe";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(233, 85);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(61, 20);
            linkLabel3.TabIndex = 13;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "I Expect";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // NoteDetailsForm
            // 
            AcceptButton = button2;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button3;
            ClientSize = new Size(723, 244);
            Controls.Add(linkLabel3);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NoteDetailsForm";
            ShowInTaskbar = false;
            Text = "Note Details";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private Button button1;
        private Button button2;
        private Button button3;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
    }
}