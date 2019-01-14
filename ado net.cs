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
string name = "John; Delete from Employee; --" //this user input will clear the employee table
string command = "Select * from Employee where Name like '" + name "%'";

//Prevent SQL Injection (parameterized query)
string command = "Select * from Employee where Name like @Name";
sqlCommand.Parameters.AddWithValue("@Name", TextBox1.Text + "%");


//Calling stored procedure (which has 4 parameters, 3 input parameters, 1 output parameter)
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlCommand sqlCommand = new SqlCommand("spAddEmployee", sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
    sqlCommand.Parameters.AddWithValue("@Name", txtEmpoyeeName.Text);
    sqlCommand.Parameters.AddWithValue("@Gender", ddlEmpoyeeGender.SelectedValue);
    sqlCommand.Parameters.AddWithValue("@Salary", txtEmpoyeeSalary.Text);

    SqlParameter outputParameter = new SqlParameter();
    outputParameter.ParameterName = "@EmployeeID";
    outputParameter.SqlDbType = System.Data.SqlDbType.Int;
    outputParameter.Direction = System.Data.ParameterDirection.Output;

    sqlCommand.Parameters.Add(outputParameter);

    sqlConnection.Open();
    sqlCommand.ExecuteNonQuery();

    string employeeID = outputParameter.Value.ToString();
}

//SqlDataReader
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlCommand sqlCommand = new SqlCommand("Select * From Employee", sqlConnection);

    sqlConnection.Open();

    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

    DataTable dataTable = new DataTable();
    dataTable.Columns.Add("ID");
    dataTable.Columns.Add("Name");

    while (sqlDataReader.Read()) //Switch to next row
    {
        DataRow dataRow = dataTable.NewRow();

        dataRow["ID"] = sqlDataReader["ID"];
        dataRow["Name"] = sqlDataReader["Name"];

        dataTable.Rows.Add(dataRow);
    }

    GridView1.DataSource = dataTable;
    GridView1.DataBind();
}

//SqlDataReader (read multiple tables)
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlCommand sqlCommand = new SqlCommand("Select * From Employee; Select * From Person", sqlConnection);

    sqlConnection.Open();

    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

    DataTable dataTable1 = new DataTable();
    dataTable1.Columns.Add("ID");
    dataTable1.Columns.Add("Name");

    while (sqlDataReader.Read())
    {
        DataRow dataRow = dataTable1.NewRow();

        dataRow["ID"] = sqlDataReader["ID"];
        dataRow["Name"] = sqlDataReader["Name"];

        dataTable1.Rows.Add(dataRow);
    }

    GridView1.DataSource = dataTable1;
    GridView1.DataBind();

    sqlDataReader.NextResult(); //Switch to next table

    DataTable dataTable2 = new DataTable();
    dataTable2.Columns.Add("ID");
    dataTable2.Columns.Add("Name");

    while (sqlDataReader.Read())
    {
        DataRow dataRow = dataTable2.NewRow();

        dataRow["ID"] = sqlDataReader["ID"];
        dataRow["Name"] = sqlDataReader["Name"];

        dataTable2.Rows.Add(dataRow);
    }

    GridView2.DataSource = dataTable2;
    GridView2.DataBind();
}

