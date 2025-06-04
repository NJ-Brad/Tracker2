namespace Tracker
{
    partial class FlagSelectionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            radioButtonIdea = new RadioButton();
            radioButtonNone = new RadioButton();
            radioButtonIOwe = new RadioButton();
            radioButtonTheyOwe = new RadioButton();
            SuspendLayout();
            // 
            // radioButtonIdea
            // 
            radioButtonIdea.Appearance = Appearance.Button;
            radioButtonIdea.AutoSize = true;
            radioButtonIdea.CausesValidation = false;
            radioButtonIdea.Image = Properties.Resources.Idea;
            radioButtonIdea.ImageAlign = ContentAlignment.MiddleLeft;
            radioButtonIdea.Location = new Point(74, 3);
            radioButtonIdea.Name = "radioButtonIdea";
            radioButtonIdea.Size = new Size(72, 35);
            radioButtonIdea.TabIndex = 30;
            radioButtonIdea.Text = "Idea";
            radioButtonIdea.TextImageRelation = TextImageRelation.ImageBeforeText;
            radioButtonIdea.UseVisualStyleBackColor = true;
            radioButtonIdea.Click += SelectionChanged;
            // 
            // radioButtonNone
            // 
            radioButtonNone.Appearance = Appearance.Button;
            radioButtonNone.AutoSize = true;
            radioButtonNone.CausesValidation = false;
            radioButtonNone.Location = new Point(3, 3);
            radioButtonNone.Name = "radioButtonNone";
            radioButtonNone.Size = new Size(65, 35);
            radioButtonNone.TabIndex = 29;
            radioButtonNone.Text = "None";
            radioButtonNone.UseVisualStyleBackColor = true;
            radioButtonNone.CheckedChanged += SelectionChanged;
            // 
            // radioButtonIOwe
            // 
            radioButtonIOwe.Appearance = Appearance.Button;
            radioButtonIOwe.AutoSize = true;
            radioButtonIOwe.CausesValidation = false;
            radioButtonIOwe.Image = Properties.Resources.ArrowRight;
            radioButtonIOwe.Location = new Point(152, 3);
            radioButtonIOwe.Name = "radioButtonIOwe";
            radioButtonIOwe.Size = new Size(84, 35);
            radioButtonIOwe.TabIndex = 31;
            radioButtonIOwe.Text = "I Owe";
            radioButtonIOwe.TextImageRelation = TextImageRelation.ImageBeforeText;
            radioButtonIOwe.UseVisualStyleBackColor = true;
            radioButtonIOwe.Click += SelectionChanged;
            // 
            // radioButtonTheyOwe
            // 
            radioButtonTheyOwe.Appearance = Appearance.Button;
            radioButtonTheyOwe.AutoSize = true;
            radioButtonTheyOwe.CausesValidation = false;
            radioButtonTheyOwe.Image = Properties.Resources.ArrowLeft;
            radioButtonTheyOwe.Location = new Point(242, 3);
            radioButtonTheyOwe.Name = "radioButtonTheyOwe";
            radioButtonTheyOwe.Size = new Size(116, 35);
            radioButtonTheyOwe.TabIndex = 32;
            radioButtonTheyOwe.Text = "They Owe";
            radioButtonTheyOwe.TextImageRelation = TextImageRelation.ImageBeforeText;
            radioButtonTheyOwe.UseVisualStyleBackColor = true;
            radioButtonTheyOwe.Click += SelectionChanged;
            // 
            // FlagSelectionControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(radioButtonTheyOwe);
            Controls.Add(radioButtonIOwe);
            Controls.Add(radioButtonIdea);
            Controls.Add(radioButtonNone);
            Name = "FlagSelectionControl";
            Size = new Size(467, 45);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton radioButtonIdea;
        private RadioButton radioButtonNone;
        private RadioButton radioButtonIOwe;
        private RadioButton radioButtonTheyOwe;
    }
}
