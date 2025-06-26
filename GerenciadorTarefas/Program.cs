using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Data; // Correto, pois bate com o namespace do AppDbContext

var builder = WebApplication.CreateBuilder(args);

// Configura a conexão com SQL Server via appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Adiciona suporte a controllers e views (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); // <-- Adicione esta linha
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota padrão do MVC para o controlador "Tarefas"
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tarefas}/{action=Index}/{id?}");

app.Run();

