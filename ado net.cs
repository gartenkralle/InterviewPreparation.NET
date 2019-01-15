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
//requires an active and open connection to the data source
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlCommand sqlCommand = new SqlCommand("spAddEmployee", sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
    sqlCommand.Parameters.AddWithValue("@Name", "John");
    sqlCommand.Parameters.AddWithValue("@Gender", "Male");
    sqlCommand.Parameters.AddWithValue("@Salary", 50);

    SqlParameter outputParameter = new SqlParameter();
    outputParameter.ParameterName = "@EmployeeID";
    outputParameter.SqlDbType = System.Data.SqlDbType.Int;
    outputParameter.Direction = System.Data.ParameterDirection.Output;

    sqlCommand.Parameters.Add(outputParameter);

    sqlConnection.Open();
    sqlCommand.ExecuteNonQuery();

    string employeeID = outputParameter.Value.ToString();
}

//Calling stored procedure (which has 4 parameters, 3 input parameters, 1 output parameter)
//requires NO active and open connection to the data source
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("spAddEmployee", sqlConnection);
    sqlDataAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Name", "John");
    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Gender", "Male");
    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Salary", 100);

    SqlParameter outputParameter = new SqlParameter();
    outputParameter.ParameterName = "@EmployeeID";
    outputParameter.SqlDbType = System.Data.SqlDbType.Int;
    outputParameter.Direction = System.Data.ParameterDirection.Output;

    sqlDataAdapter.SelectCommand.Parameters.Add(outputParameter);

    DataSet dataSet = new DataSet();
    sqlDataAdapter.Fill(dataSet);

    dataSet.Tables[0].TableName = "Employees";

    GridView1.DataSource = dataSet.Tables["Employees"];
    GridView1.DataBind();
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
//requires an active and open connection to the data source
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlCommand sqlCommand = new SqlCommand("Select * From Employee; Select * From Department", sqlConnection);

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

//SqlDataAdapter
//SqlDataAdapter requires NO active and open connection to the data source
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Employee; Select * From Department", sqlConnection);

    DataSet dataSet = new DataSet();
    sqlDataAdapter.Fill(dataSet);

    dataSet.Tables[0].TableName = "Employees";
    dataSet.Tables[1].TableName = "Departments";

    GridView1.DataSource = dataSet.Tables["Employees"];
    GridView1.DataBind();

    GridView2.DataSource = dataSet.Tables["Departments"];
    GridView2.DataBind();
}


//Caching a DataSet
if (Cache["Employees"] == null)
{
    string connectionString = "data source=.; database=Sample; integrated security=SSPI";

    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    {
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Employee", sqlConnection);

        DataSet dataSet = new DataSet();
        sqlDataAdapter.Fill(dataSet);

        dataSet.Tables[0].TableName = "Employees";

        Cache["Employees"] = dataSet.Tables["Employees"];

        GridView1.DataSource = dataSet.Tables["Employees"];
        GridView1.DataBind();
    }
}
else
{
    GridView1.DataSource = (DataTable)Cache["Employees"];
    GridView1.DataBind();
}

//SqlCommandBuilder
//generates Insert, Update, Delete statements based on the Select statement for a single table
//Change TableRow of DataSet + Update call results in an Update command
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    string sqlQuery = "Select * From Employee";
    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConnection);

    DataSet dataSet = new DataSet();
    sqlDataAdapter.Fill(dataSet, "Employees");

    DataRow dataRow = dataSet.Tables["Employees"].Rows[0];
    dataRow["Salary"] = 20; // Change

    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
    int rowsUpdatedCount = sqlDataAdapter.Update(dataSet, "Employees"); // Update

    GridView1.DataSource = dataSet.Tables["Employees"];
    GridView1.DataBind();
}

//Insert TableRow in DataSet + Update call results in an Insert command
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    string sqlQuery = "Select * From Employee";
    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConnection);

    DataSet dataSet = new DataSet();
    sqlDataAdapter.Fill(dataSet, "Employees");

    DataRow dataRow = dataSet.Tables["Employees"].NewRow();
    dataRow["Name"] = "Heinz";
    dataRow["Salary"] = 5;
    dataRow["Gender"] = "Male";

    dataSet.Tables["Employees"].Rows.Add(dataRow); // Insert

    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
    int rowsUpdatedCount = sqlDataAdapter.Update(dataSet, "Employees"); // Update

    GridView1.DataSource = dataSet.Tables["Employees"];
    GridView1.DataBind();
}

//Delete TableRow from DataSet + Update call results in an Delete command
using (SqlConnection sqlConnection = new SqlConnection(connectionString))
{
    string sqlQuery = "Select * From Employee";
    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConnection);

    DataSet dataSet = new DataSet();
    sqlDataAdapter.Fill(dataSet, "Employees");

    DataRow dataRow = dataSet.Tables["Employees"].Rows[0];
    dataRow.Delete(); // Delete

    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
    int rowsUpdatedCount = sqlDataAdapter.Update(dataSet, "Employees"); // Update

    GridView1.DataSource = dataSet.Tables["Employees"];
    GridView1.DataBind();
}

