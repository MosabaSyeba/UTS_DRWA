using UtsPos.Data;
using UtsPos.Models;
using UtsPOS.Data;
using UtsPOS.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ICategory, CategoryADO>();
builder.Services.AddSingleton<IProduct, ProductADO>();
builder.Services.AddSingleton<ICustomer, CustomerADO>();
builder.Services.AddSingleton<IEmployee, EmployeeADO>();
builder.Services.AddSingleton<ISale, SaleADO>();
builder.Services.AddSingleton<ISaleItem, SaleItemADO>();
builder.Services.AddSingleton<IViewProductWithCategory, ViewProductCategoryADO>();
builder.Services.AddSingleton<IViewSalesAndProductWithCustomer, ViewSalesAndProductWithCustomerADO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("api/uts72230640/categories", (ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
});

app.MapGet("api/uts72230640/categories/{id}", (ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});

app.MapPost("api/uts72230640/categories", (ICategory categoryData, string name) =>
{
    var category = new Category { Categoryname  = name };
    var newCategory = categoryData.AddCategory(category);
    return newCategory;
});

app.MapPut("api/uts72230640/categories", (ICategory categoryData, int id, string name) =>
{
    var category = new Category { categoryId = id, Categoryname = name };
    var updatedCategory = categoryData.UpdateCategory(category);
    return updatedCategory;
});


app.MapDelete("api/uts72230640/categories/{id}", (ICategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent(); 
});


app.MapGet("api/uts72230640/products", (IProduct product) =>
{
    var porducts = product.GetProducts();
    return porducts;
});

app.MapGet("api/uts72230640/products/{id}", (IProduct product, int id) =>
{
    var category = product.GetProductById(id);
    return category;
});

app.MapPost("api/uts72230640/products", (IProduct product,Product products) =>
{
    var newProduct = product.AddProduct(products);
    return newProduct;
});

app.MapPut("api/uts72230640/products", (IProduct product,Product products) =>
{
    var updateProduct = product.UpdateProduct(products);
    return updateProduct;
});

app.MapDelete("api/uts72230640/products/{id}", (IProduct product, int id) =>
{
    product.DeleteProduct(id);
    return Results.NoContent(); 
});


app.MapGet("api/uts72230640/customers", (ICustomer customerData) =>
{
    var customers = customerData.GetCustomers();
    return customers;
});

app.MapGet("api/uts72230640/customers/{id}", (ICustomer customerData, int id) =>
{
    var customer = customerData.GetCustomerById(id);
    return customer;
});

app.MapPost("api/uts72230640/customers", (ICustomer customerData, Customer customer) =>
{
    var newCustomer = customerData.AddCustomer(customer);
    return newCustomer;
});

app.MapPut("api/uts72230640/customers", (ICustomer customerData, Customer customer) =>
{
    var updateCustomer = customerData.UpdateCustomer(customer);
    return updateCustomer;
});

app.MapDelete("api/uts72230640/customers/{id}", (ICustomer customerData, int id) =>
{
    customerData.DeleteCustomer(id);
    return Results.NoContent(); 
});

app.MapGet("api/uts72230640/sales", (ISale s) =>
{
    var sales = s.GetSales();
    return sales;
});

app.MapGet("api/uts72230640/sales/{id}", (ISale s, int id) =>
{
    var sale = s.GetSaleById(id);
    return sale;
});

app.MapPost("api/uts72230640/sales", (ISale s, Sale sale) =>
{
    var newSale = s.AddSale(sale);
    return newSale;
});

app.MapPut("api/uts72230640/sales", (ISale s, Sale sale) =>
{
    var updatedSale = s.UpdateSale(sale);
    return updatedSale;
});

app.MapDelete("api/uts72230640/sales/{id}", (ISale s, int id) =>
{
    s.DeleteSale(id);
    return Results.NoContent(); 
});

app.MapGet("api/uts72230640/saleitems", (ISaleItem saleItemData) =>
{
    var saleItems = saleItemData.GetSaleItems();
    return saleItems;
});

app.MapGet("api/uts72230640/saleitems/{id}", (ISaleItem saleItemData, int id) =>
{
    var saleItem = saleItemData.GetSaleItemById(id);
    return saleItem;
});

app.MapPost("api/uts72230640/saleitems", (ISaleItem saleItemData, SaleItem saleItem) =>
{
    var newSaleItem = saleItemData.AddSaleItem(saleItem);
    return newSaleItem;
});

app.MapPut("api/uts72230640/saleitems", (ISaleItem saleItemData, SaleItem saleItem) =>
{
    var updatedSaleItem = saleItemData.UpdateSaleItem(saleItem);
    return updatedSaleItem;
});

app.MapDelete("api/uts72230640/saleitems/{id}", (ISaleItem saleItemData, int id) =>
{
    saleItemData.DeleteSaleItem(id);
    return Results.NoContent(); 
});

app.MapGet("api/uts72230640/employees", (IEmployee employeeData) =>
{
    var employees = employeeData.GetEmployees();
    return employees;
});

app.MapGet("api/uts72230640/employees/{id}", (IEmployee employeeData, int id) =>
{
    var employee = employeeData.GetEmployeeById(id);
    return employee;
});

app.MapPost("api/uts72230640/employees", (IEmployee employeeData, Employee employee) =>
{
    var newEmployee = employeeData.AddEmployee(employee);
    return newEmployee;
});

app.MapPut("api/uts72230640/employees", (IEmployee employeeData, Employee employee) =>
{
    var updateEmployee = employeeData.UpdateEmployee(employee);
    return updateEmployee;
});

app.MapDelete("api/uts72230640/employees/{id}", (IEmployee employeeData, int id) =>
{
    employeeData.DeleteEmployee(id);
    return Results.NoContent(); 
});

app.MapGet("api/uts72230640/productAndCategory", (IViewProductWithCategory pc) =>
{
    var productAndCategory = pc.GetProductCategory();
    return productAndCategory;
});

app.MapGet("api/uts72230640/viewComplit", (IViewSalesAndProductWithCustomer vc) =>
{
    var viewComplit = vc.GetViewComplite();
    return viewComplit;
});

app.MapGet("api/uts72230640/viewComplit/{id}", (IViewSalesAndProductWithCustomer vc, int id) =>
{
    var viewComplit = vc.GetViewCompliteById(id);
    return viewComplit;
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
