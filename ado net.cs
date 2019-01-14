//ADO.NET
//Abstraction layer to interact with data source like database or xml file.

//Advantages:
//Full capabilities of sql server

//Disadvantages:
//SqlCommand is prone to sql injection (on client side)
//No intellisense for command string (on client side)
//Typos result in runtime errors, not compile time errors (on client side)
//Transfering SqlCommand to sql server generates more network traffic than stored procedures or functions

//First Example
protected void Page_Load(object sender, EventArgs e)
{
    string connectionString = "data source=.; database=Sample; integrated security=SSPI";

    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    {
        SqlCommand sqlCommand = new SqlCommand("Select * From Employee", sqlConnection);

        sqlConnection.Open();
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

        GridView1.DataSource = sqlDataReader;
        GridView1.DataBind();
    }// automatically calls sqlConnection.Close();
}

//Typically store connection string in web.config or app.config and access with:
string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


//ExecuteReader (rows of data will be returned)
SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

//ExecuteScalar (single value will be returned)
int rowCount = (int)sqlCommand.ExecuteScalar();

//ExecuteNonQuery (single value will be returned which indicate the effected rows)
//Insert, Update, Delete
int affectedRowsCount = sqlCommand.ExecuteNonQuery();


//SQL Injection
string name = "John; Delete from Employee; --" //given by user input
string command = "Select * from Employee where Name like '" + name "%'";

//Prevent SQL Injection (parameterized query)
string command = "Select * from Employee where Name like @Name";
sqlCommand.Parameters.AddWithValue("@Name", TextBox1.Text);

