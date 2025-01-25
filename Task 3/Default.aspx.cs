using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;

namespace Task_3
{
    public partial class _Default : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["UsersDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
            UpdateUserCount();
        }

        ///-----------------------------------------------------------------------------------------------------------////


        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string groupName = GroupNameTextBox.Text;
            int numberOfGroups;
            dynamic userId = IdTextBox.Text;



            if (int.TryParse(NumberOfGroupsTextBox.Text, out numberOfGroups))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (UserId, FirstName, LastName, GroupName, NumberOfGroups) VALUES (@UserId, @FirstName, @LastName, @GroupName, @NumberOfGroups)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);

                    string groupNamesParam = string.IsNullOrEmpty(groupName) ? "" : groupName;
                    command.Parameters.AddWithValue("@GroupName", groupNamesParam);

                    command.Parameters.AddWithValue("@NumberOfGroups", numberOfGroups);

                                    
                    connection.Open();
                    command.ExecuteNonQuery();

                    ClearFields();
                    BindGrid();
                }
            }
            else
            {
                if(!int.TryParse(NumberOfGroupsTextBox.Text, out _))
                {
                    NumberOfGroupsTextBox.Text = "Invalid input. Please check all fields.";
                }
                
            }

        }

        ///-----------------------------------------------------------------------------------------------------------////

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            int id = int.Parse(IdTextBox.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserId = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }

            ClearFields();
            BindGrid();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string groupName = GroupNameTextBox.Text;
            int numberOfGroups;
            dynamic userId = IdTextBox.Text;

            if (int.TryParse(NumberOfGroupsTextBox.Text, out numberOfGroups)) 
            {


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Users SET firstName = @FirstName, lastName = @LastName, groupName = @GroupName, numberOfGroups = @NumberOfGroups WHERE UserId = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);

                    string groupNamesParam = string.IsNullOrEmpty(groupName) ? "" : groupName;
                    command.Parameters.AddWithValue("@GroupName", groupNamesParam);

                    command.Parameters.AddWithValue("@NumberOfGroups", numberOfGroups);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                ClearFields();
                BindGrid();
            }
        }


        private void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                UserGridView.DataSource = dt;
                UserGridView.DataBind();
            }

            UpdateUserCount();
        }

        ///-----------------------------------------------------------------------------------------------------------////
        
        private void ClearFields()
        {
            IdTextBox.Text = string.Empty;
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            NumberOfGroupsTextBox.Text = string.Empty;
            GroupNameTextBox.Text = string.Empty;
        }

        ///-----------------------------------------------------------------------------------------------------------////

        
        protected void UserGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = UserGridView.SelectedRow;
            IdTextBox.Text = row.Cells[0].Text;
            FirstNameTextBox.Text= row.Cells[1].Text;
            LastNameTextBox.Text = row.Cells[2].Text;
            GroupNameTextBox.Text = row.Cells[3].Text;
            NumberOfGroupsTextBox.Text = row.Cells[4].Text;            
        }

        ///-----------------------------------------------------------------------------------------------------------////


        private void UpdateUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int userCount = (int)command.ExecuteScalar(); 
                UserCountLabel.Text = $"Total Users: {userCount}"; 
            }
        }

    }
}