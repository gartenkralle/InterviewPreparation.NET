//ADO.NET
//Abstraction layer to interact with data source like database or xml file.

//Advantages:
//Full capabilities of sql server

//Disadvantages:
//SqlCommand is prone to sql injection
//No intellisens for command string
//Typos result in runtime errors, not compile time errors
//Transfering SqlCommand to sql server generates more network traffic than stored procedures or functions

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

//Typically store connection string in web.config or app.config and access with:
string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

