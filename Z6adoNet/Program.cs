var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddAuthentication("CookieAuthentication")
 .AddCookie("CookieAuthentication", config =>
 {
     config.Cookie.HttpOnly = true;
     config.Cookie.SecurePolicy = CookieSecurePolicy.None;
     config.Cookie.Name = "UserLoginCookie";
     config.LoginPath = "/Login/UserLogin";
     config.Cookie.SameSite = SameSiteMode.Strict;
 });

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/admin");
});
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();    //added
app.UseAuthentication();  //added

app.UseAuthorization();

app.MapRazorPages();

app.Run();
