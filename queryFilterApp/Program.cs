using Microsoft.EntityFrameworkCore;
using queryFilterApp.Models;
using queryFilterApp.Services.ProductService;

var builder = WebApplication.CreateBuilder(args); // <--- 1. Create the Builder (ASP.NET convention)

// default services added when creating 'web api project' template
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding a database service with configuration -- connection string read from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// adding our own service -- CRUD services should be registered with transient lifetimes
builder.Services.AddTransient<IProductService, ProductService>();


var app = builder.Build(); // <--- 2. Build the App (ASP.NET convention)


// default middleware when choosing create 'web api project'
app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers();

app.Run(); // <--- 3. Run the App (ASP.NET convention)