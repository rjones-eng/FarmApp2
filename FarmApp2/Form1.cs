using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FarmApp2
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=cows.db;Version=3;";

        private Report currentReport;

        public Form1()
        {
            InitializeComponent();
            CreateDatabaseAndTable();
            InitializeInputListView();
            LoadReportsIntoTreeView(); // This should load all reports at startup
            currentReport = new Report();
            buttonAddCow.Click += ButtonAddCow_Click;
            buttonAddReport.Click += ButtonAddReport_Click;
            buttonDeleteCow.Click += ButtonDeleteCow_Click; // Add this line
        }

        private void InitializeInputListView()
        {
            listViewCowsInput.View = View.Details;
            listViewCowsInput.Columns.Add("ID", 50);
            listViewCowsInput.Columns.Add("Weight", 100);
            listViewCowsInput.Columns.Add("Price (GBP)", 100); // Move Price column before Date Sold
            listViewCowsInput.Columns.Add("Date Sold", 150); // Move Date Sold column after Price
            listViewCowsInput.FullRowSelect = true;
        }



        private void LoadReportsIntoTreeView()
        {
            // Save the current expansion state
            var expansionState = SaveExpansionState(treeViewReports.Nodes);

            treeViewReports.Nodes.Clear();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT ReportId, UserProvidedId, Weight, DateSold, Price FROM Cows ORDER BY DateSold";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    Dictionary<string, TreeNode> yearNodes = new Dictionary<string, TreeNode>();
                    Dictionary<string, TreeNode> monthNodes = new Dictionary<string, TreeNode>();
                    Dictionary<string, TreeNode> reportNodes = new Dictionary<string, TreeNode>();

                    while (reader.Read())
                    {
                        DateTime dateSold = Convert.ToDateTime(reader["DateSold"]);
                        string year = dateSold.Year.ToString();
                        string month = dateSold.ToString("MMMM");
                        string reportId = reader["ReportId"].ToString();

                        // Ensure year node exists
                        if (!yearNodes.ContainsKey(year))
                        {
                            var yearNode = new TreeNode(year);
                            treeViewReports.Nodes.Add(yearNode);
                            yearNodes[year] = yearNode;

                            // Expand the current year node by default
                            if (year == DateTime.Now.Year.ToString())
                            {
                                yearNode.Expand();
                            }
                        }

                        // Ensure month node exists under the year node
                        string yearMonthKey = year + month;
                        if (!monthNodes.ContainsKey(yearMonthKey))
                        {
                            var monthNode = new TreeNode(month);
                            yearNodes[year].Nodes.Add(monthNode);
                            monthNodes[yearMonthKey] = monthNode;
                        }

                        // Ensure report node exists under the month node
                        string yearMonthReportKey = yearMonthKey + reportId;
                        if (!reportNodes.ContainsKey(yearMonthReportKey))
                        {
                            var reportNode = new TreeNode($"Report ID: {reportId}");
                            monthNodes[yearMonthKey].Nodes.Add(reportNode);
                            reportNodes[yearMonthReportKey] = reportNode;
                        }

                        // Add the cow node under the report node
                        var cowNode = new TreeNode($"ID: {reader["UserProvidedId"]}, Weight: {reader["Weight"]} kg, Price: £{reader["Price"]:F2}, Date Sold: {dateSold.ToShortDateString()}");
                        reportNodes[yearMonthReportKey].Nodes.Add(cowNode);
                    }
                }
            }

            // Restore the expansion state
            RestoreExpansionState(treeViewReports.Nodes, expansionState);
        }


        private Dictionary<string, bool> SaveExpansionState(TreeNodeCollection nodes)
        {
            var state = new Dictionary<string, bool>();
            foreach (TreeNode node in nodes)
            {
                SaveExpansionState(node, state);
            }
            return state;
        }

        private void SaveExpansionState(TreeNode node, Dictionary<string, bool> state)
        {
            state[node.FullPath] = node.IsExpanded;
            foreach (TreeNode child in node.Nodes)
            {
                SaveExpansionState(child, state);
            }
        }

        private void RestoreExpansionState(TreeNodeCollection nodes, Dictionary<string, bool> state)
        {
            foreach (TreeNode node in nodes)
            {
                RestoreExpansionState(node, state);
            }
        }

        private void RestoreExpansionState(TreeNode node, Dictionary<string, bool> state)
        {
            if (state.TryGetValue(node.FullPath, out bool isExpanded))
            {
                if (isExpanded)
                {
                    node.Expand();
                }
                else
                {
                    node.Collapse();
                }
            }
            foreach (TreeNode child in node.Nodes)
            {
                RestoreExpansionState(child, state);
            }
        }





        private void CreateDatabaseAndTable()
        {
            // Ensure the database file exists
            if (!System.IO.File.Exists("cows.db"))
            {
                SQLiteConnection.CreateFile("cows.db");
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                // Create the table with the correct schema if it does not exist
                string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Cows (
                InternalID INTEGER PRIMARY KEY,
                UserProvidedID INTEGER,
                Weight REAL,
                DateSold TEXT,
                Price REAL, -- New column
                ReportID TEXT
            )";
                using (var createCommand = new SQLiteCommand(createTableQuery, connection))
                {
                    createCommand.ExecuteNonQuery();
                }
            }
        }


        private void InsertCow(Cow cow, string reportID)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertQuery = "INSERT INTO Cows (InternalID, UserProvidedID, Weight, DateSold, Price, ReportID) VALUES (@InternalID, @UserProvidedID, @Weight, @DateSold, @Price, @ReportID)";
                        using (var command = new SQLiteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@InternalID", cow.InternalID);
                            command.Parameters.AddWithValue("@UserProvidedID", cow.UserProvidedID);
                            command.Parameters.AddWithValue("@Weight", cow.Weight);
                            command.Parameters.AddWithValue("@DateSold", cow.DateSold.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@Price", cow.Price);
                            command.Parameters.AddWithValue("@ReportID", reportID);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting cow: {ex.Message}");
            }
        }


        private void ButtonAddCow_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxID.Text, out int userProvidedId) &&
                double.TryParse(textBoxWeight.Text, out double weight) &&
                decimal.TryParse(textBoxPrice.Text, out decimal price))
            {
                DateTime dateSold = dateTimePickerDateSold.Value;
                int internalId = GenerateUniqueInternalID();
                Cow cow = new Cow(internalId, userProvidedId, weight, dateSold, price);
                currentReport.AddCow(cow);
                AddCowToListView(cow, listViewCowsInput);
                MessageBox.Show("Cow added to report successfully!");

                // Clear the text boxes
                textBoxID.Text = string.Empty;
                textBoxWeight.Text = string.Empty;
                textBoxPrice.Text = string.Empty;
                //dateTimePickerDateSold.Value = DateTime.Now; // Optionally reset the date picker to the current date
            }
            else
            {
                MessageBox.Show("Please enter valid Id, Weight, and Price.");
            }
        }


        private void ButtonDeleteCow_Click(object sender, EventArgs e)
        {
            if (listViewCowsInput.SelectedItems.Count > 0)
            {
                // Build the confirmation message
                string confirmationMessage = "Are you sure you want to delete the following cow(s)?\n\n";
                foreach (ListViewItem selectedItem in listViewCowsInput.SelectedItems)
                {
                    confirmationMessage += $"ID: {selectedItem.Text}, Weight: {selectedItem.SubItems[1].Text}, Price: {selectedItem.SubItems[2].Text}, Date Sold: {selectedItem.SubItems[3].Text}\n";
                }

                // Show the confirmation message box
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // If the user confirms, proceed with deletion
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem selectedItem in listViewCowsInput.SelectedItems)
                    {
                        int internalId = int.Parse(selectedItem.Tag.ToString()); // Use InternalID stored in Tag
                        DeleteCowFromDatabase(internalId);
                        listViewCowsInput.Items.Remove(selectedItem);

                        // Remove the cow from the currentReport.Cows list
                        currentReport.Cows.RemoveAll(c => c.InternalID == internalId);
                    }
                    MessageBox.Show("Selected cow(s) deleted successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a cow to delete.");
            }
        }


        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("DeleteMenuItem_Click called"); // debugging message
            if (listViewCowsInput.SelectedItems.Count > 0)
            {
                // Build the confirmation message
                string confirmationMessage = "Are you sure you want to delete the following cow(s)?\n\n";
                foreach (ListViewItem selectedItem in listViewCowsInput.SelectedItems)
                {
                    confirmationMessage += $"ID: {selectedItem.Text}, Weight: {selectedItem.SubItems[1].Text}, Price: {selectedItem.SubItems[2].Text}, Date Sold: {selectedItem.SubItems[3].Text}\n";
                }

                // Show the confirmation message box
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // If the user confirms, proceed with deletion
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem selectedItem in listViewCowsInput.SelectedItems)
                    {
                        int internalId = int.Parse(selectedItem.Tag.ToString()); // Use InternalID stored in Tag
                        DeleteCowFromDatabase(internalId);
                        listViewCowsInput.Items.Remove(selectedItem);

                        // Remove the cow from the currentReport.Cows list
                        currentReport.Cows.RemoveAll(c => c.InternalID == internalId);
                    }
                    MessageBox.Show("Selected cow(s) deleted successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a cow to delete.");
            }
        }





        private void DeleteCowFromDatabase(int internalId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Cows WHERE InternalID = @InternalID";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InternalID", internalId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting cow: {ex.Message}");
            }
        }



        private int GenerateUniqueInternalID()
        {
            return DateTime.Now.Ticks.GetHashCode();
        }


        private void AddCowToListView(Cow cow, System.Windows.Forms.ListView listView)
        {
            var item = new ListViewItem(cow.UserProvidedID.ToString());
            item.SubItems.Add(cow.Weight.ToString());
            item.SubItems.Add($"£{cow.Price:F2}");
            item.SubItems.Add(cow.DateSold.ToShortDateString());
            item.Tag = cow.InternalID; // Store InternalID in Tag
            listView.Items.Add(item);
            cows.Add(cow); // Add cow to the in-memory collection
        }


        private void ButtonAddReport_Click(object sender, EventArgs e)
        {
            if (currentReport.Cows.Count == 0)
            {
                MessageBox.Show("Cannot add a report with no cows. Please add cows to the report before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build the confirmation message
            string confirmationMessage = "Are you sure you want to add the following report?\n\n";
            foreach (var cow in currentReport.Cows)
            {
                confirmationMessage += $"{cow}\n";
            }

            // Show the confirmation message box
            DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Add Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user confirms, proceed with adding the report
            if (result == DialogResult.Yes)
            {
                string reportId = GenerateReportID(DateTime.Now);
                AddReportToDatabase(currentReport, reportId);
                currentReport = new Report();
                listViewCowsInput.Items.Clear();
                LoadReportsIntoTreeView();
                MessageBox.Show("Report added to database successfully!");
            }
        }

        private void AddReportToDatabase(Report report, string reportId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var cow in report.Cows)
                    {
                        string insertQuery = "INSERT INTO Cows (InternalID, UserProvidedID, Weight, DateSold, Price, ReportID) VALUES (@InternalID, @UserProvidedID, @Weight, @DateSold, @Price, @ReportID)";
                        using (var command = new SQLiteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@InternalID", cow.InternalID);
                            command.Parameters.AddWithValue("@UserProvidedID", cow.UserProvidedID);
                            command.Parameters.AddWithValue("@Weight", cow.Weight);
                            command.Parameters.AddWithValue("@DateSold", cow.DateSold.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@Price", cow.Price);
                            command.Parameters.AddWithValue("@ReportID", reportId);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
        }



        private List<Cow> cows = new List<Cow>();

        public class Cow
        {
            public int InternalID { get; set; }
            public int UserProvidedID { get; set; }
            public double Weight { get; set; }
            public DateTime DateSold { get; set; }
            public decimal Price { get; set; } // New property

            public Cow(int internalID, int userProvidedID, double weight, DateTime dateSold, decimal price)
            {
                InternalID = internalID;
                UserProvidedID = userProvidedID;
                Weight = weight;
                DateSold = dateSold;
                Price = price;
            }

            public override string ToString()
            {
                return $"Cow ID: {UserProvidedID}, Weight: {Weight} kg, Date Sold: {DateSold.ToShortDateString()}, Price: £{Price}";
            }
        }


        private string GenerateReportID(DateTime date)
        {
            string datePart = date.ToString("dd-MM-yyyy-");
            char letterPart = 'A';

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ReportID FROM Cows WHERE ReportID LIKE @DatePart || '%'";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DatePart", datePart);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string reportID = reader["ReportID"].ToString();
                            char currentLetter = reportID[reportID.Length - 1];
                            if (currentLetter >= letterPart)
                            {
                                letterPart = (char)(currentLetter + 1);
                            }
                        }
                    }
                }
            }

            return datePart + letterPart;
        }

        public class Report
        {
            public List<Cow> Cows { get; set; }

            public Report()
            {
                Cows = new List<Cow>();
            }

            public void AddCow(Cow cow)
            {
                Cows.Add(cow);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddCow_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxWeight_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
