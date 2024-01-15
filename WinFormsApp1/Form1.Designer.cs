using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            kostnadInput = new NumericUpDown();
            betaladInput = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            outputBox = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)kostnadInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)betaladInput).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(394, 61);
            button1.Name = "button1";
            button1.Size = new Size(133, 88);
            button1.TabIndex = 3;
            button1.Text = "Beräkna";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // kostnadInput
            // 
            kostnadInput.Location = new Point(15, 47);
            kostnadInput.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            kostnadInput.Name = "kostnadInput";
            kostnadInput.Size = new Size(364, 39);
            kostnadInput.TabIndex = 4;
            // 
            // betaladInput
            // 
            betaladInput.Location = new Point(15, 124);
            betaladInput.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            betaladInput.Name = "betaladInput";
            betaladInput.Size = new Size(364, 39);
            betaladInput.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 12);
            label1.Name = "label1";
            label1.Size = new Size(161, 32);
            label1.TabIndex = 7;
            label1.Text = "Ange kostnad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 89);
            label2.Name = "label2";
            label2.Size = new Size(238, 32);
            label2.TabIndex = 8;
            label2.Text = "Ange betalad mängd";
            // 
            // outputBox
            // 
            outputBox.Location = new Point(15, 181);
            outputBox.Name = "outputBox";
            outputBox.ReadOnly = true;
            outputBox.Size = new Size(364, 292);
            outputBox.TabIndex = 9;
            outputBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(539, 485);
            Controls.Add(outputBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(betaladInput);
            Controls.Add(kostnadInput);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Växeluträknare";
            ((System.ComponentModel.ISupportInitialize)kostnadInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)betaladInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var kostnad = Convert.ToInt32(kostnadInput.Value);
            var betald = Convert.ToInt32(betaladInput.Value);

            outputBox.Text = returnChange(kostnad, betald);
        }
        private string returnChange(int cost, int payed)
        {
            String returnString = "";
            // Här deklarerar vi all olika sedlar/mynt vi vill dela upp betalningen i.
            int[] sedlar = { 500, 200, 100, 50, 20, 10, 5, 1 };
            Dictionary<int, Dictionary<string, string>> sedlarmynt = new Dictionary<int, Dictionary<string, string>>{
                {500, new Dictionary<string, string>{{"femhundralapp", "femhundralappar"}}},
                {200, new Dictionary<string, string>{{"tvåhundralapp", "tvåhundralappar"}}},
                {100, new Dictionary<string, string>{{"hundralapp", "hundralappar"}}},
                {50, new Dictionary<string, string>{{"femtiolapp", "femtiolappar"}}},
                {20, new Dictionary<string, string>{{"tjuga", "tjugor"}}},
                {10, new Dictionary<string, string>{{"tiokrona", "tiokronor"}}},
                {5, new Dictionary<string, string>{{"femkrona", "femkronor"}}},
                {1, new Dictionary<string, string>{{"enkrona", "enrkonor"}}}
            };
            // Vi kollar att vi fått in rimliga värden
            if (payed > cost)
            {
                returnString = "Växel tillbaka:";
                // Räknar ut växel
                int change = payed - cost;
                // För varje sedel/mynt i vår lista
                foreach (var outerDict in sedlarmynt)
                {
                    // Först hämtar vi värdet i vår dictionary
                    int outerKey = outerDict.Key;
                    // Sedan hämtar vi texten som tillhör värdet
                    var innerDict = outerDict.Value;
                    // Vi sätter texten till singular
                    string text = innerDict.First().Key;
                    // Om det är plural av ett mynt/sedel
                    if (change / outerKey > 1)
                    {
                        // Vi ändrar texten till plural och skriver ut antal och text
                        text = innerDict.First().Value;
                        returnString = returnString + Environment.NewLine + change / outerKey + " " + text;
                    }
                    // Om det är singular av ett mynt/sedel
                    else if (change / outerKey == 1)
                    {
                        // Vi skriver ut antal och text
                        returnString = returnString + Environment.NewLine + change / outerKey + " " + text;
                    }
                    // Här sätter vi växel till det kvarvarande värdet
                    change = change % outerKey;
                }
                return returnString;
            }
            else
            {
                // Om input inte är rimligt (vi har betalat mindre än kostnaden) så skriver vi ut felmeddelande
                // Man skulle kunna tänka sig här att man skriver ut kvarvarande belopp istället.
                return "Invalid input";
            }
        }
        private Button button1;
        private NumericUpDown kostnadInput;
        private NumericUpDown betaladInput;
        private Label label1;
        private Label label2;
        private RichTextBox outputBox;
    }
}