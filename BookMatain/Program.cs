var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<BookMatain.Service.ICodeService, BookMatain.Service.CodeService>();
builder.Services.AddScoped<BookMatain.Service.IBookService, BookMatain.Service.BookService>();

builder.Services.AddScoped<BookMatain.Dao.ICodeDao, BookMatain.Dao.CodeDao>();
builder.Services.AddScoped<BookMatain.Dao.IBookDao, BookMatain.Dao.BookDao >();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
