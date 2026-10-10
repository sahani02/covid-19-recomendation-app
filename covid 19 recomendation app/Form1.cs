namespace covid_19_recomendation_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string vaccination, tick1, tick2, tick3;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) // "Vaccinated" selected
            {
                vaccination = "Vaccinated"; // Set vaccination status
                comboBox1.Enabled = true;  // Enable comboBox for vaccine selection
                comboBox1.SelectedIndex = 0; // Set the default selection
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) // "Not Vaccinated" selected
            {
                vaccination = "Not Vaccinated"; // Set vaccination status
                comboBox1.Enabled = false; // Disable comboBox for vaccine selection
                comboBox1.Text = "No Vaccine"; // Display default text
            }
        }



        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null) // Ensure an item is selected
            {
                // Remove the selected item from listBox2
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedItem = listBox1.SelectedItem.ToString();

                // Add the selected item to listBox2 if it's not already there
                if (!listBox2.Items.Contains(selectedItem))
                {
                    listBox2.Items.Add(selectedItem);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if all safety questions are answered "Yes"
            bool allSafetyQuestionsYes = (tick1 == "Yes" && tick2 == "Yes" && tick3 == "Yes");

            // Count the number of symptoms selected in listBox2
            int symptomCount = listBox2.Items.Count;

            // Collect user details for output
            string name = textBox1.Text; // Replace with actual input field value
            string age = numericUpDown1.Value.ToString(); // Replace with actual input field value
            string nic = textBox2.Text; // Replace with actual input field value
            string selectedVaccine = comboBox1.Enabled ? comboBox1.SelectedItem.ToString() : "No Vaccine";

            // Collect symptoms
            string symptoms = listBox2.Items.Count > 0 ? string.Join("\n", listBox2.Items.Cast<string>()) : "None";

            // Collect safety question answers
            string safetyQuestions =
                $"Do you wear masks in public? : {tick1}\n" +
                $"Do you sanitize your hands? : {tick2}\n" +
                $"Do you follow social distancing? : {tick3}";

            // Determine recommendation and formatting
            string recommendation = "";
            Color backgroundColor = Color.Gray;

            if (symptomCount > 3)
            {
                // Danger condition
                backgroundColor = Color.Red;
                recommendation = "You may be in Danger! Visit the nearest treatment center.";
            }
            else if (!allSafetyQuestionsYes || vaccination == "Not Vaccinated" || (symptomCount >= 1 && symptomCount <= 3))
            {
                // Home quarantine recommended
                backgroundColor = Color.Orange;
                recommendation = "You may not be Safe! Home quarantine is recommended.";
            }
            else if (allSafetyQuestionsYes && vaccination == "Vaccinated" && symptomCount == 0)
            {
                // Safe condition
                backgroundColor = Color.Green;
                recommendation = "You are Safe! No action is needed.";
            }

            // Build the output string
            string output =
                "******* Personal Details *******\n" +
                $"Name: {name}\n" +
                $"Age: {age} years old\n" +
                $"NIC: {nic}\n" +
                "******* Vaccination Details *******\n" +
                $"Vaccine: {selectedVaccine}\n" +
                "******* Symptoms *******\n" +
                $"{symptoms}\n" +
                "******* Safety Questions *******\n" +
                $"{safetyQuestions}\n" +
                "******* Recommendation *******\n" +
                recommendation;

            // Update the RichTextBox
            richTextBox1.BackColor = backgroundColor;
            richTextBox1.ForeColor = Color.White;
            richTextBox1.Text = output;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tick1 = checkBox1.Checked ? "Yes" : "No";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            tick2 = checkBox2.Checked ? "Yes" : "No";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            tick3 = checkBox3.Checked ? "Yes" : "No";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Gray; // Set default background color
            richTextBox1.Text = ""; // Initially empty
            comboBox1.Enabled = false; // Disable comboBox until vaccination is selected
        }
    }
}
