var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication().AddCookie("MyCookie", options =>
{
    options.Cookie.Name = "MyCookie";// Oluþturduðumuz cookienin adý.
    options.LoginPath = "/Account/Login";// Giriþ yapmadýysa bu sayfaya yönlendiriyor.
    options.AccessDeniedPath = "/Account/AccessDenied";// Yetkisi yetersiz bir sayfaya ulaþmaya çalýþtýðýnda yönlendirilir.
    options.ExpireTimeSpan = TimeSpan.FromSeconds(30);// 15 dakika içinde iþlem yapmassa sistemden atar.
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustTeacher",
        policy => policy.RequireClaim("Role", "Teacher", "Manager"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Home/Error");
    //app.UseHsts();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "error",
        pattern: "/error/{statusCode}",
        defaults: new { controller = "error", action = "HandleError" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
//app.MapControllerRoute(
    //name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
