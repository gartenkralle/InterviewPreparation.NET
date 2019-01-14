//First Example

protected void Page_Load(object sender, EventArgs e)
{
    SqlConnection sqlConnection = new SqlConnection("data source=.; database=Sample; integrated security=SSPI");
    SqlCommand sqlCommand = new SqlCommand("Select * From Employee", sqlConnection);

    sqlConnection.Open();
    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

    GridView1.DataSource = sqlDataReader;
    GridView1.DataBind();

    sqlConnection.Close();
}

